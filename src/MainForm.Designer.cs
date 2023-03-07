
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BeHappy
{
	partial class MainForm
	{
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageNewJob = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxOperations = new System.Windows.Forms.GroupBox();
            this.btnPreview = new BeHappy.CheckBoxButton();
            this.btnEnqueue = new BeHappy.CheckBoxButton();
            this.btnShowScript = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.linkLblReloadPlugins = new System.Windows.Forms.LinkLabel();
            this.btnExportScript = new System.Windows.Forms.Button();
            this.groupBoxTweak = new System.Windows.Forms.GroupBox();
            this.numericUpDownHeader = new System.Windows.Forms.NumericUpDown();
            this.cbxHeader = new System.Windows.Forms.CheckBox();
            this.numericUpDownChMask = new System.Windows.Forms.NumericUpDown();
            this.cbxChMask = new System.Windows.Forms.CheckBox();
            this.numericUpDownBuffer = new System.Windows.Forms.NumericUpDown();
            this.cbxBuffer = new System.Windows.Forms.CheckBox();
            this.cbxEnsureMP3VBRSync = new System.Windows.Forms.CheckBox();
            this.numericUpDownSplitB = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSplitA = new System.Windows.Forms.NumericUpDown();
            this.cbxSplit = new System.Windows.Forms.CheckBox();
            this.numericUpDownDelay = new System.Windows.Forms.NumericUpDown();
            this.cbxDelay = new System.Windows.Forms.CheckBox();
            this.groupBoxDsp = new System.Windows.Forms.GroupBox();
            this.cbxDisableDSP = new System.Windows.Forms.CheckBox();
            this.lstDSP = new System.Windows.Forms.CheckedListBox();
            this.btnMoveDownDSP = new System.Windows.Forms.Button();
            this.btnConfigureDSP = new System.Windows.Forms.Button();
            this.btnMoveUpDSP = new System.Windows.Forms.Button();
            this.groupBoxSource = new BeHappy.GroupBoxLinkLabel();
            this.labelDragDrop = new System.Windows.Forms.Label();
            this.lstSourceFiles = new System.Windows.Forms.ComboBox();
            this.lstAudioSource = new System.Windows.Forms.ComboBox();
            this.linkLabelClear = new System.Windows.Forms.LinkLabel();
            this.linkLabelOpen = new System.Windows.Forms.LinkLabel();
            this.linkLabelSourceConfig = new System.Windows.Forms.LinkLabel();
            this.groupBoxDestination = new BeHappy.GroupBoxLinkLabel();
            this.lstEncoder = new System.Windows.Forms.ComboBox();
            this.txtOutputFileName = new System.Windows.Forms.TextBox();
            this.linkLabelSave = new System.Windows.Forms.LinkLabel();
            this.linkLabelEncoderConfig = new System.Windows.Forms.LinkLabel();
            this.tabPageJobControl = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.jobListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSimpleLog = new System.Windows.Forms.TextBox();
            this.chkKeepOutput = new System.Windows.Forms.CheckBox();
            this.lblPriority = new System.Windows.Forms.Label();
            this.cboPriority = new System.Windows.Forms.ComboBox();
            this.btnMoveDownJob = new System.Windows.Forms.Button();
            this.labelNumJobs = new System.Windows.Forms.Label();
            this.btnDeleteJob = new System.Windows.Forms.Button();
            this.numericUpDownJobs = new System.Windows.Forms.NumericUpDown();
            this.btnAbort = new System.Windows.Forms.Button();
            this.linkLabelAutoJobs = new System.Windows.Forms.LinkLabel();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnMoveUpJob = new System.Windows.Forms.Button();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.cbxVisualStyle = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabelUrl1 = new System.Windows.Forms.LinkLabel();
            this.linkLabelUrl2 = new System.Windows.Forms.LinkLabel();
            this.linkLabelUrl3 = new System.Windows.Forms.LinkLabel();
            this.linkLabelUrl4 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtGPL = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1.SuspendLayout();
            this.tabPageNewJob.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxOperations.SuspendLayout();
            this.groupBoxTweak.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChMask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBuffer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSplitB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSplitA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelay)).BeginInit();
            this.groupBoxDsp.SuspendLayout();
            this.groupBoxSource.SuspendLayout();
            this.groupBoxDestination.SuspendLayout();
            this.tabPageJobControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJobs)).BeginInit();
            this.tabPageInfo.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageNewJob);
            this.tabControl1.Controls.Add(this.tabPageJobControl);
            this.tabControl1.Controls.Add(this.tabPageInfo);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(744, 457);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageNewJob
            // 
            this.tabPageNewJob.Controls.Add(this.tableLayoutPanel1);
            this.tabPageNewJob.Location = new System.Drawing.Point(4, 22);
            this.tabPageNewJob.Name = "tabPageNewJob";
            this.tabPageNewJob.Size = new System.Drawing.Size(736, 431);
            this.tabPageNewJob.TabIndex = 0;
            this.tabPageNewJob.Text = "New Job";
            this.tabPageNewJob.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel1.Controls.Add(this.groupBoxOperations, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxTweak, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxDsp, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxSource, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxDestination, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.3431F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.6569F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(736, 431);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // groupBoxOperations
            // 
            this.groupBoxOperations.Controls.Add(this.btnPreview);
            this.groupBoxOperations.Controls.Add(this.btnEnqueue);
            this.groupBoxOperations.Controls.Add(this.btnShowScript);
            this.groupBoxOperations.Controls.Add(this.label19);
            this.groupBoxOperations.Controls.Add(this.linkLblReloadPlugins);
            this.groupBoxOperations.Controls.Add(this.btnExportScript);
            this.groupBoxOperations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxOperations.Location = new System.Drawing.Point(569, 251);
            this.groupBoxOperations.Name = "groupBoxOperations";
            this.tableLayoutPanel1.SetRowSpan(this.groupBoxOperations, 2);
            this.groupBoxOperations.Size = new System.Drawing.Size(164, 177);
            this.groupBoxOperations.TabIndex = 6;
            this.groupBoxOperations.TabStop = false;
            this.groupBoxOperations.Text = "[5] Operations";
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.CheckBoxText = "Omit encoder script";
            this.btnPreview.Checked = false;
            this.btnPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreview.Location = new System.Drawing.Point(12, 87);
            this.btnPreview.MinimumSize = new System.Drawing.Size(80, 40);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(146, 40);
            this.btnPreview.TabIndex = 1;
            this.btnPreview.Text = "&Preview";
            this.btnPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPreview.Click += new System.EventHandler(this.startPreview);
            // 
            // btnEnqueue
            // 
            this.btnEnqueue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnqueue.CheckBoxText = "Start jobs instantly";
            this.btnEnqueue.Checked = false;
            this.btnEnqueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnqueue.Location = new System.Drawing.Point(12, 131);
            this.btnEnqueue.MinimumSize = new System.Drawing.Size(80, 40);
            this.btnEnqueue.Name = "btnEnqueue";
            this.btnEnqueue.Size = new System.Drawing.Size(146, 40);
            this.btnEnqueue.TabIndex = 0;
            this.btnEnqueue.Text = "En&queue";
            this.btnEnqueue.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEnqueue.Click += new System.EventHandler(this.submitJobToJobControl);
            // 
            // btnShowScript
            // 
            this.btnShowScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowScript.Location = new System.Drawing.Point(60, 59);
            this.btnShowScript.Name = "btnShowScript";
            this.btnShowScript.Size = new System.Drawing.Size(44, 20);
            this.btnShowScript.TabIndex = 7;
            this.btnShowScript.Text = "Show";
            this.btnShowScript.Click += new System.EventHandler(this.BtnShowScriptClick);
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(12, 63);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(37, 13);
            this.label19.TabIndex = 6;
            this.label19.Text = "Script:";
            // 
            // linkLblReloadPlugins
            // 
            this.linkLblReloadPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLblReloadPlugins.AutoSize = true;
            this.linkLblReloadPlugins.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLblReloadPlugins.Location = new System.Drawing.Point(12, 41);
            this.linkLblReloadPlugins.Name = "linkLblReloadPlugins";
            this.linkLblReloadPlugins.Size = new System.Drawing.Size(75, 13);
            this.linkLblReloadPlugins.TabIndex = 5;
            this.linkLblReloadPlugins.TabStop = true;
            this.linkLblReloadPlugins.Text = "ReloadPlugins";
            this.toolTip1.SetToolTip(this.linkLblReloadPlugins, "Reload all extension plugins and save-state file. (Can crash the application)");
            this.linkLblReloadPlugins.Visible = false;
            this.linkLblReloadPlugins.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelReloadPluginsLinkClicked);
            this.linkLblReloadPlugins.MouseEnter += new System.EventHandler(this.LinkLabelMouseEnter);
            this.linkLblReloadPlugins.MouseLeave += new System.EventHandler(this.LinkLabelMouseLeave);
            // 
            // btnExportScript
            // 
            this.btnExportScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportScript.Location = new System.Drawing.Point(110, 59);
            this.btnExportScript.Name = "btnExportScript";
            this.btnExportScript.Size = new System.Drawing.Size(47, 20);
            this.btnExportScript.TabIndex = 1;
            this.btnExportScript.Text = "Export";
            this.btnExportScript.Click += new System.EventHandler(this.exportAviSynthScriptToFile);
            // 
            // groupBoxTweak
            // 
            this.groupBoxTweak.Controls.Add(this.numericUpDownHeader);
            this.groupBoxTweak.Controls.Add(this.cbxHeader);
            this.groupBoxTweak.Controls.Add(this.numericUpDownChMask);
            this.groupBoxTweak.Controls.Add(this.cbxChMask);
            this.groupBoxTweak.Controls.Add(this.numericUpDownBuffer);
            this.groupBoxTweak.Controls.Add(this.cbxBuffer);
            this.groupBoxTweak.Controls.Add(this.cbxEnsureMP3VBRSync);
            this.groupBoxTweak.Controls.Add(this.numericUpDownSplitB);
            this.groupBoxTweak.Controls.Add(this.numericUpDownSplitA);
            this.groupBoxTweak.Controls.Add(this.cbxSplit);
            this.groupBoxTweak.Controls.Add(this.numericUpDownDelay);
            this.groupBoxTweak.Controls.Add(this.cbxDelay);
            this.groupBoxTweak.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTweak.Location = new System.Drawing.Point(569, 3);
            this.groupBoxTweak.Name = "groupBoxTweak";
            this.tableLayoutPanel1.SetRowSpan(this.groupBoxTweak, 2);
            this.groupBoxTweak.Size = new System.Drawing.Size(164, 242);
            this.groupBoxTweak.TabIndex = 5;
            this.groupBoxTweak.TabStop = false;
            this.groupBoxTweak.Text = "[2] Tweak";
            // 
            // numericUpDownHeader
            // 
            this.numericUpDownHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownHeader.Enabled = false;
            this.numericUpDownHeader.Location = new System.Drawing.Point(78, 209);
            this.numericUpDownHeader.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownHeader.Name = "numericUpDownHeader";
            this.numericUpDownHeader.Size = new System.Drawing.Size(79, 20);
            this.numericUpDownHeader.TabIndex = 11;
            this.toolTip1.SetToolTip(this.numericUpDownHeader, "0 = WAV\r\n1 = W64\r\n2 = RF64");
            // 
            // cbxHeader
            // 
            this.cbxHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxHeader.AutoSize = true;
            this.cbxHeader.Location = new System.Drawing.Point(6, 212);
            this.cbxHeader.Name = "cbxHeader";
            this.cbxHeader.Size = new System.Drawing.Size(52, 17);
            this.cbxHeader.TabIndex = 10;
            this.cbxHeader.Text = "Head";
            this.toolTip1.SetToolTip(this.cbxHeader, "Force header written to encoders stdin.\r\n\r\n0 = WAV\r\n1 = W64\r\n2 = RF64");
            this.cbxHeader.CheckedChanged += new System.EventHandler(this.cbxHeader_CheckedChanged);
            // 
            // numericUpDownChMask
            // 
            this.numericUpDownChMask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownChMask.Enabled = false;
            this.numericUpDownChMask.Location = new System.Drawing.Point(77, 183);
            this.numericUpDownChMask.Maximum = new decimal(new int[] {
            262143,
            0,
            0,
            0});
            this.numericUpDownChMask.Name = "numericUpDownChMask";
            this.numericUpDownChMask.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownChMask.TabIndex = 9;
            // 
            // cbxChMask
            // 
            this.cbxChMask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxChMask.AutoSize = true;
            this.cbxChMask.Location = new System.Drawing.Point(6, 186);
            this.cbxChMask.Name = "cbxChMask";
            this.cbxChMask.Size = new System.Drawing.Size(65, 17);
            this.cbxChMask.TabIndex = 8;
            this.cbxChMask.Text = "ChMask";
            this.toolTip1.SetToolTip(this.cbxChMask, "Override ChannelMask for WAVE_FORMAT_EXTENSIBLE header.");
            this.cbxChMask.CheckedChanged += new System.EventHandler(this.cbxChMask_CheckedChanged);
            // 
            // numericUpDownBuffer
            // 
            this.numericUpDownBuffer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownBuffer.Enabled = false;
            this.numericUpDownBuffer.Location = new System.Drawing.Point(78, 157);
            this.numericUpDownBuffer.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBuffer.Name = "numericUpDownBuffer";
            this.numericUpDownBuffer.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownBuffer.TabIndex = 7;
            this.numericUpDownBuffer.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbxBuffer
            // 
            this.cbxBuffer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxBuffer.AutoSize = true;
            this.cbxBuffer.Location = new System.Drawing.Point(6, 160);
            this.cbxBuffer.Name = "cbxBuffer";
            this.cbxBuffer.Size = new System.Drawing.Size(54, 17);
            this.cbxBuffer.TabIndex = 6;
            this.cbxBuffer.Text = "Buffer";
            this.toolTip1.SetToolTip(this.cbxBuffer, "Add function \'NicBufferAudio(x)\'\r\n\r\nBuffer Audio by \'x\' s.");
            this.cbxBuffer.CheckedChanged += new System.EventHandler(this.cbxBuffer_CheckedChanged);
            // 
            // cbxEnsureMP3VBRSync
            // 
            this.cbxEnsureMP3VBRSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxEnsureMP3VBRSync.AutoSize = true;
            this.cbxEnsureMP3VBRSync.Location = new System.Drawing.Point(6, 54);
            this.cbxEnsureMP3VBRSync.Name = "cbxEnsureMP3VBRSync";
            this.cbxEnsureMP3VBRSync.Size = new System.Drawing.Size(136, 17);
            this.cbxEnsureMP3VBRSync.TabIndex = 5;
            this.cbxEnsureMP3VBRSync.Text = "Ensure MP3 VBR Sync";
            this.toolTip1.SetToolTip(this.cbxEnsureMP3VBRSync, "Add function \'EnsureVBRMP3Sync()\'\r\n\r\nSome black magic to avoid desync.");
            this.cbxEnsureMP3VBRSync.UseVisualStyleBackColor = true;
            // 
            // numericUpDownSplitB
            // 
            this.numericUpDownSplitB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownSplitB.Enabled = false;
            this.numericUpDownSplitB.Location = new System.Drawing.Point(78, 129);
            this.numericUpDownSplitB.Name = "numericUpDownSplitB";
            this.numericUpDownSplitB.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownSplitB.TabIndex = 4;
            // 
            // numericUpDownSplitA
            // 
            this.numericUpDownSplitA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownSplitA.Enabled = false;
            this.numericUpDownSplitA.Location = new System.Drawing.Point(78, 106);
            this.numericUpDownSplitA.Name = "numericUpDownSplitA";
            this.numericUpDownSplitA.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownSplitA.TabIndex = 3;
            // 
            // cbxSplit
            // 
            this.cbxSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxSplit.AutoSize = true;
            this.cbxSplit.Location = new System.Drawing.Point(6, 120);
            this.cbxSplit.Name = "cbxSplit";
            this.cbxSplit.Size = new System.Drawing.Size(46, 17);
            this.cbxSplit.TabIndex = 2;
            this.cbxSplit.Text = "Split";
            this.toolTip1.SetToolTip(this.cbxSplit, "Add function \'Trim(x, y)\'\r\n\r\nTrim audio from frame \'x\' to \'y\', and discards the r" +
        "est.");
            this.cbxSplit.CheckedChanged += new System.EventHandler(this.enableSplit);
            // 
            // numericUpDownDelay
            // 
            this.numericUpDownDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownDelay.Enabled = false;
            this.numericUpDownDelay.Location = new System.Drawing.Point(78, 77);
            this.numericUpDownDelay.Name = "numericUpDownDelay";
            this.numericUpDownDelay.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownDelay.TabIndex = 1;
            // 
            // cbxDelay
            // 
            this.cbxDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxDelay.AutoSize = true;
            this.cbxDelay.Location = new System.Drawing.Point(6, 80);
            this.cbxDelay.Name = "cbxDelay";
            this.cbxDelay.Size = new System.Drawing.Size(53, 17);
            this.cbxDelay.TabIndex = 0;
            this.cbxDelay.Text = "Delay";
            this.toolTip1.SetToolTip(this.cbxDelay, "Add function \'DelayAudio(x)\'\r\n\r\nDelay Audio by \'x\' ms.");
            this.cbxDelay.CheckedChanged += new System.EventHandler(this.enableDelay);
            // 
            // groupBoxDsp
            // 
            this.groupBoxDsp.Controls.Add(this.cbxDisableDSP);
            this.groupBoxDsp.Controls.Add(this.lstDSP);
            this.groupBoxDsp.Controls.Add(this.btnMoveDownDSP);
            this.groupBoxDsp.Controls.Add(this.btnConfigureDSP);
            this.groupBoxDsp.Controls.Add(this.btnMoveUpDSP);
            this.groupBoxDsp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDsp.Location = new System.Drawing.Point(3, 83);
            this.groupBoxDsp.Name = "groupBoxDsp";
            this.tableLayoutPanel1.SetRowSpan(this.groupBoxDsp, 2);
            this.groupBoxDsp.Size = new System.Drawing.Size(560, 264);
            this.groupBoxDsp.TabIndex = 5;
            this.groupBoxDsp.TabStop = false;
            this.groupBoxDsp.Text = "[3] Digital Signal Processing";
            // 
            // cbxDisableDSP
            // 
            this.cbxDisableDSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxDisableDSP.AutoSize = true;
            this.cbxDisableDSP.Location = new System.Drawing.Point(6, 237);
            this.cbxDisableDSP.Name = "cbxDisableDSP";
            this.cbxDisableDSP.Size = new System.Drawing.Size(61, 17);
            this.cbxDisableDSP.TabIndex = 1;
            this.cbxDisableDSP.Text = "Disable";
            this.cbxDisableDSP.CheckedChanged += new System.EventHandler(this.disableDSP);
            // 
            // lstDSP
            // 
            this.lstDSP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDSP.IntegralHeight = false;
            this.lstDSP.Location = new System.Drawing.Point(6, 19);
            this.lstDSP.Name = "lstDSP";
            this.lstDSP.Size = new System.Drawing.Size(548, 210);
            this.lstDSP.TabIndex = 0;
            this.lstDSP.SelectedIndexChanged += new System.EventHandler(this.lstDSP_SelectedIndexChanged);
            // 
            // btnMoveDownDSP
            // 
            this.btnMoveDownDSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDownDSP.Location = new System.Drawing.Point(479, 235);
            this.btnMoveDownDSP.Name = "btnMoveDownDSP";
            this.btnMoveDownDSP.Size = new System.Drawing.Size(75, 23);
            this.btnMoveDownDSP.TabIndex = 4;
            this.btnMoveDownDSP.Text = "Move Do&wn";
            this.btnMoveDownDSP.Click += new System.EventHandler(this.moveDownDSP);
            // 
            // btnConfigureDSP
            // 
            this.btnConfigureDSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfigureDSP.Enabled = false;
            this.btnConfigureDSP.Location = new System.Drawing.Point(317, 235);
            this.btnConfigureDSP.Name = "btnConfigureDSP";
            this.btnConfigureDSP.Size = new System.Drawing.Size(75, 23);
            this.btnConfigureDSP.TabIndex = 2;
            this.btnConfigureDSP.Text = "&Configure";
            this.btnConfigureDSP.Click += new System.EventHandler(this.configureDSP);
            // 
            // btnMoveUpDSP
            // 
            this.btnMoveUpDSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveUpDSP.Location = new System.Drawing.Point(398, 235);
            this.btnMoveUpDSP.Name = "btnMoveUpDSP";
            this.btnMoveUpDSP.Size = new System.Drawing.Size(75, 23);
            this.btnMoveUpDSP.TabIndex = 3;
            this.btnMoveUpDSP.Text = "Move &Up";
            this.btnMoveUpDSP.Click += new System.EventHandler(this.moveUpDSP);
            // 
            // groupBoxSource
            // 
            this.groupBoxSource.AllowDrop = true;
            this.groupBoxSource.Controls.Add(this.labelDragDrop);
            this.groupBoxSource.Controls.Add(this.lstSourceFiles);
            this.groupBoxSource.Controls.Add(this.lstAudioSource);
            this.groupBoxSource.Controls.Add(this.linkLabelClear);
            this.groupBoxSource.Controls.Add(this.linkLabelOpen);
            this.groupBoxSource.Controls.Add(this.linkLabelSourceConfig);
            this.groupBoxSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSource.ForeColor = System.Drawing.Color.Transparent;
            this.groupBoxSource.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.groupBoxSource.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSource.Name = "groupBoxSource";
            this.groupBoxSource.Size = new System.Drawing.Size(560, 74);
            this.groupBoxSource.TabIndex = 7;
            this.groupBoxSource.TabStop = false;
            this.groupBoxSource.Text = "[1] &Source";
            this.groupBoxSource.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstSourceFilesDragDrop);
            this.groupBoxSource.DragEnter += new System.Windows.Forms.DragEventHandler(this.LstSourceFilesDragEnter);
            // 
            // labelDragDrop
            // 
            this.labelDragDrop.AllowDrop = true;
            this.labelDragDrop.AutoSize = true;
            this.labelDragDrop.BackColor = System.Drawing.SystemColors.Window;
            this.labelDragDrop.ForeColor = System.Drawing.Color.Gray;
            this.labelDragDrop.Location = new System.Drawing.Point(15, 25);
            this.labelDragDrop.Name = "labelDragDrop";
            this.labelDragDrop.Size = new System.Drawing.Size(148, 13);
            this.labelDragDrop.TabIndex = 6;
            this.labelDragDrop.Text = "Drag\'n Drop is enabled here...";
            this.labelDragDrop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstSourceFiles
            // 
            this.lstSourceFiles.AllowDrop = true;
            this.lstSourceFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSourceFiles.Location = new System.Drawing.Point(6, 21);
            this.lstSourceFiles.MaxDropDownItems = 15;
            this.lstSourceFiles.Name = "lstSourceFiles";
            this.lstSourceFiles.Size = new System.Drawing.Size(467, 21);
            this.lstSourceFiles.TabIndex = 5;
            this.toolTip1.SetToolTip(this.lstSourceFiles, resources.GetString("lstSourceFiles.ToolTip"));
            this.lstSourceFiles.DropDown += new System.EventHandler(this.LstSourceFilesDropDown);
            this.lstSourceFiles.SelectedIndexChanged += new System.EventHandler(this.LstSourceFilesSelectedIndexChanged);
            this.lstSourceFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstSourceFilesDragDrop);
            this.lstSourceFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.LstSourceFilesDragEnter);
            // 
            // lstAudioSource
            // 
            this.lstAudioSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAudioSource.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstAudioSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstAudioSource.Location = new System.Drawing.Point(6, 47);
            this.lstAudioSource.MaxDropDownItems = 15;
            this.lstAudioSource.Name = "lstAudioSource";
            this.lstAudioSource.Size = new System.Drawing.Size(467, 21);
            this.lstAudioSource.Sorted = true;
            this.lstAudioSource.TabIndex = 2;
            this.toolTip1.SetToolTip(this.lstAudioSource, "The entry is grayed out if one of the source files is not compatible with this pl" +
        "ugin.\r\n\r\nUse right click for more options.");
            this.lstAudioSource.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LstAudioSourceDrawItem);
            this.lstAudioSource.SelectedIndexChanged += new System.EventHandler(this.lstAudioSource_SelectedIndexChanged);
            // 
            // linkLabelClear
            // 
            this.linkLabelClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelClear.AutoSize = true;
            this.linkLabelClear.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabelClear.Location = new System.Drawing.Point(479, 25);
            this.linkLabelClear.MinimumSize = new System.Drawing.Size(31, 13);
            this.linkLabelClear.Name = "linkLabelClear";
            this.linkLabelClear.Size = new System.Drawing.Size(31, 13);
            this.linkLabelClear.TabIndex = 7;
            this.linkLabelClear.TabStop = true;
            this.linkLabelClear.Text = "Clear";
            this.linkLabelClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelClearLinkClicked);
            this.linkLabelClear.MouseEnter += new System.EventHandler(this.LinkLabelMouseEnter);
            this.linkLabelClear.MouseLeave += new System.EventHandler(this.LinkLabelMouseLeave);
            // 
            // linkLabelOpen
            // 
            this.linkLabelOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelOpen.AutoSize = true;
            this.linkLabelOpen.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabelOpen.Location = new System.Drawing.Point(519, 25);
            this.linkLabelOpen.MinimumSize = new System.Drawing.Size(35, 13);
            this.linkLabelOpen.Name = "linkLabelOpen";
            this.linkLabelOpen.Size = new System.Drawing.Size(35, 13);
            this.linkLabelOpen.TabIndex = 1;
            this.linkLabelOpen.TabStop = true;
            this.linkLabelOpen.Text = "Add...";
            this.toolTip1.SetToolTip(this.linkLabelOpen, "Left click here to add files, right click to add a folder.");
            this.linkLabelOpen.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelOpenLinkClicked);
            this.linkLabelOpen.MouseEnter += new System.EventHandler(this.LinkLabelMouseEnter);
            this.linkLabelOpen.MouseLeave += new System.EventHandler(this.LinkLabelMouseLeave);
            // 
            // linkLabelSourceConfig
            // 
            this.linkLabelSourceConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelSourceConfig.AutoSize = true;
            this.linkLabelSourceConfig.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabelSourceConfig.Location = new System.Drawing.Point(479, 50);
            this.linkLabelSourceConfig.Name = "linkLabelSourceConfig";
            this.linkLabelSourceConfig.Size = new System.Drawing.Size(61, 13);
            this.linkLabelSourceConfig.TabIndex = 4;
            this.linkLabelSourceConfig.TabStop = true;
            this.linkLabelSourceConfig.Text = "Configure...";
            this.linkLabelSourceConfig.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelConfigureAudioSourceLinkClicked);
            this.linkLabelSourceConfig.MouseEnter += new System.EventHandler(this.LinkLabelMouseEnter);
            this.linkLabelSourceConfig.MouseLeave += new System.EventHandler(this.LinkLabelMouseLeave);
            // 
            // groupBoxDestination
            // 
            this.groupBoxDestination.Controls.Add(this.lstEncoder);
            this.groupBoxDestination.Controls.Add(this.txtOutputFileName);
            this.groupBoxDestination.Controls.Add(this.linkLabelSave);
            this.groupBoxDestination.Controls.Add(this.linkLabelEncoderConfig);
            this.groupBoxDestination.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDestination.ForeColor = System.Drawing.Color.Transparent;
            this.groupBoxDestination.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.groupBoxDestination.Location = new System.Drawing.Point(3, 353);
            this.groupBoxDestination.Name = "groupBoxDestination";
            this.groupBoxDestination.Size = new System.Drawing.Size(560, 75);
            this.groupBoxDestination.TabIndex = 8;
            this.groupBoxDestination.TabStop = false;
            this.groupBoxDestination.Text = "[4] &Destination";
            // 
            // lstEncoder
            // 
            this.lstEncoder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstEncoder.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstEncoder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstEncoder.Location = new System.Drawing.Point(6, 48);
            this.lstEncoder.MaxDropDownItems = 14;
            this.lstEncoder.Name = "lstEncoder";
            this.lstEncoder.Size = new System.Drawing.Size(467, 21);
            this.lstEncoder.Sorted = true;
            this.lstEncoder.TabIndex = 2;
            this.toolTip1.SetToolTip(this.lstEncoder, "The entry is grayed out if the encoder executable could not be found.\r\n\r\nUse righ" +
        "t click for more options.");
            this.lstEncoder.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LstEncoderDrawItem);
            this.lstEncoder.SelectedIndexChanged += new System.EventHandler(this.lstEncoder_SelectedIndexChanged);
            // 
            // txtOutputFileName
            // 
            this.txtOutputFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputFileName.Location = new System.Drawing.Point(6, 22);
            this.txtOutputFileName.Name = "txtOutputFileName";
            this.txtOutputFileName.Size = new System.Drawing.Size(467, 20);
            this.txtOutputFileName.TabIndex = 0;
            // 
            // linkLabelSave
            // 
            this.linkLabelSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelSave.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabelSave.Location = new System.Drawing.Point(479, 25);
            this.linkLabelSave.Name = "linkLabelSave";
            this.linkLabelSave.Size = new System.Drawing.Size(75, 13);
            this.linkLabelSave.TabIndex = 1;
            this.linkLabelSave.TabStop = true;
            this.linkLabelSave.Text = "Save...";
            this.linkLabelSave.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.selectTargetFile);
            this.linkLabelSave.MouseEnter += new System.EventHandler(this.LinkLabelMouseEnter);
            this.linkLabelSave.MouseLeave += new System.EventHandler(this.LinkLabelMouseLeave);
            // 
            // linkLabelEncoderConfig
            // 
            this.linkLabelEncoderConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelEncoderConfig.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabelEncoderConfig.Location = new System.Drawing.Point(479, 51);
            this.linkLabelEncoderConfig.Name = "linkLabelEncoderConfig";
            this.linkLabelEncoderConfig.Size = new System.Drawing.Size(75, 13);
            this.linkLabelEncoderConfig.TabIndex = 4;
            this.linkLabelEncoderConfig.TabStop = true;
            this.linkLabelEncoderConfig.Text = "Configure...";
            this.linkLabelEncoderConfig.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.configureEncoder);
            this.linkLabelEncoderConfig.MouseEnter += new System.EventHandler(this.LinkLabelMouseEnter);
            this.linkLabelEncoderConfig.MouseLeave += new System.EventHandler(this.LinkLabelMouseLeave);
            // 
            // tabPageJobControl
            // 
            this.tabPageJobControl.Controls.Add(this.splitContainer1);
            this.tabPageJobControl.Location = new System.Drawing.Point(4, 22);
            this.tabPageJobControl.Name = "tabPageJobControl";
            this.tabPageJobControl.Size = new System.Drawing.Size(736, 431);
            this.tabPageJobControl.TabIndex = 1;
            this.tabPageJobControl.Text = "Queue";
            this.tabPageJobControl.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.jobListView);
            this.splitContainer1.Panel1MinSize = 80;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Panel2MinSize = 180;
            this.splitContainer1.Size = new System.Drawing.Size(736, 431);
            this.splitContainer1.SplitterDistance = 159;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 3;
            // 
            // jobListView
            // 
            this.jobListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.jobListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader7,
            this.columnHeader5,
            this.columnHeader6});
            this.jobListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jobListView.FullRowSelect = true;
            this.jobListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.jobListView.HideSelection = false;
            this.jobListView.LabelWrap = false;
            this.jobListView.Location = new System.Drawing.Point(0, 0);
            this.jobListView.Name = "jobListView";
            this.jobListView.ShowGroups = false;
            this.jobListView.ShowItemToolTips = true;
            this.jobListView.Size = new System.Drawing.Size(732, 155);
            this.jobListView.TabIndex = 1;
            this.jobListView.UseCompatibleStateImageBehavior = false;
            this.jobListView.View = System.Windows.Forms.View.Details;
            this.jobListView.SelectedIndexChanged += new System.EventHandler(this.jobListView_SelectedIndexChanged);
            this.jobListView.DoubleClick += new System.EventHandler(this.toggleJobStatus);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Job";
            this.columnHeader1.Width = 115;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "State";
            this.columnHeader2.Width = 70;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Start";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Stop";
            this.columnHeader4.Width = 120;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Progress";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 55;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Source";
            this.columnHeader5.Width = 115;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Destination";
            this.columnHeader6.Width = 115;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 9;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.28668F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.126411F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.22122F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.28668F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.28668F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.28668F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.094808F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.20542F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.20542F));
            this.tableLayoutPanel2.Controls.Add(this.txtSimpleLog, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.chkKeepOutput, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblPriority, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cboPriority, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnMoveDownJob, 8, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelNumJobs, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnDeleteJob, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDownJobs, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnAbort, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.linkLabelAutoJobs, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnStop, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.btnDeleteAll, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnStart, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.btnMoveUpJob, 7, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(732, 263);
            this.tableLayoutPanel2.TabIndex = 17;
            // 
            // txtSimpleLog
            // 
            this.txtSimpleLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.txtSimpleLog, 8);
            this.txtSimpleLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSimpleLog.Location = new System.Drawing.Point(85, 40);
            this.txtSimpleLog.Multiline = true;
            this.txtSimpleLog.Name = "txtSimpleLog";
            this.txtSimpleLog.ReadOnly = true;
            this.tableLayoutPanel2.SetRowSpan(this.txtSimpleLog, 6);
            this.txtSimpleLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSimpleLog.Size = new System.Drawing.Size(644, 220);
            this.txtSimpleLog.TabIndex = 4;
            this.txtSimpleLog.WordWrap = false;
            // 
            // chkKeepOutput
            // 
            this.chkKeepOutput.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkKeepOutput.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.chkKeepOutput, 2);
            this.chkKeepOutput.Location = new System.Drawing.Point(3, 10);
            this.chkKeepOutput.Name = "chkKeepOutput";
            this.chkKeepOutput.Size = new System.Drawing.Size(123, 17);
            this.chkKeepOutput.TabIndex = 10;
            this.chkKeepOutput.Text = "&Keep output on error";
            this.toolTip1.SetToolTip(this.chkKeepOutput, "Keeps the output on job abort or error.");
            this.chkKeepOutput.UseVisualStyleBackColor = true;
            this.chkKeepOutput.CheckedChanged += new System.EventHandler(this.chkKeepOutput_CheckedChanged);
            // 
            // lblPriority
            // 
            this.lblPriority.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new System.Drawing.Point(152, 12);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(82, 13);
            this.lblPriority.TabIndex = 11;
            this.lblPriority.Text = "P&rocess Priority:";
            this.toolTip1.SetToolTip(this.lblPriority, "Changes the priority of the currently running encoder.");
            // 
            // cboPriority
            // 
            this.cboPriority.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPriority.FormattingEnabled = true;
            this.cboPriority.Location = new System.Drawing.Point(248, 8);
            this.cboPriority.Name = "cboPriority";
            this.cboPriority.Size = new System.Drawing.Size(76, 21);
            this.cboPriority.TabIndex = 12;
            this.toolTip1.SetToolTip(this.cboPriority, "Changes the priority of the currently running encoder.");
            this.cboPriority.SelectedIndexChanged += new System.EventHandler(this.cboPriority_SelectedIndexChanged);
            // 
            // btnMoveDownJob
            // 
            this.btnMoveDownJob.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnMoveDownJob.Location = new System.Drawing.Point(634, 7);
            this.btnMoveDownJob.Name = "btnMoveDownJob";
            this.btnMoveDownJob.Size = new System.Drawing.Size(75, 23);
            this.btnMoveDownJob.TabIndex = 2;
            this.btnMoveDownJob.Text = "Move Do&wn";
            this.toolTip1.SetToolTip(this.btnMoveDownJob, "Moving jobs around does currently not effect the job execution order,\r\nif the lis" +
        "t contains running jobs.");
            this.btnMoveDownJob.Click += new System.EventHandler(this.moveDownJob);
            // 
            // labelNumJobs
            // 
            this.labelNumJobs.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelNumJobs.AutoSize = true;
            this.labelNumJobs.Location = new System.Drawing.Point(337, 12);
            this.labelNumJobs.Name = "labelNumJobs";
            this.labelNumJobs.Size = new System.Drawing.Size(69, 13);
            this.labelNumJobs.TabIndex = 14;
            this.labelNumJobs.Text = "Parallel Jobs:";
            // 
            // btnDeleteJob
            // 
            this.btnDeleteJob.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDeleteJob.Location = new System.Drawing.Point(3, 44);
            this.btnDeleteJob.Name = "btnDeleteJob";
            this.btnDeleteJob.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteJob.TabIndex = 0;
            this.btnDeleteJob.Text = "&Delete";
            this.toolTip1.SetToolTip(this.btnDeleteJob, "Delete selected jobs.\r\n(Except processing jobs)");
            this.btnDeleteJob.Click += new System.EventHandler(this.deleteJob);
            // 
            // numericUpDownJobs
            // 
            this.numericUpDownJobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownJobs.Location = new System.Drawing.Point(412, 3);
            this.numericUpDownJobs.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDownJobs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownJobs.Name = "numericUpDownJobs";
            this.numericUpDownJobs.Size = new System.Drawing.Size(76, 20);
            this.numericUpDownJobs.TabIndex = 13;
            this.toolTip1.SetToolTip(this.numericUpDownJobs, resources.GetString("numericUpDownJobs.ToolTip"));
            this.numericUpDownJobs.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAbort.Enabled = false;
            this.btnAbort.Location = new System.Drawing.Point(3, 230);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(75, 24);
            this.btnAbort.TabIndex = 7;
            this.btnAbort.Text = "A&bort";
            this.toolTip1.SetToolTip(this.btnAbort, "Stop joblist execution immediately and kill all encoder processes.");
            this.btnAbort.Click += new System.EventHandler(this.abortEncoding);
            // 
            // linkLabelAutoJobs
            // 
            this.linkLabelAutoJobs.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.linkLabelAutoJobs.AutoSize = true;
            this.linkLabelAutoJobs.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabelAutoJobs.Location = new System.Drawing.Point(494, 12);
            this.linkLabelAutoJobs.Name = "linkLabelAutoJobs";
            this.linkLabelAutoJobs.Size = new System.Drawing.Size(29, 13);
            this.linkLabelAutoJobs.TabIndex = 15;
            this.linkLabelAutoJobs.TabStop = true;
            this.linkLabelAutoJobs.Text = "Auto";
            this.toolTip1.SetToolTip(this.linkLabelAutoJobs, "Set the number of jobs to a recommened value (70% of detected CPU cores).");
            this.linkLabelAutoJobs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelAutoJobsLinkClicked);
            this.linkLabelAutoJobs.MouseEnter += new System.EventHandler(this.LinkLabelMouseEnter);
            this.linkLabelAutoJobs.MouseLeave += new System.EventHandler(this.LinkLabelMouseLeave);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(3, 191);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 24);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "S&top";
            this.toolTip1.SetToolTip(this.btnStop, "Stop joblist processing after currently running job.");
            this.btnStop.Click += new System.EventHandler(this.stopEncoding);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDeleteAll.Location = new System.Drawing.Point(3, 81);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteAll.TabIndex = 9;
            this.btnDeleteAll.Text = "Delete &All";
            this.toolTip1.SetToolTip(this.btnDeleteAll, "Delete all jobs.\r\n(Except processing, waiting and postponed jobs)");
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnStart.Location = new System.Drawing.Point(3, 154);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 24);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "&Start";
            this.btnStart.Click += new System.EventHandler(this.startJobs);
            // 
            // btnMoveUpJob
            // 
            this.btnMoveUpJob.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnMoveUpJob.Location = new System.Drawing.Point(553, 7);
            this.btnMoveUpJob.Name = "btnMoveUpJob";
            this.btnMoveUpJob.Size = new System.Drawing.Size(75, 23);
            this.btnMoveUpJob.TabIndex = 1;
            this.btnMoveUpJob.Text = "Move &Up";
            this.toolTip1.SetToolTip(this.btnMoveUpJob, "Moving jobs around does currently not effect the job execution order,\r\nif the lis" +
        "t contains running jobs.");
            this.btnMoveUpJob.Click += new System.EventHandler(this.moveUpJob);
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.Controls.Add(this.tabControl2);
            this.tabPageInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInfo.Size = new System.Drawing.Size(736, 431);
            this.tabPageInfo.TabIndex = 5;
            this.tabPageInfo.Text = "Info";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPageAbout);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(730, 425);
            this.tabControl2.TabIndex = 1;
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.BackColor = System.Drawing.Color.White;
            this.tabPageAbout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPageAbout.BackgroundImage")));
            this.tabPageAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabPageAbout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPageAbout.Controls.Add(this.cbxVisualStyle);
            this.tabPageAbout.Controls.Add(this.linkLabel1);
            this.tabPageAbout.Controls.Add(this.label18);
            this.tabPageAbout.Controls.Add(this.label16);
            this.tabPageAbout.Controls.Add(this.label17);
            this.tabPageAbout.Controls.Add(this.label15);
            this.tabPageAbout.Controls.Add(this.label14);
            this.tabPageAbout.Controls.Add(this.label13);
            this.tabPageAbout.Controls.Add(this.label12);
            this.tabPageAbout.Controls.Add(this.label11);
            this.tabPageAbout.Controls.Add(this.label10);
            this.tabPageAbout.Controls.Add(this.label9);
            this.tabPageAbout.Controls.Add(this.label8);
            this.tabPageAbout.Controls.Add(this.label7);
            this.tabPageAbout.Controls.Add(this.label6);
            this.tabPageAbout.Controls.Add(this.label5);
            this.tabPageAbout.Controls.Add(this.label4);
            this.tabPageAbout.Controls.Add(this.label3);
            this.tabPageAbout.Controls.Add(this.linkLabelUrl1);
            this.tabPageAbout.Controls.Add(this.linkLabelUrl2);
            this.tabPageAbout.Controls.Add(this.linkLabelUrl3);
            this.tabPageAbout.Controls.Add(this.linkLabelUrl4);
            this.tabPageAbout.Controls.Add(this.label2);
            this.tabPageAbout.Controls.Add(this.label1);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Size = new System.Drawing.Size(722, 399);
            this.tabPageAbout.TabIndex = 4;
            this.tabPageAbout.Text = "About";
            // 
            // cbxVisualStyle
            // 
            this.cbxVisualStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxVisualStyle.AutoSize = true;
            this.cbxVisualStyle.Checked = true;
            this.cbxVisualStyle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxVisualStyle.Location = new System.Drawing.Point(591, 374);
            this.cbxVisualStyle.Name = "cbxVisualStyle";
            this.cbxVisualStyle.Size = new System.Drawing.Size(107, 17);
            this.cbxVisualStyle.TabIndex = 27;
            this.cbxVisualStyle.Text = "Visual Style Color";
            this.cbxVisualStyle.UseVisualStyleBackColor = true;
            this.cbxVisualStyle.CheckedChanged += new System.EventHandler(this.cbxVisualStyle_CheckedChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(4, 341);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(156, 13);
            this.linkLabel1.TabIndex = 26;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Tag = "https://www.github.com/jones1913/BeHappy";
            this.linkLabel1.Text = "BeHappy workspace @ GitHub";
            this.toolTip1.SetToolTip(this.linkLabel1, "https://www.github.com/jones1913/BeHappy");
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelUrlLinkClicked);
            this.linkLabel1.MouseEnter += new System.EventHandler(this.LinkLabelMouseEnter);
            this.linkLabel1.MouseLeave += new System.EventHandler(this.LinkLabelMouseLeave);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(4, 129);
            this.label18.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(186, 13);
            this.label18.TabIndex = 25;
            this.label18.Text = "with DSP and encoder plugin support.";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(147, 187);
            this.label16.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(63, 13);
            this.label16.TabIndex = 24;
            this.label16.Text = "[contributor]";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 187);
            this.label17.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 13);
            this.label17.TabIndex = 23;
            this.label17.Text = "alwa";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(274, 204);
            this.label15.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(57, 13);
            this.label15.TabIndex = 22;
            this.label15.Text = "Tom Keller";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(274, 187);
            this.label14.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(30, 13);
            this.label14.TabIndex = 21;
            this.label14.Text = "siella";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(274, 170);
            this.label13.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 13);
            this.label13.TabIndex = 20;
            this.label13.Text = "tebasuna51";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(274, 153);
            this.label12.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(155, 13);
            this.label12.TabIndex = 19;
            this.label12.Text = "--- General Contributors ---";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(147, 204);
            this.label11.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "[contributor]";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(147, 170);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "[founder]";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(147, 238);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "[contributor]";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(147, 221);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "[contributor]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 238);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "jones1913";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 204);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Chumbo";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 221);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "shon3i";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 170);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Dmitry Alexandrov (dimzon)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 153);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "--- Developers ---";
            // 
            // linkLabelUrl1
            // 
            this.linkLabelUrl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelUrl1.AutoSize = true;
            this.linkLabelUrl1.Location = new System.Drawing.Point(4, 375);
            this.linkLabelUrl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.linkLabelUrl1.Name = "linkLabelUrl1";
            this.linkLabelUrl1.Size = new System.Drawing.Size(226, 13);
            this.linkLabelUrl1.TabIndex = 9;
            this.linkLabelUrl1.TabStop = true;
            this.linkLabelUrl1.Tag = "https://gleitz.info/forum/index.php?thread/26818-behappy-audio-encoding-mit-avisy" +
    "nth/";
            this.linkLabelUrl1.Text = "BeHappy related thread @ Gleitz Forum (GER)";
            this.toolTip1.SetToolTip(this.linkLabelUrl1, "https://gleitz.info/forum/index.php?thread/26818-behappy-audio-encoding-mit-avisy" +
        "nth/");
            this.linkLabelUrl1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelUrlLinkClicked);
            this.linkLabelUrl1.MouseEnter += new System.EventHandler(this.LinkLabelMouseEnter);
            this.linkLabelUrl1.MouseLeave += new System.EventHandler(this.LinkLabelMouseLeave);
            // 
            // linkLabelUrl2
            // 
            this.linkLabelUrl2.AutoSize = true;
            this.linkLabelUrl2.Location = new System.Drawing.Point(4, 114);
            this.linkLabelUrl2.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.linkLabelUrl2.Name = "linkLabelUrl2";
            this.linkLabelUrl2.Size = new System.Drawing.Size(49, 13);
            this.linkLabelUrl2.TabIndex = 8;
            this.linkLabelUrl2.TabStop = true;
            this.linkLabelUrl2.Tag = "http://www.avisynth.nl/";
            this.linkLabelUrl2.Text = "AviSynth";
            this.toolTip1.SetToolTip(this.linkLabelUrl2, "http://www.avisynth.nl/");
            this.linkLabelUrl2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelUrlLinkClicked);
            this.linkLabelUrl2.MouseEnter += new System.EventHandler(this.LinkLabelMouseEnter);
            this.linkLabelUrl2.MouseLeave += new System.EventHandler(this.LinkLabelMouseLeave);
            // 
            // linkLabelUrl3
            // 
            this.linkLabelUrl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelUrl3.AutoSize = true;
            this.linkLabelUrl3.Location = new System.Drawing.Point(4, 358);
            this.linkLabelUrl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.linkLabelUrl3.Name = "linkLabelUrl3";
            this.linkLabelUrl3.Size = new System.Drawing.Size(231, 13);
            this.linkLabelUrl3.TabIndex = 7;
            this.linkLabelUrl3.TabStop = true;
            this.linkLabelUrl3.Tag = "https://forum.doom9.org/showthread.php?t=104686";
            this.linkLabelUrl3.Text = "BeHappy related thread @ Doom9 forum (ENG)";
            this.toolTip1.SetToolTip(this.linkLabelUrl3, "https://forum.doom9.org/showthread.php?t=104686");
            this.linkLabelUrl3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelUrlLinkClicked);
            this.linkLabelUrl3.MouseEnter += new System.EventHandler(this.LinkLabelMouseEnter);
            this.linkLabelUrl3.MouseLeave += new System.EventHandler(this.LinkLabelMouseLeave);
            // 
            // linkLabelUrl4
            // 
            this.linkLabelUrl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelUrl4.AutoSize = true;
            this.linkLabelUrl4.Location = new System.Drawing.Point(4, 324);
            this.linkLabelUrl4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.linkLabelUrl4.Name = "linkLabelUrl4";
            this.linkLabelUrl4.Size = new System.Drawing.Size(128, 13);
            this.linkLabelUrl4.TabIndex = 6;
            this.linkLabelUrl4.TabStop = true;
            this.linkLabelUrl4.Tag = "https://www.github.com/jones1913/BeHappy/wiki";
            this.linkLabelUrl4.Text = "BeHappy WIKI @ GitHub";
            this.toolTip1.SetToolTip(this.linkLabelUrl4, "https://www.github.com/jones1913/BeHappy/wiki");
            this.linkLabelUrl4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelUrlLinkClicked);
            this.linkLabelUrl4.MouseEnter += new System.EventHandler(this.LinkLabelMouseEnter);
            this.linkLabelUrl4.MouseLeave += new System.EventHandler(this.LinkLabelMouseLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(193, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "this software is distributed under terms of GPL";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 114);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "- based audio encoding";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtGPL);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(722, 399);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "License";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtGPL
            // 
            this.txtGPL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGPL.Location = new System.Drawing.Point(3, 3);
            this.txtGPL.Multiline = true;
            this.txtGPL.Name = "txtGPL";
            this.txtGPL.ReadOnly = true;
            this.txtGPL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtGPL.Size = new System.Drawing.Size(716, 393);
            this.txtGPL.TabIndex = 0;
            this.txtGPL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Multiselect = true;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 1000;
            this.toolTip1.AutoPopDelay = 20000;
            this.toolTip1.BackColor = System.Drawing.SystemColors.Window;
            this.toolTip1.InitialDelay = 1000;
            this.toolTip1.ReshowDelay = 200;
            this.toolTip1.UseAnimation = false;
            this.toolTip1.UseFading = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 457);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(744, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(527, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.AutoToolTip = true;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 16);
            this.toolStripProgressBar1.Step = 1;
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.toolStripProgressBar1.ToolTipText = "JobQueue progress.";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 479);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(650, 415);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.Closed += new System.EventHandler(this.MainForm_Closed);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.SizeChanged += new System.EventHandler(this.MainFormSizeChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabPageNewJob.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxOperations.ResumeLayout(false);
            this.groupBoxOperations.PerformLayout();
            this.groupBoxTweak.ResumeLayout(false);
            this.groupBoxTweak.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChMask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBuffer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSplitB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSplitA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelay)).EndInit();
            this.groupBoxDsp.ResumeLayout(false);
            this.groupBoxDsp.PerformLayout();
            this.groupBoxSource.ResumeLayout(false);
            this.groupBoxSource.PerformLayout();
            this.groupBoxDestination.ResumeLayout(false);
            this.groupBoxDestination.PerformLayout();
            this.tabPageJobControl.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJobs)).EndInit();
            this.tabPageInfo.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPageAbout.ResumeLayout(false);
            this.tabPageAbout.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.LinkLabel linkLabel1;
		private BeHappy.CheckBoxButton btnEnqueue;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Button btnShowScript;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.LinkLabel linkLblReloadPlugins;
		private System.Windows.Forms.LinkLabel linkLabelAutoJobs;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.LinkLabel linkLabelClear;
		private System.Windows.Forms.ComboBox lstSourceFiles;
		private System.Windows.Forms.Label labelDragDrop;
		private System.Windows.Forms.LinkLabel linkLabelEncoderConfig;
		private System.Windows.Forms.LinkLabel linkLabelSourceConfig;
		private System.Windows.Forms.LinkLabel linkLabelSave;
		private System.Windows.Forms.LinkLabel linkLabelOpen;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		
		private TabControl tabControl1;
		private System.Windows.Forms.GroupBox groupBoxTweak;
		private System.Windows.Forms.NumericUpDown numericUpDownSplitB;
		private System.Windows.Forms.NumericUpDown numericUpDownSplitA;
		private System.Windows.Forms.CheckBox cbxSplit;
		private System.Windows.Forms.NumericUpDown numericUpDownDelay;
		private System.Windows.Forms.CheckBox cbxDelay;
		private System.Windows.Forms.GroupBox groupBoxOperations;
		private BeHappy.GroupBoxLinkLabel groupBoxSource;
		private System.Windows.Forms.ComboBox lstAudioSource;
		private BeHappy.GroupBoxLinkLabel groupBoxDestination;
		private System.Windows.Forms.ComboBox lstEncoder;
		private System.Windows.Forms.TextBox txtOutputFileName;
		private System.Windows.Forms.GroupBox groupBoxDsp;
		private System.Windows.Forms.Button btnExportScript;
		private System.Windows.Forms.CheckedListBox lstDSP;
		private System.Windows.Forms.Button btnMoveUpDSP;
		private System.Windows.Forms.Button btnMoveDownDSP;
		private System.Windows.Forms.CheckBox cbxDisableDSP;
		private System.Windows.Forms.TabPage tabPageNewJob;
		private System.Windows.Forms.TabPage tabPageJobControl;
		private System.Windows.Forms.TabPage tabPageAbout;
		private System.Windows.Forms.Button btnDeleteJob;
		private System.Windows.Forms.Button btnMoveUpJob;
		private System.Windows.Forms.Button btnMoveDownJob;
		private System.Windows.Forms.TextBox txtSimpleLog;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnAbort;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.ListView jobListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Button btnConfigureDSP;
		private BeHappy.CheckBoxButton btnPreview;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private TabPage tabPageInfo;
		private TextBox txtGPL;
		private LinkLabel linkLabelUrl1;
		private LinkLabel linkLabelUrl2;
		private LinkLabel linkLabelUrl3;
		private LinkLabel linkLabelUrl4;
		private NumericUpDown numericUpDownBuffer;
		private CheckBox cbxBuffer;
		private NumericUpDown numericUpDownChMask;
		private CheckBox cbxChMask;
		private NumericUpDown numericUpDownHeader;
		private CheckBox cbxHeader;
		private Button btnDeleteAll;
		private CheckBox chkKeepOutput;
		private IContainer components;
		private ComboBox cboPriority;
		private Label lblPriority;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.NumericUpDown numericUpDownJobs;
		private System.Windows.Forms.Label labelNumJobs;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.CheckBox cbxEnsureMP3VBRSync;
        private TableLayoutPanel tableLayoutPanel2;
        private CheckBox cbxVisualStyle;
    }
}
