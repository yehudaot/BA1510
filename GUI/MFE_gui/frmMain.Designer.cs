namespace mfe_gui
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.softwareUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDefaultIComPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveComPort = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.btnSetControlIdentifier = new System.Windows.Forms.Button();
            this.txtSetControlIdentifier = new System.Windows.Forms.TextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.txtIdentifier = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.chkDontUpdate = new System.Windows.Forms.CheckBox();
            this.chkReset = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cmbPAGain = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radLow = new System.Windows.Forms.RadioButton();
            this.radHigh = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radAnt1 = new System.Windows.Forms.RadioButton();
            this.radAnt0 = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radRx = new System.Windows.Forms.RadioButton();
            this.radTx = new System.Windows.Forms.RadioButton();
            this.btnSendControlMsg = new System.Windows.Forms.Button();
            this.btnGetMomenteryStatus = new System.Windows.Forms.Button();
            this.btnGetVersion = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSetMode = new System.Windows.Forms.Button();
            this.cmbMode = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtCommTestWaitTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCommTestIterations = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnGetGeneralCalibData = new System.Windows.Forms.Button();
            this.btnSetGeneralCalib = new System.Windows.Forms.Button();
            this.btnSaveGeneralCalib = new System.Windows.Forms.Button();
            this.btnLoadGeneralCalib = new System.Windows.Forms.Button();
            this.gridGeneralCalib = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnGetLowCalibData = new System.Windows.Forms.Button();
            this.btnSetLowCalib = new System.Windows.Forms.Button();
            this.btnSaveLowCalib = new System.Windows.Forms.Button();
            this.btnLoadLowCalib = new System.Windows.Forms.Button();
            this.gridLowCalib = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnGetHighCalib = new System.Windows.Forms.Button();
            this.btnSetHighCalib = new System.Windows.Forms.Button();
            this.btnSaveHighCalib = new System.Windows.Forms.Button();
            this.btnLoadHighCalib = new System.Windows.Forms.Button();
            this.gridHighCalib = new System.Windows.Forms.DataGridView();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnOpenUpdateFrm = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGeneralCalib)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLowCalib)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHighCalib)).BeginInit();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1097, 24);
            this.menuStrip1.TabIndex = 28;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.softwareUpdateToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // softwareUpdateToolStripMenuItem
            // 
            this.softwareUpdateToolStripMenuItem.Name = "softwareUpdateToolStripMenuItem";
            this.softwareUpdateToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.softwareUpdateToolStripMenuItem.Text = "Software &Update";
            this.softwareUpdateToolStripMenuItem.Click += new System.EventHandler(this.softwareUpdateToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "A&bout";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(37, 415);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 22);
            this.btnClearLog.TabIndex = 31;
            this.btnClearLog.Text = "Clear";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // lstLog
            // 
            this.lstLog.FormattingEnabled = true;
            this.lstLog.HorizontalScrollbar = true;
            this.lstLog.Location = new System.Drawing.Point(37, 443);
            this.lstLog.Name = "lstLog";
            this.lstLog.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstLog.Size = new System.Drawing.Size(546, 160);
            this.lstLog.TabIndex = 30;
            this.lstLog.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lstLog_MouseMove);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDefaultIComPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSaveComPort);
            this.groupBox1.Location = new System.Drawing.Point(37, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(124, 105);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // txtDefaultIComPort
            // 
            this.txtDefaultIComPort.Location = new System.Drawing.Point(13, 42);
            this.txtDefaultIComPort.Name = "txtDefaultIComPort";
            this.txtDefaultIComPort.Size = new System.Drawing.Size(100, 20);
            this.txtDefaultIComPort.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Com Port:";
            // 
            // btnSaveComPort
            // 
            this.btnSaveComPort.Location = new System.Drawing.Point(24, 67);
            this.btnSaveComPort.Name = "btnSaveComPort";
            this.btnSaveComPort.Size = new System.Drawing.Size(75, 23);
            this.btnSaveComPort.TabIndex = 35;
            this.btnSaveComPort.Text = "Save";
            this.btnSaveComPort.UseVisualStyleBackColor = true;
            this.btnSaveComPort.Click += new System.EventHandler(this.btnSaveComPort_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(61, 55);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 41;
            this.btnConnect.Text = "Connect";
            this.toolTip1.SetToolTip(this.btnConnect, "Click To Connect");
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(382, 84);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(201, 202);
            this.txtStatus.TabIndex = 42;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox12);
            this.groupBox3.Controls.Add(this.groupBox10);
            this.groupBox3.Controls.Add(this.groupBox8);
            this.groupBox3.Controls.Add(this.groupBox7);
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.btnSendControlMsg);
            this.groupBox3.Location = new System.Drawing.Point(175, 53);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(195, 384);
            this.groupBox3.TabIndex = 46;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Control";
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.btnSetControlIdentifier);
            this.groupBox12.Controls.Add(this.txtSetControlIdentifier);
            this.groupBox12.Location = new System.Drawing.Point(16, 257);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(143, 44);
            this.groupBox12.TabIndex = 52;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Set Control Identifier:";
            // 
            // btnSetControlIdentifier
            // 
            this.btnSetControlIdentifier.Location = new System.Drawing.Point(85, 13);
            this.btnSetControlIdentifier.Name = "btnSetControlIdentifier";
            this.btnSetControlIdentifier.Size = new System.Drawing.Size(41, 23);
            this.btnSetControlIdentifier.TabIndex = 103;
            this.btnSetControlIdentifier.Text = "Set";
            this.btnSetControlIdentifier.UseVisualStyleBackColor = true;
            this.btnSetControlIdentifier.Click += new System.EventHandler(this.btnSetControlIdentifier_Click);
            // 
            // txtSetControlIdentifier
            // 
            this.txtSetControlIdentifier.Location = new System.Drawing.Point(15, 15);
            this.txtSetControlIdentifier.Name = "txtSetControlIdentifier";
            this.txtSetControlIdentifier.Size = new System.Drawing.Size(62, 20);
            this.txtSetControlIdentifier.TabIndex = 102;
            this.txtSetControlIdentifier.Text = "1";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.txtIdentifier);
            this.groupBox10.Location = new System.Drawing.Point(16, 306);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(143, 44);
            this.groupBox10.TabIndex = 51;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Next Control Identifier:";
            // 
            // txtIdentifier
            // 
            this.txtIdentifier.Enabled = false;
            this.txtIdentifier.Location = new System.Drawing.Point(15, 16);
            this.txtIdentifier.Name = "txtIdentifier";
            this.txtIdentifier.Size = new System.Drawing.Size(110, 20);
            this.txtIdentifier.TabIndex = 51;
            this.txtIdentifier.Text = "1";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.chkDontUpdate);
            this.groupBox8.Controls.Add(this.chkReset);
            this.groupBox8.Location = new System.Drawing.Point(16, 212);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(163, 42);
            this.groupBox8.TabIndex = 50;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Reset(6) - Dont Update(7)";
            // 
            // chkDontUpdate
            // 
            this.chkDontUpdate.AutoSize = true;
            this.chkDontUpdate.Location = new System.Drawing.Point(70, 18);
            this.chkDontUpdate.Name = "chkDontUpdate";
            this.chkDontUpdate.Size = new System.Drawing.Size(87, 17);
            this.chkDontUpdate.TabIndex = 49;
            this.chkDontUpdate.Text = "Dont Update";
            this.chkDontUpdate.UseVisualStyleBackColor = true;
            // 
            // chkReset
            // 
            this.chkReset.AutoSize = true;
            this.chkReset.Location = new System.Drawing.Point(15, 18);
            this.chkReset.Name = "chkReset";
            this.chkReset.Size = new System.Drawing.Size(54, 17);
            this.chkReset.TabIndex = 48;
            this.chkReset.Text = "Reset";
            this.chkReset.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.cmbPAGain);
            this.groupBox7.Location = new System.Drawing.Point(16, 66);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(163, 52);
            this.groupBox7.TabIndex = 49;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "PA Gain (Bits 1-3)";
            // 
            // cmbPAGain
            // 
            this.cmbPAGain.FormattingEnabled = true;
            this.cmbPAGain.Location = new System.Drawing.Point(22, 19);
            this.cmbPAGain.Name = "cmbPAGain";
            this.cmbPAGain.Size = new System.Drawing.Size(121, 21);
            this.cmbPAGain.TabIndex = 48;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radLow);
            this.groupBox6.Controls.Add(this.radHigh);
            this.groupBox6.Location = new System.Drawing.Point(16, 166);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(143, 42);
            this.groupBox6.TabIndex = 49;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "High / Low (Bit 5)";
            // 
            // radLow
            // 
            this.radLow.AutoSize = true;
            this.radLow.Checked = true;
            this.radLow.Location = new System.Drawing.Point(15, 19);
            this.radLow.Name = "radLow";
            this.radLow.Size = new System.Drawing.Size(45, 17);
            this.radLow.TabIndex = 48;
            this.radLow.TabStop = true;
            this.radLow.Text = "Low";
            this.radLow.UseVisualStyleBackColor = true;
            // 
            // radHigh
            // 
            this.radHigh.AutoSize = true;
            this.radHigh.Location = new System.Drawing.Point(78, 19);
            this.radHigh.Name = "radHigh";
            this.radHigh.Size = new System.Drawing.Size(47, 17);
            this.radHigh.TabIndex = 0;
            this.radHigh.Text = "High";
            this.radHigh.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radAnt1);
            this.groupBox5.Controls.Add(this.radAnt0);
            this.groupBox5.Location = new System.Drawing.Point(16, 120);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(132, 42);
            this.groupBox5.TabIndex = 48;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "TxAnt (Bit 4)";
            // 
            // radAnt1
            // 
            this.radAnt1.AutoSize = true;
            this.radAnt1.Location = new System.Drawing.Point(78, 19);
            this.radAnt1.Name = "radAnt1";
            this.radAnt1.Size = new System.Drawing.Size(50, 17);
            this.radAnt1.TabIndex = 48;
            this.radAnt1.TabStop = true;
            this.radAnt1.Text = "Ant 1";
            this.radAnt1.UseVisualStyleBackColor = true;
            // 
            // radAnt0
            // 
            this.radAnt0.AutoSize = true;
            this.radAnt0.Checked = true;
            this.radAnt0.Location = new System.Drawing.Point(15, 19);
            this.radAnt0.Name = "radAnt0";
            this.radAnt0.Size = new System.Drawing.Size(50, 17);
            this.radAnt0.TabIndex = 0;
            this.radAnt0.TabStop = true;
            this.radAnt0.Text = "Ant 0";
            this.radAnt0.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radRx);
            this.groupBox4.Controls.Add(this.radTx);
            this.groupBox4.Location = new System.Drawing.Point(16, 22);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(110, 42);
            this.groupBox4.TabIndex = 47;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "TxOn (Bit 0)";
            // 
            // radRx
            // 
            this.radRx.AutoSize = true;
            this.radRx.Checked = true;
            this.radRx.Location = new System.Drawing.Point(15, 19);
            this.radRx.Name = "radRx";
            this.radRx.Size = new System.Drawing.Size(38, 17);
            this.radRx.TabIndex = 48;
            this.radRx.TabStop = true;
            this.radRx.Text = "Rx";
            this.radRx.UseVisualStyleBackColor = true;
            // 
            // radTx
            // 
            this.radTx.AutoSize = true;
            this.radTx.Location = new System.Drawing.Point(59, 19);
            this.radTx.Name = "radTx";
            this.radTx.Size = new System.Drawing.Size(37, 17);
            this.radTx.TabIndex = 0;
            this.radTx.Text = "Tx";
            this.radTx.UseVisualStyleBackColor = true;
            // 
            // btnSendControlMsg
            // 
            this.btnSendControlMsg.Location = new System.Drawing.Point(16, 357);
            this.btnSendControlMsg.Name = "btnSendControlMsg";
            this.btnSendControlMsg.Size = new System.Drawing.Size(163, 23);
            this.btnSendControlMsg.TabIndex = 47;
            this.btnSendControlMsg.Text = "Send Control Message";
            this.btnSendControlMsg.UseVisualStyleBackColor = true;
            this.btnSendControlMsg.Click += new System.EventHandler(this.btnSendControlMsg_Click);
            // 
            // btnGetMomenteryStatus
            // 
            this.btnGetMomenteryStatus.Location = new System.Drawing.Point(401, 55);
            this.btnGetMomenteryStatus.Name = "btnGetMomenteryStatus";
            this.btnGetMomenteryStatus.Size = new System.Drawing.Size(163, 23);
            this.btnGetMomenteryStatus.TabIndex = 48;
            this.btnGetMomenteryStatus.Text = "Get Bit Status";
            this.btnGetMomenteryStatus.UseVisualStyleBackColor = true;
            this.btnGetMomenteryStatus.Click += new System.EventHandler(this.btnGetMomenteryStatus_Click);
            // 
            // btnGetVersion
            // 
            this.btnGetVersion.Location = new System.Drawing.Point(401, 292);
            this.btnGetVersion.Name = "btnGetVersion";
            this.btnGetVersion.Size = new System.Drawing.Size(163, 23);
            this.btnGetVersion.TabIndex = 50;
            this.btnGetVersion.Text = "Get Version";
            this.btnGetVersion.UseVisualStyleBackColor = true;
            this.btnGetVersion.Click += new System.EventHandler(this.btnGetVersion_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSetMode);
            this.groupBox2.Controls.Add(this.cmbMode);
            this.groupBox2.Location = new System.Drawing.Point(401, 330);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(163, 78);
            this.groupBox2.TabIndex = 51;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mode";
            // 
            // btnSetMode
            // 
            this.btnSetMode.Location = new System.Drawing.Point(38, 46);
            this.btnSetMode.Name = "btnSetMode";
            this.btnSetMode.Size = new System.Drawing.Size(76, 23);
            this.btnSetMode.TabIndex = 52;
            this.btnSetMode.Text = "Set Mode";
            this.btnSetMode.UseVisualStyleBackColor = true;
            this.btnSetMode.Click += new System.EventHandler(this.btnSetMode_Click);
            // 
            // cmbMode
            // 
            this.cmbMode.FormattingEnabled = true;
            this.cmbMode.Location = new System.Drawing.Point(22, 19);
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Size = new System.Drawing.Size(121, 21);
            this.cmbMode.TabIndex = 48;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.button1);
            this.groupBox11.Controls.Add(this.txtCommTestWaitTime);
            this.groupBox11.Controls.Add(this.label3);
            this.groupBox11.Controls.Add(this.txtCommTestIterations);
            this.groupBox11.Controls.Add(this.label2);
            this.groupBox11.Location = new System.Drawing.Point(37, 308);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(124, 100);
            this.groupBox11.TabIndex = 99;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Test Comm Channel";
            this.toolTip1.SetToolTip(this.groupBox11, "Test thc communication channel by sending a series of \'Get Momentery Status\' Mess" +
        "ages");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 94;
            this.button1.Text = "Test Comm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // txtCommTestWaitTime
            // 
            this.txtCommTestWaitTime.Location = new System.Drawing.Point(70, 41);
            this.txtCommTestWaitTime.Name = "txtCommTestWaitTime";
            this.txtCommTestWaitTime.Size = new System.Drawing.Size(48, 20);
            this.txtCommTestWaitTime.TabIndex = 96;
            this.txtCommTestWaitTime.Text = "1";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 30);
            this.label3.TabIndex = 98;
            this.label3.Text = "Milisecond Between:";
            // 
            // txtCommTestIterations
            // 
            this.txtCommTestIterations.Location = new System.Drawing.Point(70, 17);
            this.txtCommTestIterations.Name = "txtCommTestIterations";
            this.txtCommTestIterations.Size = new System.Drawing.Size(48, 20);
            this.txtCommTestIterations.TabIndex = 95;
            this.txtCommTestIterations.Text = "1000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 97;
            this.label2.Text = "Iterations:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(613, 46);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(484, 557);
            this.tabControl1.TabIndex = 90;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnGetGeneralCalibData);
            this.tabPage1.Controls.Add(this.btnSetGeneralCalib);
            this.tabPage1.Controls.Add(this.btnSaveGeneralCalib);
            this.tabPage1.Controls.Add(this.btnLoadGeneralCalib);
            this.tabPage1.Controls.Add(this.gridGeneralCalib);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(476, 531);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            // 
            // btnGetGeneralCalibData
            // 
            this.btnGetGeneralCalibData.Location = new System.Drawing.Point(327, 416);
            this.btnGetGeneralCalibData.Name = "btnGetGeneralCalibData";
            this.btnGetGeneralCalibData.Size = new System.Drawing.Size(131, 48);
            this.btnGetGeneralCalibData.TabIndex = 135;
            this.btnGetGeneralCalibData.Text = "Upload General Calibration Parameters From MFE";
            this.btnGetGeneralCalibData.UseVisualStyleBackColor = true;
            this.btnGetGeneralCalibData.Click += new System.EventHandler(this.btnGetGeneralCalibData_Click);
            // 
            // btnSetGeneralCalib
            // 
            this.btnSetGeneralCalib.Location = new System.Drawing.Point(327, 470);
            this.btnSetGeneralCalib.Name = "btnSetGeneralCalib";
            this.btnSetGeneralCalib.Size = new System.Drawing.Size(131, 48);
            this.btnSetGeneralCalib.TabIndex = 134;
            this.btnSetGeneralCalib.Text = "Download General Calibration Parameters To MFE";
            this.btnSetGeneralCalib.UseVisualStyleBackColor = true;
            this.btnSetGeneralCalib.Click += new System.EventHandler(this.btnSetGeneralCalib_Click);
            // 
            // btnSaveGeneralCalib
            // 
            this.btnSaveGeneralCalib.Location = new System.Drawing.Point(327, 340);
            this.btnSaveGeneralCalib.Name = "btnSaveGeneralCalib";
            this.btnSaveGeneralCalib.Size = new System.Drawing.Size(131, 48);
            this.btnSaveGeneralCalib.TabIndex = 133;
            this.btnSaveGeneralCalib.Text = "Save General Calibration To File";
            this.btnSaveGeneralCalib.UseVisualStyleBackColor = true;
            this.btnSaveGeneralCalib.Click += new System.EventHandler(this.btnSaveGeneralCalib_Click);
            // 
            // btnLoadGeneralCalib
            // 
            this.btnLoadGeneralCalib.Location = new System.Drawing.Point(327, 286);
            this.btnLoadGeneralCalib.Name = "btnLoadGeneralCalib";
            this.btnLoadGeneralCalib.Size = new System.Drawing.Size(131, 48);
            this.btnLoadGeneralCalib.TabIndex = 132;
            this.btnLoadGeneralCalib.Text = "Load General Calibration From File";
            this.btnLoadGeneralCalib.UseVisualStyleBackColor = true;
            this.btnLoadGeneralCalib.Click += new System.EventHandler(this.btnLoadGeneralCalib_Click);
            // 
            // gridGeneralCalib
            // 
            this.gridGeneralCalib.AllowUserToAddRows = false;
            this.gridGeneralCalib.AllowUserToDeleteRows = false;
            this.gridGeneralCalib.AllowUserToResizeColumns = false;
            this.gridGeneralCalib.AllowUserToResizeRows = false;
            this.gridGeneralCalib.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridGeneralCalib.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridGeneralCalib.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.gridGeneralCalib.Location = new System.Drawing.Point(18, 16);
            this.gridGeneralCalib.Name = "gridGeneralCalib";
            this.gridGeneralCalib.Size = new System.Drawing.Size(303, 502);
            this.gridGeneralCalib.TabIndex = 131;
            this.gridGeneralCalib.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.Grid_CellValidating);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnGetLowCalibData);
            this.tabPage2.Controls.Add(this.btnSetLowCalib);
            this.tabPage2.Controls.Add(this.btnSaveLowCalib);
            this.tabPage2.Controls.Add(this.btnLoadLowCalib);
            this.tabPage2.Controls.Add(this.gridLowCalib);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(476, 531);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Low";
            // 
            // btnGetLowCalibData
            // 
            this.btnGetLowCalibData.Location = new System.Drawing.Point(327, 416);
            this.btnGetLowCalibData.Name = "btnGetLowCalibData";
            this.btnGetLowCalibData.Size = new System.Drawing.Size(131, 48);
            this.btnGetLowCalibData.TabIndex = 136;
            this.btnGetLowCalibData.Text = "Upload Low Calibration Parameters From MFE";
            this.btnGetLowCalibData.UseVisualStyleBackColor = true;
            this.btnGetLowCalibData.Click += new System.EventHandler(this.btnGetLowCalibData_Click);
            // 
            // btnSetLowCalib
            // 
            this.btnSetLowCalib.Location = new System.Drawing.Point(327, 470);
            this.btnSetLowCalib.Name = "btnSetLowCalib";
            this.btnSetLowCalib.Size = new System.Drawing.Size(131, 48);
            this.btnSetLowCalib.TabIndex = 134;
            this.btnSetLowCalib.Text = "Download Low Calibration Parameters To MFE";
            this.btnSetLowCalib.UseVisualStyleBackColor = true;
            this.btnSetLowCalib.Click += new System.EventHandler(this.btnSetLowCalib_Click);
            // 
            // btnSaveLowCalib
            // 
            this.btnSaveLowCalib.Location = new System.Drawing.Point(327, 340);
            this.btnSaveLowCalib.Name = "btnSaveLowCalib";
            this.btnSaveLowCalib.Size = new System.Drawing.Size(131, 48);
            this.btnSaveLowCalib.TabIndex = 133;
            this.btnSaveLowCalib.Text = "Save Low Calibration To File";
            this.btnSaveLowCalib.UseVisualStyleBackColor = true;
            this.btnSaveLowCalib.Click += new System.EventHandler(this.btnSaveLowCalib_Click);
            // 
            // btnLoadLowCalib
            // 
            this.btnLoadLowCalib.Location = new System.Drawing.Point(327, 286);
            this.btnLoadLowCalib.Name = "btnLoadLowCalib";
            this.btnLoadLowCalib.Size = new System.Drawing.Size(131, 48);
            this.btnLoadLowCalib.TabIndex = 132;
            this.btnLoadLowCalib.Text = "Load Low Calibration From File";
            this.btnLoadLowCalib.UseVisualStyleBackColor = true;
            this.btnLoadLowCalib.Click += new System.EventHandler(this.btnLoadLowCalib_Click);
            // 
            // gridLowCalib
            // 
            this.gridLowCalib.AllowUserToAddRows = false;
            this.gridLowCalib.AllowUserToDeleteRows = false;
            this.gridLowCalib.AllowUserToResizeColumns = false;
            this.gridLowCalib.AllowUserToResizeRows = false;
            this.gridLowCalib.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridLowCalib.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLowCalib.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.gridLowCalib.Location = new System.Drawing.Point(18, 16);
            this.gridLowCalib.Name = "gridLowCalib";
            this.gridLowCalib.Size = new System.Drawing.Size(303, 502);
            this.gridLowCalib.TabIndex = 131;
            this.gridLowCalib.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.Grid_CellValidating);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnGetHighCalib);
            this.tabPage3.Controls.Add(this.btnSetHighCalib);
            this.tabPage3.Controls.Add(this.btnSaveHighCalib);
            this.tabPage3.Controls.Add(this.btnLoadHighCalib);
            this.tabPage3.Controls.Add(this.gridHighCalib);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(476, 531);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "High";
            // 
            // btnGetHighCalib
            // 
            this.btnGetHighCalib.Location = new System.Drawing.Point(327, 416);
            this.btnGetHighCalib.Name = "btnGetHighCalib";
            this.btnGetHighCalib.Size = new System.Drawing.Size(131, 48);
            this.btnGetHighCalib.TabIndex = 137;
            this.btnGetHighCalib.Text = "Upload High Calibration Parameters From MFE";
            this.btnGetHighCalib.UseVisualStyleBackColor = true;
            this.btnGetHighCalib.Click += new System.EventHandler(this.btnGetHighCalib_Click);
            // 
            // btnSetHighCalib
            // 
            this.btnSetHighCalib.Location = new System.Drawing.Point(327, 470);
            this.btnSetHighCalib.Name = "btnSetHighCalib";
            this.btnSetHighCalib.Size = new System.Drawing.Size(131, 48);
            this.btnSetHighCalib.TabIndex = 134;
            this.btnSetHighCalib.Text = "Download High Calibration Parameters To MFE";
            this.btnSetHighCalib.UseVisualStyleBackColor = true;
            this.btnSetHighCalib.Click += new System.EventHandler(this.btnSetHighCalib_Click);
            // 
            // btnSaveHighCalib
            // 
            this.btnSaveHighCalib.Location = new System.Drawing.Point(327, 340);
            this.btnSaveHighCalib.Name = "btnSaveHighCalib";
            this.btnSaveHighCalib.Size = new System.Drawing.Size(131, 48);
            this.btnSaveHighCalib.TabIndex = 133;
            this.btnSaveHighCalib.Text = "Save High Calibration To File";
            this.btnSaveHighCalib.UseVisualStyleBackColor = true;
            this.btnSaveHighCalib.Click += new System.EventHandler(this.btnSaveHighCalib_Click);
            // 
            // btnLoadHighCalib
            // 
            this.btnLoadHighCalib.Location = new System.Drawing.Point(327, 286);
            this.btnLoadHighCalib.Name = "btnLoadHighCalib";
            this.btnLoadHighCalib.Size = new System.Drawing.Size(131, 48);
            this.btnLoadHighCalib.TabIndex = 132;
            this.btnLoadHighCalib.Text = "Load High Calibration From File";
            this.btnLoadHighCalib.UseVisualStyleBackColor = true;
            this.btnLoadHighCalib.Click += new System.EventHandler(this.btnLoadHighCalib_Click);
            // 
            // gridHighCalib
            // 
            this.gridHighCalib.AllowUserToAddRows = false;
            this.gridHighCalib.AllowUserToDeleteRows = false;
            this.gridHighCalib.AllowUserToResizeColumns = false;
            this.gridHighCalib.AllowUserToResizeRows = false;
            this.gridHighCalib.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridHighCalib.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridHighCalib.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.gridHighCalib.Location = new System.Drawing.Point(18, 16);
            this.gridHighCalib.Name = "gridHighCalib";
            this.gridHighCalib.Size = new System.Drawing.Size(303, 502);
            this.gridHighCalib.TabIndex = 131;
            this.gridHighCalib.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.Grid_CellValidating);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnOpenUpdateFrm);
            this.groupBox9.Location = new System.Drawing.Point(37, 204);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(124, 100);
            this.groupBox9.TabIndex = 92;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Software Update";
            // 
            // btnOpenUpdateFrm
            // 
            this.btnOpenUpdateFrm.Location = new System.Drawing.Point(24, 25);
            this.btnOpenUpdateFrm.Name = "btnOpenUpdateFrm";
            this.btnOpenUpdateFrm.Size = new System.Drawing.Size(75, 60);
            this.btnOpenUpdateFrm.TabIndex = 92;
            this.btnOpenUpdateFrm.Text = "Open Update Window";
            this.btnOpenUpdateFrm.UseVisualStyleBackColor = true;
            this.btnOpenUpdateFrm.Click += new System.EventHandler(this.softwareUpdateToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 615);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnGetVersion);
            this.Controls.Add(this.btnGetMomenteryStatus);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmMain";
            this.Text = "BA1510 (MFE) GUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridGeneralCalib)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridLowCalib)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridHighCalib)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDefaultIComPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveComPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox chkReset;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox cmbPAGain;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton radLow;
        private System.Windows.Forms.RadioButton radHigh;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radAnt1;
        private System.Windows.Forms.RadioButton radAnt0;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radRx;
        private System.Windows.Forms.RadioButton radTx;
        private System.Windows.Forms.Button btnSendControlMsg;
        private System.Windows.Forms.Button btnGetMomenteryStatus;
        private System.Windows.Forms.Button btnGetVersion;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSetMode;
        private System.Windows.Forms.ComboBox cmbMode;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnSetLowCalib;
        private System.Windows.Forms.Button btnSaveLowCalib;
        private System.Windows.Forms.Button btnLoadLowCalib;
        private System.Windows.Forms.DataGridView gridLowCalib;
        private System.Windows.Forms.Button btnSetHighCalib;
        private System.Windows.Forms.Button btnSaveHighCalib;
        private System.Windows.Forms.Button btnLoadHighCalib;
        private System.Windows.Forms.DataGridView gridHighCalib;
        private System.Windows.Forms.Button btnSetGeneralCalib;
        private System.Windows.Forms.Button btnSaveGeneralCalib;
        private System.Windows.Forms.Button btnLoadGeneralCalib;
        private System.Windows.Forms.DataGridView gridGeneralCalib;
        private System.Windows.Forms.Button btnGetGeneralCalibData;
        private System.Windows.Forms.Button btnGetLowCalibData;
        private System.Windows.Forms.Button btnGetHighCalib;
        private System.Windows.Forms.ToolStripMenuItem softwareUpdateToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button btnOpenUpdateFrm;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TextBox txtIdentifier;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtCommTestIterations;
        private System.Windows.Forms.TextBox txtCommTestWaitTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Button btnSetControlIdentifier;
        private System.Windows.Forms.TextBox txtSetControlIdentifier;
        private System.Windows.Forms.CheckBox chkDontUpdate;
    }
}