using MSFactoryDLL;
using System;
using SolumReaderID3000.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SolumReaderID3000
{
    public partial class fMain : Form
    {
        UDPControl udpChat ;
        public fMain()
        {
            udpChat = new UDPControl("192.168.1.14");
            InitializeComponent();
            InitReader();
            InitModel();
            InitLogCSV();
            EnableControl(true);
            this.title.OnProgramExitting += OnProgramExitting;
            btnLoadModel_Click(null, null);

            //maximum form 
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
                this.WindowState = FormWindowState.Normal;

            udpChat.StartServer();
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

        public LogCSV logCSV;
        private string currentModel = string.Empty;
        private string strHeaders = string.Empty;
        private void InitLogCSV()
        {
            try
            {
                Action action = () =>
                {
                    ClassCommon.FolderAutoCreate = new FolderAutoCreate(Application.StartupPath, 30);

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
            }
        }

        private void ReaderControl_OnImageGrabbed(System.Drawing.Bitmap bitmap, List<string> strCode, List<MsPolygon> ROIs)
        {
            Action action = () =>
            {
                imageBox1.ClearGraphics();
                imageBox1.Image = bitmap;

                foreach (var item in ROIs)
                {
                    imageBox1.AddGraphics(item);
                }
                if (strCode.Count == 0)
                {
                    ClassifyResult.Instance.RunResult = ClassifyResult.eFinalResult.NG;
                    logCSV.SaveLog($"{ClassifyResult.Instance.Total},{currentModel},NA,NA,{ClassifyResult.Instance.RunResult}");
                }
                else
                {
                    foreach (var item in strCode)
                    {
                        MsString msString = new MsString(item, new System.Drawing.PointF(10, 10), new Font("Arial", 15.0f), new SolidBrush(Color.Green));
                        imageBox1.AddGraphics(msString);
                        if (string.IsNullOrWhiteSpace(item))
                            ClassifyResult.Instance.RunResult = ClassifyResult.eFinalResult.NG;
                        else
                        {
                            ClassifyResult.Instance.RunResult = ClassifyResult.eFinalResult.OK;
                        }

                        logCSV.SaveLog($"{ClassifyResult.Instance.Total},{currentModel},{item.Replace("\0", "")},{ClassifyResult.Instance.RunResult}");
                    }
                }
                ClassifyResult.Instance.Save();
            };
            if (imageBox1.InvokeRequired)
                imageBox1.Invoke(action);
            else
                action();
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
                GetIndexModel(cbbModel.Text);
                if (sender != null)
                    ClassifyResult.Instance.ResetData();
                LoadModel(SettingParams.Instance.Parameters[IndexModel]);
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
            ClassifyResult.Instance.Model = setting.ModelName;
            ClassifyResult.Instance.Save();
            currentModel = setting.ModelName;
            cbbModel.Text = currentModel;
            numberLength.Value= setting.Length;
            tbformat.Text = setting.Format;
            this.title.TitleName = $"{ClassifyResult.Instance.ApplicationName} [{currentModel}]";
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
                Length =(int) numberLength.Value,
                Format=tbformat.Text,
                
            });
            InitModel();
            EnableControl(true);
        }

        private void btnSaveModel_Click(object sender, EventArgs e)
        {
            if (IndexModel != -1&& IndexModel >= 0)
            //if (IndexModel > 0)
            {
                SettingParams.Instance.Parameters[IndexModel].Exposure = (float)numExposure.Value;
                SettingParams.Instance.Parameters[IndexModel].Gain = (float)numGain.Value;
                SettingParams.Instance.Parameters[IndexModel].Length=(int) numberLength.Value;
                SettingParams.Instance.Parameters[IndexModel].Format= tbformat.Text;

                InitModel();
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

        public bool CheckFormatWithRegex( string code)
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
          bool a =  CheckFormatWithRegex("Model@sdadsasol@umsadhsauhms");
          bool b = Checklen("Model@sdadsasol@umsadhsauhms");
            udpChat.SendMessage("Model@sdadsasol@umsadhsauhms");

        }


    }
}
