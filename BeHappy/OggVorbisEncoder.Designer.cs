
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BeHappy.OggVorbis
{
	partial class OggVorbisConfigurationForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OggVorbisConfigurationForm));
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.vQuality = new System.Windows.Forms.TrackBar();
			this.rbtnVBR = new System.Windows.Forms.RadioButton();
			this.vBitrate = new System.Windows.Forms.TrackBar();
			this.rbtnABR = new System.Windows.Forms.RadioButton();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtCLI = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.vQuality)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.vBitrate)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			//
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(336, 217);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "OK";
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(416, 217);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 3;
			this.button2.Text = "Cancel";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.White;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(4, 7);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(128, 96);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                              | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.AutoSize = true;
			this.groupBox1.Controls.Add(this.vQuality);
			this.groupBox1.Controls.Add(this.rbtnVBR);
			this.groupBox1.Controls.Add(this.vBitrate);
			this.groupBox1.Controls.Add(this.rbtnABR);
			this.groupBox1.Location = new System.Drawing.Point(138, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(353, 183);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Bitrate management";
			// 
			// vQuality
			// 
			this.vQuality.Dock = System.Windows.Forms.DockStyle.Top;
			this.vQuality.Location = new System.Drawing.Point(3, 122);
			this.vQuality.Maximum = 1000;
			this.vQuality.Minimum = -200;
			this.vQuality.Name = "vQuality";
			this.vQuality.Size = new System.Drawing.Size(347, 58);
			this.vQuality.TabIndex = 17;
			this.vQuality.TickFrequency = 20;
			this.vQuality.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.vQuality.Value = 6;
			this.vQuality.ValueChanged += new System.EventHandler(this.vBitrate_ValueChanged);
			// 
			// rbtnVBR
			// 
			this.rbtnVBR.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnVBR.Location = new System.Drawing.Point(3, 98);
			this.rbtnVBR.Name = "rbtnVBR";
			this.rbtnVBR.Size = new System.Drawing.Size(347, 24);
			this.rbtnVBR.TabIndex = 14;
			this.rbtnVBR.Text = "Variable bit rate";
			this.rbtnVBR.CheckedChanged += new System.EventHandler(this.rbtnABR_CheckedChanged);
			// 
			// vBitrate
			// 
			this.vBitrate.Dock = System.Windows.Forms.DockStyle.Top;
			this.vBitrate.Location = new System.Drawing.Point(3, 40);
			this.vBitrate.Maximum = 496;
			this.vBitrate.Minimum = 8;
			this.vBitrate.Name = "vBitrate";
			this.vBitrate.Size = new System.Drawing.Size(347, 58);
			this.vBitrate.TabIndex = 15;
			this.vBitrate.TickFrequency = 8;
			this.vBitrate.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.vBitrate.Value = 12;
			this.vBitrate.ValueChanged += new System.EventHandler(this.vBitrate_ValueChanged);
			// 
			// rbtnABR
			// 
			this.rbtnABR.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnABR.Location = new System.Drawing.Point(3, 16);
			this.rbtnABR.Name = "rbtnABR";
			this.rbtnABR.Size = new System.Drawing.Size(347, 24);
			this.rbtnABR.TabIndex = 16;
			this.rbtnABR.Text = "Average bit rate";
			this.rbtnABR.CheckedChanged += new System.EventHandler(this.rbtnABR_CheckedChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                              | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.txtCLI);
			this.groupBox3.Location = new System.Drawing.Point(138, 166);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(353, 46);
			this.groupBox3.TabIndex = 12;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Additional CLI arguments";
			// 
			// txtCLI
			// 
			this.txtCLI.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtCLI.Location = new System.Drawing.Point(3, 16);
			this.txtCLI.Name = "txtCLI";
			this.txtCLI.Size = new System.Drawing.Size(347, 20);
			this.txtCLI.TabIndex = 0;
			// 
			// OggVorbisConfigurationForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(494, 243);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "OggVorbisConfigurationForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "OggVorbis Encoder Configuration";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.vQuality)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.vBitrate)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private PictureBox pictureBox1;
		private GroupBox groupBox1;
		public TrackBar vQuality;
		public RadioButton rbtnVBR;
		public TrackBar vBitrate;
		public RadioButton rbtnABR;
		private GroupBox groupBox3;
		public TextBox txtCLI;
	}
}
