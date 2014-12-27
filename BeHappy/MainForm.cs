using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using BeHappy.Extensibility;
using BeHappy.Extensions;
using System.Threading;
using System.Data;

namespace BeHappy
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class MainForm : Form
	{
		private string encoder_dir;
		private string ds_player="mplayer2";
		private ProcessPriorityClass m_enumCurrentPriority;
		private int m_iCurrentPriorityIndex;

		/// <summary>
		/// Gets or sets the current priority level
		/// </summary>
		private ProcessPriorityClass CurrentPriority {
			get {return m_enumCurrentPriority;}
			set {m_enumCurrentPriority = value;}
		}

		/// <summary>
		/// Gets or sets the current priority selected item's index
		/// </summary>
		private int CurrentPriorityIndex {
			get {return m_iCurrentPriorityIndex;}
			set {m_iCurrentPriorityIndex = value;}
		}
		
		private ToolStripItem[] toolStripItemsJoblist;
		
		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			toolStripItemsJoblist = new ToolStripItem[]{new ToolStripMenuItem("Reset Status", null, toggleJobStatus),
				new ToolStripMenuItem("Remove", null, deleteJob),
				new ToolStripSeparator(),
				new ToolStripMenuItem("Up", null, moveUpJob),
				new ToolStripMenuItem("Down", null, moveDownJob)};

			contextMenuStrip1.Items.AddRange(toolStripItemsJoblist);
			bKeepOutput = false;
			m_iCurrentPriorityIndex = 0;
			m_enumCurrentPriority = ProcessPriorityClass.Idle;

			using (TextReader r = new StreamReader(this.GetType().Assembly.GetManifestResourceStream("BeHappy.gpl.txt")))
			{
				txtGPL.Text = r.ReadToEnd();
			}

			using (Stream ricon = this.GetType().Assembly.GetManifestResourceStream("BeHappy.App.ico"))
			{
				this.Icon = new Icon(ricon);
			}

			numericUpDownDelay.Minimum = numericUpDownSplitA.Minimum = numericUpDownSplitB.Minimum = decimal.MinValue+16;
			numericUpDownDelay.Maximum = numericUpDownSplitA.Maximum = numericUpDownSplitB.Maximum = decimal.MaxValue-16;

			this.Text = string.Format("{0} v{1} by {2}", Application.ProductName, Application.ProductVersion, Application.CompanyName);
			
			if (Directory.Exists(getExeDirectory()+"\\encoder"))
				encoder_dir="encoder\\";
			else
				encoder_dir="";

			loadExtensionsAndApplyConfiguration();
			
			linkLabelSourceConfig.Enabled = linkLabelSourceReset.Enabled = currentSource.IsSupportConfiguration;
			linkLabelEncoderConfig.Enabled = linkLabelEncoderReset.Enabled = currentEncoder.IsSupportConfiguration;
			
			// 20080126 Chumbo mod
			SetDSPMoveButtonState();
			InitPriorityControls();

//			 MessageBox.Show(@"res://" + Application.ExecutablePath + "/#32512");
//			 this.Icon = new Icon(@"res://BeHappy.exe/32512");//@"res://" + Application.ExecutablePath + "/#32512"); //("res://BeHappy.exe/#32512"); // null; // Application.Icon;
			
			var resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			toolTip1.SetToolTip(numericUpDownJobs, resources.GetString("numericUpDownJobs.ToolTip") + "\n\n[ " + Environment.ProcessorCount + " ]  CPU cores detected.");
			toolTip1.SetToolTip(labelNumJobs, resources.GetString("numericUpDownJobs.ToolTip") + "\n\n[ " + Environment.ProcessorCount + " ]  CPU cores detected.");
			toolTip1.SetToolTip(labelDragDrop, resources.GetString("lstSourceFiles.ToolTip"));
			
			PropertyInfo pInf = GetType().GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
			pInf.SetValue(jobListView, true, null);	//remove flickering, seems to have only effect on ListViews
			
			toolStripStatusLabel1.Text = "Hint:  Left click on  \'Add...\'  to add files, right click on it to add a folder.";
		}

		private void loadExtensionsAndApplyConfiguration()
		{
			//1) let's load all extensions
			ArrayList audioSources = new ArrayList();
			ArrayList dspProcessors = new ArrayList();
			ArrayList audioEncoders = new ArrayList();
			Extension extension = Extension.Default;
			string ExtensionDirectory = getExeDirectory() + "\\extensions";
			if(!Directory.Exists(ExtensionDirectory))
				ExtensionDirectory = getExeDirectory();

			fillFromExtension(extension, audioEncoders, dspProcessors, audioSources);

			foreach(string file in Directory.GetFiles(ExtensionDirectory, "*.extension"))
			{
				__retry:
					try	{
					extension = Extension.LoadFromFile(file);
					fillFromExtension(extension, audioEncoders, dspProcessors, audioSources);
				}
				catch(Exception e) {
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
			//let's load configuration    cc
			Configuration c = new Configuration();
			try {
				c = Configuration.LoadFromFile(getConfigFileName());
			}
			catch {}

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
				if(c.JobList.Jobs != null)
				{
//					foreach(Job job in c.JobList.Jobs)
//					{
//						jobListView.Items.Add(createJobItems(job));
//					}
					jobListView.Items.AddRange(createJobItems(c.JobList.Jobs));
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
			//                      else
			//                      {
			//                              BeHappy.NeroDigitalAAC.Encoder.preferMP4overM4A = true;
			//                              this.ds_player = "mplayer";
			//                      }
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
					XmlElement      conf = c[i.UniqueID] as XmlElement;
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

		// 20090216 Chumbo mod
		// added exception handling to make sure items already
		// added to collections do not crash the application
		private void saveConfiguration()
		{
			// 20090216 Chumbo mod
			try {
				Configuration c = new Configuration();
				ArrayList col = new ArrayList();

				// 20090216 Chumbo mod
				try {
					col.AddRange(lstEncoder.Items);
				}
				catch (Exception ex) {
					MessageBox.Show("The following exception occurred while adding the encoder items:\r\n" +
					                ex.Message, "Save Configuration Exception");
				}

				// 20090216 Chumbo mod
				try {
					col.AddRange(lstAudioSource.Items);
				}
				catch (Exception ex) {
					MessageBox.Show("The following exception occurred while adding the audio source items:\r\n" +
					                ex.Message, "Save Configuration Exception");
				}

				// 20090216 Chumbo mod
				try {
					col.AddRange(lstDSP.Items);
				}
				catch (Exception ex) {
					MessageBox.Show("The following exception occurred while adding the DSP items:\r\n" +
					                ex.Message, "Save Configuration Exception");
				}

				foreach (ExtensionItemBase i in col)
				{
					if (i.IsSupportConfiguration)
					{
						XmlElement e = i.SaveConfiguration();
						if (e != null)
						{
							// 20090216 Chumbo mod
							if (!c.Settings.Contains(i.UniqueID))
							{
								c.Settings.Add(i.UniqueID, e);
							}
						}
					}
				}

				col.Clear();
				foreach (ListViewItem i in jobListView.Items)
				{
					// 20090216 Chumbo mod
					if (!col.Contains(i.Tag))
					{
						col.Add(i.Tag);
					}
				}

				c.JobList = new JobList();
				c.JobList.Jobs = (Job[])col.ToArray(typeof(Job));
				col.Clear();

				foreach (ExtensionItemBase i in lstDSP.Items)
				{
					// 20090216 Chumbo mod
					if (!col.Contains(i.UniqueID))
					{
						col.Add(i.UniqueID);
					}
				}

				c.DspOrder = (Guid[])col.ToArray(typeof(Guid));
				c.CurrentEncoder = currentEncoder.UniqueID;

				c.GuiPosition = new GuiPosition();
				c.GuiPosition.iTop = this.Top;
				c.GuiPosition.iLeft = this.Left;
				c.GuiPosition.iWidth = this.Width;
				c.GuiPosition.iHeight = this.Height;
				c.MiscSettings = new MiscSettings();
				c.MiscSettings.directShowPlayer = this.ds_player;
				c.MiscSettings.preferMP4overM4A = BeHappy.NeroDigitalAAC.Encoder.preferMP4overM4A;
				c.SaveToFile(getConfigFileName());
			}
			// 20090216 Chumbo mod
			catch (Exception ex) {
				MessageBox.Show(ex.Message, "Save Configuration Exception");
			}
		}

		private string getConfigFileName()
		{
			return getExeDirectory() + @"\BeHappy.State";
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
				sb.AppendFormat("{0}# [BeHappy: Delay Audio by {1} ms ]{0}DelayAudio( {1}.0/1000.0){0}{0}", Environment.NewLine, (long)numericUpDownDelay.Value);
			}
			//#warning other tweaks here !!!

			if (cbxSplit.Checked)
			{
				sb.AppendFormat("{1}{0}# [BeHappy: Create fictive 1000fps video for triming]{0}{1}{0}AudioDubEx(BlankClip(length=Int(1000*AudioLengthF(last)/Audiorate(last)), width=32, height=32, pixel_type=\"RGB24\", fps=1000), last){0}{0}", Environment.NewLine, SEPARATOP);
				sb.AppendFormat("{0}{0}trim({1},{2}){0}{0}", Environment.NewLine, (long)numericUpDownSplitA.Value, (long)numericUpDownSplitB.Value);
				sb.AppendFormat("{0}{0}{1}{0}# [BeHappy: Kill video]{0}{1}{0}AudioDubEx(Tone(), last){0}{0}", Environment.NewLine, SEPARATOP);
			}

			if(cbxBuffer.Checked)
			{
				sb.Append(SEPARATOP);
				sb.AppendFormat("{0}# [BeHappy: Buffer Audio by {1} s ]{0}NicBufferAudio( last, {1}){0}{0}", Environment.NewLine, (long)numericUpDownBuffer.Value);
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

		private string[] sourceFiles
		{
			get {string[] items = new string[lstSourceFiles.Items.Count];
				for(int i = 0; i < items.Length; i++)
				{
					items[i] = lstSourceFiles.Items[i].ToString();
				}
				return items;
			}
		}
		
		private string sourceFileName
		{
			get {return lstSourceFiles.Items.Count > 0 ? lstSourceFiles.SelectedItem.ToString() : string.Empty;}
		}

		private string targetFileName
		{
			get {return txtOutputFileName.Text.Trim();}
			set {txtOutputFileName.Text = value.Trim();
				toolTip1.SetToolTip(txtOutputFileName, txtOutputFileName.Text);
			}
		}

		private void selectSourceFile(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string[] files;
			
			if (e.Button == MouseButtons.Left)
			{
				StringBuilder sb = new StringBuilder();
				
				foreach (FileRelatedExtensionItemBase enc in lstAudioSource.Items)
				{
					sb.AppendFormat("{0} ({1})|{1}|", enc.Title, enc.GetFilesMask());
				}
				sb.Append("All files (*.*)|*.*");
				openFileDialog1.Filter = sb.ToString();
				openFileDialog1.FilterIndex = lstAudioSource.SelectedIndex + 1;
				
				if (openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					if (openFileDialog1.FilterIndex <= lstAudioSource.Items.Count)
						lstAudioSource.SelectedIndex = openFileDialog1.FilterIndex - 1;
					
					files = openFileDialog1.FileNames;
				}
				else return;
			}
			else if (e.Button == MouseButtons.Right)
			{
				folderBrowserDialog1.ShowNewFolderButton = false;
				
				if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
				{
					files = Directory.GetFiles(folderBrowserDialog1.SelectedPath);
				}
				else return;
			}
			else
			{
				toolStripStatusLabel1.Text = "Left click = add files / right click = add folder.";
				return;
			}
			
			AddFiles(files);
			toolStripStatusLabel1.Text = string.Format("{0} source files added, {1} elements total.", files.Length, sourceFiles.Length);
		}
		
		void LstSourceFilesDragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				List<string> files = new List<string>((string[])e.Data.GetData(DataFormats.FileDrop));
				int flscount = 0;
				string[] fls;
				
				if (files.All(f => (File.GetAttributes(f) & FileAttributes.Directory) == FileAttributes.Directory))
				{
					foreach (string f in files)
					{
						try {fls = Directory.GetFiles(f);
						}
						catch (Exception ex) {
							toolStripStatusLabel1.Text = ex.Message;
							continue;
						}
						
						AddFiles(fls);
						flscount += fls.Length;
					}
					
					toolStripStatusLabel1.Text = string.Format("Added {0} files from {1} folders. {2} elements total.", flscount, files.Count, sourceFiles.Length);
				}
				else
				{
					fls = files.FindAll(f => (File.GetAttributes(f) & FileAttributes.Directory) != FileAttributes.Directory).ToArray();
					AddFiles(fls);
					
					toolStripStatusLabel1.Text = string.Format("{0} source files added, {1} elements total.", fls.Length, sourceFiles.Length);
				}
			}
		}

		void LstSourceFilesDragEnter(object sender, DragEventArgs e)
		{
			labelDragDrop.Visible = false;
			
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.All;
		}
		
		void AddFiles(string[] files)
		{
			labelDragDrop.Visible = false;
			lstSourceFiles.Items.AddRange(files);
			lstSourceFiles.SelectedItem = lstSourceFiles.Items[0];
			
			if (sourceFiles.Length == 1)
			{
				int cnt = 0;
				lstSourceFiles.DropDownStyle = ComboBoxStyle.DropDown;
				string target = Path.ChangeExtension(sourceFileName, currentEncoder.GetFirstExtension());
				while ((string.Compare(target, sourceFileName) == 0) | File.Exists(target))
				{
					target = Path.Combine(Path.GetDirectoryName(target), String.Format("{0}_{1:d3}{2}", Path.GetFileNameWithoutExtension(sourceFileName), ++cnt, Path.GetExtension(target)));
				}
				
				targetFileName = target;
			}
			else
			{
				lstSourceFiles.DropDownStyle = ComboBoxStyle.DropDownList;
				targetFileName = Path.GetDirectoryName(sourceFileName);
			}
		}
		
		private void selectTargetFile(object sender, System.EventArgs e)
		{
			if (sourceFiles.Length < 1)
			{
				toolStripStatusLabel1.Text = "No source file(s) selected.";
				return;
			}
			else if (sourceFiles.Length == 1)
			{
				StringBuilder sb = new StringBuilder();
				
				foreach(FileRelatedExtensionItemBase enc in lstEncoder.Items)
				{
					sb.AppendFormat("{0} ({1})|{1}|", enc.Title, enc.GetFilesMask());
				}
				sb.Append("All files (*.*)|*.*");
				saveFileDialog1.Filter = sb.ToString();
				saveFileDialog1.FilterIndex = lstEncoder.SelectedIndex + 1;
				saveFileDialog1.FileName = String.IsNullOrWhiteSpace(targetFileName) ? String.Empty : Path.GetFileName(targetFileName);
				
				if (saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					targetFileName = saveFileDialog1.FileName;
					if (saveFileDialog1.FilterIndex <= lstEncoder.Items.Count)
						lstEncoder.SelectedIndex = saveFileDialog1.FilterIndex - 1;
				}
			}
			else
			{
				folderBrowserDialog1.SelectedPath = targetFileName;
				folderBrowserDialog1.ShowNewFolderButton = true;
				
				if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
				{
					targetFileName = folderBrowserDialog1.SelectedPath;
				}
			}
		}
		
		void LinkLabelClearClick(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(lstSourceFiles, String.Empty);
			lstSourceFiles.Items.Clear();
			lstSourceFiles.DropDownStyle = ComboBoxStyle.DropDown;
			lstSourceFiles.Text = String.Empty;
			lstSourceFiles.DropDownWidth = lstSourceFiles.Width;
			toolStripStatusLabel1.Text = String.Empty;
			labelDragDrop.Visible = false;
		}

		void LstSourceFilesSelectedIndexChanged(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(lstSourceFiles, sourceFileName);
		}
		
		void LstSourceFilesDropDown(object sender, EventArgs e)
		{
			ComboBox scb = (ComboBox)sender;
			int width = scb.DropDownWidth;
			Graphics g = scb.CreateGraphics();
			int sbWidth = (scb.Items.Count > scb.MaxDropDownItems) ? SystemInformation.VerticalScrollBarWidth : 0;
			int newWidth;
			foreach (string s in (scb.Items))
			{
				newWidth = (int)g.MeasureString(s, scb.Font).Width + sbWidth;
				if (width < newWidth )
				{
					width = newWidth;
				}
			}
			scb.DropDownWidth = width;
			labelDragDrop.Visible = false;
		}


		
		#region JobList management

		private Job[] createJobs()
		{
			Job[] jbs = new Job[sourceFiles.Length];
			AudioEncoder enc = currentEncoder;
			int htype = cbxHeader.Checked ? (int)(numericUpDownHeader.Value) : 0;
			int cmask = cbxChMask.Checked ? (int)(numericUpDownChMask.Value) : -1;
			int cnt;
			string targetFile = targetFileName;
			string targetDir;
			                                      
			for (int i = 0; i < jbs.Length; i++)
			{
				jbs[i] = new Job();
				cnt = 0;
				
				if (jbs.Length > 1)
				{
					targetDir = targetFileName;
					targetFile = Path.Combine(targetDir, Path.ChangeExtension(Path.GetFileName(sourceFiles[i]), enc.GetFirstExtension()));
					
					while (File.Exists(targetFile))
					{
						targetFile = Path.Combine(targetDir, String.Format("{0}_{1:d3}.{2}", Path.GetFileNameWithoutExtension(sourceFiles[i]), ++cnt, enc.GetFirstExtension()));
					}
				}
				
				jbs[i].AviSynthScript = createAvsScript(sourceFiles[i], targetFile, enc);
				jbs[i].CommandLine = enc.GetExecutableArguments(Path.GetExtension(targetFile).ToLower());
				jbs[i].EncoderExecutable = encoder_dir + enc.ExecutableFileName;
				if(jbs[i].EncoderExecutable.Length == encoder_dir.Length)
					jbs[i].EncoderExecutable = null;
				jbs[i].SourceFile = sourceFiles[i];
				jbs[i].TargetFile = targetFile;
				jbs[i].SendRiffHeader = enc.WriteRiffHeader;
				jbs[i].HeaderType = htype;
				jbs[i].ChannelMask = cmask;
			}
			
			return jbs;
		}

		private void submitJobToJobControl(object sender, System.EventArgs e)
		{
			// make sure we have a source and a target
			if ((sourceFiles.Length < 1) || String.IsNullOrWhiteSpace(targetFileName))
			{
//				MessageBox.Show("You must select a source and a destination file before proceeding.",
//				                "Source/Destination required",
//				                MessageBoxButtons.OK, MessageBoxIcon.Information);
				
				toolStripStatusLabel1.Text = "You must select a source and a destination file before proceeding.";
				return;
			}
			
			Job[] jbs = createJobs();
			var items = createJobItems(jbs);

			// now that we have a multi-select list, let's deselect
			// all items prior to selecting the job that was just
			// submitted to the job control
			deselectAllJobs();

			jobListView.Items.AddRange(items);
			
			if (jobs != null)
			{
				foreach (var item in items)
				{
					jobs.Add(new Jobs(item));
				}
			}
			
			tabControl1.SelectedTab = tabPageJobControl;
		}

		private static void updateListViewItem(ListViewItem item)
		{
			Job job = item.Tag as Job;
			item.SubItems[1].Text = job.State.ToString();
			item.SubItems[2].Text = job.State < JobState.Processing ? string.Empty : job.StartAt.ToString();
			item.SubItems[3].Text = job.State < JobState.Error ? string.Empty : job.StopAt.ToString();
			item.SubItems[4].Text = job.Progress.ToString("D2") + " %";
			item.SubItems[5].Text = job.SourceFile;
			item.SubItems[6].Text = job.TargetFile;
		}

		private static ListViewItem[] createJobItems(Job[] jbs)
		{
			var items = new ListViewItem[jbs.Length];
			
			for (int i = 0; i < jbs.Length; i++)
			{
				items[i] = new ListViewItem(jbs[i].Name);
				items[i].Tag = jbs[i];
				for(int j = 0; j < 6; j++)
					items[i].SubItems.Add(string.Empty);
				updateListViewItem(items[i]);
			}
			
			return items;
		}

		private void toggleJobStatus(object sender, System.EventArgs e)
		{
			if(jobListView.SelectedItems.Count == 0)
				return;
			
			foreach (ListViewItem item in jobListView.SelectedItems)
			{
				if(((Job)item.Tag).State == JobState.Processing)
					continue;
				if(((Job)item.Tag).State != JobState.Waiting)
					((Job)item.Tag).State = JobState.Waiting;
				else
					((Job)item.Tag).State = JobState.Postponed;
				
				((Job)item.Tag).Progress = 0;
				updateListViewItem(item);
			}
		}

		private void moveUpJob(object sender, System.EventArgs e)
		{
			if(jobListView.SelectedItems.Count==0)
				return;
			ListViewItem item =     jobListView.SelectedItems[0];
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
			ListViewItem item =     jobListView.SelectedItems[0];
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
						if (jobs != null) jobs.RemoveAll(j => j.M_item == item);
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

		private void btnDeleteAll_Click(object sender, EventArgs e)
		{
			if (jobListView.Items.Count == 0)
				return;
			lock (lockObject)
			{
				int idxOfItemInProcess = 0;
				while (jobListView.Items.Count > 0)
				{
					ListViewItem item = jobListView.Items[idxOfItemInProcess];
					if (((Job)item.Tag).State == JobState.Postponed ||
					    ((Job)item.Tag).State == JobState.Processing ||
					    ((Job)item.Tag).State == JobState.Waiting)
					{
						idxOfItemInProcess++;
					}
					else
					{
						if (jobs != null) jobs.RemoveAll(j => j.M_item == item);
						jobListView.Items.Remove(item);
					}

					if (idxOfItemInProcess == jobListView.Items.Count)
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

			// 20080126 Chumbo mod
			SetDSPMoveButtonState();
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

			// 20080126 Chumbo mod
			SetDSPMoveButtonState();
		}

		private string getExeDirectory()
		{
			return  Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
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
			btnConfigureDSP.Enabled = b && a;
		}

		private void enableDelay(object sender, System.EventArgs e)
		{
			numericUpDownDelay.Enabled=(sender as CheckBox).Checked;
		}

		private void enableSplit(object sender, System.EventArgs e)
		{
			numericUpDownSplitA.Enabled=numericUpDownSplitB.Enabled=(sender as CheckBox).Checked;
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
			get {return this.GetType();}
		}

		private void exportAviSynthScriptToFile(object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(sourceFileName))
			{
				toolStripStatusLabel1.Text = "No source file selected!";
				return;
			}
			
			saveFileDialog1.DefaultExt = "avs";
			saveFileDialog1.Filter = "AviSynth script (*.avs)|*.avs";
			saveFileDialog1.FileName = Path.HasExtension(targetFileName) ? Path.ChangeExtension(targetFileName, ".avs") : Path.ChangeExtension(sourceFileName, ".avs");
			
			if(saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				using(TextWriter w = new StreamWriter(saveFileDialog1.FileName, false, Encoding.Default))
				{
					w.WriteLine(createAvsScript());
				}
			}
		}

		private void lstEncoder_SelectedIndexChanged(object sender, EventArgs e)
		{
			linkLabelEncoderConfig.Enabled = linkLabelEncoderReset.Enabled = currentEncoder.IsSupportConfiguration;
			
			if(sourceFiles.Length > 1 || targetFileName.Length == 0)
				return;
			if(!currentEncoder.IsSupportedException(Path.GetExtension(targetFileName)))
			{
				int cnt = 0;
				string tname = Path.GetFileNameWithoutExtension(targetFileName);
				targetFileName = Path.ChangeExtension(targetFileName, currentEncoder.GetFirstExtension());
				
				while (File.Exists(targetFileName))
				{
					targetFileName = Path.Combine(Path.GetDirectoryName(targetFileName), String.Format("{0}_{1:d3}.{2}", tname, ++cnt, currentEncoder.GetFirstExtension()));
				}
			}
		}
		
		void LstEncoderDrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index == -1) return;
			
			var cb = (ComboBox)sender;
			var brsh = new SolidBrush(e.ForeColor);
			
			if (!(string.IsNullOrEmpty(((Extensions.AudioEncoder)cb.Items[e.Index]).ExecutableFileName) || File.Exists(Path.Combine(Application.StartupPath, encoder_dir, ((Extensions.AudioEncoder)cb.Items[e.Index]).ExecutableFileName))))
			{
				brsh.Color = Color.Gray;
			}
			
			e.DrawBackground();
			e.Graphics.DrawString(cb.Items[e.Index].ToString(), e.Font, brsh, e.Bounds);
			e.DrawFocusRectangle();
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
			get	{return (lstAudioSource.SelectedItem as AudioSource);}
		}

		private AudioEncoder currentEncoder
		{
			get	{return (lstEncoder.SelectedItem as AudioEncoder);}
		}

		private void lstAudioSource_SelectedIndexChanged(object sender, EventArgs e)
		{
			linkLabelSourceConfig.Enabled = linkLabelSourceReset.Enabled = currentSource.IsSupportConfiguration;
		}

		private void lstDSP_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ExtensionItemBase item = lstDSP.SelectedItem as ExtensionItemBase;
			if (item != null)
				btnConfigureDSP.Enabled = item.IsSupportConfiguration;
			else
				btnConfigureDSP.Enabled = false;

			// 20080126 Chumbo mod
			SetDSPMoveButtonState();
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
			if (string.IsNullOrEmpty(sourceFileName))
			{
				toolStripStatusLabel1.Text = "No source file selected!";
				return;
			}
			
			using(TextWriter w = new StreamWriter(m_tempFileName, false, Encoding.Default))
			{
				w.WriteLine(createAvsScript(cbxOmitEncoderScript.Checked));
			}
			Process.Start(getPlayer(), m_tempFileName);
		}

		#region Job Encoding

		private bool bKeepOutput;
		private bool breakAfterCurrentJob;
		private List<Jobs> jobs;
		private int wjcount;
		private System.Windows.Forms.Timer timer;
		
		private void startJobs(object sender, System.EventArgs e)
		{
			wjcount = 0;
			jobs = new List<Jobs>();
			
			for (int i = 0; i < jobListView.Items.Count; i++)
			{
				jobs.Add(new Jobs(jobListView.Items[i]));
				if (jobs[i].M_job.State == JobState.Waiting) wjcount++;
			}
			
			if (wjcount == 0)
			{
				jobs = null;
				return;
			}
			
			toolStripProgressBar1.Maximum = wjcount + 1;
			toolStripProgressBar1.Value = 1;
			
			timer = new System.Windows.Forms.Timer();
			timer.Interval = 800;
			timer.Tick += tickEventHandler;
			
			breakAfterCurrentJob = false;
			btnAbort.Enabled = btnStop.Enabled = true;
			btnStart.Enabled = false;
			cboPriority.Enabled = true;
			toolStripStatusLabel1.Text = "Begin JobQueue processing...";
			
			cboPriority.SelectedIndex = 3;				//set default to Idle
			CurrentPriority = GetSelectedPriority();
			CurrentPriorityIndex = cboPriority.SelectedIndex;
			
			executeNextJob();
			timer.Start();
		}
		
		// timer is used to a) start jobs sequential (prevent avisynth crashes)
		//					b) update jobListView progress (reduce gui overhead)
		private void tickEventHandler(object sender, EventArgs e)
		{
			foreach (ListViewItem item in jobListView.Items)
			{
				item.SubItems[4].Text = ((Job)item.Tag).Progress.ToString("D2") + " %";
			}
			
			if (!breakAfterCurrentJob)
				executeNextJob();
		}

		private void executeNextJob()
		{
			// if number of running jobs is less than number of max allowed jobs
			if (jobs.FindAll(j => j.M_job.State == JobState.Processing).Count < (int)numericUpDownJobs.Value)
			{
				// if previous running job has reached at least progress of 1%
				if (jobs.FindLast(j => j.M_job.State == JobState.Processing) != null && jobs.FindLast(j => j.M_job.State == JobState.Processing).M_job.Progress < 1)
					return;
				
				foreach (Jobs jbs in jobs)
				{
					if (jbs.M_job.State != JobState.Waiting)
						continue;
					
					deselectAllJobs();
					jbs.M_item.Selected = true;
					txtSimpleLog.Text = String.Empty;
					jbs.M_job.State = JobState.Processing;
					jbs.M_job.StartAt = DateTime.Now;
					jbs.M_job.bKeepOutput = bKeepOutput;
					
					DataRow dr = ((DataRowView)cboPriority.SelectedItem).Row;
					jbs.M_job.iPriority = Convert.ToInt32(dr.ItemArray.GetValue(1));
					
					updateListViewItem(jbs.M_item);
					jbs.AppendToLog("Starting job " + jbs.M_job.Name);
					
					jbs.M_encoder = new Encoder(jbs.M_job);
					jbs.M_encoder.SetKeepOutput(jbs.M_job.bKeepOutput);
					jbs.M_encoder.SetPriority((ProcessPriorityClass)Convert.ToInt32(dr.ItemArray.GetValue(1)));
					jbs.M_encoder.EncoderCallback += encoderCallback;
					jbs.M_encoder.Start();
					return;
				}
			}
		}
		
		private void jobsFinished()
		{
			timer.Stop();
			btnStart.Enabled = true;
			btnAbort.Enabled = btnStop.Enabled = false;
			cboPriority.Enabled = false;
			
			jobs = null;
			if (breakAfterCurrentJob) toolStripStatusLabel1.Text = "Stopped by user request. Check job states/logs for errors.";
			else toolStripStatusLabel1.Text = "Finished jobqueue processing. Check job states/logs for errors.";
//			toolStripProgressBar1.Value = 0;
		}
		
		private void deselectAllJobs()
		{
			foreach (ListViewItem li in jobListView.Items)
				li.Selected = false;
		}

		private void encoderCallback(Job sender, EncoderCallbackEventArgs s)
		{
			if(!this.InvokeRequired)
			{
				int ji = jobs.FindIndex(j => j.M_job == sender);	//get the index of the sending job
				
				switch(s.Type)
				{
					case EncoderCallbackEventArgs.EventType.Progress:
//						this.toolStripProgressBar1.Value = (int)s.Progress;		//too much gui overhead when running multiple jobs parallel
						break;
					case EncoderCallbackEventArgs.EventType.Done:
						jobs[ji].M_job.State = JobState.Done;
						goto cmon;
					case EncoderCallbackEventArgs.EventType.Terminated:
						jobs[ji].M_job.State = JobState.Aborted;
						goto cmon;
					case EncoderCallbackEventArgs.EventType.Error:
						jobs[ji].M_job.State = JobState.Error;
						
					cmon:
						jobs[ji].M_job.StopAt = DateTime.Now;
						jobs[ji].AppendToLog(String.IsNullOrEmpty(s.Message) ? sender.State.ToString() : (sender.State.ToString() + ": " + s.Message));
						jobs[ji].AppendStdStreamsToLog();
						updateListViewItem(jobs[ji].M_item);
						toolStripProgressBar1.PerformStep();
						if (jobs[ji].M_item.Selected) txtSimpleLog.Text = jobs[ji].M_log;
						if (!jobs.Exists(j => j.M_job.State == JobState.Processing || (j.M_job.State == JobState.Waiting && breakAfterCurrentJob == false)))
							jobsFinished();
						break;
					case EncoderCallbackEventArgs.EventType.Notify:
						jobs[ji].AppendToLog(s.Message);
						if (jobs[ji].M_item.Selected) txtSimpleLog.Text = jobs[ji].M_log;
						break;
					case EncoderCallbackEventArgs.EventType.StdErr:
						jobs[ji].M_stderr.Append(s.Message);
						break;
					case EncoderCallbackEventArgs.EventType.StdOut:
						jobs[ji].M_stdout.Append(s.Message);
						break;
					case EncoderCallbackEventArgs.EventType.KeepOutput:
						break;
				}
			}
			else
			{
				this.Invoke(new EncoderStatusCallbackDelegate(encoderCallback), new object[]{sender, s});
			}
		}
		
		private void abortEncoding(object sender, System.EventArgs e)
		{
			breakAfterCurrentJob = true;
			timer.Stop();
			jobs.ForEach(j => {if (j.M_job.State == JobState.Processing) j.M_encoder.Abort();});
		}

		private void stopEncoding(object sender, System.EventArgs e)
		{
			toolStripStatusLabel1.Text = "Break after running job.";
			breakAfterCurrentJob = true;
			btnStop.Enabled=false;
		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (jobs == null) return;
			
			if (MessageBox.Show("Abort all running jobs and exit?", "Job processing not finished!",
			                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
			                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				if (jobs != null) abortEncoding(sender, e);
			}
			else e.Cancel = true;
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

		void LinkLabelAutoJobsClick(object sender, EventArgs e)
		{
			if (Environment.ProcessorCount == 2)
				numericUpDownJobs.Value = 2;
			else numericUpDownJobs.Value = Math.Round((Environment.ProcessorCount * 70) / 100m);
		}

		#endregion

		// 20080126 Chumbo mod
		private void SetDSPMoveButtonState()
		{
			// set move button states
			if (lstDSP.SelectedItems.Count == 0 ||
			    lstDSP.SelectedItems.Count > 1)
			{
				this.btnMoveUpDSP.Enabled = false;
				this.btnMoveDownDSP.Enabled = false;
			}
			else if (lstDSP.SelectedIndices.Contains(0))
			{
				this.btnMoveUpDSP.Enabled = false;
				this.btnMoveDownDSP.Enabled = lstDSP.Items.Count > 1;
			}
			else if (lstDSP.SelectedIndices.Contains(lstDSP.Items.Count - 1))
			{
				this.btnMoveUpDSP.Enabled = true;
				this.btnMoveDownDSP.Enabled = false;
			}
			else
			{
				this.btnMoveUpDSP.Enabled = true;
				this.btnMoveDownDSP.Enabled = true;
			}
		}

		private void SetJobMoveButtonState()
		{
			// set move button and toolStripItemsJoblist.Items states
			if (jobListView.SelectedItems.Count == 0 ||
			    jobListView.SelectedItems.Count > 1)
			{
				this.btnMoveUpJob.Enabled = toolStripItemsJoblist[3].Enabled = false;
				this.btnMoveDownJob.Enabled = toolStripItemsJoblist[4].Enabled = false;
			}
			else if (jobListView.SelectedItems[0].Index == 0)
			{
				this.btnMoveUpJob.Enabled = toolStripItemsJoblist[3].Enabled = false;
				this.btnMoveDownJob.Enabled = toolStripItemsJoblist[4].Enabled = jobListView.Items.Count > 1;
			}
			else if (jobListView.SelectedItems[0].Index == jobListView.Items.Count - 1)
			{
				this.btnMoveUpJob.Enabled = toolStripItemsJoblist[3].Enabled = true;
				this.btnMoveDownJob.Enabled = toolStripItemsJoblist[4].Enabled = false;
			}
			else
			{
				this.btnMoveUpJob.Enabled = toolStripItemsJoblist[3].Enabled = true;
				this.btnMoveDownJob.Enabled = toolStripItemsJoblist[4].Enabled = true;
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

		private void cbxHeader_CheckedChanged(object sender, EventArgs e)
		{
			numericUpDownHeader.Enabled = cbxHeader.Checked;
		}

		private void cbxChMask_CheckedChanged(object sender, EventArgs e)
		{
			numericUpDownChMask.Enabled = cbxChMask.Checked;
		}

		private void cbxBuffer_CheckedChanged(object sender, EventArgs e)
		{
			numericUpDownBuffer.Enabled = cbxBuffer.Checked;
		}

		private void chkKeepOutput_CheckedChanged(object sender, EventArgs e)
		{
			bKeepOutput = chkKeepOutput.Checked;
			
			if (jobs != null)
			{
				jobs.ForEach(j => {if (j.M_job.State == JobState.Processing) j.M_encoder.SetKeepOutput(bKeepOutput);});
			}
		}

		private void InitPriorityControls()
		{
			cboPriority.DataSource = CreatePriorityDataSource();
			cboPriority.DisplayMember = "DisplayItem";
			cboPriority.ValueMember   = "ValueItem";
			cboPriority.Enabled = false;
		}

		public ICollection CreatePriorityDataSource()
		{
			// Create a table to store data for the DropDownList control.
			DataTable dt = new DataTable();
			
			// Define the columns of the table.
			dt.Columns.Add(new DataColumn("DisplayItem", typeof(String)));
			dt.Columns.Add(new DataColumn("ValueItem", typeof(String)));
			
			dt.Rows.Add(CreateDataRow(ResourceGlobal.ItemPriorityAboveNormal,
			                          ((int)ProcessPriorityClass.AboveNormal).ToString(), dt));
			dt.Rows.Add(CreateDataRow(ResourceGlobal.ItemPriorityBelowNormal,
			                          ((int)ProcessPriorityClass.BelowNormal).ToString(), dt));
			dt.Rows.Add(CreateDataRow(ResourceGlobal.ItemPriorityHigh,
			                          ((int)ProcessPriorityClass.High).ToString(), dt));
			dt.Rows.Add(CreateDataRow(ResourceGlobal.ItemPriorityIdle,
			                          ((int)ProcessPriorityClass.Idle).ToString(), dt));
			dt.Rows.Add(CreateDataRow(ResourceGlobal.ItemPriorityNormal,
			                          ((int)ProcessPriorityClass.Normal).ToString(), dt));
			
			/*
			 * Create a DataView from the DataTable to act as the data source
			 * for the DropDownList control.
			 */
			DataView dv = new DataView(dt);
			return dv;
		}

		public DataRow CreateDataRow(string strDisplayItem, string strValueItem, DataTable dt)
		{
			/*
			 * Create a DataRow using the DataTable defined in the
			 * CreatePriorityDataSource method.
			 */
			DataRow dr = dt.NewRow();
			
			dr[0] = strDisplayItem;
			dr[1] = strValueItem;

			return dr;
		}

		/// <summary>
		/// Priority change list event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cboPriority_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (jobs != null)
			{
				ProcessPriorityClass enumItem = GetSelectedPriority();

				// if the priority selected is High, let's make sure the
				// user wants to do this as it can tie up the system so
				// verify their choice or at least make them consider the
				// action to take
				bool bSetPriority = true;
				if (enumItem == ProcessPriorityClass.High)
				{
					bSetPriority = (System.Windows.Forms.DialogResult.Yes == MessageBox.Show(ResourceGlobal.MsgHighPriorityWarning,
					                                                                         ResourceGlobal.CaptionHighPriorityWarning,
					                                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Warning));
				}

				if (bSetPriority)
				{
					try {
						jobs.ForEach(j => {if (j.M_job.State == JobState.Processing) j.M_encoder.SetPriority(enumItem);});
					} catch (Exception ex) {
						toolStripStatusLabel1.Text = ex.Message;}
					
					CurrentPriority = enumItem;
					CurrentPriorityIndex = cboPriority.SelectedIndex;
				}
				else
				{
					// set the selection back to what it was
					cboPriority.SelectedIndex = CurrentPriorityIndex;
				}
			}
		}

		protected ProcessPriorityClass GetSelectedPriority()
		{
			// do this safely and return Idle by default if a crash occurs
			try
			{
				DataRowView drv = (DataRowView)cboPriority.SelectedItem;

				DataRow dr = drv.Row;
				return (ProcessPriorityClass)Convert.ToInt32(dr["ValueItem"].ToString());
			}
			catch (Exception) { }

			return ProcessPriorityClass.Idle;
		}
		
		void MainFormSizeChanged(object sender, EventArgs e)
		{
			//adjust jobListView columns on resizing mainForm
			columnHeader1.Width = columnHeader5.Width = columnHeader6.Width = (int)((this.Width - (columnHeader2.Width + columnHeader3.Width + columnHeader4.Width + columnHeader7.Width)) * 33) /100 - 16;
		}
		
		void ContextMenuStrip1Opening(object sender, CancelEventArgs e)
		{
			e.Cancel = jobListView.SelectedItems.Count < 1;
		}
		
		
	}


	internal class Jobs
	{
		public Encoder M_encoder {get; set;}
		public bool M_breakAfterCurrentJob {get; set;}
		public StringBuilder M_stdout {get; set;}
		public StringBuilder M_stderr {get; set;}
		private StringBuilder m_log;
		public string M_log {get {return M_job.Log;}}
		public Job M_job {get; set;}
		private ListViewItem m_item;
		public ListViewItem M_item {get {return m_item;} set {m_item = value; M_job = (Job)value.Tag;}}
		private Regex m_cleanUpStdOutRegex = new Regex(@"\n[^\n]+\r", RegexOptions.Compiled | RegexOptions.CultureInvariant);
		
		public Jobs()
		{
			m_log = new StringBuilder();
			M_stderr = new StringBuilder();
			M_stdout = new StringBuilder();
		}
		
		public Jobs(ListViewItem item) :this()
		{
			M_item = item;
		}
		
		public void AppendToLog(string s)
		{
			m_log.AppendLine(s);
			M_job.Log = m_log.ToString();
		}
		
		public void AppendStdStreamsToLog()
		{
			if (M_stdout.Length != 0)
			{
				m_log.AppendLine("#### Encoder StdOut ####");
				m_log.AppendLine(m_cleanUpStdOutRegex.Replace(M_stdout.ToString().Replace(Environment.NewLine, "\n"), Environment.NewLine));
			}
			if (M_stderr.Length != 0)
			{
				m_log.AppendLine("#### Encoder StdErr ####");
				m_log.AppendLine(m_cleanUpStdOutRegex.Replace(M_stderr.ToString().Replace(Environment.NewLine, "\n"), Environment.NewLine));
			}
			
			M_job.Log = m_log.ToString();
		}
		
	}

}
