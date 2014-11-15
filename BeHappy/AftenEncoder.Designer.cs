
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BeHappy.Aften
{
	partial class EncoderConfigurationForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EncoderConfigurationForm));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.vQuality = new System.Windows.Forms.TrackBar();
			this.rbtnVBR = new System.Windows.Forms.RadioButton();
			this.vBitrate = new System.Windows.Forms.TrackBar();
			this.rbtnCBR = new System.Windows.Forms.RadioButton();
			this.lstDynRangeLevel = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cbxReadToEndOfFile = new System.Windows.Forms.CheckBox();
			this.cbxIndependentLR = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.lstCenterMixLevel = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.lstSurroundMixLevel = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.munDiaNorm = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtCLI = new System.Windows.Forms.TextBox();
			this.lstDPL = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.vQuality)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.vBitrate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.munDiaNorm)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(4, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(128, 96);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(336, 318);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "OK";
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(416, 318);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "Cancel";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.vQuality);
			this.groupBox1.Controls.Add(this.rbtnVBR);
			this.groupBox1.Controls.Add(this.vBitrate);
			this.groupBox1.Controls.Add(this.rbtnCBR);
			this.groupBox1.Location = new System.Drawing.Point(138, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(353, 151);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Bitrate management";
			// 
			// vQuality
			// 
			this.vQuality.Dock = System.Windows.Forms.DockStyle.Top;
			this.vQuality.Location = new System.Drawing.Point(3, 106);
			this.vQuality.Maximum = 1023;
			this.vQuality.Minimum = 1;
			this.vQuality.Name = "vQuality";
			this.vQuality.Size = new System.Drawing.Size(347, 42);
			this.vQuality.TabIndex = 17;
			this.vQuality.TickFrequency = 32;
			this.vQuality.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.vQuality.Value = 1;
			this.vQuality.ValueChanged += new System.EventHandler(this.vQuality_ValueChanged);
			// 
			// rbtnVBR
			// 
			this.rbtnVBR.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnVBR.Location = new System.Drawing.Point(3, 82);
			this.rbtnVBR.Name = "rbtnVBR";
			this.rbtnVBR.Size = new System.Drawing.Size(347, 24);
			this.rbtnVBR.TabIndex = 14;
			this.rbtnVBR.Text = "Variable bit rate";
			this.rbtnVBR.CheckedChanged += new System.EventHandler(this.rbtnCBR_CheckedChanged);
			// 
			// vBitrate
			// 
			this.vBitrate.Dock = System.Windows.Forms.DockStyle.Top;
			this.vBitrate.Location = new System.Drawing.Point(3, 40);
			this.vBitrate.Maximum = 640;
			this.vBitrate.Minimum = 64;
			this.vBitrate.Name = "vBitrate";
			this.vBitrate.Size = new System.Drawing.Size(347, 42);
			this.vBitrate.TabIndex = 15;
			this.vBitrate.TickFrequency = 32;
			this.vBitrate.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.vBitrate.Value = 448;
			this.vBitrate.ValueChanged += new System.EventHandler(this.vBitrate_ValueChanged);
			// 
			// rbtnCBR
			// 
			this.rbtnCBR.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnCBR.Location = new System.Drawing.Point(3, 16);
			this.rbtnCBR.Name = "rbtnCBR";
			this.rbtnCBR.Size = new System.Drawing.Size(347, 24);
			this.rbtnCBR.TabIndex = 13;
			this.rbtnCBR.Text = "Constant bit rate";
			this.rbtnCBR.CheckedChanged += new System.EventHandler(this.rbtnCBR_CheckedChanged);
			// 
			// lstDynRangeLevel
			// 
			this.lstDynRangeLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstDynRangeLevel.FormattingEnabled = true;
			this.lstDynRangeLevel.Location = new System.Drawing.Point(194, 243);
			this.lstDynRangeLevel.Name = "lstDynRangeLevel";
			this.lstDynRangeLevel.Size = new System.Drawing.Size(81, 21);
			this.lstDynRangeLevel.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(42, 246);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(146, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Dynamic Range Compression";
			// 
			// cbxReadToEndOfFile
			// 
			this.cbxReadToEndOfFile.AutoSize = true;
			this.cbxReadToEndOfFile.Location = new System.Drawing.Point(284, 242);
			this.cbxReadToEndOfFile.Name = "cbxReadToEndOfFile";
			this.cbxReadToEndOfFile.Size = new System.Drawing.Size(117, 17);
			this.cbxReadToEndOfFile.TabIndex = 12;
			this.cbxReadToEndOfFile.Text = "Read to End of File";
			this.cbxReadToEndOfFile.UseVisualStyleBackColor = true;
			// 
			// cbxIndependentLR
			// 
			this.cbxIndependentLR.AutoSize = true;
			this.cbxIndependentLR.Location = new System.Drawing.Point(284, 218);
			this.cbxIndependentLR.Name = "cbxIndependentLR";
			this.cbxIndependentLR.Size = new System.Drawing.Size(155, 17);
			this.cbxIndependentLR.TabIndex = 13;
			this.cbxIndependentLR.Text = "Independent L+R channels";
			this.cbxIndependentLR.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(102, 163);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(86, 13);
			this.label2.TabIndex = 15;
			this.label2.Text = "Center Mix Level";
			// 
			// lstCenterMixLevel
			// 
			this.lstCenterMixLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstCenterMixLevel.FormattingEnabled = true;
			this.lstCenterMixLevel.Location = new System.Drawing.Point(194, 162);
			this.lstCenterMixLevel.Name = "lstCenterMixLevel";
			this.lstCenterMixLevel.Size = new System.Drawing.Size(81, 21);
			this.lstCenterMixLevel.TabIndex = 14;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(90, 192);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(98, 13);
			this.label3.TabIndex = 17;
			this.label3.Text = "Surround Mix Level";
			// 
			// lstSurroundMixLevel
			// 
			this.lstSurroundMixLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstSurroundMixLevel.FormattingEnabled = true;
			this.lstSurroundMixLevel.Location = new System.Drawing.Point(194, 189);
			this.lstSurroundMixLevel.Name = "lstSurroundMixLevel";
			this.lstSurroundMixLevel.Size = new System.Drawing.Size(81, 21);
			this.lstSurroundMixLevel.TabIndex = 16;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(85, 219);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(103, 13);
			this.label4.TabIndex = 19;
			this.label4.Text = "Dialog Normalization";
			// 
			// munDiaNorm
			// 
			this.munDiaNorm.Location = new System.Drawing.Point(194, 217);
			this.munDiaNorm.Maximum = new decimal(new int[] {
									31,
									0,
									0,
									0});
			this.munDiaNorm.Name = "munDiaNorm";
			this.munDiaNorm.Size = new System.Drawing.Size(81, 20);
			this.munDiaNorm.TabIndex = 20;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(281, 165);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(58, 13);
			this.label5.TabIndex = 22;
			this.label5.Text = "DPL Mode";
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.txtCLI);
			this.groupBox3.Location = new System.Drawing.Point(42, 267);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(449, 46);
			this.groupBox3.TabIndex = 11;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Additional CLI arguments";
			// 
			// txtCLI
			// 
			this.txtCLI.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtCLI.Location = new System.Drawing.Point(3, 16);
			this.txtCLI.Name = "txtCLI";
			this.txtCLI.Size = new System.Drawing.Size(443, 20);
			this.txtCLI.TabIndex = 0;
			// 
			// lstDPL
			// 
			this.lstDPL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstDPL.FormattingEnabled = true;
			this.lstDPL.Location = new System.Drawing.Point(344, 162);
			this.lstDPL.Name = "lstDPL";
			this.lstDPL.Size = new System.Drawing.Size(147, 21);
			this.lstDPL.TabIndex = 21;
			// 
			// EncoderConfigurationForm
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(494, 344);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.lstDPL);
			this.Controls.Add(this.munDiaNorm);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lstSurroundMixLevel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lstCenterMixLevel);
			this.Controls.Add(this.cbxIndependentLR);
			this.Controls.Add(this.cbxReadToEndOfFile);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstDynRangeLevel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "EncoderConfigurationForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Aften AC3 Encoder Configuration";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.vQuality)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.vBitrate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.munDiaNorm)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox3;
		public System.Windows.Forms.RadioButton rbtnVBR;
		public System.Windows.Forms.RadioButton rbtnCBR;
		public TrackBar vBitrate;
		public TrackBar vQuality;
		public TextBox txtCLI;
		//            internal ComboBox lstBandwidth;
		internal ComboBox lstDynRangeLevel;
		private Label label1;
		internal CheckBox cbxReadToEndOfFile;
		internal CheckBox cbxIndependentLR;
		private Label label2;
		internal ComboBox lstCenterMixLevel;
		private Label label3;
		internal ComboBox lstSurroundMixLevel;
		private Label label4;
		internal NumericUpDown munDiaNorm;
		private Label label5;
		internal ComboBox lstDPL;
	}
}
