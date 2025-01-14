using MSFactoryDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolumReaderID3000
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }
        public event Action<string> SerialPortSettingsChanged;

        private void Setting_Load(object sender, EventArgs e)
        {
            ckbSaveRunImage.Checked = ClassifyResult.Instance.SaveRunImage;
            tbNameApp.Text=ClassifyResult.Instance.ApplicationName;
            tbserver.Text=ClassifyResult.Instance.ServerIP;
            numberRport.Value= ClassifyResult.Instance.RPort;
            numberSport.Value= ClassifyResult.Instance.Sport;
            numbaud.Value= ClassifyResult.Instance.baudrate;

            cbCOMPorts.Items.AddRange(SerialPort.GetPortNames());
            if (cbCOMPorts.Items.Count > 0)
            {
                cbCOMPorts.SelectedItem = ClassifyResult.Instance.SerialPort;
            }
           

        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            bool changecom=false;
            ClassifyResult.Instance.SaveRunImage = ckbSaveRunImage.Checked;
            string serverIP = tbserver.Text;
            if (IsValidIPAddress(serverIP))
            {
                ClassifyResult.Instance.ServerIP = tbserver.Text;
                Console.WriteLine("Địa chỉ IP hợp lệ.");
            }
            else
            {
                Console.WriteLine("Địa chỉ IP không hợp lệ.");
                MessageBox.Show("Invalid IP address.", "Check IP format", MessageBoxButtons.OK);
                return;

            }            
            ClassifyResult.Instance.ApplicationName = tbNameApp.Text;
            if (ClassifyResult.Instance.RPort != (int)numberRport.Value)
            {
                ClassifyResult.Instance.RPort = (int)numberRport.Value;
            }
            if (ClassifyResult.Instance.Sport != (int)numberSport.Value)
            {
                ClassifyResult.Instance.Sport = (int)numberSport.Value;
            } 
            if (ClassifyResult.Instance.baudrate != (int)numbaud.Value)
            {
                ClassifyResult.Instance.baudrate = (int)numbaud.Value;
                changecom = true;
            }
            if (!string.IsNullOrWhiteSpace(cbCOMPorts.Text))
            {
                ClassifyResult.Instance.SerialPort = cbCOMPorts.Text;
                changecom= true;
            }
            ClassifyResult.Instance.Save();
            if (changecom)
            {
                SerialPortSettingsChanged?.Invoke(ClassifyResult.Instance.SerialPort);
            }   
            
            Global.date= tbdate.Text;
            Global.MODEL=tbMODEL.Text;
            Global.seri=tbseri.Text;
            Global.SECCODE1=tbseccode1.Text;
            Global.SERI=txtSERI.Text;          
            Global.DATE=txtDATE.Text;
            Global.Qty =(int) numqty.Value;




        }
        public static bool IsValidIPAddress(string ipAddress)
        {
            return IPAddress.TryParse(ipAddress, out _);
        }
    }
}
