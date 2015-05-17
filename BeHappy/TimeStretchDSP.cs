using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BeHappy.Extensibility;
using System.Xml.Serialization;

namespace BeHappy.TimeStretch
{
	internal partial class ConfigurationDialog : BeHappy.ConfigurationFormBase
	{
		public ConfigurationDialog()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// init default
			m_strTimeStretchMethod = "tempo";

			rbtnFrameRate_CheckedChanged(null, null);
		}

		public string m_strTimeStretchMethod;
		
		private void rbtnFrameRate_CheckedChanged(object sender, EventArgs e)
		{
			numCustom.Enabled = numCustomFrom.Enabled = numCustomTo.Enabled = linkLblCalc.Enabled = !(numRateFrom.Enabled = (numRateTo.Enabled = rbtnFrameRate.Checked));
		}

		private void rdoCtlRate_CheckedChanged(object sender, EventArgs e)
		{
			/*
                AVISynth docs define the following:
                Sound TEMPO can be increased or decreased while maintaining the original pitch.
                Sound PITCH can be increased or decreased while maintaining the original tempo.
                Change playback RATE that affects both tempo and pitch at the same time.

            (1) Tempo changed, pitch correction.
                (hint: changes the length of the audio track while preserving the original pitch)
            (2) Pitch changed preserving tempo.
                (hint: manipulates pitch, but Track length will kept unchanged)
            (3) Rate, tempo and no pitch correction.
                (hint: changes the length of the audio track without preserving the original pitch )
			 */
			
			if (rdoCtlTempo.Checked == true)
				m_strTimeStretchMethod = "tempo";
			else if (rdoCtlRate.Checked == true)
				m_strTimeStretchMethod = "rate";
			else
				m_strTimeStretchMethod = "pitch";
		}
		
		void LinkLblCalcLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			errorProvider1.SetError(numCustom, string.Empty);
			decimal cval = 0;
			try{
				cval = (numCustomFrom.Value * 100) / numCustomTo.Value;
				numCustom.Value = cval;
			} catch (Exception) {
				errorProvider1.SetError(numCustom, string.Format("The value for custom time must be between {0} and {1}!\n\nYour result is: {2}", numCustom.Minimum, numCustom.Maximum, cval));
			}
		}
	}

	[XmlRoot("TimeStretchDSP", Namespace = Constants.DefaultXmlNamespace)]
	public sealed class DSP : IDigitalSignalProcessor, ISupportConfiguration
	{
		[XmlRoot("TimeStretchDSP.Configuration", Namespace = Constants.DefaultXmlNamespace)]
		public sealed class Config
		{
			public enum Mode
			{
				FrameRate,
				Custom
			}

			public bool Custom = false;
			public float FromRate = 25.0F;
			public float ToRate = 23.976F;
			public float Tempo = 105F;
			public string Control = "tempo";

			internal float ActualTempo {
				get {
					return Custom ? Tempo : (100.0F * ToRate) / FromRate;
				}
			}

			internal string Title {
				get {
					return Custom ? ("Custom: " + Tempo + "%") : (FromRate + " -> " + ToRate);
				}
			}
		}

		private Config c;

		public DSP()
		{
			(this as ISupportConfiguration).ResetConfiguration();
		}

		#region IExtensionItemCommon Members

		string IExtensionItemCommon.GetTitle()
		{
			return "TimeStretch - " + this.c.Title;
		}

		string IExtensionItemCommon.GetScript()
		{
			return string.Format(System.Globalization.CultureInfo.InvariantCulture,
			                     "TimeStretchPlugin(last, {0}={1})", this.c.Control, this.c.ActualTempo);
		}
		
		string IExtensionItemCommon.GetAvsPlugin()
		{
			return "TimeStretch.dll";
		}

		#endregion

		#region ISupportConfiguration Members

		ConfigurationResult ISupportConfiguration.Configure(IWin32Window owner)
		{
			using (ConfigurationDialog f = new ConfigurationDialog())
			{
				f.numCustom.Value = (decimal)this.c.Tempo;
				f.numRateFrom.Value = (decimal)this.c.FromRate;
				f.numRateTo.Value = (decimal)this.c.ToRate;
				f.rdoCtlTempo.Checked = true;
				f.rbtnFrameRate.Checked = !(f.rbtnCustom.Checked = this.c.Custom);

				f.rdoCtlRate.Checked = (this.c.Control == "rate");
				f.rdoCtlPitch.Checked = (this.c.Control == "pitch");
				f.rdoCtlTempo.Checked = (this.c.Control == "tempo");

				if (f.ShowDialog(owner) != DialogResult.OK)
					return ConfigurationResult.Cancel;
				this.c.Tempo = (float)f.numCustom.Value;
				this.c.FromRate = (float)f.numRateFrom.Value;
				this.c.ToRate = (float)f.numRateTo.Value;
				this.c.Custom = f.rbtnCustom.Checked;
				this.c.Control = f.m_strTimeStretchMethod;
				return ConfigurationResult.OK;
			}
		}

		System.Xml.XmlElement ISupportConfiguration.SaveConfiguration()
		{
			return Utility.SerializeObject(this.c);
		}

		void ISupportConfiguration.LoadConfiguration(System.Xml.XmlElement configuration)
		{
			this.c = (Config)Utility.DeSerializeObject(this.c.GetType(), configuration);
		}

		void ISupportConfiguration.ResetConfiguration()
		{
			this.c = new Config();
		}

		#endregion
	}
}

