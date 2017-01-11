
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BeHappy.DSP.ConfigurationForms
{
	partial class ConfigureFormForNormalizeDSP
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
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(222, 49);
			this.button1.Name = "button1";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(298, 49);
			this.button2.Name = "button2";
			// 
			// trackBar1
			// 
			this.trackBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.trackBar1.Location = new System.Drawing.Point(0, 0);
			this.trackBar1.Maximum = 200;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(374, 42);
			this.trackBar1.TabIndex = 0;
			this.trackBar1.TickFrequency = 5;
			this.trackBar1.Value = 1;
			this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.Location = new System.Drawing.Point(4, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(180, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "label1";
			// 
			// ConfigureFormForNormalizeDSP
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(374, 74);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.trackBar1);
			this.Name = "ConfigureFormForNormalizeDSP";
			this.Controls.SetChildIndex(this.trackBar1, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.button1, 0);
			this.Controls.SetChildIndex(this.button2, 0);
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.ResumeLayout(false);

		}
		
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.IContainer components = null;
	}
}
