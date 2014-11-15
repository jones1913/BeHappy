using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BeHappy.Extensibility;
using System.Xml;
using System.Xml.Serialization;
using BeHappy;


namespace BeHappy.Aften
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

			//                lstBandwidth.Items.Add("Adaptive");
			//                for (int i = 0; i <= 60; ++i) lstBandwidth.Items.Add(i.ToString());
			lstDynRangeLevel.Items.AddRange(EnumProxy.CreateArray(typeof(Encoder.Config.DynRange)));

			lstCenterMixLevel.Items.AddRange(EnumProxy.CreateArray(typeof(Encoder.Config.CenterMix)));
			lstSurroundMixLevel.Items.AddRange(EnumProxy.CreateArray(typeof(Encoder.Config.SurrondMix)));
			lstDPL.Items.AddRange(EnumProxy.CreateArray(typeof(Encoder.Config.DolbySurround)));

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
			if (vBitrate.Value > 607) vBitrate.Value=640;
			else if (vBitrate.Value > 543) vBitrate.Value=576;
			else if (vBitrate.Value > 479) vBitrate.Value=512;
			else if (vBitrate.Value > 415) vBitrate.Value=448;
			else if (vBitrate.Value > 351) vBitrate.Value=384;
			else if (vBitrate.Value > 287) vBitrate.Value=320;
			else if (vBitrate.Value > 239) vBitrate.Value=256;
			else if (vBitrate.Value > 207) vBitrate.Value=224;
			else if (vBitrate.Value > 175) vBitrate.Value=192;
			else if (vBitrate.Value > 143) vBitrate.Value=160;
			else if (vBitrate.Value > 119) vBitrate.Value=128;
			else if (vBitrate.Value > 103) vBitrate.Value=112;
			else if (vBitrate.Value > 87) vBitrate.Value=96;
			else if (vBitrate.Value > 71) vBitrate.Value=80;
			else vBitrate.Value=64;
			rbtnCBR.Text = String.Format("Constant Bitrate @ {0} kbit/s", vBitrate.Value);
		}

		private void vQuality_ValueChanged(object sender, EventArgs e)
		{
			rbtnVBR.Text = String.Format("Variable Bitrate (Q={0}) ", vQuality.Value);
		}
	}

	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[XmlRoot("Aften", Namespace = Constants.DefaultXmlNamespace)]
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
				f.vBitrate.Value = Math.Max(Math.Min(m_config.Bitrate, f.vBitrate.Maximum), f.vBitrate.Minimum);
				f.vQuality.Value = m_config.Quality;
				//                    f.lstBandwidth.SelectedIndex = m_config.Bandwidth + 1;
				f.lstDynRangeLevel.SelectedItem = EnumProxy.Create(m_config.DynRangeLevel);
				f.lstDPL.SelectedItem = EnumProxy.Create(m_config.DolbySurroundMode);
				f.lstCenterMixLevel.SelectedItem = EnumProxy.Create(m_config.CenterMixLevel);
				f.lstSurroundMixLevel.SelectedItem = EnumProxy.Create(m_config.SurrondMixLevel);
				f.munDiaNorm.Value = m_config.DialogNormalization;
				f.cbxIndependentLR.Checked = m_config.IndependentLR;
				f.cbxReadToEndOfFile.Checked = m_config.ReadToEndOfFile;

				f.rbtnCBR.Checked=!(f.rbtnVBR.Checked = m_config.Mode == BitrateManagementMode.VBR);
				f.txtCLI.Text = m_config.CLI;

				if (f.ShowDialog(owner) == DialogResult.OK)
				{
					m_config.Bitrate = f.vBitrate.Value;
					m_config.Quality = f.vQuality.Value;

					if (f.rbtnCBR.Checked)
						m_config.Mode = BitrateManagementMode.CBR;
					else
						m_config.Mode = BitrateManagementMode.VBR;
					//                        m_config.Bandwidth = f.lstBandwidth.SelectedIndex - 1;
					m_config.DynRangeLevel = (Config.DynRange)(f.lstDynRangeLevel.SelectedItem as EnumProxy).RealValue;
					m_config.CenterMixLevel = (Config.CenterMix)(f.lstCenterMixLevel.SelectedItem as EnumProxy).RealValue;
					m_config.SurrondMixLevel = (Config.SurrondMix)(f.lstSurroundMixLevel.SelectedItem as EnumProxy).RealValue;
					m_config.DolbySurroundMode = (Config.DolbySurround)(f.lstDPL.SelectedItem as EnumProxy).RealValue;
					m_config.DialogNormalization = (int)f.munDiaNorm.Value;
					m_config.IndependentLR = f.cbxIndependentLR.Checked;
					m_config.ReadToEndOfFile = f.cbxReadToEndOfFile.Checked;

					m_config.CLI = f.txtCLI.Text;

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
			return "Aften AC3 " + m_config.GetDescription();
		}

		/// <summary>
		/// Return executable name
		/// example: oggenc.exe
		/// </summary>
		/// <returns>executable name</returns>
		public string GetExecutableName()
		{
			return "Aften.exe";
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
			return m_config.GetCommandLine() + " - \"{0}\"";
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

		private static string[] ac3ext ={ "ac3"};

		/// <summary>
		/// List of supported file extensions
		/// </summary>
		/// <returns>array of strings</returns>
		public string[] GetListOfSupportedExtensions()
		{
			return ac3ext;
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

		[XmlRoot("Aften.Configuration", Namespace = Constants.DefaultXmlNamespace)]
		public class Config
		{

			public enum DynRange
			{
				[EnumTitle("Film Light", ", DRC: FL")]
				_filmlig = 0,
				[EnumTitle("Film Standard", ", DRC: FS")]
				_filmsta = 1,
				[EnumTitle("Music Light", ", DRC: ML")]
				_musilig = 2,
				[EnumTitle("Music Standard", ", DRC: MS")]
				_musista = 3,
				[EnumTitle("Speech", ", DRC: Sp")]
				_speech0 = 4,
				[EnumTitle("None", ", DRC: None")]
				_none000 = 5
			}
			public enum CenterMix
			{
				[EnumTitle("-3.0 dB",null)]
				_3dot0dB = 0,
				[EnumTitle("-4.5 dB", ", CM:-4.5dB")]
				_4dot5dB = 1,
				[EnumTitle("-6.0 dB", ", CM:-6.0dB")]
				_6dot0dB = 2
			}
			public enum SurrondMix
			{
				[EnumTitle("-3.0 dB", null)]
				_3dB = 0,
				[EnumTitle("-6.0 dB", ", SM:-6.0dB")]
				_6dB = 1,
				[EnumTitle("0 dB", ", SM:0dB")]
				zero = 2
			}
			public enum DolbySurround
			{
				[EnumTitle("Not indicated", null)]
				NotIndicated = 0,
				[EnumTitle("Not DolbySurround Encoded", ", NDS")]
				NotDolbySurroundEncoded = 1,
				[EnumTitle("DolbySurround Encoded", ", DS")]
				DolbySurroundEncoded = 2
			}


			public int Quality;
			public int Bitrate;
			public BitrateManagementMode Mode;
			//                public int Bandwidth;
			public DynRange DynRangeLevel;
			public bool ReadToEndOfFile;
			public bool IndependentLR;
			public CenterMix CenterMixLevel;
			public SurrondMix SurrondMixLevel;
			public DolbySurround DolbySurroundMode;
			public int DialogNormalization;
			public string CLI;

			public Config()
			{
				Bitrate = 448;
				Quality = 200;
				Mode = BitrateManagementMode.CBR;
				//                    Bandwidth = -1;
				DynRangeLevel = DynRange._none000;
				ReadToEndOfFile = true;
				IndependentLR = true;
				CenterMixLevel = CenterMix._3dot0dB;
				SurrondMixLevel = SurrondMix._3dB;
				DolbySurroundMode = DolbySurround.NotIndicated;
				DialogNormalization = 31;
				CLI = "";
			}

			internal string GetDescription()
			{
				//                    string desc = (this.Mode==BitrateManagementMode.VBR?("VBR (Q=" + this.Quality + ")"):("CBR @ " + this.Bitrate + " kbps")) + (this.Bandwidth != -1 ? (", BW:" + this.Bandwidth) : string.Empty);
				string desc = (this.Mode==BitrateManagementMode.VBR?("VBR (Q=" + this.Quality + ")"):("CBR @ " + this.Bitrate + " kbps"));
				if (this.IndependentLR)
					desc += ", L+R";
				if (this.ReadToEndOfFile)
					desc += ", ReadToEof";
				//                    desc += string.Empty + EnumProxy.Create(this.SurrondMixLevel).Tag + EnumProxy.Create(this.CenterMixLevel).Tag + EnumProxy.Create(this.DolbySurroundMode).Tag;
				desc += string.Empty + EnumProxy.Create(this.DynRangeLevel).Tag + EnumProxy.Create(this.SurrondMixLevel).Tag + EnumProxy.Create(this.CenterMixLevel).Tag + EnumProxy.Create(this.DolbySurroundMode).Tag;
				return desc;
			}

			internal string GetCommandLine()
			{
				System.Text.StringBuilder sb = new System.Text.StringBuilder("-v 0 ");

				if (this.Mode == BitrateManagementMode.VBR)
					sb.Append("-q " + this.Quality);
				else
				{
					//                       if (this.Bitrate > 607) sb.Append("-b 640");
					//                       else if (this.Bitrate > 543) sb.Append("-b 576");
					//                       else if (this.Bitrate > 479) sb.Append("-b 512");
					//                       else if (this.Bitrate > 415) sb.Append("-b 448");
					//                       else if (this.Bitrate > 351) sb.Append("-b 384");
					//                       else if (this.Bitrate > 287) sb.Append("-b 320");
					//                       else if (this.Bitrate > 239) sb.Append("-b 256");
					//                       else if (this.Bitrate > 207) sb.Append("-b 224");
					//                       else if (this.Bitrate > 175) sb.Append("-b 192");
					//                       else if (this.Bitrate > 143) sb.Append("-b 160");
					//                       else if (this.Bitrate > 119) sb.Append("-b 128");
					//                       else if (this.Bitrate > 103) sb.Append("-b 112");
					//                       else if (this.Bitrate > 87) sb.Append("-b 96");
					//                       else if (this.Bitrate > 71) sb.Append("-b 80");
					//                       else sb.Append("-b 64");
					sb.Append("-b " + this.Bitrate);
				}

				//                    if (this.Bandwidth >= 0)
				//                        sb.Append("-w " + this.Bandwidth);

				sb.AppendFormat(
					//                        " -m {0} -readtoeof {1} -cmix {2} -smix {3} -dsur {4} -dnorm {5}",
					" -m {0} -readtoeof {1} -cmix {2} -smix {3} -dsur {4} -dnorm {5} -dynrng {6}",
					this.IndependentLR ? 0 : 1,
					this.ReadToEndOfFile ? 1 : 0,
					(int)this.CenterMixLevel,
					(int)this.SurrondMixLevel,
					(int)this.DolbySurroundMode,
					this.DialogNormalization,
					(int)this.DynRangeLevel);

				sb.Append(" ");
				sb.Append(this.CLI.Trim());

				return sb.ToString();
			}
		}
	}
}
