namespace SolumReaderID3000
{
    partial class fMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imageBox1 = new MSFactoryDLL.ImageBox();
            this.dgvLogCSV = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlParams = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ptbLoGo = new System.Windows.Forms.PictureBox();
            this.tblog = new System.Windows.Forms.RichTextBox();
            this.grbReaderParams = new System.Windows.Forms.GroupBox();
            this.grbformat = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tbformat = new System.Windows.Forms.TextBox();
            this.numberLength = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numGain = new System.Windows.Forms.NumericUpDown();
            this.numExposure = new System.Windows.Forms.NumericUpDown();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnSaveAsNew = new System.Windows.Forms.Button();
            this.btnSaveModel = new System.Windows.Forms.Button();
            this.lbFrameRate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grbSaveNewModel = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSaveNew = new System.Windows.Forms.Button();
            this.txtNewModelName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlModel = new System.Windows.Forms.Panel();
            this.cbbModel = new System.Windows.Forms.ComboBox();
            this.btnLoadModel = new System.Windows.Forms.Button();
            this.title = new SconnectCamdeco.Title();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogCSV)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlParams.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbLoGo)).BeginInit();
            this.grbReaderParams.SuspendLayout();
            this.grbformat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExposure)).BeginInit();
            this.grbSaveNewModel.SuspendLayout();
            this.pnlModel.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageBox1
            // 
            this.imageBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(67)))), ((int)(((byte)(78)))));
            this.imageBox1.GridColorAlternate = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(67)))), ((int)(((byte)(78)))));
            this.imageBox1.IsViewPlusMarker = false;
            this.imageBox1.Location = new System.Drawing.Point(3, 3);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(752, 505);
            this.imageBox1.TabIndex = 1;
            // 
            // dgvLogCSV
            // 
            this.dgvLogCSV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogCSV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLogCSV.Location = new System.Drawing.Point(3, 514);
            this.dgvLogCSV.Name = "dgvLogCSV";
            this.dgvLogCSV.Size = new System.Drawing.Size(752, 182);
            this.dgvLogCSV.TabIndex = 18;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel1.Controls.Add(this.dgvLogCSV, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.imageBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlParams, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 62);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.2F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.8F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1108, 699);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pnlParams
            // 
            this.pnlParams.Controls.Add(this.panel1);
            this.pnlParams.Controls.Add(this.tblog);
            this.pnlParams.Controls.Add(this.grbReaderParams);
            this.pnlParams.Controls.Add(this.grbSaveNewModel);
            this.pnlParams.Controls.Add(this.pnlModel);
            this.pnlParams.Location = new System.Drawing.Point(761, 3);
            this.pnlParams.Name = "pnlParams";
            this.tableLayoutPanel1.SetRowSpan(this.pnlParams, 2);
            this.pnlParams.Size = new System.Drawing.Size(344, 693);
            this.pnlParams.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ptbLoGo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 644);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 49);
            this.panel1.TabIndex = 16;
            // 
            // ptbLoGo
            // 
            this.ptbLoGo.BackColor = System.Drawing.Color.White;
            this.ptbLoGo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ptbLoGo.Image = global::SolumReaderID3000.Properties.Resources.Logo;
            this.ptbLoGo.Location = new System.Drawing.Point(0, 0);
            this.ptbLoGo.Name = "ptbLoGo";
            this.ptbLoGo.Size = new System.Drawing.Size(344, 49);
            this.ptbLoGo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbLoGo.TabIndex = 14;
            this.ptbLoGo.TabStop = false;
            // 
            // tblog
            // 
            this.tblog.BackColor = System.Drawing.Color.White;
            this.tblog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblog.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblog.ForeColor = System.Drawing.Color.Black;
            this.tblog.Location = new System.Drawing.Point(0, 466);
            this.tblog.Name = "tblog";
            this.tblog.ReadOnly = true;
            this.tblog.Size = new System.Drawing.Size(344, 227);
            this.tblog.TabIndex = 15;
            this.tblog.Text = "";
            // 
            // grbReaderParams
            // 
            this.grbReaderParams.Controls.Add(this.grbformat);
            this.grbReaderParams.Controls.Add(this.numGain);
            this.grbReaderParams.Controls.Add(this.numExposure);
            this.grbReaderParams.Controls.Add(this.btnRemove);
            this.grbReaderParams.Controls.Add(this.btnSaveAsNew);
            this.grbReaderParams.Controls.Add(this.btnSaveModel);
            this.grbReaderParams.Controls.Add(this.lbFrameRate);
            this.grbReaderParams.Controls.Add(this.label3);
            this.grbReaderParams.Controls.Add(this.label2);
            this.grbReaderParams.Controls.Add(this.label1);
            this.grbReaderParams.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbReaderParams.ForeColor = System.Drawing.Color.White;
            this.grbReaderParams.Location = new System.Drawing.Point(0, 153);
            this.grbReaderParams.Name = "grbReaderParams";
            this.grbReaderParams.Size = new System.Drawing.Size(344, 313);
            this.grbReaderParams.TabIndex = 6;
            this.grbReaderParams.TabStop = false;
            this.grbReaderParams.Text = "READER";
            this.grbReaderParams.Visible = false;
            // 
            // grbformat
            // 
            this.grbformat.Controls.Add(this.button1);
            this.grbformat.Controls.Add(this.tbformat);
            this.grbformat.Controls.Add(this.numberLength);
            this.grbformat.Controls.Add(this.label7);
            this.grbformat.Controls.Add(this.label8);
            this.grbformat.ForeColor = System.Drawing.Color.White;
            this.grbformat.Location = new System.Drawing.Point(10, 170);
            this.grbformat.Name = "grbformat";
            this.grbformat.Size = new System.Drawing.Size(325, 131);
            this.grbformat.TabIndex = 11;
            this.grbformat.TabStop = false;
            this.grbformat.Text = "Format Code";
            this.grbformat.Visible = false;
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(234, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 50);
            this.button1.TabIndex = 10;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbformat
            // 
            this.tbformat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbformat.Location = new System.Drawing.Point(89, 64);
            this.tbformat.Name = "tbformat";
            this.tbformat.Size = new System.Drawing.Size(232, 23);
            this.tbformat.TabIndex = 9;
            // 
            // numberLength
            // 
            this.numberLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.numberLength.Location = new System.Drawing.Point(92, 30);
            this.numberLength.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numberLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberLength.Name = "numberLength";
            this.numberLength.Size = new System.Drawing.Size(120, 23);
            this.numberLength.TabIndex = 8;
            this.numberLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numberLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(20, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 17);
            this.label7.TabIndex = 4;
            this.label7.Text = "Format:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(20, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 17);
            this.label8.TabIndex = 3;
            this.label8.Text = "Length:";
            // 
            // numGain
            // 
            this.numGain.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.numGain.Location = new System.Drawing.Point(99, 67);
            this.numGain.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numGain.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numGain.Name = "numGain";
            this.numGain.Size = new System.Drawing.Size(120, 23);
            this.numGain.TabIndex = 8;
            this.numGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numGain.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numExposure
            // 
            this.numExposure.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.numExposure.Location = new System.Drawing.Point(99, 30);
            this.numExposure.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numExposure.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numExposure.Name = "numExposure";
            this.numExposure.Size = new System.Drawing.Size(120, 23);
            this.numExposure.TabIndex = 8;
            this.numExposure.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numExposure.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnRemove
            // 
            this.btnRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnRemove.ForeColor = System.Drawing.Color.Black;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(246, 137);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(85, 27);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSaveAsNew
            // 
            this.btnSaveAsNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveAsNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSaveAsNew.ForeColor = System.Drawing.Color.Black;
            this.btnSaveAsNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSaveAsNew.Location = new System.Drawing.Point(246, 75);
            this.btnSaveAsNew.Name = "btnSaveAsNew";
            this.btnSaveAsNew.Size = new System.Drawing.Size(85, 50);
            this.btnSaveAsNew.TabIndex = 7;
            this.btnSaveAsNew.Text = "Save As New";
            this.btnSaveAsNew.UseVisualStyleBackColor = true;
            this.btnSaveAsNew.Click += new System.EventHandler(this.btnSaveAsNew_Click);
            // 
            // btnSaveModel
            // 
            this.btnSaveModel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSaveModel.ForeColor = System.Drawing.Color.Black;
            this.btnSaveModel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSaveModel.Location = new System.Drawing.Point(246, 19);
            this.btnSaveModel.Name = "btnSaveModel";
            this.btnSaveModel.Size = new System.Drawing.Size(85, 50);
            this.btnSaveModel.TabIndex = 7;
            this.btnSaveModel.Text = "Save";
            this.btnSaveModel.UseVisualStyleBackColor = true;
            this.btnSaveModel.Click += new System.EventHandler(this.btnSaveModel_Click);
            // 
            // lbFrameRate
            // 
            this.lbFrameRate.AutoSize = true;
            this.lbFrameRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbFrameRate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbFrameRate.Location = new System.Drawing.Point(146, 106);
            this.lbFrameRate.Name = "lbFrameRate";
            this.lbFrameRate.Size = new System.Drawing.Size(24, 17);
            this.lbFrameRate.TabIndex = 5;
            this.lbFrameRate.Text = "60";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(16, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "FrameRate:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(16, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Gain:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(16, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Exposure:";
            // 
            // grbSaveNewModel
            // 
            this.grbSaveNewModel.Controls.Add(this.btnCancel);
            this.grbSaveNewModel.Controls.Add(this.btnSaveNew);
            this.grbSaveNewModel.Controls.Add(this.txtNewModelName);
            this.grbSaveNewModel.Controls.Add(this.label5);
            this.grbSaveNewModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbSaveNewModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.grbSaveNewModel.ForeColor = System.Drawing.Color.White;
            this.grbSaveNewModel.Location = new System.Drawing.Point(0, 33);
            this.grbSaveNewModel.Name = "grbSaveNewModel";
            this.grbSaveNewModel.Size = new System.Drawing.Size(344, 120);
            this.grbSaveNewModel.TabIndex = 9;
            this.grbSaveNewModel.TabStop = false;
            this.grbSaveNewModel.Text = "Save as new model";
            this.grbSaveNewModel.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(250, 61);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 45);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSaveNew
            // 
            this.btnSaveNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSaveNew.ForeColor = System.Drawing.Color.Black;
            this.btnSaveNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSaveNew.Location = new System.Drawing.Point(153, 61);
            this.btnSaveNew.Name = "btnSaveNew";
            this.btnSaveNew.Size = new System.Drawing.Size(85, 45);
            this.btnSaveNew.TabIndex = 7;
            this.btnSaveNew.Text = "Save new model";
            this.btnSaveNew.UseVisualStyleBackColor = true;
            this.btnSaveNew.Click += new System.EventHandler(this.btnSaveNew_Click);
            // 
            // txtNewModelName
            // 
            this.txtNewModelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNewModelName.Location = new System.Drawing.Point(103, 32);
            this.txtNewModelName.Name = "txtNewModelName";
            this.txtNewModelName.Size = new System.Drawing.Size(232, 23);
            this.txtNewModelName.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(8, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "Model name:";
            // 
            // pnlModel
            // 
            this.pnlModel.Controls.Add(this.cbbModel);
            this.pnlModel.Controls.Add(this.btnLoadModel);
            this.pnlModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlModel.Location = new System.Drawing.Point(0, 0);
            this.pnlModel.Name = "pnlModel";
            this.pnlModel.Size = new System.Drawing.Size(344, 33);
            this.pnlModel.TabIndex = 8;
            // 
            // cbbModel
            // 
            this.cbbModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbbModel.FormattingEnabled = true;
            this.cbbModel.Location = new System.Drawing.Point(0, 0);
            this.cbbModel.Name = "cbbModel";
            this.cbbModel.Size = new System.Drawing.Size(262, 28);
            this.cbbModel.TabIndex = 7;
            // 
            // btnLoadModel
            // 
            this.btnLoadModel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLoadModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnLoadModel.Location = new System.Drawing.Point(262, 0);
            this.btnLoadModel.Name = "btnLoadModel";
            this.btnLoadModel.Size = new System.Drawing.Size(82, 33);
            this.btnLoadModel.TabIndex = 8;
            this.btnLoadModel.Text = "Load";
            this.btnLoadModel.UseVisualStyleBackColor = true;
            this.btnLoadModel.Click += new System.EventHandler(this.btnLoadModel_Click);
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(59)))));
            this.title.Dock = System.Windows.Forms.DockStyle.Top;
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(1108, 62);
            this.title.TabIndex = 0;
            this.title.TitleLogo = global::SolumReaderID3000.Properties.Resources.solum;
            this.title.TitleName = "SOLUM QR READER";
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(67)))), ((int)(((byte)(78)))));
            this.ClientSize = new System.Drawing.Size(1108, 761);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogCSV)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlParams.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptbLoGo)).EndInit();
            this.grbReaderParams.ResumeLayout(false);
            this.grbReaderParams.PerformLayout();
            this.grbformat.ResumeLayout(false);
            this.grbformat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExposure)).EndInit();
            this.grbSaveNewModel.ResumeLayout(false);
            this.grbSaveNewModel.PerformLayout();
            this.pnlModel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SconnectCamdeco.Title title;
        private MSFactoryDLL.ImageBox imageBox1;
        private System.Windows.Forms.DataGridView dgvLogCSV;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlParams;
        private System.Windows.Forms.GroupBox grbReaderParams;
        private System.Windows.Forms.NumericUpDown numGain;
        private System.Windows.Forms.NumericUpDown numExposure;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnSaveAsNew;
        private System.Windows.Forms.Button btnSaveModel;
        private System.Windows.Forms.Label lbFrameRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grbSaveNewModel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSaveNew;
        private System.Windows.Forms.TextBox txtNewModelName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlModel;
        private System.Windows.Forms.ComboBox cbbModel;
        private System.Windows.Forms.Button btnLoadModel;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.PictureBox ptbLoGo;
        private System.Windows.Forms.RichTextBox tblog;
        private System.Windows.Forms.GroupBox grbformat;
        private System.Windows.Forms.TextBox tbformat;
        private System.Windows.Forms.NumericUpDown numberLength;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
    }
}

