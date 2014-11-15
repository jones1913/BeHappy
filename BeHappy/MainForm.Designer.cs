
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
			this.btnExportScript = new System.Windows.Forms.Button();
			this.btnAddToJobList = new System.Windows.Forms.Button();
			this.cbxOmitEncoderScript = new System.Windows.Forms.CheckBox();
			this.btnPreview = new System.Windows.Forms.Button();
			this.groupBoxSource = new System.Windows.Forms.GroupBox();
			this.labelDragDrop = new System.Windows.Forms.Label();
			this.lstSourceFiles = new System.Windows.Forms.ComboBox();
			this.linkLabelClear = new System.Windows.Forms.LinkLabel();
			this.linkLabelSourceReset = new System.Windows.Forms.LinkLabel();
			this.linkLabelSourceConfig = new System.Windows.Forms.LinkLabel();
			this.linkLabelOpen = new System.Windows.Forms.LinkLabel();
			this.lstAudioSource = new System.Windows.Forms.ComboBox();
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
			this.groupBoxDestination = new System.Windows.Forms.GroupBox();
			this.linkLabelEncoderReset = new System.Windows.Forms.LinkLabel();
			this.linkLabelEncoderConfig = new System.Windows.Forms.LinkLabel();
			this.linkLabelSave = new System.Windows.Forms.LinkLabel();
			this.lstEncoder = new System.Windows.Forms.ComboBox();
			this.txtOutputFileName = new System.Windows.Forms.TextBox();
			this.tabPageJobControl = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.labelNumJobs = new System.Windows.Forms.Label();
			this.numericUpDownJobs = new System.Windows.Forms.NumericUpDown();
			this.cboPriority = new System.Windows.Forms.ComboBox();
			this.btnDeleteJob = new System.Windows.Forms.Button();
			this.lblPriority = new System.Windows.Forms.Label();
			this.btnDeleteAll = new System.Windows.Forms.Button();
			this.chkKeepOutput = new System.Windows.Forms.CheckBox();
			this.btnStart = new System.Windows.Forms.Button();
			this.txtSimpleLog = new System.Windows.Forms.TextBox();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnAbort = new System.Windows.Forms.Button();
			this.btnMoveDownJob = new System.Windows.Forms.Button();
			this.btnMoveUpJob = new System.Windows.Forms.Button();
			this.jobListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tabPageInfo = new System.Windows.Forms.TabPage();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.tabPageAbout = new System.Windows.Forms.TabPage();
			this.linkLabelUrl1 = new System.Windows.Forms.LinkLabel();
			this.linkLabelUrl2 = new System.Windows.Forms.LinkLabel();
			this.linkLabelUrl3 = new System.Windows.Forms.LinkLabel();
			this.linkLabelUrl4 = new System.Windows.Forms.LinkLabel();
			this.linkLabelUrl5 = new System.Windows.Forms.LinkLabel();
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
			this.groupBoxSource.SuspendLayout();
			this.groupBoxTweak.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeader)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownChMask)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownBuffer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSplitB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSplitA)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelay)).BeginInit();
			this.groupBoxDsp.SuspendLayout();
			this.groupBoxDestination.SuspendLayout();
			this.tabPageJobControl.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.panel1.SuspendLayout();
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
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPageNewJob);
			this.tabControl1.Controls.Add(this.tabPageJobControl);
			this.tabControl1.Controls.Add(this.tabPageInfo);
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(730, 426);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPageNewJob
			// 
			this.tabPageNewJob.Controls.Add(this.tableLayoutPanel1);
			this.tabPageNewJob.Location = new System.Drawing.Point(4, 22);
			this.tabPageNewJob.Name = "tabPageNewJob";
			this.tabPageNewJob.Size = new System.Drawing.Size(722, 400);
			this.tabPageNewJob.TabIndex = 0;
			this.tabPageNewJob.Text = "New Job";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
			this.tableLayoutPanel1.Controls.Add(this.groupBoxOperations, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.groupBoxSource, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBoxTweak, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBoxDsp, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.groupBoxDestination, 0, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(722, 400);
			this.tableLayoutPanel1.TabIndex = 7;
			// 
			// groupBoxOperations
			// 
			this.groupBoxOperations.Controls.Add(this.btnExportScript);
			this.groupBoxOperations.Controls.Add(this.btnAddToJobList);
			this.groupBoxOperations.Controls.Add(this.cbxOmitEncoderScript);
			this.groupBoxOperations.Controls.Add(this.btnPreview);
			this.groupBoxOperations.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxOperations.Location = new System.Drawing.Point(555, 239);
			this.groupBoxOperations.Name = "groupBoxOperations";
			this.tableLayoutPanel1.SetRowSpan(this.groupBoxOperations, 2);
			this.groupBoxOperations.Size = new System.Drawing.Size(164, 158);
			this.groupBoxOperations.TabIndex = 6;
			this.groupBoxOperations.TabStop = false;
			this.groupBoxOperations.Text = "[5] Operations";
			// 
			// btnExportScript
			// 
			this.btnExportScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExportScript.Location = new System.Drawing.Point(12, 100);
			this.btnExportScript.Name = "btnExportScript";
			this.btnExportScript.Size = new System.Drawing.Size(146, 23);
			this.btnExportScript.TabIndex = 1;
			this.btnExportScript.Text = "Export Avisynth Script";
			this.btnExportScript.Click += new System.EventHandler(this.exportAviSynthScriptToFile);
			// 
			// btnAddToJobList
			// 
			this.btnAddToJobList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddToJobList.Location = new System.Drawing.Point(12, 129);
			this.btnAddToJobList.Name = "btnAddToJobList";
			this.btnAddToJobList.Size = new System.Drawing.Size(146, 23);
			this.btnAddToJobList.TabIndex = 0;
			this.btnAddToJobList.Text = "En&queue";
			this.btnAddToJobList.Click += new System.EventHandler(this.submitJobToJobControl);
			// 
			// cbxOmitEncoderScript
			// 
			this.cbxOmitEncoderScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cbxOmitEncoderScript.AutoSize = true;
			this.cbxOmitEncoderScript.Checked = true;
			this.cbxOmitEncoderScript.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxOmitEncoderScript.Location = new System.Drawing.Point(25, 52);
			this.cbxOmitEncoderScript.Name = "cbxOmitEncoderScript";
			this.cbxOmitEncoderScript.Size = new System.Drawing.Size(117, 17);
			this.cbxOmitEncoderScript.TabIndex = 0;
			this.cbxOmitEncoderScript.Text = "Omit encoder script";
			this.cbxOmitEncoderScript.UseVisualStyleBackColor = false;
			// 
			// btnPreview
			// 
			this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPreview.Location = new System.Drawing.Point(12, 48);
			this.btnPreview.Name = "btnPreview";
			this.btnPreview.Size = new System.Drawing.Size(146, 47);
			this.btnPreview.TabIndex = 1;
			this.btnPreview.Text = "&Preview";
			this.btnPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnPreview.Click += new System.EventHandler(this.startPreview);
			// 
			// groupBoxSource
			// 
			this.groupBoxSource.Controls.Add(this.labelDragDrop);
			this.groupBoxSource.Controls.Add(this.lstSourceFiles);
			this.groupBoxSource.Controls.Add(this.linkLabelClear);
			this.groupBoxSource.Controls.Add(this.linkLabelSourceReset);
			this.groupBoxSource.Controls.Add(this.linkLabelSourceConfig);
			this.groupBoxSource.Controls.Add(this.linkLabelOpen);
			this.groupBoxSource.Controls.Add(this.lstAudioSource);
			this.groupBoxSource.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBoxSource.Location = new System.Drawing.Point(3, 3);
			this.groupBoxSource.Name = "groupBoxSource";
			this.groupBoxSource.Size = new System.Drawing.Size(546, 74);
			this.groupBoxSource.TabIndex = 3;
			this.groupBoxSource.TabStop = false;
			this.groupBoxSource.Text = "[1] &Source";
			// 
			// labelDragDrop
			// 
			this.labelDragDrop.AllowDrop = true;
			this.labelDragDrop.AutoSize = true;
			this.labelDragDrop.BackColor = System.Drawing.SystemColors.Window;
			this.labelDragDrop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelDragDrop.ForeColor = System.Drawing.SystemColors.InactiveCaption;
			this.labelDragDrop.Location = new System.Drawing.Point(166, 23);
			this.labelDragDrop.Name = "labelDragDrop";
			this.labelDragDrop.Size = new System.Drawing.Size(148, 13);
			this.labelDragDrop.TabIndex = 6;
			this.labelDragDrop.Text = "Drag\'n Drop is enabled here...";
			this.labelDragDrop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelDragDrop.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstSourceFilesDragDrop);
			this.labelDragDrop.DragEnter += new System.Windows.Forms.DragEventHandler(this.LstSourceFilesDragEnter);
			// 
			// lstSourceFiles
			// 
			this.lstSourceFiles.AllowDrop = true;
			this.lstSourceFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lstSourceFiles.Location = new System.Drawing.Point(6, 19);
			this.lstSourceFiles.MaxDropDownItems = 15;
			this.lstSourceFiles.Name = "lstSourceFiles";
			this.lstSourceFiles.Size = new System.Drawing.Size(456, 21);
			this.lstSourceFiles.TabIndex = 5;
			this.toolTip1.SetToolTip(this.lstSourceFiles, resources.GetString("lstSourceFiles.ToolTip"));
			this.lstSourceFiles.DropDown += new System.EventHandler(this.LstSourceFilesDropDown);
			this.lstSourceFiles.SelectedIndexChanged += new System.EventHandler(this.LstSourceFilesSelectedIndexChanged);
			this.lstSourceFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstSourceFilesDragDrop);
			this.lstSourceFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.LstSourceFilesDragEnter);
			// 
			// linkLabelClear
			// 
			this.linkLabelClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelClear.AutoSize = true;
			this.linkLabelClear.Location = new System.Drawing.Point(468, 23);
			this.linkLabelClear.Name = "linkLabelClear";
			this.linkLabelClear.Size = new System.Drawing.Size(31, 13);
			this.linkLabelClear.TabIndex = 7;
			this.linkLabelClear.TabStop = true;
			this.linkLabelClear.Text = "Clear";
			this.linkLabelClear.Click += new System.EventHandler(this.LinkLabelClearClick);
			// 
			// linkLabelSourceReset
			// 
			this.linkLabelSourceReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelSourceReset.AutoSize = true;
			this.linkLabelSourceReset.Location = new System.Drawing.Point(438, 48);
			this.linkLabelSourceReset.Name = "linkLabelSourceReset";
			this.linkLabelSourceReset.Size = new System.Drawing.Size(35, 13);
			this.linkLabelSourceReset.TabIndex = 3;
			this.linkLabelSourceReset.TabStop = true;
			this.linkLabelSourceReset.Text = "Reset";
			this.linkLabelSourceReset.Click += new System.EventHandler(this.resetAudioSource);
			// 
			// linkLabelSourceConfig
			// 
			this.linkLabelSourceConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelSourceConfig.AutoSize = true;
			this.linkLabelSourceConfig.Location = new System.Drawing.Point(479, 48);
			this.linkLabelSourceConfig.Name = "linkLabelSourceConfig";
			this.linkLabelSourceConfig.Size = new System.Drawing.Size(61, 13);
			this.linkLabelSourceConfig.TabIndex = 4;
			this.linkLabelSourceConfig.TabStop = true;
			this.linkLabelSourceConfig.Text = "Configure...";
			this.linkLabelSourceConfig.Click += new System.EventHandler(this.configureAudioSource);
			// 
			// linkLabelOpen
			// 
			this.linkLabelOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelOpen.AutoSize = true;
			this.linkLabelOpen.Location = new System.Drawing.Point(505, 23);
			this.linkLabelOpen.Name = "linkLabelOpen";
			this.linkLabelOpen.Size = new System.Drawing.Size(35, 13);
			this.linkLabelOpen.TabIndex = 1;
			this.linkLabelOpen.TabStop = true;
			this.linkLabelOpen.Text = "Add...";
			this.toolTip1.SetToolTip(this.linkLabelOpen, "Left click here to add files, right click to add a folder.");
			this.linkLabelOpen.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.selectSourceFile);
			// 
			// lstAudioSource
			// 
			this.lstAudioSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lstAudioSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstAudioSource.Location = new System.Drawing.Point(6, 45);
			this.lstAudioSource.MaxDropDownItems = 15;
			this.lstAudioSource.Name = "lstAudioSource";
			this.lstAudioSource.Size = new System.Drawing.Size(426, 21);
			this.lstAudioSource.Sorted = true;
			this.lstAudioSource.TabIndex = 2;
			this.lstAudioSource.SelectedIndexChanged += new System.EventHandler(this.lstAudioSource_SelectedIndexChanged);
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
			this.groupBoxTweak.Location = new System.Drawing.Point(555, 3);
			this.groupBoxTweak.Name = "groupBoxTweak";
			this.tableLayoutPanel1.SetRowSpan(this.groupBoxTweak, 2);
			this.groupBoxTweak.Size = new System.Drawing.Size(164, 230);
			this.groupBoxTweak.TabIndex = 5;
			this.groupBoxTweak.TabStop = false;
			this.groupBoxTweak.Text = "[2] Tweak";
			// 
			// numericUpDownHeader
			// 
			this.numericUpDownHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownHeader.Enabled = false;
			this.numericUpDownHeader.Location = new System.Drawing.Point(119, 200);
			this.numericUpDownHeader.Maximum = new decimal(new int[] {
									4,
									0,
									0,
									0});
			this.numericUpDownHeader.Name = "numericUpDownHeader";
			this.numericUpDownHeader.Size = new System.Drawing.Size(40, 20);
			this.numericUpDownHeader.TabIndex = 11;
			// 
			// cbxHeader
			// 
			this.cbxHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cbxHeader.AutoSize = true;
			this.cbxHeader.Location = new System.Drawing.Point(6, 194);
			this.cbxHeader.Name = "cbxHeader";
			this.cbxHeader.Size = new System.Drawing.Size(96, 30);
			this.cbxHeader.TabIndex = 10;
			this.cbxHeader.Text = "Head 0:WAV\r\n1:W64 2:RF64";
			this.cbxHeader.CheckedChanged += new System.EventHandler(this.cbxHeader_CheckedChanged);
			// 
			// numericUpDownChMask
			// 
			this.numericUpDownChMask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownChMask.Enabled = false;
			this.numericUpDownChMask.Location = new System.Drawing.Point(77, 171);
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
			this.cbxChMask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cbxChMask.AutoSize = true;
			this.cbxChMask.Location = new System.Drawing.Point(6, 174);
			this.cbxChMask.Name = "cbxChMask";
			this.cbxChMask.Size = new System.Drawing.Size(65, 17);
			this.cbxChMask.TabIndex = 8;
			this.cbxChMask.Text = "ChMask";
			this.cbxChMask.CheckedChanged += new System.EventHandler(this.cbxChMask_CheckedChanged);
			// 
			// numericUpDownBuffer
			// 
			this.numericUpDownBuffer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownBuffer.Enabled = false;
			this.numericUpDownBuffer.Location = new System.Drawing.Point(78, 145);
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
			this.cbxBuffer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cbxBuffer.AutoSize = true;
			this.cbxBuffer.Location = new System.Drawing.Point(6, 148);
			this.cbxBuffer.Name = "cbxBuffer";
			this.cbxBuffer.Size = new System.Drawing.Size(54, 17);
			this.cbxBuffer.TabIndex = 6;
			this.cbxBuffer.Text = "Buffer";
			this.cbxBuffer.CheckedChanged += new System.EventHandler(this.cbxBuffer_CheckedChanged);
			// 
			// cbxEnsureMP3VBRSync
			// 
			this.cbxEnsureMP3VBRSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cbxEnsureMP3VBRSync.AutoSize = true;
			this.cbxEnsureMP3VBRSync.Location = new System.Drawing.Point(6, 42);
			this.cbxEnsureMP3VBRSync.Name = "cbxEnsureMP3VBRSync";
			this.cbxEnsureMP3VBRSync.Size = new System.Drawing.Size(136, 17);
			this.cbxEnsureMP3VBRSync.TabIndex = 5;
			this.cbxEnsureMP3VBRSync.Text = "Ensure MP3 VBR Sync";
			this.cbxEnsureMP3VBRSync.UseVisualStyleBackColor = true;
			// 
			// numericUpDownSplitB
			// 
			this.numericUpDownSplitB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownSplitB.Enabled = false;
			this.numericUpDownSplitB.Location = new System.Drawing.Point(78, 117);
			this.numericUpDownSplitB.Name = "numericUpDownSplitB";
			this.numericUpDownSplitB.Size = new System.Drawing.Size(80, 20);
			this.numericUpDownSplitB.TabIndex = 4;
			// 
			// numericUpDownSplitA
			// 
			this.numericUpDownSplitA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownSplitA.Enabled = false;
			this.numericUpDownSplitA.Location = new System.Drawing.Point(78, 94);
			this.numericUpDownSplitA.Name = "numericUpDownSplitA";
			this.numericUpDownSplitA.Size = new System.Drawing.Size(80, 20);
			this.numericUpDownSplitA.TabIndex = 3;
			// 
			// cbxSplit
			// 
			this.cbxSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cbxSplit.AutoSize = true;
			this.cbxSplit.Location = new System.Drawing.Point(6, 109);
			this.cbxSplit.Name = "cbxSplit";
			this.cbxSplit.Size = new System.Drawing.Size(46, 17);
			this.cbxSplit.TabIndex = 2;
			this.cbxSplit.Text = "Split";
			this.cbxSplit.CheckedChanged += new System.EventHandler(this.enableSplit);
			// 
			// numericUpDownDelay
			// 
			this.numericUpDownDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownDelay.Enabled = false;
			this.numericUpDownDelay.Location = new System.Drawing.Point(78, 65);
			this.numericUpDownDelay.Name = "numericUpDownDelay";
			this.numericUpDownDelay.Size = new System.Drawing.Size(80, 20);
			this.numericUpDownDelay.TabIndex = 1;
			// 
			// cbxDelay
			// 
			this.cbxDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cbxDelay.AutoSize = true;
			this.cbxDelay.Location = new System.Drawing.Point(6, 68);
			this.cbxDelay.Name = "cbxDelay";
			this.cbxDelay.Size = new System.Drawing.Size(53, 17);
			this.cbxDelay.TabIndex = 0;
			this.cbxDelay.Text = "Delay";
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
			this.groupBoxDsp.Size = new System.Drawing.Size(546, 234);
			this.groupBoxDsp.TabIndex = 5;
			this.groupBoxDsp.TabStop = false;
			this.groupBoxDsp.Text = "[3] Digital Signal Processing";
			// 
			// cbxDisableDSP
			// 
			this.cbxDisableDSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbxDisableDSP.AutoSize = true;
			this.cbxDisableDSP.Location = new System.Drawing.Point(6, 207);
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
			this.lstDSP.Size = new System.Drawing.Size(534, 180);
			this.lstDSP.TabIndex = 0;
			this.lstDSP.SelectedIndexChanged += new System.EventHandler(this.lstDSP_SelectedIndexChanged);
			// 
			// btnMoveDownDSP
			// 
			this.btnMoveDownDSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveDownDSP.Location = new System.Drawing.Point(465, 205);
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
			this.btnConfigureDSP.Location = new System.Drawing.Point(303, 205);
			this.btnConfigureDSP.Name = "btnConfigureDSP";
			this.btnConfigureDSP.Size = new System.Drawing.Size(75, 23);
			this.btnConfigureDSP.TabIndex = 2;
			this.btnConfigureDSP.Text = "&Configure";
			this.btnConfigureDSP.Click += new System.EventHandler(this.configureDSP);
			// 
			// btnMoveUpDSP
			// 
			this.btnMoveUpDSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveUpDSP.Location = new System.Drawing.Point(384, 205);
			this.btnMoveUpDSP.Name = "btnMoveUpDSP";
			this.btnMoveUpDSP.Size = new System.Drawing.Size(75, 23);
			this.btnMoveUpDSP.TabIndex = 3;
			this.btnMoveUpDSP.Text = "Move &Up";
			this.btnMoveUpDSP.Click += new System.EventHandler(this.moveUpDSP);
			// 
			// groupBoxDestination
			// 
			this.groupBoxDestination.Controls.Add(this.linkLabelEncoderReset);
			this.groupBoxDestination.Controls.Add(this.linkLabelEncoderConfig);
			this.groupBoxDestination.Controls.Add(this.linkLabelSave);
			this.groupBoxDestination.Controls.Add(this.lstEncoder);
			this.groupBoxDestination.Controls.Add(this.txtOutputFileName);
			this.groupBoxDestination.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBoxDestination.Location = new System.Drawing.Point(3, 323);
			this.groupBoxDestination.Name = "groupBoxDestination";
			this.groupBoxDestination.Size = new System.Drawing.Size(546, 74);
			this.groupBoxDestination.TabIndex = 4;
			this.groupBoxDestination.TabStop = false;
			this.groupBoxDestination.Text = "[4] &Destination";
			// 
			// linkLabelEncoderReset
			// 
			this.linkLabelEncoderReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelEncoderReset.AutoSize = true;
			this.linkLabelEncoderReset.Location = new System.Drawing.Point(438, 48);
			this.linkLabelEncoderReset.Name = "linkLabelEncoderReset";
			this.linkLabelEncoderReset.Size = new System.Drawing.Size(35, 13);
			this.linkLabelEncoderReset.TabIndex = 3;
			this.linkLabelEncoderReset.TabStop = true;
			this.linkLabelEncoderReset.Text = "Reset";
			this.linkLabelEncoderReset.Click += new System.EventHandler(this.resetEncoder);
			// 
			// linkLabelEncoderConfig
			// 
			this.linkLabelEncoderConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelEncoderConfig.AutoSize = true;
			this.linkLabelEncoderConfig.Location = new System.Drawing.Point(479, 48);
			this.linkLabelEncoderConfig.Name = "linkLabelEncoderConfig";
			this.linkLabelEncoderConfig.Size = new System.Drawing.Size(61, 13);
			this.linkLabelEncoderConfig.TabIndex = 4;
			this.linkLabelEncoderConfig.TabStop = true;
			this.linkLabelEncoderConfig.Text = "Configure...";
			this.linkLabelEncoderConfig.Click += new System.EventHandler(this.configureEncoder);
			// 
			// linkLabelSave
			// 
			this.linkLabelSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelSave.AutoSize = true;
			this.linkLabelSave.Location = new System.Drawing.Point(499, 22);
			this.linkLabelSave.Name = "linkLabelSave";
			this.linkLabelSave.Size = new System.Drawing.Size(41, 13);
			this.linkLabelSave.TabIndex = 1;
			this.linkLabelSave.TabStop = true;
			this.linkLabelSave.Text = "Save...";
			this.linkLabelSave.Click += new System.EventHandler(this.selectTargetFile);
			// 
			// lstEncoder
			// 
			this.lstEncoder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lstEncoder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstEncoder.Location = new System.Drawing.Point(6, 45);
			this.lstEncoder.MaxDropDownItems = 14;
			this.lstEncoder.Name = "lstEncoder";
			this.lstEncoder.Size = new System.Drawing.Size(426, 21);
			this.lstEncoder.Sorted = true;
			this.lstEncoder.TabIndex = 2;
			this.lstEncoder.SelectedIndexChanged += new System.EventHandler(this.lstEncoder_SelectedIndexChanged);
			// 
			// txtOutputFileName
			// 
			this.txtOutputFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtOutputFileName.Location = new System.Drawing.Point(6, 19);
			this.txtOutputFileName.Name = "txtOutputFileName";
			this.txtOutputFileName.Size = new System.Drawing.Size(487, 20);
			this.txtOutputFileName.TabIndex = 0;
			// 
			// tabPageJobControl
			// 
			this.tabPageJobControl.Controls.Add(this.tableLayoutPanel2);
			this.tabPageJobControl.Location = new System.Drawing.Point(4, 22);
			this.tabPageJobControl.Name = "tabPageJobControl";
			this.tabPageJobControl.Size = new System.Drawing.Size(722, 400);
			this.tabPageJobControl.TabIndex = 1;
			this.tabPageJobControl.Text = "Queue";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.jobListView, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.73379F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.26621F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(722, 400);
			this.tableLayoutPanel2.TabIndex = 2;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.labelNumJobs);
			this.panel1.Controls.Add(this.numericUpDownJobs);
			this.panel1.Controls.Add(this.cboPriority);
			this.panel1.Controls.Add(this.btnDeleteJob);
			this.panel1.Controls.Add(this.lblPriority);
			this.panel1.Controls.Add(this.btnDeleteAll);
			this.panel1.Controls.Add(this.chkKeepOutput);
			this.panel1.Controls.Add(this.btnStart);
			this.panel1.Controls.Add(this.txtSimpleLog);
			this.panel1.Controls.Add(this.btnStop);
			this.panel1.Controls.Add(this.btnAbort);
			this.panel1.Controls.Add(this.btnMoveDownJob);
			this.panel1.Controls.Add(this.btnMoveUpJob);
			this.panel1.Location = new System.Drawing.Point(3, 185);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(716, 212);
			this.panel1.TabIndex = 2;
			// 
			// labelNumJobs
			// 
			this.labelNumJobs.AutoSize = true;
			this.labelNumJobs.Location = new System.Drawing.Point(318, 9);
			this.labelNumJobs.Name = "labelNumJobs";
			this.labelNumJobs.Size = new System.Drawing.Size(69, 13);
			this.labelNumJobs.TabIndex = 14;
			this.labelNumJobs.Text = "Parallel Jobs:";
			// 
			// numericUpDownJobs
			// 
			this.numericUpDownJobs.Location = new System.Drawing.Point(393, 6);
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
			this.numericUpDownJobs.Size = new System.Drawing.Size(44, 20);
			this.numericUpDownJobs.TabIndex = 13;
			this.toolTip1.SetToolTip(this.numericUpDownJobs, resources.GetString("numericUpDownJobs.ToolTip"));
			this.numericUpDownJobs.Value = new decimal(new int[] {
									1,
									0,
									0,
									0});
			// 
			// cboPriority
			// 
			this.cboPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPriority.FormattingEnabled = true;
			this.cboPriority.Location = new System.Drawing.Point(222, 5);
			this.cboPriority.Name = "cboPriority";
			this.cboPriority.Size = new System.Drawing.Size(90, 21);
			this.cboPriority.TabIndex = 12;
			this.toolTip1.SetToolTip(this.cboPriority, "Changes the priority of the currently running encoder.");
			this.cboPriority.SelectedIndexChanged += new System.EventHandler(this.cboPriority_SelectedIndexChanged);
			// 
			// btnDeleteJob
			// 
			this.btnDeleteJob.Location = new System.Drawing.Point(3, 32);
			this.btnDeleteJob.Name = "btnDeleteJob";
			this.btnDeleteJob.Size = new System.Drawing.Size(75, 23);
			this.btnDeleteJob.TabIndex = 0;
			this.btnDeleteJob.Text = "&Delete";
			this.toolTip1.SetToolTip(this.btnDeleteJob, "Delete selected jobs.\r\n(Except processing jobs)");
			this.btnDeleteJob.Click += new System.EventHandler(this.deleteJob);
			// 
			// lblPriority
			// 
			this.lblPriority.AutoSize = true;
			this.lblPriority.Location = new System.Drawing.Point(134, 9);
			this.lblPriority.Name = "lblPriority";
			this.lblPriority.Size = new System.Drawing.Size(82, 13);
			this.lblPriority.TabIndex = 11;
			this.lblPriority.Text = "P&rocess Priority:";
			this.toolTip1.SetToolTip(this.lblPriority, "Changes the priority of the currently running encoder.");
			// 
			// btnDeleteAll
			// 
			this.btnDeleteAll.Location = new System.Drawing.Point(3, 61);
			this.btnDeleteAll.Name = "btnDeleteAll";
			this.btnDeleteAll.Size = new System.Drawing.Size(75, 23);
			this.btnDeleteAll.TabIndex = 9;
			this.btnDeleteAll.Text = "Delete &All";
			this.toolTip1.SetToolTip(this.btnDeleteAll, "Delete all jobs.\r\n(Except processing, waiting and postponed jobs)");
			this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
			// 
			// chkKeepOutput
			// 
			this.chkKeepOutput.AutoSize = true;
			this.chkKeepOutput.Location = new System.Drawing.Point(5, 7);
			this.chkKeepOutput.Name = "chkKeepOutput";
			this.chkKeepOutput.Size = new System.Drawing.Size(123, 17);
			this.chkKeepOutput.TabIndex = 10;
			this.chkKeepOutput.Text = "&Keep output on error";
			this.toolTip1.SetToolTip(this.chkKeepOutput, "Keeps the output on job abort or error.");
			this.chkKeepOutput.UseVisualStyleBackColor = true;
			this.chkKeepOutput.CheckedChanged += new System.EventHandler(this.chkKeepOutput_CheckedChanged);
			// 
			// btnStart
			// 
			this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnStart.Location = new System.Drawing.Point(3, 126);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 24);
			this.btnStart.TabIndex = 5;
			this.btnStart.Text = "&Start";
			this.btnStart.Click += new System.EventHandler(this.startJobs);
			// 
			// txtSimpleLog
			// 
			this.txtSimpleLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSimpleLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtSimpleLog.Location = new System.Drawing.Point(86, 32);
			this.txtSimpleLog.Multiline = true;
			this.txtSimpleLog.Name = "txtSimpleLog";
			this.txtSimpleLog.ReadOnly = true;
			this.txtSimpleLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtSimpleLog.Size = new System.Drawing.Size(625, 177);
			this.txtSimpleLog.TabIndex = 4;
			this.txtSimpleLog.WordWrap = false;
			// 
			// btnStop
			// 
			this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnStop.Enabled = false;
			this.btnStop.Location = new System.Drawing.Point(3, 156);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 24);
			this.btnStop.TabIndex = 6;
			this.btnStop.Text = "S&top";
			this.toolTip1.SetToolTip(this.btnStop, "Stop joblist processing after currently running job.");
			this.btnStop.Click += new System.EventHandler(this.stopEncoding);
			// 
			// btnAbort
			// 
			this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAbort.Enabled = false;
			this.btnAbort.Location = new System.Drawing.Point(3, 185);
			this.btnAbort.Name = "btnAbort";
			this.btnAbort.Size = new System.Drawing.Size(75, 24);
			this.btnAbort.TabIndex = 7;
			this.btnAbort.Text = "A&bort";
			this.toolTip1.SetToolTip(this.btnAbort, "Stop joblist execution immediately and kill all encoder processes.");
			this.btnAbort.Click += new System.EventHandler(this.abortEncoding);
			// 
			// btnMoveDownJob
			// 
			this.btnMoveDownJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveDownJob.Location = new System.Drawing.Point(636, 3);
			this.btnMoveDownJob.Name = "btnMoveDownJob";
			this.btnMoveDownJob.Size = new System.Drawing.Size(75, 23);
			this.btnMoveDownJob.TabIndex = 2;
			this.btnMoveDownJob.Text = "Move Do&wn";
			this.toolTip1.SetToolTip(this.btnMoveDownJob, "Moving jobs around does currently not effect the job execution order,\r\nif the lis" +
						"t contains running jobs.");
			this.btnMoveDownJob.Click += new System.EventHandler(this.moveDownJob);
			// 
			// btnMoveUpJob
			// 
			this.btnMoveUpJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveUpJob.Location = new System.Drawing.Point(555, 3);
			this.btnMoveUpJob.Name = "btnMoveUpJob";
			this.btnMoveUpJob.Size = new System.Drawing.Size(75, 23);
			this.btnMoveUpJob.TabIndex = 1;
			this.btnMoveUpJob.Text = "Move &Up";
			this.toolTip1.SetToolTip(this.btnMoveUpJob, "Moving jobs around does currently not effect the job execution order,\r\nif the lis" +
						"t contains running jobs.");
			this.btnMoveUpJob.Click += new System.EventHandler(this.moveUpJob);
			// 
			// jobListView
			// 
			this.jobListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader1,
									this.columnHeader2,
									this.columnHeader3,
									this.columnHeader4,
									this.columnHeader7,
									this.columnHeader5,
									this.columnHeader6});
			this.jobListView.ContextMenuStrip = this.contextMenuStrip1;
			this.jobListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.jobListView.FullRowSelect = true;
			this.jobListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.jobListView.HideSelection = false;
			this.jobListView.LabelWrap = false;
			this.jobListView.Location = new System.Drawing.Point(3, 3);
			this.jobListView.Name = "jobListView";
			this.jobListView.ShowGroups = false;
			this.jobListView.ShowItemToolTips = true;
			this.jobListView.Size = new System.Drawing.Size(716, 176);
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
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.contextMenuStrip1.ShowImageMargin = false;
			this.contextMenuStrip1.Size = new System.Drawing.Size(128, 26);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1Opening);
			// 
			// tabPageInfo
			// 
			this.tabPageInfo.Controls.Add(this.tabControl2);
			this.tabPageInfo.Location = new System.Drawing.Point(4, 22);
			this.tabPageInfo.Name = "tabPageInfo";
			this.tabPageInfo.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageInfo.Size = new System.Drawing.Size(722, 400);
			this.tabPageInfo.TabIndex = 5;
			this.tabPageInfo.Text = "Info";
			this.tabPageInfo.UseVisualStyleBackColor = true;
			// 
			// tabControl2
			// 
			this.tabControl2.Controls.Add(this.tabPageAbout);
			this.tabControl2.Controls.Add(this.tabPage2);
			this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl2.Location = new System.Drawing.Point(3, 3);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(716, 394);
			this.tabControl2.TabIndex = 1;
			// 
			// tabPageAbout
			// 
			this.tabPageAbout.BackColor = System.Drawing.Color.White;
			this.tabPageAbout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPageAbout.BackgroundImage")));
			this.tabPageAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.tabPageAbout.Controls.Add(this.linkLabelUrl1);
			this.tabPageAbout.Controls.Add(this.linkLabelUrl2);
			this.tabPageAbout.Controls.Add(this.linkLabelUrl3);
			this.tabPageAbout.Controls.Add(this.linkLabelUrl4);
			this.tabPageAbout.Controls.Add(this.linkLabelUrl5);
			this.tabPageAbout.Controls.Add(this.label2);
			this.tabPageAbout.Controls.Add(this.label1);
			this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
			this.tabPageAbout.Name = "tabPageAbout";
			this.tabPageAbout.Size = new System.Drawing.Size(708, 368);
			this.tabPageAbout.TabIndex = 4;
			this.tabPageAbout.Text = "About";
			// 
			// linkLabelUrl1
			// 
			this.linkLabelUrl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.linkLabelUrl1.Location = new System.Drawing.Point(0, 255);
			this.linkLabelUrl1.Name = "linkLabelUrl1";
			this.linkLabelUrl1.Size = new System.Drawing.Size(708, 24);
			this.linkLabelUrl1.TabIndex = 9;
			this.linkLabelUrl1.TabStop = true;
			this.linkLabelUrl1.Tag = "http://forum.mediatory.ru/viewtopic.php?t=3754";
			this.linkLabelUrl1.Text = "BeHappy related thread @ Mediatory forum (RUS)";
			this.toolTip1.SetToolTip(this.linkLabelUrl1, "http://forum.mediatory.ru/viewtopic.php?t=3754");
			this.linkLabelUrl1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_Click);
			// 
			// linkLabelUrl2
			// 
			this.linkLabelUrl2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.linkLabelUrl2.Location = new System.Drawing.Point(0, 279);
			this.linkLabelUrl2.Name = "linkLabelUrl2";
			this.linkLabelUrl2.Size = new System.Drawing.Size(708, 24);
			this.linkLabelUrl2.TabIndex = 8;
			this.linkLabelUrl2.TabStop = true;
			this.linkLabelUrl2.Tag = "http://www.avisynth.nl/";
			this.linkLabelUrl2.Text = "AviSynth Homepage (ENG)";
			this.toolTip1.SetToolTip(this.linkLabelUrl2, "http://www.avisynth.nl/");
			this.linkLabelUrl2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_Click);
			// 
			// linkLabelUrl3
			// 
			this.linkLabelUrl3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.linkLabelUrl3.Location = new System.Drawing.Point(0, 303);
			this.linkLabelUrl3.Name = "linkLabelUrl3";
			this.linkLabelUrl3.Size = new System.Drawing.Size(708, 24);
			this.linkLabelUrl3.TabIndex = 7;
			this.linkLabelUrl3.TabStop = true;
			this.linkLabelUrl3.Tag = "http://forum.doom9.org/showthread.php?t=104686";
			this.linkLabelUrl3.Text = "BeHappy related thread @ Doom9 forum (ENG)";
			this.toolTip1.SetToolTip(this.linkLabelUrl3, "http://forum.doom9.org/showthread.php?t=104686");
			this.linkLabelUrl3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_Click);
			// 
			// linkLabelUrl4
			// 
			this.linkLabelUrl4.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.linkLabelUrl4.Location = new System.Drawing.Point(0, 327);
			this.linkLabelUrl4.Name = "linkLabelUrl4";
			this.linkLabelUrl4.Size = new System.Drawing.Size(708, 24);
			this.linkLabelUrl4.TabIndex = 6;
			this.linkLabelUrl4.TabStop = true;
			this.linkLabelUrl4.Tag = "http://www.codeplex.com/BeHappy";
			this.linkLabelUrl4.Text = "BeHappy workspace @ www.codeplex.com (ENG)";
			this.toolTip1.SetToolTip(this.linkLabelUrl4, "http://www.codeplex.com/BeHappy");
			this.linkLabelUrl4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_Click);
			// 
			// linkLabelUrl5
			// 
			this.linkLabelUrl5.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.linkLabelUrl5.Location = new System.Drawing.Point(0, 351);
			this.linkLabelUrl5.Name = "linkLabelUrl5";
			this.linkLabelUrl5.Size = new System.Drawing.Size(708, 17);
			this.linkLabelUrl5.TabIndex = 5;
			this.linkLabelUrl5.TabStop = true;
			this.linkLabelUrl5.Tag = "http://dimzon541.narod.ru/";
			this.linkLabelUrl5.Text = "dimzon\'s Homepage (RUS)";
			this.toolTip1.SetToolTip(this.linkLabelUrl5, "http://dimzon541.narod.ru/");
			this.linkLabelUrl5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Location = new System.Drawing.Point(193, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(211, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "this software distributed under terms of GPL";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 115);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(186, 26);
			this.label1.TabIndex = 2;
			this.label1.Text = "AviSynth based audio encoding\r\nwith DSP and encoder plugin support.";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.txtGPL);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(708, 368);
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
			this.txtGPL.Size = new System.Drawing.Size(702, 362);
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
			this.toolTip1.InitialDelay = 1000;
			this.toolTip1.ReshowDelay = 200;
			this.toolTip1.UseAnimation = false;
			this.toolTip1.UseFading = false;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripStatusLabel1,
									this.toolStripProgressBar1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 429);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.statusStrip1.ShowItemToolTips = true;
			this.statusStrip1.Size = new System.Drawing.Size(730, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(513, 17);
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
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(730, 451);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.tabControl1);
			this.DoubleBuffered = true;
			this.MinimumSize = new System.Drawing.Size(625, 415);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
			this.Closed += new System.EventHandler(this.MainForm_Closed);
			this.SizeChanged += new System.EventHandler(this.MainFormSizeChanged);
			this.tabControl1.ResumeLayout(false);
			this.tabPageNewJob.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.groupBoxOperations.ResumeLayout(false);
			this.groupBoxOperations.PerformLayout();
			this.groupBoxSource.ResumeLayout(false);
			this.groupBoxSource.PerformLayout();
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
			this.groupBoxDestination.ResumeLayout(false);
			this.groupBoxDestination.PerformLayout();
			this.tabPageJobControl.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
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
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.LinkLabel linkLabelClear;
		private System.Windows.Forms.ComboBox lstSourceFiles;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.Label labelDragDrop;
		private System.Windows.Forms.LinkLabel linkLabelEncoderConfig;
		private System.Windows.Forms.LinkLabel linkLabelEncoderReset;
		private System.Windows.Forms.LinkLabel linkLabelSourceConfig;
		private System.Windows.Forms.LinkLabel linkLabelSourceReset;
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
		private System.Windows.Forms.GroupBox groupBoxSource;
		private System.Windows.Forms.ComboBox lstAudioSource;
		private System.Windows.Forms.GroupBox groupBoxDestination;
		private System.Windows.Forms.ComboBox lstEncoder;
		private System.Windows.Forms.TextBox txtOutputFileName;
		private System.Windows.Forms.GroupBox groupBoxDsp;
		private System.Windows.Forms.Button btnAddToJobList;
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
		private System.Windows.Forms.CheckBox cbxOmitEncoderScript;
		private System.Windows.Forms.Button btnPreview;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private TabPage tabPageInfo;
		private TextBox txtGPL;
		private LinkLabel linkLabelUrl1;
		private LinkLabel linkLabelUrl2;
		private LinkLabel linkLabelUrl3;
		private LinkLabel linkLabelUrl4;
		private LinkLabel linkLabelUrl5;
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
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.CheckBox cbxEnsureMP3VBRSync;

	}
}
