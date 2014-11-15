using System.ComponentModel;
using System.Drawing;
using System.IO;
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

