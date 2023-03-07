using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BeHappy.DSP.ConfigurationForms
{
	internal partial class ConfigurationFormForMultiOption : ConfigurationFormBase
	{
		public ConfigurationFormForMultiOption()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
		}

		private RadioButton[] radioButtons;
		private CheckBox[] checkBoxes;
		private ComboBox[] dropDowns;
		private NumericUpDown[] numericUpDowns;
		private TrackBar trackBar;
		private BeHappy.Extensions.MultiOptionBase.RadioButtonOption[] radioButtonOptions;
		private int rbIndex;
		private BeHappy.Extensions.MultiOptionBase.MultiOptionConfig config;
		private LinkLabel linkLabelUrl;
		private LinkLabel linkLabelInfo;
		private TextBox cmdArgsTextbox;
		private const string cmdArgsTextboxCaption = "custom arguments";
		private int[] trackBarValues;
		
		internal void Init(BeHappy.Extensions.MultiOptionBase pOptions, BeHappy.Extensions.MultiOptionBase.MultiOptionConfig pConfig)
		{
			int minH = 16;
			this.Text = pOptions.ToString();
			int sumH = 0;
			radioButtonOptions = pOptions.Radiobuttons;
			config = pConfig;
			
			if (!String.IsNullOrEmpty(pOptions.Info))
			{
				linkLabelInfo = new LinkLabel();
				linkLabelInfo.AutoSize = true;
				linkLabelInfo.Text = "Info";
				linkLabelInfo.BackColor = Color.Transparent;
				linkLabelInfo.LinkBehavior = LinkBehavior.HoverUnderline;
				linkLabelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
				linkLabelInfo.MouseEnter += (s, e) => (s as LinkLabel).LinkColor = Color.Red;
				linkLabelInfo.MouseLeave += (s, e) => (s as LinkLabel).LinkColor = Color.Blue;
				linkLabelInfo.Click += (s, e) => MessageBox.Show(pOptions.Info.Replace("\\n", Environment.NewLine), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			
			if (pOptions.LogoBitmap != null && pOptions.LogoBitmap.Length != 0)
			{
				using(Stream s = new MemoryStream(pOptions.LogoBitmap))
				{
					Image bmp = Image.FromStream(s);
					minH = bmp.Height + 36;
					logoBox.Image = bmp;
					logoBox.Width = bmp.Width + 4;
					logoBox.SizeMode = PictureBoxSizeMode.CenterImage;
//					logoBox.BorderStyle = BorderStyle.FixedSingle;
					
					if (linkLabelInfo != null)
					{
//						toolTip1.SetToolTip(logoBox, pOptions.Info.Replace("\\n", Environment.NewLine));
						logoBox.Controls.Add(linkLabelInfo);
						linkLabelInfo.Location = new Point(logoBox.Width - linkLabelInfo.Width, logoBox.Height - linkLabelInfo.Height);
					}
				}
			}
			else if (linkLabelInfo != null)
			{
				logoBox.Controls.Add(linkLabelInfo);
				logoBox.Width = linkLabelInfo.Width;
				linkLabelInfo.Location = new Point(logoBox.Left, logoBox.Height - linkLabelInfo.Height);
			}
			
			if (radioButtonOptions != null)
			{
				rbIndex = pOptions.GetCurrentRadiobuttonIndex;
				radioButtons = new RadioButton[radioButtonOptions.Length];
				RadioButton rb;
				
				if (radioButtonOptions.ToList().Exists(o => o.Trackbar != null))
				{
					trackBar = new TrackBar();
					trackBar.AutoSize = false;
					trackBar.Height = 32;
					trackBar.ValueChanged += new EventHandler(trackBar_ValueChanged);
					
					// get trackbar values from config or set to default
					if (pConfig.TrackBarValues == null || (pConfig.TrackBarValues.Length != radioButtonOptions.Length))
					{
						trackBarValues = new int[radioButtonOptions.Length];
						
						for (int i = 0; i < radioButtonOptions.Length; i++)
						{
							trackBarValues[i] = radioButtonOptions[i].Trackbar == null ? 0 :
								(radioButtonOptions[i].Trackbar.DefaultValue < radioButtonOptions[i].Trackbar.Min ?
								 radioButtonOptions[i].Trackbar.Min : radioButtonOptions[i].Trackbar.DefaultValue);
								// use trackbar min value if default value is less than min
						}
						
					}
					else trackBarValues = pConfig.TrackBarValues;
				}
				
				// create radiobuttons and assign text
				for (int i = 0; i < radioButtonOptions.Length; i++)
				{
					rb = new RadioButton();
					radioButtons[i] = rb;
					this.flowLayoutPanel1.Controls.Add(rb);
					rb.AutoSize = true;
					rb.Dock = DockStyle.Fill;
					if (!String.IsNullOrEmpty(radioButtonOptions[i].ToolTip))
						this.toolTip1.SetToolTip(rb, radioButtonOptions[i].ToolTip.Replace("\\n", Environment.NewLine));
					
					rb.CheckedChanged += new EventHandler(rb_CheckedChanged);
					
					if (radioButtonOptions[i].Trackbar != null)
					{
						if (radioButtonOptions[i].Trackbar.FixedValues)
							radioButtons[i].Text = String.Format(radioButtonOptions[i].Name, radioButtonOptions[i].Trackbar.Values[trackBarValues[i]]);
						else radioButtons[i].Text = String.Format(radioButtonOptions[i].Name, trackBarValues[i] * radioButtonOptions[i].Trackbar.Multiplier);
					}
					else radioButtons[i].Text = radioButtonOptions[i].Name;
				}
				
				radioButtons[rbIndex].Checked = true;
				this.ActiveControl = radioButtons[rbIndex];
				
				// add trackbar as last element on this panel
				if (trackBar != null) this.flowLayoutPanel1.Controls.Add(trackBar);
			}
			
			if (pOptions.Dropdowns != null)
			{
				dropDowns = new ComboBox[pOptions.Dropdowns.Length];
				ComboBox cb;
				Label lb;
				int[] indices = pOptions.GetCurrentDropdownIndices;
				
				for (int i = 0; i < pOptions.Dropdowns.Length; i++)
				{
					lb = new Label();
					lb.Text = pOptions.Dropdowns[i].Name + " :";
					tableLayoutPanel2.Controls.Add(lb);
					lb.Dock = DockStyle.Fill;
					lb.TextAlign = ContentAlignment.MiddleLeft;
					
					cb = new ComboBox();
					dropDowns[i] = cb;
					tableLayoutPanel2.Controls.Add(cb);
					cb.DropDownStyle = ComboBoxStyle.DropDownList;
					cb.Items.AddRange(pOptions.Dropdowns[i].Item);
					cb.SelectedIndex = 0;
					cb.Dock = DockStyle.Fill;
					cb.SelectedIndex = indices[i];
					
					if (!String.IsNullOrEmpty(pOptions.Dropdowns[i].ToolTip))
					{
						string s = pOptions.Dropdowns[i].ToolTip.Replace("\\n", Environment.NewLine);
						this.toolTip1.SetToolTip(cb, s);
						this.toolTip1.SetToolTip(lb, s);
					}
				}
			}
			
			if (pOptions.NumericUpdowns != null)
			{
				numericUpDowns = new NumericUpDown[pOptions.NumericUpdowns.Length];
				NumericUpDown nup;
				Label lb;
				float[] nvals = pOptions.GetCurrentNumericUpDownValues;
				
				for (int i = 0; i < pOptions.NumericUpdowns.Length; i++)
				{
					lb = new Label();
					lb.Text = pOptions.NumericUpdowns[i].Name + " :";
					tableLayoutPanel2.Controls.Add(lb);
					lb.Dock = DockStyle.Fill;
					lb.TextAlign = ContentAlignment.MiddleLeft;
					
					nup = new NumericUpDown();
					numericUpDowns[i] = nup;
					tableLayoutPanel2.Controls.Add(nup);
					nup.Dock = DockStyle.Fill;
					nup.Minimum = (decimal)pOptions.NumericUpdowns[i].Min;
					nup.Maximum = (decimal)pOptions.NumericUpdowns[i].Max;
					nup.DecimalPlaces = pOptions.NumericUpdowns[i].DecimalPlaces;
					nup.Increment = (decimal)pOptions.NumericUpdowns[i].Increment;
					nup.Value = (decimal)nvals[i];
					
					if (!String.IsNullOrEmpty(pOptions.NumericUpdowns[i].ToolTip))
					{
						string s = pOptions.NumericUpdowns[i].ToolTip.Replace("\\n", Environment.NewLine);
						this.toolTip1.SetToolTip(nup, s);
						this.toolTip1.SetToolTip(lb, s);
					}
				}
			}
			
			if (pOptions.Checkboxes != null)
			{
				checkBoxes = new CheckBox[pOptions.Checkboxes.Length];
				CheckBox cb;
				bool[] states = pOptions.GetCurrentCheckboxStates;
				
				for (int i = 0; i < pOptions.Checkboxes.Length; i++)
				{
					cb = new CheckBox();
					checkBoxes[i] = cb;
					tableLayoutPanel2.Controls.Add(cb);
					cb.Text = pOptions.Checkboxes[i].Name;
					cb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
					cb.Checked = states[i];
					
					if (!String.IsNullOrEmpty(pOptions.Checkboxes[i].ToolTip))
						this.toolTip1.SetToolTip(cb, pOptions.Checkboxes[i].ToolTip.Replace("\\n", Environment.NewLine));
				}
			}
			
			bool delColumn = false;
			
			if (tableLayoutPanel2.Controls.Count == 1)
				delColumn = true;
			    
			if (pOptions.ShowCommandTextbox == true)
			{
				cmdArgsTextbox = new TextBox();
				tableLayoutPanel2.Controls.Add(cmdArgsTextbox);
				tableLayoutPanel2.SetColumnSpan(cmdArgsTextbox, 2);
				cmdArgsTextbox.Dock = DockStyle.Fill;
				this.toolTip1.SetToolTip(cmdArgsTextbox, "Additional command line arguments passed to encoder.");
			
				if (String.IsNullOrEmpty(pConfig.CustomArgs))
				{
					cmdArgsTextbox.TextAlign = HorizontalAlignment.Center;
					cmdArgsTextbox.Text = cmdArgsTextboxCaption;
					cmdArgsTextbox.Font = new Font(this.Font, FontStyle.Italic);
					cmdArgsTextbox.ForeColor = Color.Gray;
					cmdArgsTextbox.GotFocus += (s, e) => {
						if (cmdArgsTextbox.Text == cmdArgsTextboxCaption) cmdArgsTextbox.Text = string.Empty;
						cmdArgsTextbox.ResetFont();
						cmdArgsTextbox.ResetForeColor();
						cmdArgsTextbox.TextAlign = HorizontalAlignment.Left;
					};
				}
				else cmdArgsTextbox.Text = pConfig.CustomArgs;
			}
			
			if (!String.IsNullOrEmpty(pOptions.Url))
			{
				linkLabelUrl = new LinkLabel();
				Uri url = new Uri(pOptions.Url);
				linkLabelUrl.Text = url.Host + (url.AbsolutePath == "/" ? String.Empty : url.AbsolutePath);
				if (String.IsNullOrEmpty(pOptions.UrlToolTip))
					this.toolTip1.SetToolTip(linkLabelUrl, url.AbsoluteUri);
				else
					this.toolTip1.SetToolTip(linkLabelUrl, String.Format("{0}{1}{1}{2}", url.AbsoluteUri, Environment.NewLine, pOptions.UrlToolTip));
				tableLayoutPanel2.Controls.Add(linkLabelUrl);
				tableLayoutPanel2.SetColumnSpan(linkLabelUrl, 2);
				linkLabelUrl.Margin = new Padding(0, 3, 0, 3);
				linkLabelUrl.Dock = DockStyle.Left;
				linkLabelUrl.TextAlign = ContentAlignment.MiddleCenter;
				linkLabelUrl.AutoEllipsis = true;
				linkLabelUrl.Height = 15;
				linkLabelUrl.LinkClicked += (s, e) => Process.Start(url.AbsoluteUri);
				linkLabelUrl.MouseEnter += (s, e) => (s as LinkLabel).LinkColor = Color.Red;
				linkLabelUrl.MouseLeave += (s, e) => (s as LinkLabel).LinkColor = Color.Blue;
			}
			
			tableLayoutPanel1.TabIndex = 0;
			button1.TabIndex = 1;
			button2.TabIndex = 2;
			
			// adjust the size of the window and of some controls
			this.Width = pOptions.DialogWidth;
			if (trackBar != null) trackBar.Width = flowLayoutPanel1.Width - 5;
			if (linkLabelUrl != null) linkLabelUrl.Width = flowLayoutPanel1.Width - 5;
			sumH = (this.ClientSize.Height - tableLayoutPanel1.Height) + tableLayoutPanel2.Bottom + 10;
			this.ClientSize = new Size(this.ClientSize.Width, Math.Max(minH, sumH));
			
			// remove first row of tableLayoutPanel1 if the containing flowLayoutPanel is empty
			if (flowLayoutPanel1.Controls.Count < 1)
			{
				tableLayoutPanel1.RowCount--;
				tableLayoutPanel1.RowStyles.RemoveAt(tableLayoutPanel1.GetRow(flowLayoutPanel1));
				tableLayoutPanel1.Controls.Remove(flowLayoutPanel1);
				tableLayoutPanel1.SetRow(tableLayoutPanel2, tableLayoutPanel1.GetRow(tableLayoutPanel2) - 1);
			}
			
			// remove second column of lower tableLayoutPanel if there is only one control (probably a checkbox)
			if (delColumn)
			{
				tableLayoutPanel2.ColumnCount--;
				tableLayoutPanel2.ColumnStyles.RemoveAt(1);
			}
			// remove lower tableLayoutPanel if there are no controls on it
			else if (tableLayoutPanel2.Controls.Count < 1)
			{
				tableLayoutPanel1.RowCount--;
				tableLayoutPanel1.RowStyles.RemoveAt(tableLayoutPanel1.GetRow(tableLayoutPanel2));
				tableLayoutPanel1.Controls.Remove(tableLayoutPanel2);
			}

			Utils.ChangeFontRecursive(new Control[]{this},
				Utils.HasMono ? new Font(SystemFonts.MessageBoxFont.Name, 8) : SystemFonts.MessageBoxFont);
		}

		void trackBar_ValueChanged(object sender, EventArgs e)
		{
			trackBarValues[rbIndex] = trackBar.Value;
			
			tableLayoutPanel1.SuspendLayout();

			if (radioButtonOptions[rbIndex].Trackbar.FixedValues == true)
				radioButtons[rbIndex].Text = String.Format(radioButtonOptions[rbIndex].Name, radioButtonOptions[rbIndex].Trackbar.Values[trackBar.Value]);
			else
				radioButtons[rbIndex].Text = String.Format(CultureInfo.InstalledUICulture, radioButtonOptions[rbIndex].Name, trackBar.Value * radioButtonOptions[rbIndex].Trackbar.Multiplier);

			tableLayoutPanel1.ResumeLayout();
		}

		void rb_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rb = (RadioButton)sender;
			rbIndex = radioButtons.ToList().IndexOf(rb);
			flowLayoutPanel1.SuspendLayout();
			
			if (rb.Checked)
			{
				if (radioButtonOptions[rbIndex].Trackbar != null)
				{
					trackBar.Enabled = true;
					
					if (radioButtonOptions[rbIndex].Trackbar.FixedValues == true)
					{
						trackBar.Minimum = 0;
						trackBar.Maximum = radioButtonOptions[rbIndex].Trackbar.Values.Length - 1;
						trackBar.TickFrequency = 1;
					}
					else
					{
						trackBar.Minimum = radioButtonOptions[rbIndex].Trackbar.Min;
						trackBar.Maximum = radioButtonOptions[rbIndex].Trackbar.Max;
						trackBar.TickFrequency = radioButtonOptions[rbIndex].Trackbar.TickFrequency;
					}
					
					trackBar.Value = trackBarValues[rbIndex];
					trackBar_ValueChanged(null, null);
				}
				else
				{
					if (trackBar != null)
						trackBar.Enabled = false;
				}
			}
			else
			{
				if (radioButtonOptions[rbIndex].Trackbar != null) trackBar_ValueChanged(null, null);
				else rb.Text = String.Format(radioButtonOptions[rbIndex].Name, String.Empty);
			}
			flowLayoutPanel1.ResumeLayout(true);
		}
		
		public BeHappy.Extensions.MultiOptionBase.MultiOptionConfig GetConfig()
		{
			var cfg = new BeHappy.Extensions.MultiOptionBase.MultiOptionConfig();
			
			if (checkBoxes != null)
			{
				cfg.CheckBoxConfig = new BeHappy.Extensions.MultiOptionBase.MultiOptionConfig.ConfigIndices();
				cfg.CheckBoxConfig.Checked = new bool[checkBoxes.Length];
				
				for (int i = 0; i < checkBoxes.Length; i++) {
					cfg.CheckBoxConfig.Checked[i] = checkBoxes[i].Checked;
				}
			}
			if (dropDowns != null)
			{
				cfg.DropDownConfig = new BeHappy.Extensions.MultiOptionBase.MultiOptionConfig.ConfigIndices();
				cfg.DropDownConfig.SelectedIndex = new int[dropDowns.Length];
				
				for (int i = 0; i < dropDowns.Length; i++) {
					cfg.DropDownConfig.SelectedIndex[i] = dropDowns[i].SelectedIndex;
				}
			}
			
			if (numericUpDowns != null)
			{
				cfg.NumericUpDownValues = new float[numericUpDowns.Length];
				
				for (int i = 0; i < numericUpDowns.Length; i++) {
					cfg.NumericUpDownValues[i] = (float)numericUpDowns[i].Value;
				}
			}
			
			if (radioButtons != null)
				cfg.RadioButtonIndex = rbIndex;
			if (trackBar != null)
				cfg.TrackBarValues = trackBarValues;
			if (cmdArgsTextbox != null && cmdArgsTextbox.Text != cmdArgsTextboxCaption)
				cfg.CustomArgs = cmdArgsTextbox.Text;
			
			return cfg;
		}
	}
}

