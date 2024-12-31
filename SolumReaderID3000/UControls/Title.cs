﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace SconnectCamdeco
{
    public partial class Title : UserControl
    {
        public event EventHandler OnProgramExitting;
        public Title()
        {
            InitializeComponent();
            //mouseHoverEvt.CreateMouseHoverEvent(panel2);
            lbTimer.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }

        private Image titleLogo;
        public Image TitleLogo
        {
            get { return titleLogo; }
            set
            {
                titleLogo = value;
                ptbLoGo.Image = value;
            }
        }

        private string titleName;
        public string TitleName
        {
            get { return titleName; }
            set
            {
                titleName = value.ToUpper();
                lbNameTittle.Text = value.ToUpper();
            }
        }
        public void btnExit_Click(object sender, EventArgs e)
        {
            OnProgramExitting?.Invoke(this, EventArgs.Empty);
        }
        public void Close()
        {
            string message = "Do you want to close this Application?";
            string title = "Close Window";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.ExitThread();
                Application.Exit();
                Environment.Exit(0);
            }
        }
        private void btnMinForm_Click(object sender, EventArgs e)
        {
            this.ParentForm.WindowState = FormWindowState.Minimized;
        }

        string timeReset = string.Empty;
        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            Action act = new Action(() =>
            {
                lbTimer.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                timeReset = DateTime.Now.ToString("HH:mm:ss");

            });
            if (this.InvokeRequired)
                this.Invoke(act);
            else
                act();
        }
    }


}
