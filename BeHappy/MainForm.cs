using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using BeHappy.Extensibility;
using BeHappy.Extensions;

namespace BeHappy
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : Form
	{
		private TabControl tabControl1;
		private System.Windows.Forms.ContainerControl containerControl1;
		private System.Windows.Forms.ContainerControl containerControl2;
		private System.Windows.Forms.ContainerControl containerControl3;
		private System.Windows.Forms.ContainerControl containerControl4;
        private System.Windows.Forms.GroupBox gbxTweak;
		private System.Windows.Forms.NumericUpDown numericUpDown3;
		private System.Windows.Forms.NumericUpDown numericUpDown2;
		private System.Windows.Forms.CheckBox cbxSplit;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.CheckBox cbxDelay;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox lstAudioSource;
		private System.Windows.Forms.Button btnSelectSourceFile;
		private System.Windows.Forms.TextBox txtSourceFileName;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.ComboBox lstEncoder;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox txtOutputFileName;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Button btnAddToJobList;
		private System.Windows.Forms.Button btnExportScript;
		private System.Windows.Forms.CheckedListBox lstDSP;
		private System.Windows.Forms.ContainerControl containerControl5;
		private System.Windows.Forms.Button btnMoveUpDSP;
		private System.Windows.Forms.Button btnMoveDownDSP;
		private System.Windows.Forms.CheckBox cbxDisableDSP;
		private System.Windows.Forms.TabPage tabPageNewJob;
		private System.Windows.Forms.TabPage tabPageJobControl;
		private System.Windows.Forms.TabPage tabPageAbout;
		private System.Windows.Forms.ContainerControl containerControl6;
		private System.Windows.Forms.Button btnDeleteJob;
		private System.Windows.Forms.Button btnMoveUpJob;
		private System.Windows.Forms.Button btnMoveDownJob;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.TextBox txtSimpleLog;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnAbort;
		private System.Windows.Forms.Button btnNewJob;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.ListView jobListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.SaveFileDialog saveFileDialog2;
		private System.Windows.Forms.Button btnConfigureEncoder;
		private System.Windows.Forms.Button btnConfigureAudioSource;
		private System.Windows.Forms.Button btnConfigureDSP;
		private System.Windows.Forms.CheckBox cbxOmitEncoderScript;
        private System.Windows.Forms.Button btnPreview;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem configureToolStripMenuItem;
        private ToolStripMenuItem resetToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem5;
        private ToolStripMenuItem cancelToolStripMenuItem1;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripMenuItem cancelToolStripMenuItem;
        private TabPage tabPage1;
        private TextBox txtGPL;
        private LinkLabel button5;
        private LinkLabel button4;
        private LinkLabel button3;
        private LinkLabel linkLabel1;
        private LinkLabel button1;
        private NumericUpDown numericUpDown4;
        private CheckBox cbxBuffer;
        private Button btnDeleteAll;
        private CheckBox chkKeepOutput;
        private IContainer components;

        private bool m_bKeepOutput;
        private string encoder_dir;
        private string ds_player="mplayer2";
        
		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            m_bKeepOutput = false;

            using (TextReader r = new StreamReader(this.GetType().Assembly.GetManifestResourceStream("BeHappy.gpl.txt")))
            {
                txtGPL.Text = r.ReadToEnd();
            }

            using (Stream ricon = this.GetType().Assembly.GetManifestResourceStream("BeHappy.App.ico"))
            {
                this.Icon = new Icon(ricon);
            }

			numericUpDown1.Minimum = numericUpDown2.Minimum = numericUpDown3.Minimum = decimal.MinValue+16;
			numericUpDown1.Maximum = numericUpDown2.Maximum = numericUpDown3.Maximum = decimal.MaxValue-16;

			this.Text = string.Format("{0} v{1} by {2}", Application.ProductName, Application.ProductVersion, Application.CompanyName);

			placeControls(txtSourceFileName, btnSelectSourceFile, lstAudioSource, btnConfigureAudioSource);
			placeControls(txtOutputFileName, button2, lstEncoder, btnConfigureEncoder);
			loadExtensionsAndApplyConfiguration();
			if (Directory.Exists(getExeDirectory()+"\\encoder"))
				encoder_dir="encoder\\";
			else
				encoder_dir="";
			btnConfigureAudioSource.Enabled = currentSource.IsSupportConfiguration;
			btnConfigureEncoder.Enabled = currentEncoder.IsSupportConfiguration;
			lstEncoder.SelectedIndexChanged += new EventHandler(lstEncoder_SelectedIndexChanged);
			lstAudioSource.SelectedIndexChanged+=new EventHandler(lstAudioSource_SelectedIndexChanged);
			
			// MessageBox.Show(@"res://" + Application.ExecutablePath + "/#32512");
			// this.Icon = new Icon(@"res://BeHappy.exe/32512");//@"res://" + Application.ExecutablePath + "/#32512"); //("res://BeHappy.exe/#32512"); // null; // Application.Icon;
		}

		private void placeControls(TextBox textBox, Button button, ComboBox combo, Button button2)
		{
			button.Height = textBox.Height;
			button2.Width = button.Width = textBox.Height*4/3;
			button2.Text=button.Text="...";
			combo.Left=textBox.Left=4;
			combo.Width=textBox.Width = textBox.Parent.ClientSize.Width - 4 - 4 - button.Width;
			button.Top = textBox.Top;
			button2.Left=button.Left = textBox.Right;
			combo.Anchor=textBox.Anchor=AnchorStyles.Left|AnchorStyles.Right|AnchorStyles.Top;
			button2.Anchor=button.Anchor=AnchorStyles.Right|AnchorStyles.Top;
			button2.Top=combo.Top=textBox.Bottom+2;
			button2.Height=combo.Height;
			Control parent = combo.Parent;
			button2.Enabled=false;
			parent.ClientSize = new Size(parent.ClientSize.Width,combo.Bottom+4); 
		}


        
		private void loadExtensionsAndApplyConfiguration()
		{
			//1) let's load all extensions
			ArrayList audioSources = new ArrayList();
			ArrayList dspProcessors = new ArrayList();
			ArrayList audioEncoders = new ArrayList();
			Extension extension = Extension.Default;
            string ExtensionDirectory = getExeDirectory()+"\\extensions";
            if(!Directory.Exists(ExtensionDirectory))
               ExtensionDirectory = getExeDirectory();
            	
			fillFromExtension(extension, audioEncoders, dspProcessors, audioSources);

			foreach(string file in Directory.GetFiles(ExtensionDirectory,"*.extension"))
			{
				__retry:
				try
				{
					extension = Extension.LoadFromFile(file);
					fillFromExtension(extension, audioEncoders, dspProcessors, audioSources);
				}
				catch(Exception e)
				{
					switch(MessageBox.Show("Can't load extensions from file " + file + Environment.NewLine + e.ToString(), "Can't load extension", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error ))
					{
						case DialogResult.Abort:
							throw e;
							//break;
						case DialogResult.Retry:
							goto __retry;
							//break;
						case DialogResult.Ignore:
							break;
					}
				}
			}
			//let's load configuration
			Configuration c = new Configuration();
			try
			{
				c = Configuration.LoadFromFile(getConfigFileName());
			}
			catch
			{}

            loadGuiPositionConfiguration(c);
            loadMiscConfiguration(c);
			loadPluginsConfiguration(audioSources, c.Settings);
			loadPluginsConfiguration(audioEncoders, c.Settings);
			loadPluginsConfiguration(dspProcessors, c.Settings);
			lstEncoder.Items.AddRange(audioEncoders.ToArray());
			lstAudioSource.Items.AddRange(audioSources.ToArray());

			Hashtable tbl = new Hashtable();
			foreach(ExtensionItemBase i in dspProcessors)
				tbl.Add(i.UniqueID,i);
			Guid[] dspOrder = c.DspOrder!=null?c.DspOrder:new Guid[0];
			foreach(Guid g in dspOrder)
			{
				if(tbl.Contains(g))
				{
					lstDSP.Items.Add(tbl[g]);
					tbl.Remove(g);
				}
			}
			dspProcessors = new ArrayList(tbl.Values);
			lstDSP.Items.AddRange(dspProcessors.ToArray());
			if(c.JobList!=null)
			{
				if(c.JobList.Jobs!=null)
				{
					foreach(Job job in c.JobList.Jobs)
					{
						jobListView.Items.Add(createJobItem(job));
					}
				}
			}
			foreach(ExtensionItemBase e in lstEncoder.Items)
				if(e.UniqueID==c.CurrentEncoder)
				{
					lstEncoder.SelectedItem = e;
					break;
				}
			if(null==currentEncoder)
				lstEncoder.SelectedIndex = 0;
			lstAudioSource.SelectedIndex = 0;

            SetJobMoveButtonState();
		}

		private void loadMiscConfiguration(Configuration c)
		{
			if(c.MiscSettings!=null)
			{
				BeHappy.NeroDigitalAAC.Encoder.preferMP4overM4A = c.MiscSettings.preferMP4overM4A;
				this.ds_player = c.MiscSettings.directShowPlayer;
			}
//			else
//			{
//				BeHappy.NeroDigitalAAC.Encoder.preferMP4overM4A = true;
//				this.ds_player = "mplayer";
//			}
		}
		
        private void loadGuiPositionConfiguration(Configuration c)
        {
            if (c.GuiPosition == null)
                return;

            this.Top = c.GuiPosition.iTop;
            this.Left = c.GuiPosition.iLeft;
            this.Width = c.GuiPosition.iWidth;
            this.Height = c.GuiPosition.iHeight;
        }
        
		private static void loadPluginsConfiguration(ArrayList plugins, IDictionary c)
		{
			foreach(ExtensionItemBase i in plugins)
			{
				if(i.IsSupportConfiguration)
					if(c.Contains(i.UniqueID))
					{
						XmlElement	conf = c[i.UniqueID] as XmlElement;
						if(conf!=null)
							i.LoadConfiguration(conf);
					}
			}
		}

		private static void fillFromExtension(Extension extension, ArrayList audioEncoders, ArrayList dspProcessors, ArrayList audioSources)
		{
			if(extension.AudioEncoders!=null)
				audioEncoders.AddRange(extension.AudioEncoders);
			if(extension.DigitalSignalProcessors!=null)
				dspProcessors.AddRange(extension.DigitalSignalProcessors);
			if(extension.AudioSources!=null)
				audioSources.AddRange(extension.AudioSources);
		}


		private void saveConfiguration()
		{
			Configuration c = new Configuration();
			ArrayList col = new ArrayList();
			col.AddRange(lstEncoder.Items);
			col.AddRange(lstAudioSource.Items);
			col.AddRange(lstDSP.Items);
			foreach(ExtensionItemBase i in col )
			{
				if(i.IsSupportConfiguration)
				{
					XmlElement e = i.SaveConfiguration();
					if(e!=null)
						c.Settings.Add(i.UniqueID,e);
				}
			}

			col.Clear();
			foreach(ListViewItem i in jobListView.Items)
			{
				col.Add(i.Tag);
			}
			c.JobList = new JobList();
			c.JobList.Jobs = (Job[])col.ToArray(typeof(Job));
			col.Clear();
			foreach(ExtensionItemBase i in lstDSP.Items)
				col.Add(i.UniqueID);
			c.DspOrder = (Guid[])col.ToArray(typeof(Guid));
			c.CurrentEncoder = currentEncoder.UniqueID;

            c.GuiPosition = new GuiPosition();
            c.GuiPosition.iTop      = this.Top;
            c.GuiPosition.iLeft     = this.Left;
            c.GuiPosition.iWidth    = this.Width;
            c.GuiPosition.iHeight   = this.Height;
            c.MiscSettings = new MiscSettings();
            c.MiscSettings.directShowPlayer = this.ds_player;
            c.MiscSettings.preferMP4overM4A = BeHappy.NeroDigitalAAC.Encoder.preferMP4overM4A;
			c.SaveToFile(getConfigFileName());
		}

		private string getConfigFileName()
		{
			return getExeDirectory() + @"\BeHappy.State";
		}


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

		#region Windows Form Designer generated code
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
            this.containerControl2 = new System.Windows.Forms.ContainerControl();
            this.containerControl4 = new System.Windows.Forms.ContainerControl();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lstDSP = new System.Windows.Forms.CheckedListBox();
            this.containerControl5 = new System.Windows.Forms.ContainerControl();
            this.btnConfigureDSP = new System.Windows.Forms.Button();
            this.btnMoveDownDSP = new System.Windows.Forms.Button();
            this.btnMoveUpDSP = new System.Windows.Forms.Button();
            this.cbxDisableDSP = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnConfigureEncoder = new System.Windows.Forms.Button();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lstEncoder = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtOutputFileName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConfigureAudioSource = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lstAudioSource = new System.Windows.Forms.ComboBox();
            this.btnSelectSourceFile = new System.Windows.Forms.Button();
            this.txtSourceFileName = new System.Windows.Forms.TextBox();
            this.containerControl3 = new System.Windows.Forms.ContainerControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.cbxOmitEncoderScript = new System.Windows.Forms.CheckBox();
            this.gbxTweak = new System.Windows.Forms.GroupBox();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.cbxBuffer = new System.Windows.Forms.CheckBox();
            this.cbxEnsureMP3VBRSync = new System.Windows.Forms.CheckBox();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.cbxSplit = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.cbxDelay = new System.Windows.Forms.CheckBox();
            this.containerControl1 = new System.Windows.Forms.ContainerControl();
            this.btnExportScript = new System.Windows.Forms.Button();
            this.btnAddToJobList = new System.Windows.Forms.Button();
            this.tabPageJobControl = new System.Windows.Forms.TabPage();
            this.jobListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.containerControl6 = new System.Windows.Forms.ContainerControl();
            this.chkKeepOutput = new System.Windows.Forms.CheckBox();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnNewJob = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtSimpleLog = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnMoveDownJob = new System.Windows.Forms.Button();
            this.btnMoveUpJob = new System.Windows.Forms.Button();
            this.btnDeleteJob = new System.Windows.Forms.Button();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.LinkLabel();
            this.button4 = new System.Windows.Forms.LinkLabel();
            this.button3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtGPL = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPageNewJob.SuspendLayout();
            this.containerControl2.SuspendLayout();
            this.containerControl4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.containerControl5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.containerControl3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbxTweak.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.containerControl1.SuspendLayout();
            this.tabPageJobControl.SuspendLayout();
            this.containerControl6.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageNewJob);
            this.tabControl1.Controls.Add(this.tabPageJobControl);
            this.tabControl1.Controls.Add(this.tabPageAbout);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(792, 373);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageNewJob
            // 
            this.tabPageNewJob.Controls.Add(this.containerControl2);
            this.tabPageNewJob.Controls.Add(this.containerControl1);
            this.tabPageNewJob.Location = new System.Drawing.Point(4, 22);
            this.tabPageNewJob.Name = "tabPageNewJob";
            this.tabPageNewJob.Size = new System.Drawing.Size(784, 347);
            this.tabPageNewJob.TabIndex = 0;
            this.tabPageNewJob.Text = "New Job";
            // 
            // containerControl2
            // 
            this.containerControl2.BackColor = System.Drawing.SystemColors.Control;
            this.containerControl2.Controls.Add(this.containerControl4);
            this.containerControl2.Controls.Add(this.containerControl3);
            this.containerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerControl2.Location = new System.Drawing.Point(0, 0);
            this.containerControl2.Name = "containerControl2";
            this.containerControl2.Size = new System.Drawing.Size(784, 319);
            this.containerControl2.TabIndex = 1;
            // 
            // containerControl4
            // 
            this.containerControl4.BackColor = System.Drawing.SystemColors.Control;
            this.containerControl4.Controls.Add(this.groupBox5);
            this.containerControl4.Controls.Add(this.groupBox4);
            this.containerControl4.Controls.Add(this.groupBox1);
            this.containerControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerControl4.Location = new System.Drawing.Point(0, 0);
            this.containerControl4.Name = "containerControl4";
            this.containerControl4.Size = new System.Drawing.Size(632, 319);
            this.containerControl4.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lstDSP);
            this.groupBox5.Controls.Add(this.containerControl5);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 32);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(632, 263);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "[3] Digital Signal Processing";
            // 
            // lstDSP
            // 
            this.lstDSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDSP.IntegralHeight = false;
            this.lstDSP.Location = new System.Drawing.Point(3, 16);
            this.lstDSP.Name = "lstDSP";
            this.lstDSP.Size = new System.Drawing.Size(626, 216);
            this.lstDSP.TabIndex = 1;
            this.lstDSP.SelectedIndexChanged += new System.EventHandler(this.lstDSP_SelectedIndexChanged);
            // 
            // containerControl5
            // 
            this.containerControl5.BackColor = System.Drawing.SystemColors.Control;
            this.containerControl5.Controls.Add(this.btnConfigureDSP);
            this.containerControl5.Controls.Add(this.btnMoveDownDSP);
            this.containerControl5.Controls.Add(this.btnMoveUpDSP);
            this.containerControl5.Controls.Add(this.cbxDisableDSP);
            this.containerControl5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.containerControl5.Location = new System.Drawing.Point(3, 232);
            this.containerControl5.Name = "containerControl5";
            this.containerControl5.Size = new System.Drawing.Size(626, 28);
            this.containerControl5.TabIndex = 2;
            // 
            // btnConfigureDSP
            // 
            this.btnConfigureDSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfigureDSP.Enabled = false;
            this.btnConfigureDSP.Location = new System.Drawing.Point(388, 4);
            this.btnConfigureDSP.Name = "btnConfigureDSP";
            this.btnConfigureDSP.Size = new System.Drawing.Size(75, 23);
            this.btnConfigureDSP.TabIndex = 3;
            this.btnConfigureDSP.Text = "&Configure";
            this.btnConfigureDSP.Click += new System.EventHandler(this.configureDSP);
            // 
            // btnMoveDownDSP
            // 
            this.btnMoveDownDSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDownDSP.Location = new System.Drawing.Point(548, 4);
            this.btnMoveDownDSP.Name = "btnMoveDownDSP";
            this.btnMoveDownDSP.Size = new System.Drawing.Size(75, 23);
            this.btnMoveDownDSP.TabIndex = 1;
            this.btnMoveDownDSP.Text = "Move Do&wn";
            this.btnMoveDownDSP.Click += new System.EventHandler(this.moveDownDSP);
            // 
            // btnMoveUpDSP
            // 
            this.btnMoveUpDSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveUpDSP.Location = new System.Drawing.Point(468, 4);
            this.btnMoveUpDSP.Name = "btnMoveUpDSP";
            this.btnMoveUpDSP.Size = new System.Drawing.Size(75, 23);
            this.btnMoveUpDSP.TabIndex = 0;
            this.btnMoveUpDSP.Text = "Move &Up";
            this.btnMoveUpDSP.Click += new System.EventHandler(this.moveUpDSP);
            // 
            // cbxDisableDSP
            // 
            this.cbxDisableDSP.Location = new System.Drawing.Point(4, 4);
            this.cbxDisableDSP.Name = "cbxDisableDSP";
            this.cbxDisableDSP.Size = new System.Drawing.Size(72, 20);
            this.cbxDisableDSP.TabIndex = 2;
            this.cbxDisableDSP.Text = "Disable";
            this.cbxDisableDSP.CheckedChanged += new System.EventHandler(this.disableDSP);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnConfigureEncoder);
            this.groupBox4.Controls.Add(this.lstEncoder);
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.txtOutputFileName);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Location = new System.Drawing.Point(0, 295);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(632, 24);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "[4] &Destination";
            // 
            // btnConfigureEncoder
            // 
            this.btnConfigureEncoder.ContextMenuStrip = this.contextMenuStrip2;
            this.btnConfigureEncoder.Location = new System.Drawing.Point(208, 16);
            this.btnConfigureEncoder.Name = "btnConfigureEncoder";
            this.btnConfigureEncoder.Size = new System.Drawing.Size(75, 23);
            this.btnConfigureEncoder.TabIndex = 3;
            this.btnConfigureEncoder.Text = "button14";
            this.btnConfigureEncoder.Click += new System.EventHandler(this.btnConfigureEncoder_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem5,
            this.cancelToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(133, 76);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(132, 22);
            this.toolStripMenuItem2.Text = "Configure";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.configureEncoder);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(132, 22);
            this.toolStripMenuItem3.Text = "Reset";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.resetEncoder);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(129, 6);
            // 
            // cancelToolStripMenuItem1
            // 
            this.cancelToolStripMenuItem1.Name = "cancelToolStripMenuItem1";
            this.cancelToolStripMenuItem1.Size = new System.Drawing.Size(132, 22);
            this.cancelToolStripMenuItem1.Text = "Cancel";
            // 
            // lstEncoder
            // 
            this.lstEncoder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstEncoder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstEncoder.Location = new System.Drawing.Point(4, 40);
            this.lstEncoder.Name = "lstEncoder";
            this.lstEncoder.Size = new System.Drawing.Size(624, 21);
            this.lstEncoder.Sorted = true;
            this.lstEncoder.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(76, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Click += new System.EventHandler(this.selectTargetFile);
            // 
            // txtOutputFileName
            // 
            this.txtOutputFileName.Location = new System.Drawing.Point(4, 16);
            this.txtOutputFileName.Name = "txtOutputFileName";
            this.txtOutputFileName.Size = new System.Drawing.Size(100, 20);
            this.txtOutputFileName.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConfigureAudioSource);
            this.groupBox1.Controls.Add(this.lstAudioSource);
            this.groupBox1.Controls.Add(this.btnSelectSourceFile);
            this.groupBox1.Controls.Add(this.txtSourceFileName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(632, 32);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "[1] &Source";
            // 
            // btnConfigureAudioSource
            // 
            this.btnConfigureAudioSource.ContextMenuStrip = this.contextMenuStrip1;
            this.btnConfigureAudioSource.Location = new System.Drawing.Point(196, 8);
            this.btnConfigureAudioSource.Name = "btnConfigureAudioSource";
            this.btnConfigureAudioSource.Size = new System.Drawing.Size(75, 23);
            this.btnConfigureAudioSource.TabIndex = 3;
            this.btnConfigureAudioSource.Text = "button15";
            this.btnConfigureAudioSource.Click += new System.EventHandler(this.btnConfigureEncoder_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureToolStripMenuItem,
            this.resetToolStripMenuItem,
            this.toolStripMenuItem4,
            this.cancelToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(133, 76);
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.configureToolStripMenuItem.Text = "Configure";
            this.configureToolStripMenuItem.Click += new System.EventHandler(this.configureAudioSource);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetAudioSource);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(129, 6);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.cancelToolStripMenuItem.Text = "Cancel";
            // 
            // lstAudioSource
            // 
            this.lstAudioSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAudioSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstAudioSource.Location = new System.Drawing.Point(4, 40);
            this.lstAudioSource.Name = "lstAudioSource";
            this.lstAudioSource.Size = new System.Drawing.Size(624, 21);
            this.lstAudioSource.Sorted = true;
            this.lstAudioSource.TabIndex = 2;
            // 
            // btnSelectSourceFile
            // 
            this.btnSelectSourceFile.Location = new System.Drawing.Point(76, 16);
            this.btnSelectSourceFile.Name = "btnSelectSourceFile";
            this.btnSelectSourceFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectSourceFile.TabIndex = 1;
            this.btnSelectSourceFile.Click += new System.EventHandler(this.selectSourceFile);
            // 
            // txtSourceFileName
            // 
            this.txtSourceFileName.Location = new System.Drawing.Point(4, 16);
            this.txtSourceFileName.Name = "txtSourceFileName";
            this.txtSourceFileName.Size = new System.Drawing.Size(100, 20);
            this.txtSourceFileName.TabIndex = 0;
            // 
            // containerControl3
            // 
            this.containerControl3.BackColor = System.Drawing.SystemColors.Control;
            this.containerControl3.Controls.Add(this.groupBox3);
            this.containerControl3.Controls.Add(this.gbxTweak);
            this.containerControl3.Dock = System.Windows.Forms.DockStyle.Right;
            this.containerControl3.Location = new System.Drawing.Point(632, 0);
            this.containerControl3.Name = "containerControl3";
            this.containerControl3.Size = new System.Drawing.Size(152, 319);
            this.containerControl3.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnPreview);
            this.groupBox3.Controls.Add(this.cbxOmitEncoderScript);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 146);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(152, 173);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Preview";
            // 
            // btnPreview
            // 
            this.btnPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPreview.Location = new System.Drawing.Point(3, 16);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(146, 137);
            this.btnPreview.TabIndex = 1;
            this.btnPreview.Text = "&Preview";
            this.btnPreview.Click += new System.EventHandler(this.startPreview);
            // 
            // cbxOmitEncoderScript
            // 
            this.cbxOmitEncoderScript.AutoSize = true;
            this.cbxOmitEncoderScript.Checked = true;
            this.cbxOmitEncoderScript.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxOmitEncoderScript.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cbxOmitEncoderScript.Location = new System.Drawing.Point(3, 153);
            this.cbxOmitEncoderScript.Name = "cbxOmitEncoderScript";
            this.cbxOmitEncoderScript.Size = new System.Drawing.Size(146, 17);
            this.cbxOmitEncoderScript.TabIndex = 0;
            this.cbxOmitEncoderScript.Text = "Omit encoder script";
            // 
            // gbxTweak
            // 
            this.gbxTweak.Controls.Add(this.numericUpDown4);
            this.gbxTweak.Controls.Add(this.cbxBuffer);
            this.gbxTweak.Controls.Add(this.cbxEnsureMP3VBRSync);
            this.gbxTweak.Controls.Add(this.numericUpDown3);
            this.gbxTweak.Controls.Add(this.numericUpDown2);
            this.gbxTweak.Controls.Add(this.cbxSplit);
            this.gbxTweak.Controls.Add(this.numericUpDown1);
            this.gbxTweak.Controls.Add(this.cbxDelay);
            this.gbxTweak.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbxTweak.Location = new System.Drawing.Point(0, 0);
            this.gbxTweak.Name = "gbxTweak";
            this.gbxTweak.Size = new System.Drawing.Size(152, 146);
            this.gbxTweak.TabIndex = 5;
            this.gbxTweak.TabStop = false;
            this.gbxTweak.Text = "[2] Tweak";
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Enabled = false;
            this.numericUpDown4.Location = new System.Drawing.Point(66, 117);
            this.numericUpDown4.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown4.TabIndex = 7;
            this.numericUpDown4.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbxBuffer
            // 
            this.cbxBuffer.Location = new System.Drawing.Point(2, 115);
            this.cbxBuffer.Name = "cbxBuffer";
            this.cbxBuffer.Size = new System.Drawing.Size(60, 23);
            this.cbxBuffer.TabIndex = 6;
            this.cbxBuffer.Text = "Buffer";
            this.cbxBuffer.CheckedChanged += new System.EventHandler(this.cbxBuffer_CheckedChanged);
            // 
            // cbxEnsureMP3VBRSync
            // 
            this.cbxEnsureMP3VBRSync.AutoSize = true;
            this.cbxEnsureMP3VBRSync.Location = new System.Drawing.Point(3, 20);
            this.cbxEnsureMP3VBRSync.Name = "cbxEnsureMP3VBRSync";
            this.cbxEnsureMP3VBRSync.Size = new System.Drawing.Size(136, 17);
            this.cbxEnsureMP3VBRSync.TabIndex = 5;
            this.cbxEnsureMP3VBRSync.Text = "Ensure MP3 VBR Sync";
            this.cbxEnsureMP3VBRSync.UseVisualStyleBackColor = true;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Enabled = false;
            this.numericUpDown3.Location = new System.Drawing.Point(66, 91);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown3.TabIndex = 4;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Enabled = false;
            this.numericUpDown2.Location = new System.Drawing.Point(66, 69);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown2.TabIndex = 3;
            // 
            // cbxSplit
            // 
            this.cbxSplit.Location = new System.Drawing.Point(2, 68);
            this.cbxSplit.Name = "cbxSplit";
            this.cbxSplit.Size = new System.Drawing.Size(60, 20);
            this.cbxSplit.TabIndex = 2;
            this.cbxSplit.Text = "Split";
            this.cbxSplit.CheckedChanged += new System.EventHandler(this.enableSplit);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Location = new System.Drawing.Point(66, 43);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown1.TabIndex = 1;
            // 
            // cbxDelay
            // 
            this.cbxDelay.Location = new System.Drawing.Point(2, 41);
            this.cbxDelay.Name = "cbxDelay";
            this.cbxDelay.Size = new System.Drawing.Size(60, 23);
            this.cbxDelay.TabIndex = 0;
            this.cbxDelay.Text = "Delay";
            this.cbxDelay.CheckedChanged += new System.EventHandler(this.enableDelay);
            // 
            // containerControl1
            // 
            this.containerControl1.BackColor = System.Drawing.SystemColors.Control;
            this.containerControl1.Controls.Add(this.btnExportScript);
            this.containerControl1.Controls.Add(this.btnAddToJobList);
            this.containerControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.containerControl1.Location = new System.Drawing.Point(0, 319);
            this.containerControl1.Name = "containerControl1";
            this.containerControl1.Size = new System.Drawing.Size(784, 28);
            this.containerControl1.TabIndex = 0;
            // 
            // btnExportScript
            // 
            this.btnExportScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportScript.Location = new System.Drawing.Point(512, 4);
            this.btnExportScript.Name = "btnExportScript";
            this.btnExportScript.Size = new System.Drawing.Size(132, 23);
            this.btnExportScript.TabIndex = 1;
            this.btnExportScript.Text = "Export Avisynth Script";
            this.btnExportScript.Click += new System.EventHandler(this.exportAviSynthScriptToFile);
            // 
            // btnAddToJobList
            // 
            this.btnAddToJobList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddToJobList.Location = new System.Drawing.Point(650, 4);
            this.btnAddToJobList.Name = "btnAddToJobList";
            this.btnAddToJobList.Size = new System.Drawing.Size(130, 23);
            this.btnAddToJobList.TabIndex = 0;
            this.btnAddToJobList.Text = "En&queue";
            this.btnAddToJobList.Click += new System.EventHandler(this.submitJobToJobControl);
            // 
            // tabPageJobControl
            // 
            this.tabPageJobControl.Controls.Add(this.jobListView);
            this.tabPageJobControl.Controls.Add(this.containerControl6);
            this.tabPageJobControl.Location = new System.Drawing.Point(4, 22);
            this.tabPageJobControl.Name = "tabPageJobControl";
            this.tabPageJobControl.Size = new System.Drawing.Size(784, 347);
            this.tabPageJobControl.TabIndex = 1;
            this.tabPageJobControl.Text = "Queue";
            // 
            // jobListView
            // 
            this.jobListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.jobListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jobListView.FullRowSelect = true;
            this.jobListView.HideSelection = false;
            this.jobListView.LabelWrap = false;
            this.jobListView.Location = new System.Drawing.Point(0, 0);
            this.jobListView.Name = "jobListView";
            this.jobListView.Size = new System.Drawing.Size(784, 167);
            this.jobListView.TabIndex = 1;
            this.jobListView.UseCompatibleStateImageBehavior = false;
            this.jobListView.View = System.Windows.Forms.View.Details;
            this.jobListView.DoubleClick += new System.EventHandler(this.toggleJobStatus);
            this.jobListView.SelectedIndexChanged += new System.EventHandler(this.jobListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Job";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "State";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Start";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Stop";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Source";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Destination";
            this.columnHeader6.Width = 100;
            // 
            // containerControl6
            // 
            this.containerControl6.Controls.Add(this.chkKeepOutput);
            this.containerControl6.Controls.Add(this.btnDeleteAll);
            this.containerControl6.Controls.Add(this.btnNewJob);
            this.containerControl6.Controls.Add(this.btnAbort);
            this.containerControl6.Controls.Add(this.btnStop);
            this.containerControl6.Controls.Add(this.btnStart);
            this.containerControl6.Controls.Add(this.txtSimpleLog);
            this.containerControl6.Controls.Add(this.progressBar);
            this.containerControl6.Controls.Add(this.btnMoveDownJob);
            this.containerControl6.Controls.Add(this.btnMoveUpJob);
            this.containerControl6.Controls.Add(this.btnDeleteJob);
            this.containerControl6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.containerControl6.Location = new System.Drawing.Point(0, 167);
            this.containerControl6.Name = "containerControl6";
            this.containerControl6.Size = new System.Drawing.Size(784, 180);
            this.containerControl6.TabIndex = 0;
            this.containerControl6.Text = "containerControl6";
            // 
            // chkKeepOutput
            // 
            this.chkKeepOutput.AutoSize = true;
            this.chkKeepOutput.Location = new System.Drawing.Point(178, 5);
            this.chkKeepOutput.Name = "chkKeepOutput";
            this.chkKeepOutput.Size = new System.Drawing.Size(163, 17);
            this.chkKeepOutput.TabIndex = 10;
            this.chkKeepOutput.Text = "&Keep output on Abort or error";
            this.chkKeepOutput.UseVisualStyleBackColor = true;
            this.chkKeepOutput.CheckedChanged += new System.EventHandler(this.chkKeepOutput_CheckedChanged);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Location = new System.Drawing.Point(4, 29);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteAll.TabIndex = 9;
            this.btnDeleteAll.Text = "Delete &All";
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnNewJob
            // 
            this.btnNewJob.Location = new System.Drawing.Point(84, 0);
            this.btnNewJob.Name = "btnNewJob";
            this.btnNewJob.Size = new System.Drawing.Size(88, 23);
            this.btnNewJob.TabIndex = 8;
            this.btnNewJob.Text = "Add &new Job";
            this.btnNewJob.Click += new System.EventHandler(this.createNewJob);
            // 
            // btnAbort
            // 
            this.btnAbort.Enabled = false;
            this.btnAbort.Location = new System.Drawing.Point(4, 133);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(75, 23);
            this.btnAbort.TabIndex = 7;
            this.btnAbort.Text = "A&bort";
            this.btnAbort.Click += new System.EventHandler(this.abortEncoding);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(4, 104);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "S&top";
            this.btnStop.Click += new System.EventHandler(this.stopEncoding);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(4, 77);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
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
            this.txtSimpleLog.Location = new System.Drawing.Point(84, 28);
            this.txtSimpleLog.Multiline = true;
            this.txtSimpleLog.Name = "txtSimpleLog";
            this.txtSimpleLog.ReadOnly = true;
            this.txtSimpleLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSimpleLog.Size = new System.Drawing.Size(700, 132);
            this.txtSimpleLog.TabIndex = 4;
            this.txtSimpleLog.WordWrap = false;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 164);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(784, 16);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 3;
            // 
            // btnMoveDownJob
            // 
            this.btnMoveDownJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDownJob.Location = new System.Drawing.Point(704, 0);
            this.btnMoveDownJob.Name = "btnMoveDownJob";
            this.btnMoveDownJob.Size = new System.Drawing.Size(75, 23);
            this.btnMoveDownJob.TabIndex = 2;
            this.btnMoveDownJob.Text = "Move Do&wn";
            this.btnMoveDownJob.Click += new System.EventHandler(this.moveDownJob);
            // 
            // btnMoveUpJob
            // 
            this.btnMoveUpJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveUpJob.Location = new System.Drawing.Point(624, 0);
            this.btnMoveUpJob.Name = "btnMoveUpJob";
            this.btnMoveUpJob.Size = new System.Drawing.Size(75, 23);
            this.btnMoveUpJob.TabIndex = 1;
            this.btnMoveUpJob.Text = "Move &Up";
            this.btnMoveUpJob.Click += new System.EventHandler(this.moveUpJob);
            // 
            // btnDeleteJob
            // 
            this.btnDeleteJob.Location = new System.Drawing.Point(4, 0);
            this.btnDeleteJob.Name = "btnDeleteJob";
            this.btnDeleteJob.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteJob.TabIndex = 0;
            this.btnDeleteJob.Text = "&Delete";
            this.btnDeleteJob.Click += new System.EventHandler(this.deleteJob);
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.BackColor = System.Drawing.Color.White;
            this.tabPageAbout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPageAbout.BackgroundImage")));
            this.tabPageAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabPageAbout.Controls.Add(this.button5);
            this.tabPageAbout.Controls.Add(this.button4);
            this.tabPageAbout.Controls.Add(this.button3);
            this.tabPageAbout.Controls.Add(this.linkLabel1);
            this.tabPageAbout.Controls.Add(this.button1);
            this.tabPageAbout.Controls.Add(this.label2);
            this.tabPageAbout.Controls.Add(this.label1);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Size = new System.Drawing.Size(784, 347);
            this.tabPageAbout.TabIndex = 4;
            this.tabPageAbout.Text = "About";
            // 
            // button5
            // 
            this.button5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button5.Location = new System.Drawing.Point(0, 234);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(784, 24);
            this.button5.TabIndex = 9;
            this.button5.TabStop = true;
            this.button5.Tag = "http://forum.mediatory.ru/viewtopic.php?t=3754";
            this.button5.Text = "BeHappy related thread @ Mediatory forum (RUS)";
            this.button5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_Click);
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button4.Location = new System.Drawing.Point(0, 258);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(784, 24);
            this.button4.TabIndex = 8;
            this.button4.TabStop = true;
            this.button4.Tag = "http://www.avisynth.org/";
            this.button4.Text = "AviSynth Homepage (ENG)";
            this.button4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_Click);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button3.Location = new System.Drawing.Point(0, 282);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(784, 24);
            this.button3.TabIndex = 7;
            this.button3.TabStop = true;
            this.button3.Tag = "http://forum.doom9.org/printthread.php?t=103069&pp=40";
            this.button3.Text = "BeHappy related thread @ Doom9 forum (ENG)";
            this.button3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.linkLabel1.Location = new System.Drawing.Point(0, 306);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(784, 24);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Tag = "http://workspaces.gotdotnet.com/behappy";
            this.linkLabel1.Text = "BeHappy workspace @ www.gotdotnet.com (ENG)";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.Location = new System.Drawing.Point(0, 330);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(784, 17);
            this.button1.TabIndex = 5;
            this.button1.TabStop = true;
            this.button1.Tag = "http://dimzon541.narod.ru/";
            this.button1.Text = "dimzon\'s Homepage (RUS)";
            this.button1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(193, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "this software distrubuted under terms of GPL";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "this page is under construction yet";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtGPL);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(784, 347);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "GPL";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtGPL
            // 
            this.txtGPL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGPL.Location = new System.Drawing.Point(3, 3);
            this.txtGPL.Multiline = true;
            this.txtGPL.Name = "txtGPL";
            this.txtGPL.ReadOnly = true;
            this.txtGPL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtGPL.Size = new System.Drawing.Size(778, 341);
            this.txtGPL.TabIndex = 0;
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.DefaultExt = "avs";
            this.saveFileDialog2.Filter = "AviSynth script (*.avs)|*.avs";
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(792, 373);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(450, 300);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Closed += new System.EventHandler(this.MainForm_Closed);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.tabControl1.ResumeLayout(false);
            this.tabPageNewJob.ResumeLayout(false);
            this.containerControl2.ResumeLayout(false);
            this.containerControl4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.containerControl5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.containerControl3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbxTweak.ResumeLayout(false);
            this.gbxTweak.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.containerControl1.ResumeLayout(false);
            this.tabPageJobControl.ResumeLayout(false);
            this.containerControl6.ResumeLayout(false);
            this.containerControl6.PerformLayout();
            this.tabPageAbout.ResumeLayout(false);
            this.tabPageAbout.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.CheckBox cbxEnsureMP3VBRSync;
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}

		#region AviSynth script generation

		private string createAvsScript(string sourceFileName, string targetFileName, AudioEncoder enc)
		{
			return createAvsScript(sourceFileName, targetFileName, enc, false);
		}

		private string createAvsScript(string sourceFileName, string targetFileName, AudioEncoder enc, bool omitEncoderScript)
		{
			string SEPARATOP = new string('#',40);
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("{0}{1}#Created by {2} v{3}{1}#Creation timestamp: {4}{1}{0}{1}#Source FileName:{5}{1}#Target FileName:{6}{1}{0}{1}{1}" , SEPARATOP, Environment.NewLine, Application.ProductName, Application.ProductVersion, DateTime.Now, sourceFileName, targetFileName);
			AudioSource source = lstAudioSource.SelectedItem as AudioSource;
			sb.AppendFormat("{0}{1}# [Source: {2}]{1}{0}{1}", SEPARATOP, Environment.NewLine, source.Title);
			// A part of AviSynth script
			// {0} means input file name
			// {1} means output file name
			// {2} means unique string (to use as part of identifier)
			// {3} means '{' character (to allow '{' to be used)
			sb.AppendFormat(source.ScriptBlock, sourceFileName, targetFileName, Guid.NewGuid().ToString("N"),"{","}");
            sb.Append(Environment.NewLine+Environment.NewLine);
			if(cbxEnsureMP3VBRSync.Checked)
			{
			    sb.Append("EnsureVBRMP3Sync() # Some black magic to avoid desync" + Environment.NewLine+Environment.NewLine);
            }
			if(cbxDelay.Checked)
			{
				sb.Append(SEPARATOP);
				sb.AppendFormat("{0}# [BeHappy: Delay Audio by {1} ms ]{0}DelayAudio( {1}.0/1000.0){0}{0}", Environment.NewLine, (long)numericUpDown1.Value);
			}
//#warning other tweaks here !!!

            if (cbxSplit.Checked)
            {
                sb.AppendFormat("{1}{0}# [BeHappy: Create fictive 1000fps video for triming]{0}{1}{0}AudioDubEx(BlankClip(length=Int(1000*AudioLengthF(last)/Audiorate(last)), width=32, height=32, pixel_type=\"RGB24\", fps=1000), last){0}{0}", Environment.NewLine, SEPARATOP);
                sb.AppendFormat("{0}{0}trim({1},{2}){0}{0}", Environment.NewLine, (long)numericUpDown2.Value, (long)numericUpDown3.Value);
                sb.AppendFormat("{0}{0}{1}{0}# [BeHappy: Kill video]{0}{1}{0}AudioDubEx(Tone(), last){0}{0}", Environment.NewLine, SEPARATOP);
            }

            if(cbxBuffer.Checked)
            {
                sb.Append(SEPARATOP);
                sb.AppendFormat("{0}# [BeHappy: Buffer Audio by {1} s ]{0}NicBufferAudio( last, {1}){0}{0}", Environment.NewLine, (long)numericUpDown4.Value);
            }


			if(!cbxDisableDSP.Checked)
			{
				foreach(DigitalSignalProcessor dsp in lstDSP.Items)
				{
					if(lstDSP.CheckedItems.Contains(dsp))
					{
						sb.AppendFormat("{0}{1}# [DSP: {2}]{1}{0}{1}", SEPARATOP, Environment.NewLine, dsp.Title);
						// A part of AviSynth script
						// {0} means input file name
						// {1} means output file name
						// {2} means unique string (to use as part of identifier)
						// {3} means '{' character (to allow '{' to be used)
						sb.AppendFormat(dsp.ScriptBlock, sourceFileName, targetFileName, Guid.NewGuid().ToString("N"),"{","}");
						sb.Append(Environment.NewLine+Environment.NewLine);
					}
				}
			}
			sb.AppendFormat("{0}{1}# [Encoder: {2}]{1}{0}{1}", SEPARATOP, Environment.NewLine, enc.Title);
			if(enc.ScriptBlock!=null && !omitEncoderScript)
			{
				if(enc.ScriptBlock.Length!=0)
				{
					// A part of AviSynth script
					// {0} means input file name
					// {1} means output file name
					// {2} means unique string (to use as part of identifier)
					// {3} means '{' character (to allow '{' to be used)
					sb.AppendFormat(enc.ScriptBlock,sourceFileName, targetFileName, Guid.NewGuid().ToString("N"),"{","}");
				}
			}
			return sb.ToString();
			
		}

		private string createAvsScript(bool omitEncoderScript)
		{
			return createAvsScript(sourceFileName, targetFileName, currentEncoder, omitEncoderScript);
		}

		private string createAvsScript()
		{
			return createAvsScript(false);
		}

		#endregion


		private string sourceFileName
		{
			get { return txtSourceFileName.Text.Trim(); }
			set {txtSourceFileName.Text = value.Trim();}
		}

		private string targetFileName
		{
			get { return txtOutputFileName.Text.Trim(); }
			set {txtOutputFileName.Text = value.Trim();}
		}


		private bool selectFile(FileDialog fileDialog, ComboBox openAs, TextBox ctrl )
		{
			StringBuilder sb = new StringBuilder();
			foreach(FileRelatedExtensionItemBase enc in openAs.Items)
			{
				sb.AppendFormat("{0} ({1})|{1}|", enc.Title, enc.GetFilesMask());
			}
			sb.Append("All files (*.*)|*.*");
			fileDialog.Filter = sb.ToString();
			fileDialog.FilterIndex = 1 + openAs.SelectedIndex;

            // make sure the control we're opening is the output/destination
            if (ctrl.Name == "txtOutputFileName" && ctrl.Text != "")
            {
                // set up our delimiter to break down the path and file
                char[] arDelim = new char[1];
                arDelim[0] = '\\';

                // split the path/file parts based on our delimiter
                string[] arParts = txtOutputFileName.Text.Split(arDelim);

                // we know the file is the last element in the array
                string strFileName = arParts[arParts.GetUpperBound(0)];

                // put the file name in so the user doesn't have to retype it
                // make shon3i happy. :)
                fileDialog.FileName = strFileName;
            }

			bool result;
			if(result=DialogResult.OK==fileDialog.ShowDialog())
			{
				ctrl.Text = fileDialog.FileName;
				if(fileDialog.FilterIndex<=openAs.Items.Count)
					openAs.SelectedIndex = fileDialog.FilterIndex-1;
			}
			return result;
		}

		private void selectSourceFile(object sender, System.EventArgs e)
		{
			if(selectFile(openFileDialog1,lstAudioSource,txtSourceFileName) /*&& this.targetFileName.Length==0*/)
			{
				string target =	 Path.ChangeExtension(sourceFileName, currentEncoder.GetFirstExtension());
				if(0==string.Compare(target, this.sourceFileName))
				{
					target =	
							Path.GetDirectoryName(target) 
							+ Path.DirectorySeparatorChar 
							+ Path.GetFileNameWithoutExtension(target) 
							+ "_" + Guid.NewGuid().ToString("N") 
							+ Path.GetExtension(target);
				}
				this.targetFileName = target;
			}
		}

		private void selectTargetFile(object sender, System.EventArgs e)
		{
			selectFile(saveFileDialog1, lstEncoder, txtOutputFileName);
		}

		#region JobList management

		private Job createJob()
		{
			string sourceFileName = this.sourceFileName;
			string targetFileName = this.targetFileName;
			Job job = new Job();
			AudioEncoder enc = currentEncoder;
			job.AviSynthScript = createAvsScript(sourceFileName, targetFileName, enc);
            job.CommandLine = enc.GetExecutableArguments(System.IO.Path.GetExtension(targetFileName).ToLower());
            job.EncoderExecutable=encoder_dir+enc.ExecutableFileName;
            if(job.EncoderExecutable.Length==encoder_dir.Length )
            	job.EncoderExecutable=null;
			job.SourceFile=sourceFileName;
			job.TargetFile=targetFileName;
			job.SendRiffHeader=enc.WriteRiffHeader;

			return job;
		}

		private void submitJobToJobControl(object sender, System.EventArgs e)
		{
            // make sure we have a source and a target
            if ((this.sourceFileName == null || this.sourceFileName == "") ||
                 (this.targetFileName == null || this.targetFileName == ""))
            {
                MessageBox.Show("You must select a source and a destination file before proceeding.", "Source/Destination required",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

			Job job = createJob();
			ListViewItem item = createJobItem(job);

            // now that we have a multi-select list, let's deselect
            // all items prior to selecting the job that was just
            // submitted to the job control
            deselectAllJobs();

			jobListView.Items.Add(item).Selected=true;

			tabControl1.SelectedTab = tabPageJobControl;
		}

		private void createNewJob(object sender, System.EventArgs e)
		{
			tabControl1.SelectedTab = tabPageNewJob;
		}


		private static void updateListViewItem(ListViewItem item)
		{
			Job job = item.Tag as Job;
			int i=1;
			item.SubItems[i++].Text=job.State.ToString();
			item.SubItems[i++].Text=job.State<JobState.Processing?string.Empty:job.StartAt.ToString();
			item.SubItems[i++].Text=job.State<JobState.Error?string.Empty:job.StopAt.ToString();
			item.SubItems[i++].Text=job.SourceFile;
			item.SubItems[i++].Text=job.TargetFile;
		}

		private static ListViewItem createJobItem(Job job)
		{
			ListViewItem item = new ListViewItem(job.Name);
			item.Tag = job;
			for(int i=0;i<10;i++)
				item.SubItems.Add(string.Empty);
			updateListViewItem(item);
			return item;
		}

		private void toggleJobStatus(object sender, System.EventArgs e)
		{
			ListView list = sender as ListView;
			if(list.SelectedItems.Count==0) 
				return;
			ListViewItem item = list.SelectedItems[0];
			Job job = (Job)item.Tag;
			if(job.State==JobState.Processing)
				return;
			if(job.State!=JobState.Waiting)
				job.State=JobState.Waiting;
			else
				job.State=JobState.Postponed;
			updateListViewItem(item);

		}

		private void moveUpJob(object sender, System.EventArgs e)
		{
			if(jobListView.SelectedItems.Count==0)
				return;
			ListViewItem item =	jobListView.SelectedItems[0];
			int n = item.Index;
			if(n>0)
			{
				jobListView.Items.RemoveAt(n);
				jobListView.Items.Insert(--n,item);
			}
			else
			{
				jobListView.Items.RemoveAt(n);
				jobListView.Items.Add(item);
			}
			item.Selected = true;
		
		}

		private void moveDownJob(object sender, System.EventArgs e)
		{
			if(jobListView.SelectedItems.Count==0)
				return;
			ListViewItem item =	jobListView.SelectedItems[0];
			int n = item.Index;
			if(n<jobListView.Items.Count-1)
			{
				jobListView.Items.RemoveAt(n);
				jobListView.Items.Insert(++n,item);
			}
			else
			{
				jobListView.Items.RemoveAt(n);
				jobListView.Items.Insert(0, item);
			}
			item.Selected = true;		
		}

		private void deleteJob(object sender, System.EventArgs e)
		{
            int iIdxOfSelectedItem = 0;
            while (jobListView.SelectedItems.Count > 0)
            {
                ListViewItem item = jobListView.SelectedItems[iIdxOfSelectedItem];
                lock (lockObject)
                {
                    if (((Job)item.Tag).State != JobState.Processing)
                    {
                        jobListView.Items.Remove(item);
                    }
                    else
                    {
                        iIdxOfSelectedItem++;
                    }

                    if (iIdxOfSelectedItem == jobListView.SelectedItems.Count)
                        break;
                }
            }
        }
        #endregion

        private void moveUpDSP(object sender, System.EventArgs e)
		{

			int nIndex = lstDSP.SelectedIndex;
			if(nIndex>=0)
			{
				bool ch = lstDSP.GetItemChecked(nIndex);
				object o = lstDSP.Items[nIndex];
				lstDSP.Items.RemoveAt(nIndex);
				if(nIndex==0)
					lstDSP.SelectedIndex = lstDSP.Items.Add(o,ch);
				else
				{
					lstDSP.Items.Insert(--nIndex,o);
					lstDSP.SetItemChecked(nIndex, ch);
					lstDSP.SelectedIndex = nIndex;
				}
			}

		}

		private void moveDownDSP(object sender, System.EventArgs e)
		{
			int nIndex = lstDSP.SelectedIndex;
			if(nIndex>=0)
			{
				int nCount = lstDSP.Items.Count;
				bool ch = lstDSP.GetItemChecked(nIndex);
				object o = lstDSP.Items[nIndex];
				lstDSP.Items.RemoveAt(nIndex);
				if(nIndex==nCount-1)
				{
					nIndex = 0;
				}
				else
				{
					++nIndex;
				}
				lstDSP.Items.Insert(nIndex,o);
				lstDSP.SetItemChecked(nIndex, ch);
				lstDSP.SelectedIndex = nIndex;
			}
		}

		private string getExeDirectory()
		{
			return	Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
		}

		private void disableDSP(object sender, System.EventArgs e)
		{
            bool a,b;
            ExtensionItemBase item = lstDSP.SelectedItem as ExtensionItemBase;
            if (item != null)
                b = item.IsSupportConfiguration;
            else
                b = false;
            a = (lstDSP.Enabled = btnMoveUpDSP.Enabled = btnMoveDownDSP.Enabled = !(sender as CheckBox).Checked);
            btnConfigureDSP.Enabled= b && a;
		}

		private void enableDelay(object sender, System.EventArgs e)
		{
			numericUpDown1.Enabled=(sender as CheckBox).Checked;
		}

		private void enableSplit(object sender, System.EventArgs e)
		{
			numericUpDown2.Enabled=numericUpDown3.Enabled=(sender as CheckBox).Checked;
		}

		private void MainForm_Closed(object sender, System.EventArgs e)
		{
			try
			{
				if(File.Exists(m_tempFileName))
					File.Delete(m_tempFileName);
			}
			catch{};
			saveConfiguration();
		}

		private object lockObject
		{
			get
			{
				return this.GetType();
			}
		}

		private void exportAviSynthScriptToFile(object sender, System.EventArgs e)
		{
			if(saveFileDialog2.ShowDialog()==DialogResult.OK)
			{
				using(TextWriter w = new StreamWriter(saveFileDialog2.FileName,false,Encoding.Default))
				{
					w.WriteLine(createAvsScript());
				}
			}
		}

		private void lstEncoder_SelectedIndexChanged(object sender, EventArgs e)
		{
			AudioEncoder enc = currentEncoder;
			btnConfigureEncoder.Enabled = enc.IsSupportConfiguration;
			if(0==targetFileName.Length)
				return;
			if(!enc.IsSupportedException(Path.GetExtension(targetFileName)))
			{
				targetFileName = Path.ChangeExtension(targetFileName, enc.GetFirstExtension());
			}
		}

		private void configureEncoder(object sender, System.EventArgs e)
		{
			configureItemInCombo(currentEncoder, lstEncoder);
		}

		private void configureItemInCombo(ExtensionItemBase item, ComboBox combo)
		{
            configureItemInCombo(item, combo, false);
		}

        private void configureItemInCombo(ExtensionItemBase item, ComboBox combo, bool reset)
        {
            if (reset)
                item.ResetConfiguration();
            else if (item.Configure(this) != ConfigurationResult.OK)
                return;

            int n = combo.Items.IndexOf(item);
            combo.Items[n] = item;
            combo.SelectedIndex = n;

        }
		private void configureAudioSource(object sender, System.EventArgs e)
		{
			configureItemInCombo(currentSource, lstAudioSource);
		}

		private AudioSource currentSource
		{
			get
			{
				return (lstAudioSource.SelectedItem as AudioSource);
			}
		}

		private AudioEncoder currentEncoder
		{
			get
			{
				return (lstEncoder.SelectedItem as AudioEncoder);
			}
		}

		private void lstAudioSource_SelectedIndexChanged(object sender, EventArgs e)
		{
			btnConfigureAudioSource.Enabled = currentSource.IsSupportConfiguration;
		}

		private void lstDSP_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ExtensionItemBase item = lstDSP.SelectedItem as ExtensionItemBase;
			if(item!=null)
				btnConfigureDSP.Enabled = item.IsSupportConfiguration;
			else
				btnConfigureDSP.Enabled = false;
		}

		private void configureDSP(object sender, System.EventArgs e)
		{
			DigitalSignalProcessor item = lstDSP.SelectedItem as DigitalSignalProcessor;
			if(item==null)
				return;
			if(!item.IsSupportConfiguration)
				return;
			if(item.Configure(this)==ConfigurationResult.OK)
			{
				lstDSP.Items[lstDSP.Items.IndexOf(item)]=item;
				(sender as Control).Focus();
			}
		}

		private string getPlayer()
		{
            return this.ds_player;
		}

		private string m_tempFileName = Path.GetTempPath() + "preview-" + Guid.NewGuid().ToString("n")+".avs";

		private void startPreview(object sender, System.EventArgs e)
		{
			using(TextWriter w = new StreamWriter(m_tempFileName, false, Encoding.Default))
			{
				w.WriteLine(createAvsScript(cbxOmitEncoderScript.Checked));
			}
			Process.Start(getPlayer(), m_tempFileName);
		}

		#region Job Encoding

		private Encoder m_encoder;
		private bool m_breakAfterCurrentJob;
		private ListViewItem m_item;
		private StringBuilder m_stdout;
		private StringBuilder m_stderr;
		private StringBuilder m_log;


		private void appendToLog(string s)
		{
			m_log.Append(s);
			m_log.Append(Environment.NewLine);
			Job job = m_item.Tag as Job;
			job.Log = m_log.ToString();
			if(m_item.Selected)
			{
				txtSimpleLog.Text=(job).Log;
			}
		}

		private void startJobs(object sender, System.EventArgs e)
		{
			m_breakAfterCurrentJob = false;
			btnAbort.Enabled = btnStop.Enabled = true;
			executeNextJob();
		}

		private Regex m_cleanUpStdOutRegex = new Regex(@"\n[^\n]+\r", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		private string cleanUpStdOut(string s)
		{
//			using(TextWriter w = new StreamWriter("c:\\out.txt",false, Encoding.Default))
//			{
//				w.Write(s.Replace("\\","\\\\").Replace("\"","\\\"").Replace("\n","\\n").Replace("\r","\\r"));
//			};

			//return s;
			return m_cleanUpStdOutRegex.Replace(s.Replace(Environment.NewLine, "\n"), Environment.NewLine);
		}

		private void finalizeCurrentJob(JobState state)
		{
				Job job = m_item.Tag as Job;
				lock(lockObject)
				{
					job.State = state;
					job.StopAt = DateTime.Now;
					if(m_stdout.Length!=0)
					{
						appendToLog("#### Encoder StdOut ####");
						appendToLog(cleanUpStdOut(m_stdout.ToString()));
					}
					if(m_stderr.Length!=0)
					{
						appendToLog("#### Encoder StdErr ####");
						appendToLog(cleanUpStdOut(m_stderr.ToString()));
					}
					job.Log = m_log.ToString();
					updateListViewItem(m_item);
					return;
				}
		}


		private void executeNextJob()
		{
			if(false==m_breakAfterCurrentJob)
			{
				foreach(ListViewItem i in jobListView.Items)
				{
					Job job = i.Tag as Job;
					lock(lockObject)
					{
                        if (job.State == JobState.Waiting)
                        {
                            deselectAllJobs();

                            m_stderr = new StringBuilder();
                            m_stdout = new StringBuilder();
                            m_log = new StringBuilder();
                            m_item = i;
                            i.Selected = true;
                            job.Log = string.Empty;
                            txtSimpleLog.Text = string.Empty;
                            job.State = JobState.Processing;
                            job.StartAt = DateTime.Now;
                            job.bKeepOutput = m_bKeepOutput;
                            updateListViewItem(i);
                            btnStart.Enabled = false;
                            appendToLog("Starting job " + job.Name);
                            m_encoder = new Encoder(job);
                            m_encoder.SetKeepOutput(m_bKeepOutput);
                            m_encoder.EncoderCallback += new EncoderStatusCallbackDelegate(encoderCallback);
                            m_encoder.Start();
                            return;
                        }
					}
				}
			}
			btnStart.Enabled = true;
			btnAbort.Enabled = btnStop.Enabled = false;
		}

        private void deselectAllJobs()
        {
            foreach (ListViewItem li in jobListView.Items)
                li.Selected = false;
        }

		private void encoderCallback(EncoderCallbackEventArgs s)
		{
			if(!this.InvokeRequired)
			{
				switch(s.Type)
				{
					case EncoderCallbackEventArgs.EventType.Progress:
						this.progressBar.Value=(int) s.Progress;
						break;
					case EncoderCallbackEventArgs.EventType.Done:
						appendToLog("Complete");
						finalizeCurrentJob(JobState.Done);
						executeNextJob();
						break;
					case EncoderCallbackEventArgs.EventType.Terminated:
						appendToLog("Aborted");
						finalizeCurrentJob(JobState.Aborted);
						executeNextJob();
						break;
					case EncoderCallbackEventArgs.EventType.Error:
						appendToLog("Error: " + s.Message);
						finalizeCurrentJob(JobState.Error);
						executeNextJob();
						break;
					case EncoderCallbackEventArgs.EventType.Notify:
						appendToLog(s.Message);
						break;
					case EncoderCallbackEventArgs.EventType.StdErr:
						m_stderr.Append(s.Message);
						break;
					case EncoderCallbackEventArgs.EventType.StdOut:
						m_stdout.Append(s.Message);
						break;
                    case EncoderCallbackEventArgs.EventType.KeepOutput:
                        break;
				}
			}
			else
			{
				EncoderStatusCallbackDelegate d = new EncoderStatusCallbackDelegate(encoderCallback);
				this.Invoke(d, new object[]{s});
			}

		}


		private void stopEncoding(object sender, System.EventArgs e)
		{
			m_breakAfterCurrentJob = true;
			btnStop.Enabled=false;
		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			lock(lockObject)
			{
				if(m_encoder!=null)
				{
					if(m_encoder.IsBusy)
						e.Cancel = true;
				}
			}
		}
		
		private void abortEncoding(object sender, System.EventArgs e)
		{
			stopEncoding(btnStop, e);
			m_encoder.Abort();
		}
		
		private void jobListView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string log = string.Empty;
            if (jobListView.SelectedItems.Count == 1)
            {
                log = (jobListView.SelectedItems[0].Tag as Job).Log;
                txtSimpleLog.Text = string.Empty + log;
            }
            else
            {
                txtSimpleLog.Text = string.Empty;
            }

            SetJobMoveButtonState();
		}
		
		#endregion

        private void SetJobMoveButtonState()
        {
            // set move button states
            if (jobListView.SelectedItems.Count == 0 ||
                jobListView.SelectedItems.Count > 1)
            {
                this.btnMoveUpJob.Enabled = false;
                this.btnMoveDownJob.Enabled = false;
            }
            else if (jobListView.SelectedItems[0].Index == 0)
            {
                this.btnMoveUpJob.Enabled = false;
                this.btnMoveDownJob.Enabled = jobListView.Items.Count > 1;
            }
            else if (jobListView.SelectedItems[0].Index == jobListView.Items.Count - 1)
            {
                this.btnMoveUpJob.Enabled = true;
                this.btnMoveDownJob.Enabled = false;
            }
            else
            {
                this.btnMoveUpJob.Enabled = true;
                this.btnMoveDownJob.Enabled = true;
            }
        }

        private void linkLabel1_Click(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start((sender as Control).Tag.ToString());
		}

        private void resetEncoder(object sender, EventArgs e)
        {
            configureItemInCombo(currentEncoder, lstEncoder, true);
        }

        private void resetAudioSource(object sender, EventArgs e)
        {
            configureItemInCombo(currentSource, lstAudioSource, true);
        }

        private void btnConfigureEncoder_Click(object sender, EventArgs e)
        {
            Button b = (sender as Button);
            b.ContextMenuStrip.Show(b,0,0);
        }

        private void cbxBuffer_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown4.Enabled = cbxBuffer.Checked;
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (jobListView.Items.Count == 0)
                return;

            lock (lockObject)
            {
                int idxOfItemInProcess = 0;
                while (jobListView.Items.Count > 0)
                {
                    ListViewItem li = jobListView.Items[idxOfItemInProcess];
                    if (((Job)li.Tag).State == JobState.Postponed ||
                        ((Job)li.Tag).State == JobState.Processing ||
                        ((Job)li.Tag).State == JobState.Waiting)
                    {
                        idxOfItemInProcess++;
                    }
                    else
                    {
                        jobListView.Items.Remove(li);
                    }

                    if (idxOfItemInProcess == jobListView.Items.Count)
                        break;
                }
            }
        }

        private void chkKeepOutput_CheckedChanged(object sender, EventArgs e)
        {
            m_bKeepOutput = chkKeepOutput.Checked;

            if (m_encoder != null)
                m_encoder.SetKeepOutput(m_bKeepOutput);
        }

	}
}
