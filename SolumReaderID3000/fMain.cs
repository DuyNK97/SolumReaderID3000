﻿using MSFactoryDLL;
using System;
using SolumReaderID3000.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Drawing.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.IO.Ports;
using Seagull.BarTender.Print;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace SolumReaderID3000
{
    public partial class fMain : Form
    {
        UDPControl udpChat;
        ControlTCP _client = new ControlTCP();//tcpip
        PLCInterface _plcInterface = new PLCInterface();

        //prnt label
        private LabelFormatDocument format = null;
        private const string appName = "Label Print";
        private const string dataSourced = "Data Sourced";
        private Engine engine = null;
        Hashtable listItems; // A hash table containing ListViewItems and indexed by format name.
                             // It keeps track of what formats have had their image loaded.
        Queue<int> generationQueue; // A queue containing indexes into browsingFormats
                                    // to facilitate the generation of thumbnails
        private List<string> templateVariables = new List<string>();

        public fMain()
        {



            InitializeComponent();
            InitReader();
            InitModel();
            InitLogCSV();
            EnableControl(true);
            this.title.OnProgramExitting += OnProgramExitting;
            btnLoadModel_Click(null, null);
            InitializeSerialPort(ClassifyResult.Instance.SerialPort);
            this.title.SerialPortSettingsChangedMain += SettingsForm_SerialPortSettingsChanged;

            InitLogTCP();
            InitUDPControl();
            InitPLCInterface();
            //maximum form 
            //if (this.WindowState == FormWindowState.Normal)
            //{
            //    this.WindowState = FormWindowState.Maximized;
            //}
            //else
            //    this.WindowState = FormWindowState.Normal;
            try
            {
                engine = new Engine(true);
            }
            catch (PrintEngineException exception)
            {
                // If the engine is unable to start, a PrintEngineException will be thrown.
                MessageBox.Show(this, exception.Message, appName);
                this.Close(); // Close this app. We cannot run without connection to an engine.
                return;
            }

            // Get the list of printers
            Printers printers = new Printers();
            foreach (Seagull.BarTender.Print.Printer printer in printers)
            {
                cboPrinters.Items.Add(printer.PrinterName);
            }

            if (printers.Count > 0)
            {
                // Automatically select the default printer.
                cboPrinters.SelectedItem = printers.Default.PrinterName;
            }
            listItems = new System.Collections.Hashtable();
            generationQueue = new Queue<int>();
            txtIdenticalCopies.MaxLength = 9;
            txtSerializedCopies.MaxLength = 9;



            this.title.TitleName = $"{ClassifyResult.Instance.ApplicationName}";
        }
        public void InitPLCInterface()
        {
            _plcInterface.ConnectToPLC();
            if (_plcInterface.IsConnectPLC)
            {
                ShowLog("Connect to PLC success! \n");
                Thread readDataThread = new Thread(ReadDataFromPLC);
                readDataThread.IsBackground = true; // Đảm bảo luồng này không cản trở chương trình kết thúc
                readDataThread.Start();
            }
            else
            {
                ShowLog("Connect to PLC fail! \n");
            }

        }

        private void ReadDataFromPLC()
        {
            while (!this.IsDisposed)
            {
                if (_plcInterface.ReadBitPLC())
                {
                    Console.WriteLine("Printer");
                    this.Invoke(new Action(() =>
                    {
                        lblWeight.Text = _plcInterface.ReadRegisterPLC();
                        PrintLabel(Global.date, Global.MODEL, Global.seri, Global.SECCODE1, ClassifyResult.Instance.seri.ToString("000"), Global.model, Global.DATE, Global.Qty);
                        Thread.Sleep(5000);
                        ClassifyResult.Instance.seri = 0;
                        while (_plcInterface.ReadBitPLC())
                        {
                            _plcInterface.WriteBitPLC();
                        }
                        ShowLog("ReadBit");
                    }));
                }
                Thread.Sleep(2000);
            }
        }

        public void InitUDPControl()
        {
            udpChat = new UDPControl(ClassifyResult.Instance.ServerIP);
            udpChat.StartServer();
        }
        public void InitLogTCP()
        {
            _client.Connect();
            if (_client.IsConnected)
            {
                ShowLog("Connect to TCP/IP success \n ");
            }
            else
            {
                ShowLog("Connect to TCP/IP fail! \n ");
            }
            _client.LotDataReceived += OnLotDataReceived;
        }
        private void OnLotDataReceived(string data)
        {
            ShowLog(" Data Received from MES: " + data + "\n");
            SendData(data);  //gui qua com
            HandleValidCode(imageBox1.Image.Clone() as Bitmap, DateTime.Now.ToString(), data);
        }

        private void InitModel()
        {
            cbbModel.Items.Clear();
            foreach (var item in SettingParams.Instance.Parameters)
            {
                cbbModel.Items.Add(item.ModelName);
                if (item.ModelName == ClassifyResult.Instance.Model)
                {
                    currentModel = ClassifyResult.Instance.Model;
                    cbbModel.Text = currentModel;
                    LoadModel(item);
                }
            }
            SettingParams.Instance.Save();
            ClassifyResult.Instance.Save();
        }
        private SerialPort serialPorts;
        public LogCSV logCSV;
        private string currentModel = string.Empty;
        private string strHeaders = string.Empty;
        private void InitLogCSV()
        {
            try
            {
                Action action = () =>
                {
                    ClassCommon.FolderAutoCreate = new FolderAutoCreate(Application.StartupPath, ClassifyResult.Instance.DayDelete);

                    dgvLogCSV.DataSource = null;
                    strHeaders = "TIME,NO,MODEL,";
                    strHeaders += "CODE-TYPE,CODE-STRING,STATUS";
                    string csvPath = ClassCommon.FolderAutoCreate.ListChildFolders[1];
                    logCSV = LogCSV.GetLogCSV(csvPath, $"{ClassifyResult.Instance.CreationTime}_TotalResult.csv");
                    SetFormatGridview();
                    logCSV.CSVdataGridView = dgvLogCSV;
                    logCSV.Header = strHeaders;
                };
                if (this.InvokeRequired)
                    this.Invoke(action);
                else action();
            }
            catch (Exception ex)
            {
            }
        }

        private void InitializeSerialPort(string portName)
        {
            if (string.IsNullOrEmpty(portName))
            {
                MessageBox.Show("No COM port selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            try
            {
                serialPorts = new SerialPort(portName, (int)ClassifyResult.Instance.baudrate);
                serialPorts.DataReceived += SerialPorts_DataReceived; ;
                if (!serialPorts.IsOpen)
                {
                    OpenPort(serialPorts);
                }
                else
                {
                    ShowLog($"This port opened: {portName}");
                }

                // _continue = true;
                //Thread requestThread = new Thread(RequestLoop);
                //requestThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening port {portName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Task.Run(() => ShowLog($"Error opening port {portName}: {ex.Message}"));
            }
        }

        private void SerialPorts_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ShowLog($"MSBoard: " + serialPorts.ReadLine() + "\n");
        }


        private void SettingsForm_SerialPortSettingsChanged(string newPort)
        {
            InitializeSerialPort(newPort);
        }
        public void SendData(string data)
        {
            try
            {
                if (serialPorts != null)
                {
                    if (!serialPorts.IsOpen)
                    {
                        serialPorts.Open();
                    }
                    serialPorts.WriteLine(data);

                }
            }
            catch (Exception ex)
            {
                Task.Run(() => ShowLog($"Can not send data to IO: {ex.Message}"));
            }
        }
        private void OpenPort(SerialPort port)
        {
            try
            {
                if (!port.IsOpen)
                {
                    port.Open();
                    ShowLog($"Open port {port.PortName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening port {port.PortName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowLog($"Error opening port {port.PortName}: {ex.Message} \n Please re-setting COM.", true);
                //this.Close();

            }
        }

        private void SetFormatGridview()
        {
            List<DataGridViewAutoSizeColumnMode> listSizeMode = new List<DataGridViewAutoSizeColumnMode>();
            for (int i = 0; i < strHeaders.Split(',').Length; i++)
            {
                if (i == 4)
                    listSizeMode.Add(DataGridViewAutoSizeColumnMode.Fill);
                else
                    listSizeMode.Add(DataGridViewAutoSizeColumnMode.DisplayedCells);
            }
            logCSV.GridviewSizeMode = listSizeMode;
        }

        private void OnProgramExitting(object sender, EventArgs e)
        {
            Global.IsExits = true;
            Global.readerControl.CloseDevice();
            title.Close();
        }

        private void InitReader()
        {
            Global.readerControl = new ReaderControl("02DA4115756");
            if (Global.readerControl.IsConnected)
            {
                Global.readerControl.OnImageGrabbed += ReaderControl_OnImageGrabbed;
                LoadReaderParams();
                ShowLog("02DA4115756 CONNECT SUCCESS ");
            }
        }


        private void ReaderControl_OnImageGrabbed(System.Drawing.Bitmap bitmap, List<string> strCode, List<MsPolygon> ROIs)
        {
            Action action = () =>
            {
                // Clear previous graphics and set the new image
                imageBox1.ClearGraphics();
                imageBox1.Image = bitmap;

                // Draw ROIs
                foreach (var item in ROIs)
                {
                    imageBox1.AddGraphics(item);
                }

                Bitmap bmpSaveImageGraphics = bitmap.Clone() as Bitmap;

                if (strCode.Count == 0)
                {
                    HandleNoCodeDetected(bmpSaveImageGraphics);
                    SendData("NG");
                    return;
                }

                foreach (var item in strCode)
                {
                    AddCodeGraphic(item);

                    var parts = item.Split(',');

                    if (parts.Length == 2)
                    {
                        string codetype = parts[0].Trim();  // phần đầu tiên là codetype
                        string code = parts[1].Trim().Replace("\0", "");      //

                        if (string.IsNullOrWhiteSpace(code))
                        {
                            HandleInvalidCode(bmpSaveImageGraphics, "_", "NG");
                            SendData("NG");
                        }
                        else
                        {
                            //HandleValidCode(imageBox1.Image.Clone() as Bitmap, DateTime.Now.ToString(), "NG");
                            //HandleValidCode(bmpSaveImageGraphics, DateTime.Now.ToString(), "NG");
                            _client.Send(code); //ok gui len MES
                            ClassifyResult.Instance.seri++;
                            HandleValidCode(bmpSaveImageGraphics, code, "OK");
                            SendData("OK");
                            //
                            //ProcessValidCode(bmpSaveImageGraphics, code);
                        }
                        logCSV.SaveLog($"{ClassifyResult.Instance.Total},{currentModel},{item.Replace("\0", "")},{ClassifyResult.Instance.RunResult}");
                    }
                    else
                    {
                        logCSV.SaveLog($"{ClassifyResult.Instance.Total},{currentModel},NA,NA,{ClassifyResult.Instance.RunResult}");
                        Task.Run(() => ShowLog("Incorrect Code Info: " + item));
                    }
                }
                ClassifyResult.Instance.Save();
            };
            // Execute on the UI thread if needed
            if (imageBox1.InvokeRequired)
                imageBox1.Invoke(action);
            else
                action();
        }

        private void HandleNoCodeDetected(Bitmap bmpSaveImageGraphics)
        {
            ClassifyResult.Instance.RunResult = ClassifyResult.eFinalResult.NG;
            logCSV.SaveLog($"{ClassifyResult.Instance.Total},{currentModel},NA,NA,{ClassifyResult.Instance.RunResult}");
            SaveImageGraphics("Empty", bmpSaveImageGraphics, @"NG\");
        }
        private void HandleInvalidCode(Bitmap bmpSaveImageGraphics, string code, string folder)
        {
            ClassifyResult.Instance.RunResult = ClassifyResult.eFinalResult.NG;
            SaveImageGraphics(code, bmpSaveImageGraphics, $@"NG\{folder}\");
        }
        private void HandleValidCode(Bitmap bmpSaveImageGraphics, string code, string folder)
        {
            ClassifyResult.Instance.RunResult = ClassifyResult.eFinalResult.OK;
            SaveImageGraphics(code, bmpSaveImageGraphics, folder + "\\");

        }
        private void AddCodeGraphic(string code)
        {
            var msString = new MsString(code, new System.Drawing.PointF(10, 10), new Font("Arial", 15.0f), new SolidBrush(Color.Green));
            imageBox1.AddGraphics(msString);
        }

        #region Code not use
        private void ProcessValidCode(Bitmap bmpSaveImageGraphics, string code)
        {
            if (Global.length > 0)
            {
                if (Checklen(code))
                {
                    if (Global.format == "%%" || CheckFormat(code))
                    {
                        HandleValidCode(bmpSaveImageGraphics, code, "OK");
                        //if (Global.CheckDuplicateAndAdd(code))
                        //{
                        SendData("OK");
                        //udpChat.SendMessage(code);
                        //}
                        //else 
                        //{
                        //    SendData("NG");
                        //    Task.Run(() => ShowLog("Doublicate :" + code, true));
                        //}


                    }
                    else
                    {
                        HandleInvalidCode(bmpSaveImageGraphics, code, "NG_Format");
                        SendData("NG");
                        Task.Run(() => ShowLog("NG_Format:" + code, true));
                    }
                }
                else
                {
                    HandleInvalidCode(bmpSaveImageGraphics, code, "NG_length");
                    SendData("NG");
                    Task.Run(() => ShowLog("NG_length:" + code, true));
                }
            }
            else
            {
                if (Global.format == "%%" || CheckFormat(code))
                {
                    HandleValidCode(bmpSaveImageGraphics, code, "OK");
                    //if (Global.CheckDuplicateAndAdd(code))
                    //{
                    SendData("OK");
                    //udpChat.SendMessage(code);
                    //}
                    //else
                    //{
                    //    SendData("NG");
                    //    Task.Run(() => ShowLog("Doublicate :" + code, true));
                    //}
                }
                else
                {
                    HandleInvalidCode(bmpSaveImageGraphics, code, "NG_Format");
                    SendData("NG");
                    Task.Run(() => ShowLog("NG_Format:" + code, true));
                }
            }
        }
        private void HandleValidLengthCode(Bitmap bmpSaveImageGraphics, string code)
        {
            if (Global.format == "%%" || CheckFormat(code))
            {
                ClassifyResult.Instance.RunResult = ClassifyResult.eFinalResult.OK;
                //  new Thread(() => SaveImageGraphics(code, bmpSaveImageGraphics, @"OK\"));
            }
            else
            {
                HandleInvalidCode(bmpSaveImageGraphics, code, "NG_Format");
            }
        }
        private void HandleValidFormatCode(Bitmap bmpSaveImageGraphics, string code)
        {
            ClassifyResult.Instance.RunResult = ClassifyResult.eFinalResult.OK;
            // new Thread(() => SaveImageGraphics(code, bmpSaveImageGraphics, @"OK\"));
        }
        private void SaveImage(int index, Bitmap bmpSave, string subFolder)
        {
            //string nameFile = string.Format("{0}_{1}_{2}.{3}", MachineClassify.Classify.JobCount + 1, index, DateTime.Now.ToString("HHmmss_fff"), "png");
            //string fullpath = Path.Combine(ClassCommon.FolderAutoCreate.GetFullPathByChildFolders("RunImage"), subFolder);
            //if (!Directory.Exists(fullpath))
            //{
            //    Directory.CreateDirectory(fullpath);
            //}
            //bmpSave.Save(Path.Combine(fullpath, nameFile), ImageFormat.Png);
        }
        #endregion


        private void SaveImageGraphics(string Item, Bitmap bmpSave, string subFolder)
        {
            new Thread(() =>
            {
                try
                {
                    if (ClassifyResult.Instance.SaveRunImage)
                    {
                        string nameFile = string.Format("{0}_{1}.{2}", Item, DateTime.Now.ToString("HHmmss_fff"), "png");
                        string fullpath = Path.Combine(ClassCommon.FolderAutoCreate.GetFullPathByChildFolders("ScreenCapture"), subFolder);
                        if (!Directory.Exists(fullpath))
                        {
                            Directory.CreateDirectory(fullpath);
                        }
                        bmpSave.Save(Path.Combine(fullpath, nameFile), ImageFormat.Png);
                    }
                }
                catch (Exception)
                {
                }

            }).Start();

        }

        private void LoadReaderParams()
        {
            Action action = () =>
            {
                grbReaderParams.Text = Global.readerControl.SerialNo;
                lbFrameRate.Text = Global.readerControl.FrameRate.ToString("F1");
                grbReaderParams.Visible = true;

            };
            if (pnlParams.InvokeRequired)
                pnlParams.Invoke(action);
            else
                action();
        }

        private void EnableControl(bool isViewDefault)
        {
            grbSaveNewModel.Visible = !isViewDefault;
            grbReaderParams.Visible = isViewDefault;
            pnlModel.Visible = isViewDefault;
            txtNewModelName.Clear();
            grbformat.Visible = isViewDefault;
            groupreader.Visible = isViewDefault;

        }

        private void btnSaveAsNew_Click(object sender, EventArgs e)
        {
            EnableControl(false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            EnableControl(true);
            LoadModel(SettingParams.Instance.Parameters[IndexModel]);
        }

        public int IndexModel { get; set; } = -1;
        private int GetIndexModel(string modelName)
        {
            int index = -1;
            for (int i = 0; i < SettingParams.Instance.Parameters.Count; i++)
            {
                if (SettingParams.Instance.Parameters[i].ModelName == modelName)
                {
                    index = i;
                    break;
                }
            }
            IndexModel = index;
            return index;
        }

        private void btnLoadModel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cbbModel.Text))
            {
                if (Global.Model != cbbModel.Text)
                {
                    GetIndexModel(cbbModel.Text);
                    if (sender != null)
                        ClassifyResult.Instance.ClearData();
                    LoadModel(SettingParams.Instance.Parameters[IndexModel]);
                }

            }
        }

        private void LoadModel(SettingParam setting)
        {
            numExposure.Value = (decimal)setting.Exposure;
            numGain.Value = (decimal)setting.Gain;

            Global.readerControl.Exposure = setting.Exposure;
            Global.readerControl.Gain = setting.Gain;
            Global.length = setting.Length;
            Global.format = setting.Format;
            Global.Modelqty = setting.ModelQty;
            Global.Model = setting.ModelName;
            ClassifyResult.Instance.Save();
            currentModel = setting.ModelName;
            Global.modelList = new List<string>();
            Action action = () =>
            {

                tblog.Text = string.Empty;
                this.title.TitleName = $"{ClassifyResult.Instance.ApplicationName} [{currentModel}]";
                cbbModel.Text = currentModel;
                numberLength.Value = setting.Length;
                tbformat.Text = setting.Format;
                dgvLogCSV.Rows.Clear();
            };

            if (this.InvokeRequired)
                this.Invoke(action);
            else
                action();




        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show($"Would you want to REMOVE [{currentModel}]?.", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                GetIndexModel(cbbModel.Text);
                if (IndexModel != -1)
                {
                    SettingParams.Instance.Parameters.RemoveAt(IndexModel);
                    Task.Run(() => ShowLog("Remove Model : " + cbbModel.Text));
                    InitModel();

                }
                else
                {
                    MessageBox.Show($"Please choose model.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (txtNewModelName.Text.Length == 0) return;
            foreach (var item in SettingParams.Instance.Parameters)
            {
                if (item.ModelName == txtNewModelName.Text)
                {
                    MessageBox.Show($"Model [{txtNewModelName.Text}] had exist.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            SettingParams.Instance.Parameters.Add(new SettingParam()
            {
                ModelName = txtNewModelName.Text,
                Exposure = (float)numExposure.Value,
                Gain = (float)numGain.Value,
                Length = (int)numberLength.Value,
                Format = tbformat.Text,

            });
            Task.Run(() => ShowLog("Add new model : " + txtNewModelName.Text));
            InitModel();
            EnableControl(true);
        }

        private void btnSaveModel_Click(object sender, EventArgs e)
        {
            if (IndexModel != -1 && IndexModel >= 0)
            //if (IndexModel > 0)
            {
                SettingParams.Instance.Parameters[IndexModel].Exposure = (float)numExposure.Value;
                SettingParams.Instance.Parameters[IndexModel].Gain = (float)numGain.Value;
                SettingParams.Instance.Parameters[IndexModel].Length = (int)numberLength.Value;
                SettingParams.Instance.Parameters[IndexModel].Format = tbformat.Text;
                LoadModel(SettingParams.Instance.Parameters[IndexModel]);
                //InitModel();
                MessageBox.Show($"Save setting successfully.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void ShowLog(string msg, bool error = false)
        {

            Action action = () =>
            {
                if (error)
                {
                    tblog.SelectionColor = Color.Red;
                }

                StringBuilder sb = new StringBuilder();
                sb.Append(DateTime.Now.ToString("yy-MM-dd HH:mm:ss")).Append(" - ").Append(msg).Append(Environment.NewLine);

                tblog.AppendText(sb.ToString());
                tblog.ScrollToCaret();

                tblog.SelectionColor = Color.Black;
            };

            if (this.InvokeRequired)
                this.Invoke(action);
            else
                action();
        }
        public bool Checklen(string code)
        {
            bool result = true;
            if (code.Length != Global.length)
            {
                result = false;
            }
            return result;
        }
        public bool CheckFormat(string code)
        {
            if (string.IsNullOrEmpty(Global.format))
            {
                return true;
            }

            // Tách các phần tử trong Global.format dựa trên dấu '%'
            var formatParts = Global.format.Split('%');
            int currentIndex = 0;

            foreach (var part in formatParts)
            {
                if (string.IsNullOrEmpty(part)) continue; // Bỏ qua các phần tử trống do dấu '%'

                // Tìm vị trí của phần tử trong chuỗi code
                int foundIndex = code.IndexOf(part, currentIndex);
                if (foundIndex == -1)
                {
                    // Không tìm thấy phần tử hoặc không đúng thứ tự
                    return false;
                }
                // Cập nhật chỉ số bắt đầu tìm kiếm cho phần tử tiếp theo
                currentIndex = foundIndex + part.Length;
            }

            return true; // Tất cả các phần tử đều được tìm thấy theo đúng thứ tự
        }
        public bool CheckFormat1(string code)
        {
            if (string.IsNullOrEmpty(Global.format) || string.IsNullOrEmpty(code))
            {
                return false;
            }

            // Tách các phần tử trong Global.format dựa trên dấu '%'
            var formatParts = Global.format.Split('%');

            // Kiểm tra phần đầu (nếu không phải `%`)
            if (!string.IsNullOrEmpty(formatParts[0]) && !code.StartsWith(formatParts[0]))
            {
                return false;
            }

            // Kiểm tra phần cuối (nếu không phải `%`)
            string lastPart = formatParts[formatParts.Length - 1]; // Truy xuất phần tử cuối
            if (!string.IsNullOrEmpty(lastPart) && !code.EndsWith(lastPart))
            {
                return false;
            }

            // Kiểm tra các phần giữa (nếu có)
            int currentIndex = 0;
            foreach (var part in formatParts)
            {
                if (string.IsNullOrEmpty(part)) continue; // Bỏ qua các phần tử trống do dấu '%'

                // Tìm vị trí phần tử trong code
                int foundIndex = code.IndexOf(part, currentIndex);
                if (foundIndex == -1)
                {
                    return false; // Không tìm thấy phần tử
                }

                // Cập nhật chỉ số bắt đầu tìm kiếm
                currentIndex = foundIndex + part.Length;
            }

            return true; // Tất cả các phần tử đều được tìm thấy theo đúng thứ tự
        }

        public bool CheckFormatWithRegex(string code)
        {
            if (string.IsNullOrEmpty(Global.format) || string.IsNullOrEmpty(code))
            {
                return false;
            }

            // Thay dấu '%' trong format thành regex tương ứng
            string regexPattern = "^" + Regex.Escape(Global.format).Replace("%", ".*") + "$";

            // So khớp code với regex pattern
            return Regex.IsMatch(code, regexPattern);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //bool a =  CheckFormatWithRegex("Model@sdadsasol@umsadhsauhms");
            //bool b = Checklen("Model@sdadsasol@umsadhsauhms");
            //  udpChat.SendMessage("Model@sdadsasol@umsadhsauhms");

            var fakeBitmap = GenerateFakeBitmap();
            var fakeStrCodes = GenerateFakeCodes();
            var fakeROIs = GenerateFakeROIs();

            // Gọi hàm kiểm tra với dữ liệu giả
            ReaderControl_OnImageGrabbed(fakeBitmap, fakeStrCodes, fakeROIs);


        }

        //fakedata
        private static Bitmap GenerateFakeBitmap()
        {
            // Tạo một ảnh giả, có thể là ảnh trắng 100x100 pixel
            Bitmap fakeBitmap = new Bitmap(100, 100);
            using (Graphics g = Graphics.FromImage(fakeBitmap))
            {
                g.Clear(Color.White);  // Màu nền trắng
                g.FillRectangle(Brushes.Black, 10, 10, 80, 80);  // Vẽ một hình vuông đen
            }
            return fakeBitmap;
        }

        static int a = 0;
        static int a1 = 1000;
        static int a2 = 2000;
        static int a3 = 3000;

        // Hàm giả lập các mã (strCode)
        private static List<string> GenerateFakeCodes()
        {
            a++; a1++; a2++; a3++;
            return new List<string>
            {
            $"DM Code,aModelsdasas1solumsaauhms{a}",  // Một mã hợp lệ
            $"QRcode,aModelsassolumsahsauhms{a1}",  // Một mã hợp lệ
            $"QRcode,Modelsdssolumsadhsauhms{a2}",      // Một mã trống
            $"QRcode,Modelsdssolumsadsauh1ms{a3}"   // Một mã hợp lệ
            };

        }

        // Hàm giả lập các đối tượng ROI (MsPolygon)
        private static List<MsPolygon> GenerateFakeROIs()
        {
            return new List<MsPolygon>
            {
                new MsPolygon(new PointF[]
                {
                    new PointF(10, 10),
                    new PointF(90, 10),
                    new PointF(90, 90),
                    new PointF(10, 90)
                }),
                new MsPolygon(new PointF[]
                {
                    new PointF(20, 20),
                    new PointF(80, 20),
                    new PointF(80, 80),
                    new PointF(20, 80)
                })
            };
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "BarTender Files (*.btw)|*.btw"; // Chỉ hiển thị tệp .btw
                openFileDialog.Title = "Select a BarTender file";

                // Hiển thị hộp thoại chọn file
                DialogResult result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    lock (generationQueue)
                    {
                        generationQueue.Clear();
                    }
                    listItems.Clear();

                    // Lấy đường dẫn tệp được chọn
                    string selectedFilePath = openFileDialog.FileName;
                    txtFolderPath.Text = selectedFilePath;

                    // Mở file và lấy các biến
                    format = engine.Documents.Open(selectedFilePath);
                    if (format != null)
                    {
                        // Lưu các biến substring vào danh sách
                        templateVariables.Clear(); // Xóa các biến cũ nếu có
                        foreach (var substring in format.SubStrings)
                        {
                            templateVariables.Add(substring.Name);
                        }
                        Console.WriteLine(templateVariables.ToString());
                    }
                }
            }

        }

        private void PrintLabel(string date, string MODEL, string seri, string SECCODE1, string SERI, string Model, string DATE, int qty)
        {
            lock (engine)
            {
                bool success = true;

                // Kiểm tra và thiết lập số lượng bản sao
                if (format.PrintSetup.SupportsIdenticalCopies)
                {
                    int copies = 1;
                    success = Int32.TryParse(txtIdenticalCopies.Text, out copies) && (copies >= 1);
                    if (!success)
                        ShowLog("Identical Copies must be an integer greater than or equal to 1: " + appName);
                    else
                        format.PrintSetup.IdenticalCopiesOfLabel = copies;
                }

                if (success && format.PrintSetup.SupportsSerializedLabels)
                {
                    int copies = 1;
                    success = Int32.TryParse(txtSerializedCopies.Text, out copies) && (copies >= 1);
                    if (!success)
                    {
                        ShowLog( "Serialized Copies must be an integer greater than or equal to 1: "+ appName);
                    }
                    else
                    {
                        format.PrintSetup.NumberOfSerializedLabels = copies;
                    }
                }

                if (success)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    foreach (var substring in format.SubStrings)
                    {
                        int index = templateVariables.IndexOf(substring.Name);
                        if (index >= 0 && index < templateVariables.Count)
                        {
                            switch (substring.Name)
                            {
                                case "date":
                                    substring.Value = date;
                                    break;
                                case "MODEL":
                                    substring.Value = MODEL;
                                    break;
                                case "seri":
                                    substring.Value = seri;
                                    break;
                                case "SECCODE1":
                                    substring.Value = SECCODE1;
                                    break;
                                case "SERI":
                                    substring.Value = SERI;
                                    break;
                                case "DATE":
                                    substring.Value = DATE;
                                    break;
                                case "Model":
                                    substring.Value = Model;
                                    break;
                                case "Q'ty":
                                    substring.Value = qty.ToString();
                                    break;

                                default:
                                    substring.Value = "";
                                    break;

                            }

                        }
                    }

                    if (cboPrinters.SelectedItem != null)
                        format.PrintSetup.PrinterName = cboPrinters.SelectedItem.ToString();

                    Messages messages;
                    int waitForCompletionTimeout = 10000; // 10 giây
                    Result result = format.Print(ClassifyResult.Instance.ApplicationName, waitForCompletionTimeout, out messages);
                    string messageString = "\n\nMessages:";

                    if (result == Result.Failure)
                        ShowLog("Print Failed" + messageString);
                    else
                        ShowLog("Label was successfully sent to printer.");
                }
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            PrintLabel(Global.date, Global.MODEL, Global.seri, Global.SECCODE1, ClassifyResult.Instance.seri.ToString("000"), Global.model, Global.DATE, Global.Qty);
        }

        private void btnGraphicImage_Click(object sender, EventArgs e)
        {
            string path = ClassCommon.FolderAutoCreate.GetFullPathByChildFolders("ScreenCapture");
            Process.Start(path);
        }

        private void btnRunImage_Click(object sender, EventArgs e)
        {
            string path = ClassCommon.FolderAutoCreate.GetFullPathByChildFolders("RunImage");
            Process.Start(path);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var path64 = Path.Combine(Directory.GetDirectories(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
            var path32 = @"C:\windows\system32\osk.exe";
            var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
            if (File.Exists(path))
            {
                Process.Start(path);
            }
        }
    }
}
