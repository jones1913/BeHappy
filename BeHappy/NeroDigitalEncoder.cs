using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BeHappy.Extensibility;
using System.Xml;
using System.Xml.Serialization;
using BeHappy;


namespace BeHappy
{
	public enum BitrateManagementMode
	{
		ABR,
		CBR,
		VBR
	}
	public enum AacProfile
	{
		[EnumTitle("Automatic", "AAC")]
		Auto,
		[EnumTitle("HE-AAC+PS", "HE-AAC+PS")]
		PS,
		[EnumTitle("HE-AAC", "HE-AAC")]
		HE,
		[EnumTitle("LC-AAC", "LC-AAC")]
		LC,
		[EnumTitle("HE-AAC High", "HE-AAC High")]
		High
	}
}

namespace BeHappy.NeroDigitalAAC
{
	/// <summary>
	/// Summary description for EncoderConfigurationForm.
	/// </summary>
	internal partial class EncoderConfigurationForm : System.Windows.Forms.Form
	{
		public EncoderConfigurationForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			lstProfile.Items.AddRange(EnumProxy.CreateArray(new AacProfile[] { AacProfile.Auto, AacProfile.PS, AacProfile.HE, AacProfile.LC }));

			vBitrate_ValueChanged(null, null);
			vQuality_ValueChanged(null, null);
			rbtnCBR_CheckedChanged(null, null);
		}

		private void rbtnCBR_CheckedChanged(object sender, System.EventArgs e)
		{
			vQuality.Enabled = !(vBitrate.Enabled = !rbtnVBR.Checked);
		}

		private void vBitrate_ValueChanged(object sender, EventArgs e)
		{
			rbtnABR.Text = String.Format("Average Bitrate @ {0} kbit/s", ((float)vBitrate.Value) / 10);
			rbtnCBR.Text = String.Format("Constant Bitrate @ {0} kbit/s", ((float)vBitrate.Value) / 10);
		}

