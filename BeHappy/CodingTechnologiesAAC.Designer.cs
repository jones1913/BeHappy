using System;
using System.Drawing;
using System.Windows.Forms;

namespace BeHappy.CodingTechnologiesAAC
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
        	this.label1 = new System.Windows.Forms.Label();
        	this.vBitrate = new System.Windows.Forms.TrackBar();
        	this.cbxMPEG4AAC = new System.Windows.Forms.CheckBox();
        	this.cbxPNS = new System.Windows.Forms.CheckBox();
        	this.cbxSPEECH = new System.Windows.Forms.CheckBox();
        	this.label2 = new System.Windows.Forms.Label();
        	this.lstProfile = new System.Windows.Forms.ComboBox();
        	this.lstChannelMode = new System.Windows.Forms.ComboBox();
        	this.label3 = new System.Windows.Forms.Label();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        	this.groupBox1.SuspendLayout();
        	((System.ComponentModel.ISupportInitialize)(this.vBitrate)).BeginInit();
        	this.SuspendLayout();
        	// 
        	// pictureBox1
        	// 
        	this.pictureBox1.BackColor = System.Drawing.Color.White;
        	this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
        	this.pictureBox1.Location = new System.Drawing.Point(2, 4);
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
        	this.button1.Location = new System.Drawing.Point(336, 161);
        	this.button1.Name = "button1";
        	this.button1.Size = new System.Drawing.Size(75, 23);
        	this.button1.TabIndex = 1;
        	this.button1.Text = "OK";
        	// 
        	// button2
        	// 
        	this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        	this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        	this.button2.Location = new System.Drawing.Point(416, 161);
        	this.button2.Name = "button2";
        	this.button2.Size = new System.Drawing.Size(75, 23);
        	this.button2.TabIndex = 2;
        	this.button2.Text = "Cancel";
        	// 
        	// groupBox1
        	// 
        	this.groupBox1.Controls.Add(this.label1);
        	this.groupBox1.Controls.Add(this.vBitrate);
        	this.groupBox1.Location = new System.Drawing.Point(136, 0);
        	this.groupBox1.Name = "groupBox1";
        	this.groupBox1.Size = new System.Drawing.Size(358, 105);
        	this.groupBox1.TabIndex = 9;
        	this.groupBox1.TabStop = false;
        	this.groupBox1.Text = "Bitrate management";
        	// 
        	// label1
        	// 
        	this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.label1.Location = new System.Drawing.Point(3, 16);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(352, 44);
        	this.label1.TabIndex = 19;
        	this.label1.Text = "label1";
        	// 
        	// vBitrate
        	// 
        	this.vBitrate.Dock = System.Windows.Forms.DockStyle.Bottom;
        	this.vBitrate.Location = new System.Drawing.Point(3, 60);
        	this.vBitrate.Maximum = 320;
        	this.vBitrate.Minimum = 8;
        	this.vBitrate.Name = "vBitrate";
        	this.vBitrate.Size = new System.Drawing.Size(352, 42);
        	this.vBitrate.TabIndex = 15;
        	this.vBitrate.TickFrequency = 8;
        	this.vBitrate.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
        	this.vBitrate.Value = 64;
        	this.vBitrate.ValueChanged += new System.EventHandler(this.vBitrate_ValueChanged);
        	// 
        	// cbxMPEG4AAC
        	// 
        	this.cbxMPEG4AAC.AutoSize = true;
        	this.cbxMPEG4AAC.Location = new System.Drawing.Point(181, 138);
        	this.cbxMPEG4AAC.Name = "cbxMPEG4AAC";
        	this.cbxMPEG4AAC.Size = new System.Drawing.Size(151, 17);
        	this.cbxMPEG4AAC.TabIndex = 19;
        	this.cbxMPEG4AAC.Text = "force MPEG-4 AAC stream";
        	// 
        	// cbxPNS
        	// 
        	this.cbxPNS.AutoSize = true;
        	this.cbxPNS.Location = new System.Drawing.Point(381, 138);
        	this.cbxPNS.Name = "cbxPNS";
        	this.cbxPNS.Size = new System.Drawing.Size(83, 17);
        	this.cbxPNS.TabIndex = 19;
        	this.cbxPNS.Text = "enable PNS";
        	// 
        	// cbxSPEECH
        	// 
        	this.cbxSPEECH.AutoSize = true;
        	this.cbxSPEECH.Location = new System.Drawing.Point(181, 161);
        	this.cbxSPEECH.Name = "cbxSPEECH";
        	this.cbxSPEECH.Size = new System.Drawing.Size(108, 17);
        	this.cbxSPEECH.TabIndex = 19;
        	this.cbxSPEECH.Text = "tune for SPEECH";
        	// 
        	// label2
        	// 
        	this.label2.AutoSize = true;
        	this.label2.Location = new System.Drawing.Point(139, 114);
        	this.label2.Name = "label2";
        	this.label2.Size = new System.Drawing.Size(36, 13);
        	this.label2.TabIndex = 20;
        	this.label2.Text = "Profile";
        	// 
        	// lstProfile
        	// 
        	this.lstProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.lstProfile.FormattingEnabled = true;
        	this.lstProfile.Location = new System.Drawing.Point(181, 111);
        	this.lstProfile.Name = "lstProfile";
        	this.lstProfile.Size = new System.Drawing.Size(100, 21);
        	this.lstProfile.TabIndex = 21;
        	// 
        	// lstChannelMode
        	// 
        	this.lstChannelMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.lstChannelMode.FormattingEnabled = true;
        	this.lstChannelMode.Location = new System.Drawing.Point(381, 111);
        	this.lstChannelMode.Name = "lstChannelMode";
        	this.lstChannelMode.Size = new System.Drawing.Size(110, 21);
        	this.lstChannelMode.TabIndex = 23;
        	// 
        	// label3
        	// 
        	this.label3.AutoSize = true;
        	this.label3.Location = new System.Drawing.Point(299, 114);
        	this.label3.Name = "label3";
        	this.label3.Size = new System.Drawing.Size(76, 13);
        	this.label3.TabIndex = 22;
        	this.label3.Text = "Channel Mode";
        	// 
        	// EncoderConfigurationForm
        	// 
        	this.AcceptButton = this.button1;
        	this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
        	this.CancelButton = this.button2;
        	this.ClientSize = new System.Drawing.Size(494, 187);
        	this.Controls.Add(this.lstChannelMode);
        	this.Controls.Add(this.label3);
        	this.Controls.Add(this.lstProfile);
        	this.Controls.Add(this.label2);
        	this.Controls.Add(this.button2);
        	this.Controls.Add(this.button1);
        	this.Controls.Add(this.cbxMPEG4AAC);
        	this.Controls.Add(this.cbxPNS);
        	this.Controls.Add(this.cbxSPEECH);
        	this.Controls.Add(this.groupBox1);
        	this.Controls.Add(this.pictureBox1);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        	this.Name = "EncoderConfigurationForm";
        	this.ShowInTaskbar = false;
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        	this.Text = "Coding Technologies AAC Encoder Configuration";
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        	this.groupBox1.ResumeLayout(false);
        	this.groupBox1.PerformLayout();
        	((System.ComponentModel.ISupportInitialize)(this.vBitrate)).EndInit();
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }
        
		private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        public TrackBar vBitrate;
        private Label label1;
        public CheckBox cbxMPEG4AAC;
//      public CheckBox cbxMP4mux;
        public CheckBox cbxPNS;
        public CheckBox cbxSPEECH;
        private Label label2;
        public ComboBox lstProfile;
        public ComboBox lstChannelMode;
        private Label label3;
	}
}
