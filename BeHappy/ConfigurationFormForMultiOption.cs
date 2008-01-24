using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BeHappy.DSP.ConfigurationForms
{
	internal class ConfigurationFormForMultiOption : ConfigurationFormBase
	{
		private ContainerControl containerControl1;
		private Panel panel1;
		private RadioButton[] radioButtons;
        private System.Windows.Forms.PictureBox logoBox;
        private ToolTip toolTip1;
		private IContainer components = null;

		public ConfigurationFormForMultiOption()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
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

		#region Designer generated code
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
		#endregion

		internal void Init(string title, byte[] logoBitmap,  BeHappy.Extensions.MultiOptionBase.Option[] options, int selectedIndex, int dialogWidth)
		{

            int minH=16;

			this.Text = title;

			this.panel1.SuspendLayout();

			if(logoBitmap!=null && logoBitmap.Length!=0)
			{
				using(Stream s = new MemoryStream(logoBitmap))
				{
					//Bitmap bmp =  new Bitmap(s);
                    Image bmp = Image.FromStream(s);
                    minH = bmp.Height + 4;
					logoBox.Image = bmp;
					logoBox.BorderStyle=BorderStyle.Fixed3D;
					logoBox.Width = bmp.Width+4;
					logoBox.SizeMode=PictureBoxSizeMode.CenterImage;
                    using (Bitmap b = new Bitmap(bmp))
                    {
                        logoBox.BackColor = b.GetPixel(0, 0);
                    }
				}
			}

			radioButtons = new RadioButton[options.Length];
			for(int i=options.Length; i>0;i-- )
			{
				RadioButton rb = new RadioButton();
				radioButtons[i-1]=rb;
				this.panel1.Controls.Add(rb);
				rb.TabIndex = i;//options.Length - i;
				rb.Dock=DockStyle.Top;
				rb.Text = options[i-1].ToString();
                rb.AutoSize = true;
                if (options[i-1].ToolTip != null && options[i-1].ToolTip.Length > 0)
                    this.toolTip1.SetToolTip(rb, options[i-1].ToolTip);
              
			}
			radioButtons[selectedIndex].Checked = true;
			this.panel1.ResumeLayout(true);
			panel1.TabIndex=0;
			button1.TabIndex=1;
			button2.TabIndex=2;
			this.ActiveControl=radioButtons[selectedIndex];
            if (radioButtons[radioButtons.Length - 1].Bottom < minH)
            {
                int bh = minH / radioButtons.Length;
                foreach (RadioButton r in radioButtons)
                {
                    r.AutoSize = false;
                    r.Height = bh;
                }
            }

            int btm = System.Math.Max(radioButtons[radioButtons.Length-1].Bottom,minH);
            int newH = btm +  (this.ClientSize.Height -  panel1.Height);
            newH = System.Math.Min( newH, 480);
            int wdt = this.ClientSize.Width;
            this.ClientSize = new Size(wdt, newH);
            this.Width = dialogWidth;

		}

		public int GetSelectedIndex()
		{
			for(int i=0; i<radioButtons.Length;i++ )
			{
				if(radioButtons[i].Checked)
					return i;
			}
			return 0;
		}
	}
}

