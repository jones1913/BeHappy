
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BeHappy.DSP.ConfigurationForms
{
	partial class ConfigurationFormForMultiOption
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
			this.containerControl1 = new System.Windows.Forms.ContainerControl();
			this.panel1 = new System.Windows.Forms.Panel();
			this.logoBox = new System.Windows.Forms.PictureBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.containerControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(140, 160);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(220, 160);
			// 
			// containerControl1
			// 
			this.containerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			                                                                       | System.Windows.Forms.AnchorStyles.Left)
			                                                                      | System.Windows.Forms.AnchorStyles.Right)));
			this.containerControl1.Controls.Add(this.panel1);
			this.containerControl1.Controls.Add(this.logoBox);
			this.containerControl1.Location = new System.Drawing.Point(0, 0);
			this.containerControl1.Name = "containerControl1";
			this.containerControl1.Size = new System.Drawing.Size(298, 155);
			this.containerControl1.TabIndex = 2;
			this.containerControl1.Text = "containerControl1";
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(4);
			this.panel1.Size = new System.Drawing.Size(298, 155);
			this.panel1.TabIndex = 0;
			// 
			// logoBox
			// 
			this.logoBox.Dock = System.Windows.Forms.DockStyle.Left;
			this.logoBox.Location = new System.Drawing.Point(0, 0);
			this.logoBox.Name = "logoBox";
			this.logoBox.Size = new System.Drawing.Size(0, 155);
			this.logoBox.TabIndex = 1;
			this.logoBox.TabStop = false;
			// 
			// ConfigurationFormForMultiOption
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(298, 184);
			this.Controls.Add(this.containerControl1);
			this.Name = "ConfigurationFormForMultiOption";
			this.Controls.SetChildIndex(this.containerControl1, 0);
			this.Controls.SetChildIndex(this.button1, 0);
			this.Controls.SetChildIndex(this.button2, 0);
			this.containerControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
			this.ResumeLayout(false);
		}
		
		private ContainerControl containerControl1;
		private Panel panel1;
		private RadioButton[] radioButtons;
		private System.Windows.Forms.PictureBox logoBox;
		private ToolTip toolTip1;
		private IContainer components = null;
	}
}