		private void vQuality_ValueChanged(object sender, EventArgs e)
		{
			Decimal q = ((Decimal)vQuality.Value) / vQuality.Maximum;
			rbtnVBR.Text = String.Format("Variable Bitrate (Q={0}) ", q);
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				System.Diagnostics.Process.Start((sender as Control).Tag.ToString());
			else
				MessageBox.Show((sender as Control).Tag.ToString());
		}
	}


	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[XmlRoot("NeroDigitalAAC", Namespace = Constants.DefaultXmlNamespace)]
	public sealed class Encoder : IAudioEncoder, ISupportConfiguration
	{
		/// <summary>
		/// Show configuration GUI
		/// </summary>
		/// <returns>Configuration Result</returns>
		public ConfigurationResult Configure(System.Windows.Forms.IWin32Window owner)
		{
			using (EncoderConfigurationForm f = new EncoderConfigurationForm())
			{
				f.vBitrate.Value = Math.Max(Math.Min(m_config.Bitrate / 100, f.vBitrate.Maximum), f.vBitrate.Minimum);
				f.vQuality.Value = (int)(m_config.Quality * (Decimal)f.vQuality.Maximum);

				f.rbtnVBR.Checked = m_config.Mode == BitrateManagementMode.VBR;
				f.rbtnCBR.Checked = m_config.Mode == BitrateManagementMode.CBR;
				f.rbtnABR.Checked = m_config.Mode == BitrateManagementMode.ABR;

				f.cbxCreateHintTrack.Checked = m_config.CreateHintTrack;

				f.lstProfile.SelectedItem = EnumProxy.Create(m_config.Profile);

				if (f.ShowDialog(owner) == DialogResult.OK)
				{
					m_config.Bitrate = f.vBitrate.Value * 100;
					m_config.Quality = (Decimal)f.vQuality.Value / f.vQuality.Maximum;

					m_config.CreateHintTrack = f.cbxCreateHintTrack.Checked;

					if (f.rbtnABR.Checked) m_config.Mode = BitrateManagementMode.ABR;
					if (f.rbtnCBR.Checked) m_config.Mode = BitrateManagementMode.CBR;
					if (f.rbtnVBR.Checked) m_config.Mode = BitrateManagementMode.VBR;

					m_config.Profile = (AacProfile)(f.lstProfile.SelectedItem as EnumProxy).RealValue;

					//                      m_config.SSE2 = f.cbxSSE2.Checked;

					return ConfigurationResult.OK;
				}
				else
				{
					return ConfigurationResult.Cancel;
				}
			}
		}

		public Encoder()
		{
			ResetConfiguration();
		}


		/// <summary>
		/// Must save configuration to XmlElement
		/// </summary>
		/// <returns>Persisted settings in XML form</returns>
		public XmlElement SaveConfiguration()
		{
			return Utility.SerializeObject(m_config);
		}

		/// <summary>
		/// Must load configuration from XmlElement
		/// </summary>
		/// <param name="configuration">Configuration</param>
		public void LoadConfiguration(XmlElement configuration)
		{
			m_config = (Config)Utility.DeSerializeObject(typeof(Config), configuration);
		}

		/// <summary>
		/// String used for representation in GUI
		/// </summary>
		/// <returns>Title</returns>
		public string GetTitle()
		{
			return "NeroDigital " + m_config.GetDescription();
		}

		/// <summary>
		/// Return executable name
		/// example: oggenc.exe
		/// </summary>
		/// <returns>executable name</returns>
		public string GetExecutableName()
		{
			return "neroAacEnc.exe";
		}

		/// <summary>
		/// Command line arguments, to be passed to encoder
		/// {0} means output file name
		/// {1} means samplerate in Hz
		/// {2} means bits per sample
		/// {3} means channel count
		/// {4} means samplecount
		/// {5} means size in bytes
		/// </summary>
		/// <returns>arguments</returns>
		public string GetCommandLineArguments(string targetFileExtension)
		{
			return m_config.GetCommandLine() + " -if - -of \"{0}\"";
		}

		/// <summary>
		/// If true BeHappy will send RIFF WAV header to encoder's stdin
		/// otherwize BeHappy will send just raw pcm data
		/// </summary>
		/// <returns></returns>
		public bool MustSendRiffHeader()
		{
			return true;
		}

		private static readonly string[] MP4PREF = new string[]{ "mp4", "m4a" };
		private static readonly string[] M4APREF = new string[]{ "m4a", "mp4" };
		public static bool preferMP4overM4A = true;
		public static string[] MP4Extensions
		{
			get
			{
				if (preferMP4overM4A)
					return MP4PREF;
				else
					return M4APREF;
			}
		}

		/// <summary>
		/// List of supported file extensions
		/// </summary>
		/// <returns>array of strings</returns>
		public string[] GetListOfSupportedExtensions()
		{
			return MP4Extensions;
		}

		/// <summary>
		/// A part of AviSynth script
		/// {0} means input file name
		/// {1} means output file name
		/// {2} means unique string (to use as part of identifier)
		/// {3} means '{' character (to allow '{' to be used)
		/// </summary>
		/// <returns>AviSynth script block</returns>
		public string GetScript()
		{
			return null;
		}

		/// <summary>
		///  Must reset configuration to defaults
		/// </summary>
		public void ResetConfiguration()
		{
			m_config = new Config();
		}

		private Config m_config;

		[XmlRoot("NeroDigitalAAC.Configuration", Namespace = Constants.DefaultXmlNamespace)]
		public class Config
		{
			public int Bitrate;
			public AacProfile Profile;
			public BitrateManagementMode Mode;
			public Decimal Quality;
			public bool CreateHintTrack;
			//              public bool SSE2;

			public Config()
			{
				Bitrate = 64000;
				Quality = .3M;
				Profile = AacProfile.Auto;
				Mode = BitrateManagementMode.VBR;
				CreateHintTrack = false;
				//                  SSE2 = false;
			}

			internal string GetDescription()
			{
				string encoder = EnumProxy.Create(this.Profile).Tag.ToString();
				encoder += ", ";
				switch (Mode)
				{
					case BitrateManagementMode.VBR:
						encoder += string.Format("VBR (Q={0})", Quality);
						break;
					case BitrateManagementMode.ABR:
						encoder += string.Format("ABR @ {0} kbit/s", ((float)Bitrate) / 1000.0);
						break;
					case BitrateManagementMode.CBR:
						encoder += string.Format("CBR @ {0} kbit/s", ((float)Bitrate) / 1000.0);
						break;
				}
				if (CreateHintTrack)
					encoder += ", Hint";
				//                  if (SSE2)
				//                      encoder += ", SSE";
				return encoder;
			}

			internal string GetCommandLine()
			{

				System.Text.StringBuilder sb = new System.Text.StringBuilder("-ignorelength ");
				switch (this.Profile)
				{
					case AacProfile.HE:
						sb.Append("-he ");
						break;
					case AacProfile.PS:
						sb.Append("-hev2 ");
						break;
					case AacProfile.LC:
						sb.Append("-lc ");
						break;
				}
				if (this.CreateHintTrack)
					sb.Append("-hinttrack ");

				switch (this.Mode)
				{
					case BitrateManagementMode.ABR:
						sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "-br {0} ", this.Bitrate);
						break;
					case BitrateManagementMode.CBR:
						sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "-cbr {0} ", this.Bitrate);
						break;
					case BitrateManagementMode.VBR:
						sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "-q {0} ", this.Quality);
						break;
				}

				return sb.ToString();
			}
		}
	}
}
