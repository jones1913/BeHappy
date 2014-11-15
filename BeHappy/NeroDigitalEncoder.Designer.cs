
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BeHappy.NeroDigitalAAC
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
			this.rbtnABR = new System.Windows.Forms.RadioButton();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.lstProfile = new System.Windows.Forms.ComboBox();
			this.cbxCreateHintTrack = new System.Windows.Forms.CheckBox();
			//              this.cbxSSE2 = new System.Windows.Forms.CheckBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.vQuality)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.vBitrate)).BeginInit();
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
			this.button1.Location = new System.Drawing.Point(336, 247);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "OK";
			//
			// button2
			//
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(416, 247);
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
			this.groupBox1.Controls.Add(this.rbtnABR);
			this.groupBox1.Location = new System.Drawing.Point(138, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(353, 182);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Bitrate management";
			//
			// vQuality
			//
			this.vQuality.Dock = System.Windows.Forms.DockStyle.Top;
			this.vQuality.Location = new System.Drawing.Point(3, 130);
			this.vQuality.Maximum = 100;
			this.vQuality.Name = "vQuality";
			this.vQuality.Size = new System.Drawing.Size(347, 42);
			this.vQuality.TabIndex = 17;
			this.vQuality.TickFrequency = 5;
			this.vQuality.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.vQuality.ValueChanged += new System.EventHandler(this.vQuality_ValueChanged);
			//
			// rbtnVBR
			//
			this.rbtnVBR.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnVBR.Location = new System.Drawing.Point(3, 106);
			this.rbtnVBR.Name = "rbtnVBR";
			this.rbtnVBR.Size = new System.Drawing.Size(347, 24);
			this.rbtnVBR.TabIndex = 14;
			this.rbtnVBR.Text = "Variable bit rate";
			this.rbtnVBR.CheckedChanged += new System.EventHandler(this.rbtnCBR_CheckedChanged);
			//
			// vBitrate
			//
			this.vBitrate.Dock = System.Windows.Forms.DockStyle.Top;
			this.vBitrate.Location = new System.Drawing.Point(3, 64);
			this.vBitrate.Maximum = 3200;
			this.vBitrate.Minimum = 80;
			this.vBitrate.Name = "vBitrate";
			this.vBitrate.Size = new System.Drawing.Size(347, 42);
			this.vBitrate.TabIndex = 15;
			this.vBitrate.TickFrequency = 80;
			this.vBitrate.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.vBitrate.Value = 3200;
			this.vBitrate.ValueChanged += new System.EventHandler(this.vBitrate_ValueChanged);
			//
			// rbtnCBR
			//
			this.rbtnCBR.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnCBR.Location = new System.Drawing.Point(3, 40);
			this.rbtnCBR.Name = "rbtnCBR";
			this.rbtnCBR.Size = new System.Drawing.Size(347, 24);
			this.rbtnCBR.TabIndex = 13;
			this.rbtnCBR.Text = "Constant bit rate";
			this.rbtnCBR.CheckedChanged += new System.EventHandler(this.rbtnCBR_CheckedChanged);
			//
			// rbtnABR
			//
			this.rbtnABR.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnABR.Location = new System.Drawing.Point(3, 16);
			this.rbtnABR.Name = "rbtnABR";
			this.rbtnABR.Size = new System.Drawing.Size(347, 24);
			this.rbtnABR.TabIndex = 16;
			this.rbtnABR.Text = "Average bit rate";
			this.rbtnABR.CheckedChanged += new System.EventHandler(this.rbtnCBR_CheckedChanged);
			//
			// groupBox3
			//
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                              | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.AutoSize = true;
			this.groupBox3.Controls.Add(this.lstProfile);
			this.groupBox3.Location = new System.Drawing.Point(138, 192);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(122, 40);
			this.groupBox3.TabIndex = 11;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "AAC Profile";
			//
			// lstProfile
			//
			this.lstProfile.Dock = System.Windows.Forms.DockStyle.Top;
			this.lstProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstProfile.FormattingEnabled = true;
			this.lstProfile.Location = new System.Drawing.Point(3, 16);
			this.lstProfile.Name = "lstProfile";
			this.lstProfile.Size = new System.Drawing.Size(116, 21);
			this.lstProfile.TabIndex = 0;
			//
			// cbxCreateHintTrack
			//
			this.cbxCreateHintTrack.AutoSize = true;
			this.cbxCreateHintTrack.Location = new System.Drawing.Point(266, 192);
			this.cbxCreateHintTrack.Name = "cbxCreateHintTrack";
			this.cbxCreateHintTrack.Size = new System.Drawing.Size(205, 17);
			this.cbxCreateHintTrack.TabIndex = 13;
			this.cbxCreateHintTrack.Text = "Create hint track (for streaming server)";
			//
			// cbxSSE2
			//
			//              this.cbxSSE2.AutoSize = true;
			//              this.cbxSSE2.Location = new System.Drawing.Point(266, 215);
			//              this.cbxSSE2.Name = "cbxSSE2";
			//              this.cbxSSE2.Size = new System.Drawing.Size(156, 17);
			//              this.cbxSSE2.TabIndex = 14;
			//              this.cbxSSE2.Text = "Use SSE CPU instructions";
			//
			// linkLabel1
			//
			this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 34);
			this.linkLabel1.Location = new System.Drawing.Point(1, 252);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(176, 13);
			this.linkLabel1.TabIndex = 15;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Tag = "http://www.nero.com/nerodigital/eng/Nero_Digital_Audio.html";
			this.linkLabel1.Text = "Get NeroDigital AAC encoder binary";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			//
			// EncoderConfigurationForm
			//
			this.AcceptButton = this.button1;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(494, 273);
			this.Controls.Add(this.linkLabel1);
			//              this.Controls.Add(this.cbxSSE2);
			this.Controls.Add(this.cbxCreateHintTrack);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "EncoderConfigurationForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "NeroDigital Audio Encoder Configuration";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.vQuality)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.vBitrate)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox3;
		public System.Windows.Forms.CheckBox cbxCreateHintTrack;
		public System.Windows.Forms.RadioButton rbtnVBR;
		public System.Windows.Forms.RadioButton rbtnCBR;
		public TrackBar vBitrate;
		public RadioButton rbtnABR;
		public TrackBar vQuality;
		//          public CheckBox cbxSSE2;
		private LinkLabel linkLabel1;
		public ComboBox lstProfile;
	}
}
