using System.Drawing;
using System.Windows.Forms;

namespace BeHappy.DSP.ConfigurationForms
{
	public partial class ConfigureFormForNormalizeDSP : BeHappy.ConfigurationFormBase
	{
		public ConfigureFormForNormalizeDSP()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
            Utils.ChangeFontRecursive(new Control[] { this },
				Utils.HasMono ? new Font(SystemFonts.MessageBoxFont.Name, 8) : SystemFonts.MessageBoxFont);
        }

		public int value {
			get {return trackBar1.Value;}
			set {trackBar1.Value = value;}
		}

		private void trackBar1_ValueChanged(object sender, System.EventArgs e)
		{
			label1.Text = string.Format("Normalize to {0}%", trackBar1.Value);
		}
	}
}
