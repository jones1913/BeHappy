using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BeHappy
{
	/// <summary>
	/// Summary description for ConfigurationFormBase.
	/// </summary>
	public partial class ConfigurationFormBase : System.Windows.Forms.Form
	{
		public ConfigurationFormBase()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            int x;
            x = (this.button2.Right - this.button1.Left ) + 3*(this.ClientSize.Width-this.button2.Right) + 2*(this.Width-this.ClientSize.Width);
            this.MinimumSize = new Size(x, this.MinimumSize.Height);
		}

	}
}
