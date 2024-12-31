namespace SconnectCamdeco
{
    partial class Title
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnMinForm = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lbTimer = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ptbLoGo = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbNameTittle = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbLoGo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnMinForm);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.lbTimer);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.ForeColor = System.Drawing.Color.Blue;
            this.panel2.Location = new System.Drawing.Point(732, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(304, 45);
            this.panel2.TabIndex = 6;
            // 
            // btnMinForm
            // 
            this.btnMinForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(59)))));
            this.btnMinForm.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMinForm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(102)))), ((int)(((byte)(110)))));
            this.btnMinForm.FlatAppearance.BorderSize = 2;
            this.btnMinForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinForm.ForeColor = System.Drawing.Color.White;
            this.btnMinForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMinForm.Location = new System.Drawing.Point(229, 4);
            this.btnMinForm.Margin = new System.Windows.Forms.Padding(1);
            this.btnMinForm.Name = "btnMinForm";
            this.btnMinForm.Size = new System.Drawing.Size(30, 30);
            this.btnMinForm.TabIndex = 2;
            this.btnMinForm.TabStop = false;
            this.btnMinForm.Tag = "Minimize";
            this.btnMinForm.Text = "_";
            this.btnMinForm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMinForm.UseVisualStyleBackColor = false;
            this.btnMinForm.Click += new System.EventHandler(this.btnMinForm_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(59)))));
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(102)))), ((int)(((byte)(110)))));
            this.btnExit.FlatAppearance.BorderSize = 2;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(263, 4);
            this.btnExit.Margin = new System.Windows.Forms.Padding(1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(30, 30);
            this.btnExit.TabIndex = 1;
            this.btnExit.TabStop = false;
            this.btnExit.Tag = "Exit";
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lbTimer
            // 
            this.lbTimer.AutoSize = true;
            this.lbTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.lbTimer.ForeColor = System.Drawing.Color.White;
            this.lbTimer.Location = new System.Drawing.Point(3, 13);
            this.lbTimer.Name = "lbTimer";
            this.lbTimer.Size = new System.Drawing.Size(172, 22);
            this.lbTimer.TabIndex = 4;
            this.lbTimer.Text = "__/__/__ __:__:__";
            this.lbTimer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(208, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(3, 35);
            this.label2.TabIndex = 3;
            // 
            // ptbLoGo
            // 
            this.ptbLoGo.BackColor = System.Drawing.Color.White;
            this.ptbLoGo.Dock = System.Windows.Forms.DockStyle.Left;
            this.ptbLoGo.Location = new System.Drawing.Point(0, 0);
            this.ptbLoGo.Name = "ptbLoGo";
            this.ptbLoGo.Size = new System.Drawing.Size(68, 45);
            this.ptbLoGo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbLoGo.TabIndex = 5;
            this.ptbLoGo.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1036, 3);
            this.panel1.TabIndex = 4;
            // 
            // lbNameTittle
            // 
            this.lbNameTittle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNameTittle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNameTittle.ForeColor = System.Drawing.Color.White;
            this.lbNameTittle.Location = new System.Drawing.Point(68, 0);
            this.lbNameTittle.Name = "lbNameTittle";
            this.lbNameTittle.Size = new System.Drawing.Size(664, 45);
            this.lbNameTittle.TabIndex = 7;
            this.lbNameTittle.Text = "Name title";
            this.lbNameTittle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // Title
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(59)))));
            this.Controls.Add(this.lbNameTittle);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ptbLoGo);
            this.Controls.Add(this.panel1);
            this.Name = "Title";
            this.Size = new System.Drawing.Size(1036, 48);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbLoGo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnMinForm;
        public System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lbTimer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.PictureBox ptbLoGo;
        public System.Windows.Forms.Label lbNameTittle;
        private System.Windows.Forms.Timer timer1;
    }
}
