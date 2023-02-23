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
using System.Data;
using BeHappy.src;

namespace BeHappy
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class MainForm : Form
	{
		private string encoder_dir;
		private string ds_player = "mplayer2";
		private ProcessPriorityClass m_enumCurrentPriority;
		private int m_iCurrentPriorityIndex;
		private List<ExtensionItemBase> audioSources, dspProcessors, audioEncoders;

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
		
		private ContextMenuStrip contextMenu1, contextMenu2;
		private ToolStripItem[] toolStripItemsJoblist;
		private List<ToolStripItem> toolStripItemsLstAudioSource, toolStripItemsLstAudioSourceFilters1, toolStripItemsLstAudioSourceFilters2;
		private List<ToolStripItem> toolStripItemsLstEncoder, toolStripItemsLstEncoderFilters1, toolStripItemsLstEncoderFilters2;
		private ToolStripMenuItem itemResetAudioSource, itemResetEncoder, itemFilterText, itemFilterLossy, itemFilterLossless, filterItemsPageTwo;
		private ToolStripItem[] toolStripItemsGroupBoxSource, toolStripItemsGroupBoxDest;
		private MessageWindow msgWindow;
		
		private const string groupBoxSourceText = "[1] &Source";
		private int getSourceFilesCount {
			get {return sourceFiles.Length;}
		}
		private Action setGroupBoxSource_Header;
		
		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			bKeepOutput = false;
			m_iCurrentPriorityIndex = 0;
			m_enumCurrentPriority = ProcessPriorityClass.Idle;

			setGroupBoxSource_Header = () => {
				groupBoxSource.Text = getSourceFilesCount < 1 ? groupBoxSourceText :
					String.Format("{0}  ({1} files loaded)", groupBoxSourceText, getSourceFilesCount);
			};
			
			using (TextReader r = new StreamReader(this.GetType().Assembly.GetManifestResourceStream("BeHappy.LICENSE.txt")))
			{
				txtGPL.Text = r.ReadToEnd();
			}

			using (Stream ricon = this.GetType().Assembly.GetManifestResourceStream("BeHappy.App.ico"))
			{
				this.Icon = new Icon(ricon);
			}

			numericUpDownDelay.Minimum = numericUpDownSplitA.Minimum = numericUpDownSplitB.Minimum = decimal.MinValue+16;
			numericUpDownDelay.Maximum = numericUpDownSplitA.Maximum = numericUpDownSplitB.Maximum = decimal.MaxValue-16;

			this.Text = string.Format("{0} v{1}", Application.ProductName, Application.ProductVersion);
			
			if (Directory.Exists(getExeDirectory() + "\\encoder"))
				encoder_dir = "encoder\\";
			else
				encoder_dir = "";

			contextMenu1 = new ContextMenuStrip();
			contextMenu2 = new ContextMenuStrip();
			contextMenu1.RenderMode = contextMenu2.RenderMode = ToolStripRenderMode.Professional;
			contextMenu1.ShowImageMargin = contextMenu2.ShowImageMargin = false;
			contextMenu1.Opening += ContextMenuStrip1Opening;
			contextMenu1.Opened += ContextMenuStrip1Opened;
			contextMenu2.Opening += ContextMenuGroupBoxOpening;
			lstAudioSource.ContextMenuStrip = contextMenu1;
			lstEncoder.ContextMenuStrip = contextMenu1;
			jobListView.ContextMenuStrip = contextMenu1;
			groupBoxSource.AddContextMenuStrip(contextMenu2);
			groupBoxDestination.AddContextMenuStrip(contextMenu2);
			
			BuildContextMenus();
			loadExtensionsAndApplyConfiguration();
			
			linkLabelSourceConfig.Enabled = itemResetAudioSource.Enabled = currentSource.IsSupportConfiguration;
			linkLabelEncoderConfig.Enabled = itemResetEncoder.Enabled = currentEncoder.IsSupportConfiguration;
			
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
			
		}
		
		private void loadExtensionsAndApplyConfiguration()
		{
			//1) let's load all extensions
			audioSources = new List<ExtensionItemBase>();
			dspProcessors = new List<ExtensionItemBase>();
			audioEncoders = new List<ExtensionItemBase>();
			Extension extension = Extension.Default;
			string ExtensionDirectory = getExeDirectory() + "\\extensions";
			if (!Directory.Exists(ExtensionDirectory))
				ExtensionDirectory = getExeDirectory();

			fillFromExtension(extension, audioEncoders, dspProcessors, audioSources);
			
			foreach(string file in Directory.GetFiles(ExtensionDirectory, "*.ext"))
			{
				__retry:
					try	{
					extension = Extension.LoadFromFile(file);
					fillFromExtension(extension, audioEncoders, dspProcessors, audioSources);
				}
				catch (Exception e) {
					switch(MessageBox.Show("Can't load extensions from file " + file + Environment.NewLine + e.ToString(), "Can't load extension", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error ))
					{
						case DialogResult.Abort:
							throw e;
						case DialogResult.Retry:
							goto __retry;
						case DialogResult.Ignore:
							break;
					}
				}
			}
			
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

			var tempDSP = new Dictionary<Guid, ExtensionItemBase>();
			
			foreach (ExtensionItemBase item in dspProcessors)
			{
				try {
					tempDSP.Add(item.UniqueID, item);
				} catch(Exception ex) {
					if (MessageBox.Show(String.Format("{0}{3}{3}Item: {2}{3}UID: {1}{3}", ex.Message, item.UniqueID, item, Environment.NewLine),
					                    "Exception:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK)
						continue;
				}
			}
			
			Guid[] dspOrder = c.DspOrder != null ? c.DspOrder : new Guid[0];
			
			foreach (Guid g in dspOrder)
			{
				if (tempDSP.ContainsKey(g))
				{
					lstDSP.Items.Add(tempDSP[g]);
					tempDSP.Remove(g);
				}
			}
			dspProcessors.Clear();
			dspProcessors.AddRange(tempDSP.Values);
			lstDSP.Items.AddRange(dspProcessors.ToArray());
			if (c.JobList != null)
			{
				if (c.JobList.Jobs != null)
				{
					jobListView.Items.AddRange(createJobItems(c.JobList.Jobs));
				}
			}
			foreach (ExtensionItemBase e in lstEncoder.Items)
				if (e.UniqueID == c.CurrentEncoder)
			{
				lstEncoder.SelectedItem = e;
				break;
			}
			if (currentEncoder == null)
				lstEncoder.SelectedIndex = 0;
			lstAudioSource.SelectedIndex = 0;

			SetJobMoveButtonState();
			AddContextMenuFilters(audioSources, out toolStripItemsLstAudioSourceFilters1, out toolStripItemsLstAudioSourceFilters2);
			AddContextMenuFilters(audioEncoders, out toolStripItemsLstEncoderFilters1, out toolStripItemsLstEncoderFilters2);
			
		}

		private void AddContextMenuFilters(List<ExtensionItemBase> source, out List<ToolStripItem> target1, out List<ToolStripItem> target2)
		{
			List<string> filters = new List<string>();
			
			foreach (FileRelatedExtensionItemBase item in source)
			{
				filters.AddRange(item.ListOfSupportedFileExtensions);
			}
			filters = new HashSet<string>(filters).ToList();	//removes duplicate items
			filters.Sort();
			
			target1 = new List<ToolStripItem>();
			target2 = new List<ToolStripItem>();
			
			if (filters.Count > 22)
			{
				for (int i = 0; i < filters.Count; i++)
				{
					if (i < (filters.Count / 2) /* +2 */ )
						target1.Add(new ToolStripMenuItem(filters[i], null, FilterItemsClick));
					else target2.Add(new ToolStripMenuItem(filters[i], null, FilterItemsClick));
				}
			}
			else
			{
				foreach (string s in filters)
				{
					target1.Add(new ToolStripMenuItem(s, null, FilterItemsClick));
				}
			}
			
			((ToolStripMenuItem)target1.Find(t => t.Text == "*")).Checked = true;
		}
		
		private void FilterItemsClick(object sender, EventArgs e)
		{
			var tsi = (ToolStripMenuItem)sender;
			List<ToolStripItem> allItems = new List<ToolStripItem>();
			List<ExtensionItemBase> newItems;
			List<ExtensionItemBase> comboItems;
			ComboBox combo;
			
			if (tsi.Owner is ContextMenuStrip)
				combo = (ComboBox)((ContextMenuStrip)(tsi.Owner)).SourceControl;
			else if (tsi.Owner is ToolStripDropDownMenu)
				combo = (ComboBox)((ToolStripDropDownItem)(((ToolStripDropDownMenu)(tsi.Owner)).OwnerItem)).Tag;
			else {
				toolStripStatusLabel1.Text = "Report: " + "Unexpected owner of clicked ToolStripMenuItem.";
				return;
			}
			
			if (combo == lstAudioSource) {
				allItems.AddRange(toolStripItemsLstAudioSourceFilters1);
				allItems.AddRange(toolStripItemsLstAudioSourceFilters2);
				comboItems = audioSources;
			}
			else if (combo == lstEncoder) {
				allItems.AddRange(toolStripItemsLstEncoderFilters1);
				allItems.AddRange(toolStripItemsLstEncoderFilters2);
				comboItems = audioEncoders;
			}
			else {
				toolStripStatusLabel1.Text = "Report: " + "Unexpected source control.";
				return;
			}
			
			// uncheck all related items
			for (int i = allItems.FindIndex(t => t.Text == "*"); i < allItems.Count; i++)
				{
					(allItems[i] as ToolStripMenuItem).Checked = false;
				}
			
				itemFilterLossy.Checked = itemFilterLossless.Checked = false;
				tsi.Checked = true;
			
			if (tsi.Text == "*")
			{
				newItems = comboItems;
			}
			else if (tsi == itemFilterLossless)
			{
				newItems = new List<ExtensionItemBase>();
				
				foreach (AudioEncoder item in comboItems)
				{
					if (item.IsLossless)
						newItems.Add(item);
				}
			}
			else if (tsi == itemFilterLossy)
			{
				newItems = new List<ExtensionItemBase>();
				
				foreach (AudioEncoder item in comboItems)
				{
					if (!item.IsLossless)
						newItems.Add(item);
				}
			}
			else
			{
				newItems = new List<ExtensionItemBase>();
				
				foreach (FileRelatedExtensionItemBase item in comboItems)
				{
					if (item.ListOfSupportedFileExtensions.Contains(tsi.Text))
						newItems.Add(item);
				}
			}
			
			combo.Items.Clear();
			combo.Items.AddRange(newItems.ToArray());
			
			if (combo.Items.Count > 0)
				combo.SelectedItem = combo.Items[0];
		}
		
		/// <summary>
		/// Call this before 'loadExtensionsAndApplyConfiguration()'
		/// </summary>
		private void BuildContextMenus()
		{
			itemFilterText = new ToolStripMenuItem("--- Plugin Filter ---"){TextAlign = ContentAlignment.MiddleCenter};
			itemFilterText.Enabled = false;
			itemFilterLossy = new ToolStripMenuItem("Lossy", null, FilterItemsClick);
			itemFilterLossless = new ToolStripMenuItem("Lossless", null, FilterItemsClick);
			itemResetAudioSource = new ToolStripMenuItem("Reset Configuration", null, resetAudioSource);
			itemResetEncoder = new ToolStripMenuItem("Reset Configuration", null, resetEncoder);
			toolStripItemsJoblist = new ToolStripItem[]{new ToolStripMenuItem("Toggle Status", null, toggleJobStatus),
				new ToolStripMenuItem("Remove", null, deleteJob),
				new ToolStripSeparator(),
				new ToolStripMenuItem("Up", null, moveUpJob),
				new ToolStripMenuItem("Down", null, moveDownJob)};

			toolStripItemsLstAudioSource = new List<ToolStripItem>{itemResetAudioSource,
				new ToolStripSeparator(),
				itemFilterText};
			
			toolStripItemsLstEncoder = new List<ToolStripItem>{itemResetEncoder,
				new ToolStripSeparator(),
				itemFilterText,
				itemFilterLossy,
				itemFilterLossless};
			
			toolStripItemsGroupBoxSource = new ToolStripItem[]{new ToolStripMenuItem("Add Files...", null, (s, e) => OpenFiles(false)),
				new ToolStripMenuItem("Add Folder...", null, (s, e) => OpenFiles(true)),
				itemResetAudioSource};
			 
			toolStripItemsGroupBoxDest = new ToolStripItem[]{new ToolStripMenuItem("Save...", null ,(s, e) => selectTargetFile(s, e as LinkLabelLinkClickedEventArgs)),
				itemResetEncoder};
		}
				
		private void ContextMenuGroupBoxOpening(object sender, CancelEventArgs e)
		{
			ContextMenuStrip cm = (ContextMenuStrip)sender;
			Control cl = (cm.SourceControl as LinkLabel).Parent as GroupBoxLinkLabel;
			cm.Items.Clear();
			filterItemsPageTwo = null;
			ToolStripMenuItem tempItem;
			
			if (cl == null)
			{
				e.Cancel = true;
				return;
			}
			else if (cl == groupBoxSource)
			{
				e.Cancel = false;
				cm.ShowCheckMargin = true;
				cm.Items.AddRange(toolStripItemsGroupBoxSource.ToArray());
				
				if (toolStripItemsLstAudioSourceFilters2.Count > 0)
				{
					filterItemsPageTwo = new ToolStripMenuItem(". . .", null, toolStripItemsLstAudioSourceFilters2.ToArray());
					filterItemsPageTwo.Tag = lstAudioSource;
					tempItem = new ToolStripMenuItem("Source Plugin Filter", null, toolStripItemsLstAudioSourceFilters1.ToArray());
					tempItem.DropDownItems.Insert(1, filterItemsPageTwo);
					tempItem.Tag = lstAudioSource;
					tempItem.DropDownOpened += (s, a) => filterItemsPageTwo.ShowDropDown();
					cm.Items.Add(tempItem);
					
				}
				else
				{
					cm.Items.Add(new ToolStripMenuItem("Source Plugin Filter", null, toolStripItemsLstAudioSourceFilters1.ToArray()));
				}
			}
			else if (cl == groupBoxDestination)
			{
				e.Cancel = false;
				cm.ShowCheckMargin = true;
				cm.Items.AddRange(toolStripItemsGroupBoxDest.ToArray());
				
				if (toolStripItemsLstEncoderFilters2.Count > 0)
				{
					filterItemsPageTwo = new ToolStripMenuItem(". . .", null, toolStripItemsLstEncoderFilters2.ToArray());
					filterItemsPageTwo.Tag = lstEncoder;
					tempItem = new ToolStripMenuItem("Encoder Plugin Filter", null, toolStripItemsLstEncoderFilters1.ToArray());
					tempItem.DropDownItems.Insert(0, itemFilterLossless);
					tempItem.DropDownItems.Insert(0, itemFilterLossy);
					tempItem.DropDownItems.Insert(3, filterItemsPageTwo);
					tempItem.Tag = lstEncoder;
					tempItem.DropDownOpened += (s, a) => filterItemsPageTwo.ShowDropDown();
					cm.Items.Add(tempItem);
				}
				else
				{
					cm.Items.Add(new ToolStripMenuItem("Encoder Plugin Filter", null, toolStripItemsLstEncoderFilters1.ToArray()));
				}
			}
			else
			{
				e.Cancel = true;
			}
			
		}
		
		private void loadMiscConfiguration(Configuration c)
		{
			if (c.MiscSettings != null)
			{
				this.ds_player = c.MiscSettings.directShowPlayer;
//			else
//				this.ds_player = "mplayer";
		
				this.btnPreview.Checked = c.MiscSettings.omitEncoderScriptChecked;
				this.btnEnqueue.Checked = c.MiscSettings.startJobsInstantlyChecked;
			}
		}

		private void loadGuiPositionConfiguration(Configuration c)
		{
			if (c.GuiPosition == null)
				return;

			this.Top = c.GuiPosition.iTop;
			this.Left = c.GuiPosition.iLeft;
			this.Width = c.GuiPosition.iWidth;
			this.Height = c.GuiPosition.iHeight;
			this.splitContainer1.SplitterDistance = c.GuiPosition.iSplitterDistance;
		}

		private static void loadPluginsConfiguration(List<ExtensionItemBase> plugins, IDictionary c)
		{
			foreach (ExtensionItemBase item in plugins)
			{
				if (item.IsSupportConfiguration)
					if (c.Contains(item.UniqueID))
				{
					XmlElement conf = c[item.UniqueID] as XmlElement;
					if (conf != null)
						item.LoadConfiguration(conf);
				}
			}
		}

		private static void fillFromExtension(Extension extension, List<ExtensionItemBase> audioEncoders, List<ExtensionItemBase> dspProcessors, List<ExtensionItemBase> audioSources)
		{
			if (extension.AudioEncoders != null)
				audioEncoders.AddRange(extension.AudioEncoders);
			if (extension.DigitalSignalProcessors != null)
				dspProcessors.AddRange(extension.DigitalSignalProcessors);
			if (extension.AudioSources != null)
				audioSources.AddRange(extension.AudioSources);
		}

		// added exception handling to make sure items already added to collections do not crash the application
		private void saveConfiguration()
		{
			try {
				Configuration c = new Configuration();
				ArrayList col = new ArrayList();

				try {col.AddRange(lstEncoder.Items);
				}
				catch (Exception ex) {
					MessageBox.Show("The following exception occurred while adding the encoder items:\r\n" +
					                ex.Message, "Save Configuration Exception");
				}

				try {col.AddRange(lstAudioSource.Items);
				}
				catch (Exception ex) {
					MessageBox.Show("The following exception occurred while adding the audio source items:\r\n" +
					                ex.Message, "Save Configuration Exception");
				}

				try {col.AddRange(lstDSP.Items);
				}
				catch (Exception ex) {
					MessageBox.Show("The following exception occurred while adding the DSP items:\r\n" +
					                ex.Message, "Save Configuration Exception");
				}

				foreach (ExtensionItemBase item in col)
				{
					if (item.IsSupportConfiguration)
					{
						XmlElement e = item.SaveConfiguration();
						if (e != null)
						{
							if (!c.Settings.Contains(item.UniqueID))
							{
								c.Settings.Add(item.UniqueID, e);
							}
						}
					}
				}

				col.Clear();
				foreach (ListViewItem item in jobListView.Items)
				{
					if (!col.Contains(item.Tag))
					{
						col.Add(item.Tag);
					}
				}

				c.JobList = new JobList();
				c.JobList.Jobs = (Job[])col.ToArray(typeof(Job));
				col.Clear();

				foreach (ExtensionItemBase item in lstDSP.Items)
				{
					if (!col.Contains(item.UniqueID))
					{
						col.Add(item.UniqueID);
					}
				}

				c.DspOrder = (Guid[])col.ToArray(typeof(Guid));
				c.CurrentEncoder = currentEncoder.UniqueID;

				c.GuiPosition = new GuiPosition();
				c.GuiPosition.iTop = this.Top;
				c.GuiPosition.iLeft = this.Left;
				c.GuiPosition.iWidth = this.Width;
				c.GuiPosition.iHeight = this.Height;
				c.GuiPosition.iSplitterDistance = this.splitContainer1.SplitterDistance;
				c.MiscSettings = new MiscSettings();
				c.MiscSettings.directShowPlayer = this.ds_player;
				c.MiscSettings.omitEncoderScriptChecked = this.btnPreview.Checked;
				c.MiscSettings.startJobsInstantlyChecked = this.btnEnqueue.Checked;
				c.SaveToFile(getConfigFileName());
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message, "Save Configuration Exception");
			}
		}

		private string getConfigFileName()
		{
			return Path.Combine(getExeDirectory(), "BeHappy.State");
		}


		#region AviSynth script generation

		private string createAvsScript(string sourceFileName, string targetFileName, AudioEncoder enc)
		{
			return createAvsScript(sourceFileName, targetFileName, enc, false);
		}

		private string createAvsScript(string sourceFileName, string targetFileName, AudioEncoder enc, bool omitEncoderScript)
		{
			string SEPARATOP = new string('#',40);
			StringBuilder sb2 = new StringBuilder();
			StringBuilder sb1 = new StringBuilder();
			string pluginDir = Path.Combine(getExeDirectory(), "plugins32");
			sb1.AppendFormat("{0}{1}#Created by {2} v{3}{1}#Creation timestamp: {4}{1}{0}{1}#Source FileName:{5}{1}#Target FileName:{6}{1}{0}{1}" , SEPARATOP, Environment.NewLine, Application.ProductName, Application.ProductVersion, DateTime.Now, sourceFileName, targetFileName);
			AudioSource source = lstAudioSource.SelectedItem as AudioSource;
			if (!string.IsNullOrEmpty(source.AvsPlugin))
			{
				string pluginPath = Path.Combine(pluginDir, source.AvsPlugin);
				sb1.AppendLine("#Source Plugin request: " + source.AvsPlugin);
				if (File.Exists(pluginPath))
					sb1.AppendFormat("LoadPlugin(\"{0}\"){1}", pluginPath, Environment.NewLine);
				else sb1.AppendFormat("#\"{0}\" not found in \"{1}\"{2}", source.AvsPlugin, pluginDir, Environment.NewLine);
			}
			sb2.AppendFormat("{0}{1}# [Source: {2}]{1}{0}{1}", SEPARATOP, Environment.NewLine, source.Title);
			// A part of AviSynth script
			// {0} means input file name
			// {1} means output file name
			// {2} means unique string (to use as part of identifier)
			// {3} means '{' character (to allow '{' to be used)
			// {4} means '}' character (to allow '}' to be used)
			sb2.AppendFormat(source.ScriptBlock, sourceFileName, targetFileName, Guid.NewGuid().ToString("N"), "{", "}");
			sb2.Append(Environment.NewLine + Environment.NewLine);
			if(cbxEnsureMP3VBRSync.Checked)
			{
				sb2.Append("EnsureVBRMP3Sync() # Some black magic to avoid desync" + Environment.NewLine + Environment.NewLine);
			}
			if(cbxDelay.Checked)
			{
				sb2.Append(SEPARATOP);
				sb2.AppendFormat("{0}# [BeHappy: Delay Audio by {1} ms ]{0}DelayAudio( {1}.0/1000.0){0}{0}", Environment.NewLine, (long)numericUpDownDelay.Value);
			}
			//#warning other tweaks here !!!

			if (cbxSplit.Checked)
			{
				sb2.AppendFormat("{1}{0}# [BeHappy: Create fictive 1000fps video for triming]{0}{1}{0}AudioDubEx(BlankClip(length=Int(1000*AudioLengthF(last)/Audiorate(last)), width=32, height=32, pixel_type=\"RGB24\", fps=1000), last){0}{0}", Environment.NewLine, SEPARATOP);
				sb2.AppendFormat("{0}{0}trim({1},{2}){0}{0}", Environment.NewLine, (long)numericUpDownSplitA.Value, (long)numericUpDownSplitB.Value);
				sb2.AppendFormat("{0}{0}{1}{0}# [BeHappy: Kill video]{0}{1}{0}AudioDubEx(Tone(), last){0}{0}", Environment.NewLine, SEPARATOP);
			}

			if(cbxBuffer.Checked)
			{
				sb2.Append(SEPARATOP);
				sb2.AppendFormat("{0}# [BeHappy: Buffer Audio by {1} s ]{0}NicBufferAudio( last, {1}){0}{0}", Environment.NewLine, (long)numericUpDownBuffer.Value);
			}

			if(!cbxDisableDSP.Checked)
			{
				foreach(DigitalSignalProcessor dsp in lstDSP.Items)
				{
					if(lstDSP.CheckedItems.Contains(dsp))
					{
						if (!string.IsNullOrEmpty(dsp.AvsPlugin))
						{
							string pluginPath = Path.Combine(pluginDir, dsp.AvsPlugin);
							sb1.AppendLine("#DSP Plugin request: " + dsp.AvsPlugin);
							if (File.Exists(pluginPath))
								sb1.AppendFormat("LoadPlugin(\"{0}\"){1}", pluginPath, Environment.NewLine);
							else sb1.AppendFormat("#\"{0}\" not found in \"{1}\"{2}", dsp.AvsPlugin, pluginDir, Environment.NewLine);
						}
						
						sb2.AppendFormat("{0}{1}# [DSP: {2}]{1}{0}{1}", SEPARATOP, Environment.NewLine, dsp.Title);
						// A part of AviSynth script
						// {0} means input file name
						// {1} means output file name
						// {2} means unique string (to use as part of identifier)
						// {3} means '{' character (to allow '{' to be used)
						// {4} means '}' character (to allow '}' to be used)
						sb2.AppendFormat(dsp.ScriptBlock, sourceFileName, targetFileName, Guid.NewGuid().ToString("N"), "{", "}");
						sb2.Append(Environment.NewLine + Environment.NewLine);
					}
				}
			}
			sb2.AppendFormat("{0}{1}# [Encoder: {2}]{1}# [Command: {3} {4}{1}{0}{1}", SEPARATOP, Environment.NewLine, enc.Title,
			                enc.ExecutableFileName, enc.GetExecutableArguments(Path.GetExtension(targetFileName).ToLower()));
			
			if (!String.IsNullOrEmpty(enc.ScriptBlock) && !omitEncoderScript)
			{
				if (!string.IsNullOrEmpty(enc.AvsPlugin))
				{
					string pluginPath = Path.Combine(pluginDir, enc.AvsPlugin);
					sb1.AppendLine("#Encoder Plugin request: " + enc.AvsPlugin);
					if (File.Exists(pluginPath))
						sb1.AppendFormat("LoadPlugin(\"{0}\"){1}", pluginPath, Environment.NewLine);
					else sb1.AppendFormat("#\"{0}\" not found in \"{1}\"{2}", enc.AvsPlugin, pluginDir, Environment.NewLine);
				}
				
				// A part of AviSynth script
				// {0} means input file name
				// {1} means output file name
				// {2} means unique string (to use as part of identifier)
				// {3} means '{' character (to allow '{' to be used)
				// {4} means '}' character (to allow '}' to be used)
				sb2.AppendLine("# Encoder script:");
				sb2.AppendFormat(enc.ScriptBlock, sourceFileName, targetFileName, Guid.NewGuid().ToString("N"), "{", "}");
			}
			sb1.AppendLine();
			sb1.Append(sb2.ToString());
			return sb1.ToString();
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

		private void OpenFiles(bool openFolder)
		{
			string[] files;
			
			if (openFolder)
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
				var sb = new StringBuilder();
				
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
						
			AddFiles(files);
			toolStripStatusLabel1.Text = string.Format("{0} source files added.", files.Length);
			setGroupBoxSource_Header();
		}
		
		private void LinkLabelOpenLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				OpenFiles(false);
			}
			else if (e.Button == MouseButtons.Right)
			{
				OpenFiles(true);
			}
			else
			{
				toolStripStatusLabel1.Text = "Left click = add files / right click = add folder.";
				return;
			}
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
					
					toolStripStatusLabel1.Text = string.Format("Added {0} files from {1} folders.", flscount, files.Count);
				}
				else
				{
					fls = files.FindAll(f => (File.GetAttributes(f) & FileAttributes.Directory) != FileAttributes.Directory).ToArray();
					AddFiles(fls);
					
					toolStripStatusLabel1.Text = string.Format("{0} source files added.", fls.Length);
				}
				
				setGroupBoxSource_Header();
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
			
			lstAudioSource.Refresh();
		}
		
		private void selectTargetFile(object sender, LinkLabelLinkClickedEventArgs e)
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
		
		void LinkLabelClearLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			toolTip1.SetToolTip(lstSourceFiles, String.Empty);
			lstSourceFiles.Items.Clear();
			lstSourceFiles.DropDownStyle = ComboBoxStyle.DropDown;
			lstSourceFiles.Text = String.Empty;
			lstSourceFiles.DropDownWidth = lstSourceFiles.Width;
			toolStripStatusLabel1.Text = String.Empty;
			labelDragDrop.Visible = false;
			setGroupBoxSource_Header();
			lstAudioSource.Refresh();
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
			scb.DropDownWidth = Math.Min(width, this.Width);
			labelDragDrop.Visible = false;
		}


		
		#region JobList management

		private Job[] createJobs()
		{
			Job[] jbs = new Job[sourceFiles.Length];
			AudioEncoder enc = currentEncoder;
			int htype = cbxHeader.Checked ? (int)(numericUpDownHeader.Value) : enc.HeaderType;
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
				jbs[i].EncoderExecutable = String.IsNullOrEmpty(enc.ExecutableFileName) ? null : Path.Combine(encoder_dir, enc.ExecutableFileName);
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
				toolStripStatusLabel1.Text = "You must select a source and a destination file before proceeding.";
				return;
			}
			
			Job[] jbs = createJobs();
			var items = createJobItems(jbs);

			// we have a multi-select list, let's deselect all items prior to selecting the job that was just submitted
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
			
			if (btnEnqueue.Checked && timer == null)
			{
				startJobs(sender, e);
			}
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
						if (jobs != null) jobs.RemoveAll(j => j.Item == item);
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
						if (jobs != null) jobs.RemoveAll(j => j.Item == item);
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

			SetDSPMoveButtonState();
		}

		private string getExeDirectory()
		{
			return Application.StartupPath;
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
			
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				using(TextWriter w = new StreamWriter(saveFileDialog1.FileName, false, Encoding.Default))
				{
					w.WriteLine(createAvsScript());
				}
			}
		}

		private void lstEncoder_SelectedIndexChanged(object sender, EventArgs e)
		{
			itemResetEncoder.Enabled = linkLabelEncoderConfig.Enabled = currentEncoder.IsSupportConfiguration;
			
			if (sourceFiles.Length > 1 || targetFileName.Length == 0)
				return;
			if (!currentEncoder.IsSupportedException(Path.GetExtension(targetFileName)))
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
		
				
		void LstAudioSourceDrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index == -1) return;
			
			var cb = sender as ComboBox;
			if (cb == null) return;
			var brsh = new SolidBrush(e.ForeColor);
			
			foreach (string file in sourceFiles)
			{
				if (!((Extensions.AudioSource)cb.Items[e.Index]).ListOfSupportedFileExtensions.Contains(Path.GetExtension(file).TrimStart('.')))
				{
					brsh.Color = Color.Gray;
					break;
				}
			}
			
			e.DrawBackground();
			e.Graphics.DrawString(cb.Items[e.Index].ToString(), e.Font, brsh, e.Bounds);
			e.DrawFocusRectangle();
		}

		void LstEncoderDrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index == -1) return;
			
			var cb = sender as ComboBox;
			if (cb == null) return;
			
			var brsh = new SolidBrush(e.ForeColor);
			
			if (!(string.IsNullOrEmpty(((Extensions.AudioEncoder)cb.Items[e.Index]).ExecutableFileName) || File.Exists(Path.Combine(Application.StartupPath, encoder_dir, ((Extensions.AudioEncoder)cb.Items[e.Index]).ExecutableFileName))))
			{
				brsh.Color = Color.Gray;
			}
			
			e.DrawBackground();
			e.Graphics.DrawString(cb.Items[e.Index].ToString(), e.Font, brsh, e.Bounds);
			e.DrawFocusRectangle();
		}
		
		private void configureEncoder(object sender, LinkLabelLinkClickedEventArgs e)
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
		private void LinkLabelConfigureAudioSourceLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
			itemResetAudioSource.Enabled = linkLabelSourceConfig.Enabled = currentSource.IsSupportConfiguration;
		}

		private void lstDSP_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ExtensionItemBase item = lstDSP.SelectedItem as ExtensionItemBase;
			if (item != null)
				btnConfigureDSP.Enabled = item.IsSupportConfiguration;
			else
				btnConfigureDSP.Enabled = false;

			SetDSPMoveButtonState();
		}

		private void configureDSP(object sender, System.EventArgs e)
		{
			DigitalSignalProcessor item = lstDSP.SelectedItem as DigitalSignalProcessor;
			if (item==null)
				return;
			if (!item.IsSupportConfiguration)
				return;
			if (item.Configure(this)==ConfigurationResult.OK)
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
				w.WriteLine(createAvsScript(btnPreview.Checked));
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
				if (jobs[i].Job.State == JobState.Waiting) wjcount++;
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
				if (((Job)item.Tag).State != JobState.Processing)
					continue;
				
				if (((Job)item.Tag).Progress > 0)
				{
						item.SubItems[4].Text = ((Job)item.Tag).Progress.ToString("D2") + " %";
				}
				else if (((Job)item.Tag).NeedsIndex)
					item.SubItems[4].Text = "Index";
			}
			
			if (!breakAfterCurrentJob)
				executeNextJob();
		}

		private void executeNextJob()
		{
			// if number of running jobs is less than number of max allowed jobs
			if (jobs.FindAll(j => j.Job.State == JobState.Processing).Count < (int)numericUpDownJobs.Value)
			{
				// if previous running job has reached at least progress of 1%
				if (jobs.FindLast(j => j.Job.State == JobState.Processing) != null && jobs.FindLast(j => j.Job.State == JobState.Processing).Job.Progress < 1)
					return;
				
				foreach (Jobs jbs in jobs)
				{
					if (jbs.Job.State != JobState.Waiting)
						continue;
					
					deselectAllJobs();
					jbs.Item.Selected = true;
					txtSimpleLog.Text = String.Empty;
					jbs.Job.State = JobState.Processing;
					jbs.Job.StartAt = DateTime.Now;
					jbs.Job.bKeepOutput = bKeepOutput;
					jbs.Job.CheckIndexNeeded();
					
					DataRow dr = ((DataRowView)cboPriority.SelectedItem).Row;
					jbs.Job.iPriority = Convert.ToInt32(dr.ItemArray.GetValue(1));
					
					updateListViewItem(jbs.Item);
					jbs.AppendToLog("Starting job " + jbs.Job.Name);
					
					jbs.Encoder = new Encoder(jbs.Job);
					jbs.Encoder.SetKeepOutput(jbs.Job.bKeepOutput);
					jbs.Encoder.SetPriority((ProcessPriorityClass)Convert.ToInt32(dr.ItemArray.GetValue(1)));
					jbs.Encoder.EncoderCallback += encoderCallback;
					jbs.Encoder.Start();
					return;
				}
			}
		}
		
		private void jobsFinished()
		{
			timer.Stop();
			timer = null;
			btnStart.Enabled = true;
			btnAbort.Enabled = btnStop.Enabled = false;
			cboPriority.Enabled = false;
			
			jobs = null;
			if (breakAfterCurrentJob) toolStripStatusLabel1.Text = "Stopped by user request. Check job states/logs for errors.";
			else toolStripStatusLabel1.Text = "Finished jobqueue processing. Check job states/logs for errors.";
		}
		
		private void deselectAllJobs()
		{
			foreach (ListViewItem li in jobListView.Items)
				li.Selected = false;
		}

		private void encoderCallback(Job sender, EncoderCallbackEventArgs s)
		{
			if (!this.InvokeRequired)
			{
				int ji = jobs.FindIndex(j => j.Job == sender);	//get the index of the sending job
				
				switch(s.Type)
				{
					case EncoderCallbackEventArgs.EventType.Progress:
//						this.toolStripProgressBar1.Value = (int)s.Progress;		//too much gui overhead when running multiple jobs parallel
						break;
					case EncoderCallbackEventArgs.EventType.Done:
						jobs[ji].Job.State = JobState.Done;
						goto cmon;
					case EncoderCallbackEventArgs.EventType.Terminated:
						jobs[ji].Job.State = JobState.Aborted;
						goto cmon;
					case EncoderCallbackEventArgs.EventType.Error:
						jobs[ji].Job.State = JobState.Error;
						
					cmon:
						jobs[ji].Job.StopAt = DateTime.Now;
						jobs[ji].AppendToLog(String.IsNullOrEmpty(s.Message) ? sender.State.ToString() : (sender.State.ToString() + ": " + s.Message));
						jobs[ji].AppendStdStreamsToLog();
						updateListViewItem(jobs[ji].Item);
						toolStripProgressBar1.PerformStep();
						if (jobs[ji].Item.Selected) txtSimpleLog.Text = jobs[ji].Log;
						if (!jobs.Exists(j => j.Job.State == JobState.Processing || (j.Job.State == JobState.Waiting && breakAfterCurrentJob == false)))
							jobsFinished();
						break;
					case EncoderCallbackEventArgs.EventType.Notify:
						jobs[ji].AppendToLog(s.Message);
						if (jobs[ji].Item.Selected) txtSimpleLog.Text = jobs[ji].Log;
						break;
					case EncoderCallbackEventArgs.EventType.StdErr:
						jobs[ji].Stderr.Append(s.Message);
						break;
					case EncoderCallbackEventArgs.EventType.StdOut:
						jobs[ji].Stdout.Append(s.Message);
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
		
		private void abortEncoding(object sender, EventArgs e)
		{
			breakAfterCurrentJob = true;
			timer.Stop();
			jobs.ForEach(j => {if (j.Job.State == JobState.Processing) j.Encoder.Abort();});
		}

		private void stopEncoding(object sender, System.EventArgs e)
		{
			toolStripStatusLabel1.Text = "Break after running job.";
			breakAfterCurrentJob = true;
			btnStop.Enabled = false;
		}
		
		private void MainFormLoad(object sender, EventArgs e)
		{
			string[] args = Environment.GetCommandLineArgs();
			
			if (args != null && args.Length > 0)
			{
				foreach (string s in args)
				{
					if (s == "-testmode")
					{
						linkLblReloadPlugins.Visible = true;
					}
				}
			}
		}

		private void MainForm_Closing(object sender, CancelEventArgs e)
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
		
		private void MainForm_Closed(object sender, EventArgs e)
		{
			try
			{
				if(File.Exists(m_tempFileName))
					File.Delete(m_tempFileName);
			}
			catch{};
			saveConfiguration();
		}

		private void jobListView_SelectedIndexChanged(object sender, EventArgs e)
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

		void LinkLabelAutoJobsLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (Environment.ProcessorCount == 2)
				numericUpDownJobs.Value = 2;
			else numericUpDownJobs.Value = Math.Round((Environment.ProcessorCount * 70) / 100m);
		}

		#endregion

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

		private void LinkLabelUrlLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start((sender as Control).Tag.ToString());
			(sender as LinkLabel).LinkVisited = true;
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
				jobs.ForEach(j => {if (j.Job.State == JobState.Processing) j.Encoder.SetKeepOutput(bKeepOutput);});
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
			
			// Create a DataView from the DataTable to act as the data source for the DropDownList control.
			DataView dv = new DataView(dt);
			return dv;
		}

		public DataRow CreateDataRow(string strDisplayItem, string strValueItem, DataTable dt)
		{
			// Create a DataRow using the DataTable defined in the CreatePriorityDataSource method.
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

				// if the priority selected is High, let's make sure the user wants to do this as it can tie up the system
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
						jobs.ForEach(j => {if (j.Job.State == JobState.Processing) j.Encoder.SetPriority(enumItem);});
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
			ContextMenuStrip cm = (ContextMenuStrip)sender;
			Control cl = cm.SourceControl;
			cm.Items.Clear();
			filterItemsPageTwo = null;
			
			if (cl == null)
			{
				e.Cancel = true;
				return;
			}
			else if (cl == jobListView)
			{
				e.Cancel = jobListView.SelectedItems.Count < 1;
				cm.ShowCheckMargin = false;
				cm.Items.AddRange(toolStripItemsJoblist);
			}
			else if (cl == lstAudioSource)
			{
				e.Cancel = false;
				cm.ShowCheckMargin = true;
				cm.Items.AddRange(toolStripItemsLstAudioSource.ToArray());
				cm.Items.AddRange(toolStripItemsLstAudioSourceFilters1.ToArray());
				if (toolStripItemsLstAudioSourceFilters2.Count > 0)
				{
					filterItemsPageTwo = new ToolStripMenuItem(". . .", null, toolStripItemsLstAudioSourceFilters2.ToArray());
					filterItemsPageTwo.Tag = cl;
					cm.Items.Insert(4, filterItemsPageTwo);
				}
			}
			else if (cl == lstEncoder)
			{
				e.Cancel = false;
				cm.ShowCheckMargin = true;
				cm.Items.AddRange(toolStripItemsLstEncoder.ToArray());
				cm.Items.AddRange(toolStripItemsLstEncoderFilters1.ToArray());
				if (toolStripItemsLstEncoderFilters2.Count > 0)
				{
					filterItemsPageTwo = new ToolStripMenuItem(". . .", null, toolStripItemsLstEncoderFilters2.ToArray());
					filterItemsPageTwo.Tag = cl;
					cm.Items.Insert(6, filterItemsPageTwo);
				}
			}
			else
			{
				e.Cancel = true;
			}
		}
				
		void ContextMenuStrip1Opened(object sender, EventArgs e)
		{
			if (filterItemsPageTwo != null)
				filterItemsPageTwo.ShowDropDown();
		}
		
		void LinkLabelReloadPluginsLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			lstEncoder.Items.Clear();
			lstDSP.Items.Clear();
			lstAudioSource.Items.Clear();
			loadExtensionsAndApplyConfiguration();
		}
		
		
		void LinkLabelMouseEnter(object sender, EventArgs e)
		{
			LinkLabel lb = (LinkLabel)sender;
			lb.LinkColor = lb.ActiveLinkColor;
		}
		
		void LinkLabelMouseLeave(object sender, EventArgs e)
		{
			LinkLabel lb = (LinkLabel)sender;
			lb.LinkColor = Color.Blue;
		}
		
		
		void BtnShowScriptClick(object sender, EventArgs e)
		{			
			if (msgWindow == null || msgWindow.IsDisposed)
				msgWindow = new MessageWindow();
			
			msgWindow.ClearText();
			msgWindow.AddText(createAvsScript());
			msgWindow.Show();
			msgWindow.BringToFront();
		}
		
	}


	internal class Jobs
	{
		public Encoder Encoder {get; set;}
		public bool BreakAfterCurrentJob {get; set;}
		public StringBuilder Stdout {get; set;}
		public StringBuilder Stderr {get; set;}
		private StringBuilder log;
		public string Log {get {return Job.Log;}}
		public Job Job {get; set;}
		private ListViewItem item;
		public ListViewItem Item {get {return item;} set {item = value; Job = (Job)value.Tag;}}
		private Regex cleanUpStdOutRegex = new Regex(@"\n[^\n]+\r", RegexOptions.Compiled | RegexOptions.CultureInvariant);
		
		public Jobs()
		{
			log = new StringBuilder();
			Stderr = new StringBuilder();
			Stdout = new StringBuilder();
		}
		
		public Jobs(ListViewItem item) :this()
		{
			Item = item;
		}
		
		public void AppendToLog(string s)
		{
			log.AppendLine(s);
			Job.Log = log.ToString();
		}
		
		public void AppendStdStreamsToLog()
		{
			if (Stdout.Length != 0)
			{
				log.AppendLine("#### Encoder StdOut ####");
				log.AppendLine(cleanUpStdOutRegex.Replace(Stdout.ToString().Replace(Environment.NewLine, "\n"), Environment.NewLine));
			}
			if (Stderr.Length != 0)
			{
				log.AppendLine("#### Encoder StdErr ####");
				log.AppendLine(cleanUpStdOutRegex.Replace(Stderr.ToString().Replace(Environment.NewLine, "\n"), Environment.NewLine));
			}
			
			Job.Log = log.ToString();
		}
	}
}
