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
            this.dgvLogCSV = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.imageBox1 = new MSFactoryDLL.ImageBox();
            this.pnlParams = new System.Windows.Forms.Panel();
            this.tblog = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucResult1 = new SolumReaderID3000.UControls.ucResult();
            this.grbReaderParams = new System.Windows.Forms.GroupBox();
            this.groupreader = new System.Windows.Forms.GroupBox();
            this.numGain = new System.Windows.Forms.NumericUpDown();
            this.numExposure = new System.Windows.Forms.NumericUpDown();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnSaveAsNew = new System.Windows.Forms.Button();
            this.btnSaveModel = new System.Windows.Forms.Button();
            this.lbFrameRate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Grouplot = new System.Windows.Forms.GroupBox();
            this.numqty = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.grbformat = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tbformat = new System.Windows.Forms.TextBox();
            this.numberLength = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
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
            this.groupBox1.SuspendLayout();
            this.grbReaderParams.SuspendLayout();
            this.groupreader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExposure)).BeginInit();
            this.Grouplot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numqty)).BeginInit();
            this.grbformat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberLength)).BeginInit();
            this.grbSaveNewModel.SuspendLayout();
            this.pnlModel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLogCSV
            // 
            this.dgvLogCSV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogCSV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLogCSV.Location = new System.Drawing.Point(3, 523);
            this.dgvLogCSV.Name = "dgvLogCSV";
            this.dgvLogCSV.Size = new System.Drawing.Size(752, 185);
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 50);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.2F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.8F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1108, 711);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // imageBox1
            // 
            this.imageBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(67)))), ((int)(((byte)(78)))));
            this.imageBox1.GridColorAlternate = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(67)))), ((int)(((byte)(78)))));
            this.imageBox1.IsViewPlusMarker = false;
            this.imageBox1.Location = new System.Drawing.Point(3, 3);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(752, 514);
            this.imageBox1.TabIndex = 1;
            // 
            // pnlParams
            // 
            this.pnlParams.Controls.Add(this.tblog);
            this.pnlParams.Controls.Add(this.groupBox1);
            this.pnlParams.Controls.Add(this.grbReaderParams);
            this.pnlParams.Controls.Add(this.grbSaveNewModel);
            this.pnlParams.Controls.Add(this.pnlModel);
            this.pnlParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlParams.Location = new System.Drawing.Point(761, 3);
            this.pnlParams.Name = "pnlParams";
            this.tableLayoutPanel1.SetRowSpan(this.pnlParams, 2);
            this.pnlParams.Size = new System.Drawing.Size(344, 705);
            this.pnlParams.TabIndex = 2;
            // 
            // tblog
            // 
            this.tblog.BackColor = System.Drawing.Color.White;
            this.tblog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblog.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblog.ForeColor = System.Drawing.Color.Black;
            this.tblog.Location = new System.Drawing.Point(0, 647);
            this.tblog.Name = "tblog";
            this.tblog.ReadOnly = true;
            this.tblog.Size = new System.Drawing.Size(344, 58);
            this.tblog.TabIndex = 25;
            this.tblog.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucResult1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(0, 428);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 219);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Statistical";
            // 
            // ucResult1
            // 
            this.ucResult1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucResult1.Location = new System.Drawing.Point(3, 16);
            this.ucResult1.Name = "ucResult1";
            this.ucResult1.Size = new System.Drawing.Size(338, 200);
            this.ucResult1.TabIndex = 0;
            // 
            // grbReaderParams
            // 
            this.grbReaderParams.Controls.Add(this.groupreader);
            this.grbReaderParams.Controls.Add(this.Grouplot);
            this.grbReaderParams.Controls.Add(this.grbformat);
            this.grbReaderParams.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbReaderParams.ForeColor = System.Drawing.Color.White;
            this.grbReaderParams.Location = new System.Drawing.Point(0, 134);
            this.grbReaderParams.Name = "grbReaderParams";
            this.grbReaderParams.Size = new System.Drawing.Size(344, 294);
            this.grbReaderParams.TabIndex = 6;
            this.grbReaderParams.TabStop = false;
            this.grbReaderParams.Text = "Model Config";
            this.grbReaderParams.Visible = false;
            // 
            // groupreader
            // 
            this.groupreader.Controls.Add(this.numGain);
            this.groupreader.Controls.Add(this.numExposure);
            this.groupreader.Controls.Add(this.btnRemove);
            this.groupreader.Controls.Add(this.btnSaveAsNew);
            this.groupreader.Controls.Add(this.btnSaveModel);
            this.groupreader.Controls.Add(this.lbFrameRate);
            this.groupreader.Controls.Add(this.label3);
            this.groupreader.Controls.Add(this.label2);
            this.groupreader.Controls.Add(this.label1);
            this.groupreader.ForeColor = System.Drawing.Color.White;
            this.groupreader.Location = new System.Drawing.Point(10, 19);
            this.groupreader.Name = "groupreader";
            this.groupreader.Size = new System.Drawing.Size(325, 143);
            this.groupreader.TabIndex = 13;
            this.groupreader.TabStop = false;
            this.groupreader.Text = "CodeReader";
            this.groupreader.Visible = false;
            // 
            // numGain
            // 
            this.numGain.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.numGain.Location = new System.Drawing.Point(88, 56);
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
            this.numGain.TabIndex = 16;
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
            this.numExposure.Location = new System.Drawing.Point(88, 19);
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
            this.numExposure.TabIndex = 17;
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
            this.btnRemove.Location = new System.Drawing.Point(235, 113);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(85, 27);
            this.btnRemove.TabIndex = 13;
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
            this.btnSaveAsNew.Location = new System.Drawing.Point(235, 61);
            this.btnSaveAsNew.Name = "btnSaveAsNew";
            this.btnSaveAsNew.Size = new System.Drawing.Size(85, 50);
            this.btnSaveAsNew.TabIndex = 14;
            this.btnSaveAsNew.Text = "Save As New";
            this.btnSaveAsNew.UseVisualStyleBackColor = true;
            this.btnSaveAsNew.Click += new System.EventHandler(this.btnSaveNew_Click);
            // 
            // btnSaveModel
            // 
            this.btnSaveModel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSaveModel.ForeColor = System.Drawing.Color.Black;
            this.btnSaveModel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSaveModel.Location = new System.Drawing.Point(235, 8);
            this.btnSaveModel.Name = "btnSaveModel";
            this.btnSaveModel.Size = new System.Drawing.Size(85, 50);
            this.btnSaveModel.TabIndex = 15;
            this.btnSaveModel.Text = "Save";
            this.btnSaveModel.UseVisualStyleBackColor = true;
            this.btnSaveModel.Click += new System.EventHandler(this.btnSaveModel_Click);
            // 
            // lbFrameRate
            // 
            this.lbFrameRate.AutoSize = true;
            this.lbFrameRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbFrameRate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbFrameRate.Location = new System.Drawing.Point(135, 95);
            this.lbFrameRate.Name = "lbFrameRate";
            this.lbFrameRate.Size = new System.Drawing.Size(24, 17);
            this.lbFrameRate.TabIndex = 11;
            this.lbFrameRate.Text = "60";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(5, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "FrameRate:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(5, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Gain:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(5, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Exposure:";
            // 
            // Grouplot
            // 
            this.Grouplot.Controls.Add(this.numqty);
            this.Grouplot.Controls.Add(this.label6);
            this.Grouplot.ForeColor = System.Drawing.Color.White;
            this.Grouplot.Location = new System.Drawing.Point(10, 243);
            this.Grouplot.Name = "Grouplot";
            this.Grouplot.Size = new System.Drawing.Size(325, 44);
            this.Grouplot.TabIndex = 12;
            this.Grouplot.TabStop = false;
            this.Grouplot.Text = "Lot ";
            this.Grouplot.Visible = false;
            // 
            // numqty
            // 
            this.numqty.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.numqty.Location = new System.Drawing.Point(92, 15);
            this.numqty.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numqty.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numqty.Name = "numqty";
            this.numqty.Size = new System.Drawing.Size(120, 23);
            this.numqty.TabIndex = 8;
            this.numqty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numqty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(20, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 17);
            this.label6.TabIndex = 3;
            this.label6.Text = "Q\'ty:";
            // 
            // grbformat
            // 
            this.grbformat.Controls.Add(this.button1);
            this.grbformat.Controls.Add(this.tbformat);
            this.grbformat.Controls.Add(this.numberLength);
            this.grbformat.Controls.Add(this.label7);
            this.grbformat.Controls.Add(this.label8);
            this.grbformat.ForeColor = System.Drawing.Color.White;
            this.grbformat.Location = new System.Drawing.Point(10, 163);
            this.grbformat.Name = "grbformat";
            this.grbformat.Size = new System.Drawing.Size(325, 77);
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
            this.button1.Location = new System.Drawing.Point(234, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 33);
            this.button1.TabIndex = 10;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbformat
            // 
            this.tbformat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbformat.Location = new System.Drawing.Point(89, 44);
            this.tbformat.Name = "tbformat";
            this.tbformat.Size = new System.Drawing.Size(232, 23);
            this.tbformat.TabIndex = 9;
            // 
            // numberLength
            // 
            this.numberLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.numberLength.Location = new System.Drawing.Point(92, 15);
            this.numberLength.Maximum = new decimal(new int[] {
            100000,
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
            this.label7.Location = new System.Drawing.Point(20, 47);
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
            this.label8.Location = new System.Drawing.Point(20, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 17);
            this.label8.TabIndex = 3;
            this.label8.Text = "Length:";
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
            this.grbSaveNewModel.Size = new System.Drawing.Size(344, 101);
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
            this.btnCancel.Location = new System.Drawing.Point(250, 50);
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
            this.btnSaveNew.Location = new System.Drawing.Point(153, 50);
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
            this.txtNewModelName.Location = new System.Drawing.Point(103, 21);
            this.txtNewModelName.Name = "txtNewModelName";
            this.txtNewModelName.Size = new System.Drawing.Size(232, 23);
            this.txtNewModelName.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(8, 21);
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
            this.title.Size = new System.Drawing.Size(1108, 50);
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
            this.groupBox1.ResumeLayout(false);
            this.grbReaderParams.ResumeLayout(false);
            this.groupreader.ResumeLayout(false);
            this.groupreader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExposure)).EndInit();
            this.Grouplot.ResumeLayout(false);
            this.Grouplot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numqty)).EndInit();
            this.grbformat.ResumeLayout(false);
            this.grbformat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberLength)).EndInit();
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
        private System.Windows.Forms.GroupBox grbSaveNewModel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSaveNew;
        private System.Windows.Forms.TextBox txtNewModelName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlModel;
        private System.Windows.Forms.ComboBox cbbModel;
        private System.Windows.Forms.Button btnLoadModel;
        private System.Windows.Forms.GroupBox grbformat;
        private System.Windows.Forms.TextBox tbformat;
        private System.Windows.Forms.NumericUpDown numberLength;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox Grouplot;
        private System.Windows.Forms.NumericUpDown numqty;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox tblog;
        private System.Windows.Forms.GroupBox groupreader;
        private System.Windows.Forms.NumericUpDown numGain;
        private System.Windows.Forms.NumericUpDown numExposure;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnSaveAsNew;
        private System.Windows.Forms.Button btnSaveModel;
        private System.Windows.Forms.Label lbFrameRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private UControls.ucResult ucResult1;
    }
}

