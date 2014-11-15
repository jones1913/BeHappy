using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BeHappy.Extensibility;
using System.Xml;
using System.Xml.Serialization;

namespace BeHappy.OggVorbis
{
	/// <summary>
	/// Summary description for OggVorbisConfigurationForm.
	/// </summary>
	internal partial class OggVorbisConfigurationForm : System.Windows.Forms.Form
    {
		public OggVorbisConfigurationForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            rbtnABR_CheckedChanged(null, null);
		}
		
		private double ApproximateBitrate;
		
		public Decimal Quality {
			get {Decimal quality = vQuality.Value;
				 quality/=100;
				 return quality;
			}
			set {vQuality.Value = (int)Math.Min(Math.Max(value * 100, vQuality.Minimum), vQuality.Maximum);}
		}

        private void rbtnABR_CheckedChanged(object sender, EventArgs e)
        {
            vBitrate.Enabled = !(vQuality.Enabled = rbtnVBR.Checked);
        }

        private void vBitrate_ValueChanged(object sender, EventArgs e)
        {

            rbtnABR.Text = string.Format("Average Bitrate @ {0} kbit/s", vBitrate.Value);
            if (Quality <= 4) ApproximateBitrate = (double)(Quality + 2)*16 + 32;
            if ((Quality > 4) && (Quality <= 8)) ApproximateBitrate = (double)(Quality - 4) * 32 + 128;
            if (Quality > 8 && Quality <= 9) ApproximateBitrate = (double)(Quality - 8) * 64 + 256;
            if (Quality > 9) ApproximateBitrate = (double)((Quality - 9)) * 179.8 + 320;
            rbtnVBR.Text = string.Format("Variable Bitrate Q={0} approx. {1} kb/s for stereo", Quality,ApproximateBitrate);

        }
	}



    [XmlRoot("OggVorbis", Namespace = Constants.DefaultXmlNamespace)]
    public sealed class Encoder : BeHappy.Extensibility.IAudioEncoder, BeHappy.Extensibility.ISupportConfiguration
    {
        [XmlRoot("OggVorbis.Configuration", Namespace = Constants.DefaultXmlNamespace)]
        public class Options
        {
            /// <summary>
            /// Specify quality, between -2 (very low) and 10 (very
            /// high), instead of specifying a particular bitrate.
            /// </summary>
            public decimal Quality = 3;

            public int Bitrate = 160;

            public bool VBR = true;

            public string CLI = "";
        }

        private Options m_options;
        private static string[] ext = new string[] { "ogg" };

        public Encoder()
        {
            ResetConfiguration();
        }

        /// <summary>
        /// Return executable name
        /// example: oggenc.exe
        /// </summary>
        /// <returns>executable name</returns>
        public string GetExecutableName()
        {
            return "oggenc2.exe";
        }

        /// <summary>
        /// Command line arguments, to be passed to encoder
        /// {0} means output file name
        /// {1} means samplerate in Hz
        /// {2} means bits per sample
        /// {3} means channel count
        /// {4} means samplecount
        /// {5} means size in bytes
        /// {6} means format tag (1 = int, 3 = float)
        /// </summary>
        /// <returns>arguments</returns>
        public string GetCommandLineArguments(string targetFileExtension)
        {
            if(m_options.VBR)
                return ("-Q --raw --raw-format={6} --raw-bits={2} --raw-chan={3} --raw-rate={1} --quality " + m_options.Quality.ToString(System.Globalization.CultureInfo.InvariantCulture) + " " + m_options.CLI).Trim() + " -o \"{0}\" -";
// not raw                return ("-Q --quality " + m_options.Quality.ToString(System.Globalization.CultureInfo.InvariantCulture) + " " + m_options.CLI).Trim() + " -o \"{0}\" -";
            else
                return ("-Q --raw --raw-format={6} --raw-bits={2} --raw-chan={3} --raw-rate={1} --bitrate " + m_options.Bitrate.ToString(System.Globalization.CultureInfo.InvariantCulture) + " " + m_options.CLI).Trim() + " -o \"{0}\" -";
// not raw                return ("-Q --bitrate " + m_options.Bitrate.ToString(System.Globalization.CultureInfo.InvariantCulture) + " " + m_options.CLI).Trim() + " -o \"{0}\" -";
        }

        /// <summary>
        /// If true BeHappy will send RIFF WAV header to encoder's stdin
        /// otherwize BeHappy will send just raw pcm data
        /// </summary>
        /// <returns></returns>
        public bool MustSendRiffHeader()
        {
            return false;
// not raw            return true;
        }

        /// <summary>
        /// List of supported file extensions
        /// </summary>
        /// <returns>array of strings</returns>
        public string[] GetListOfSupportedExtensions()
        {
            return ext;
        }

        /// <summary>
        /// String used for representation in GUI
        /// </summary>
        /// <returns>Title</returns>
        public string GetTitle()
        {
            if(m_options.VBR)
                return string.Format("OggVorbis, VBR (Q={0})", m_options.Quality);
            else
                return string.Format("OggVorbis, ABR @ {0} kbit/s", m_options.Bitrate);
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
            return "6==AudioChannels(last)?GetChannel(last,1,3,2,5,6,4):last";
        }

        /// <summary>
        /// Show configuration GUI
        /// </summary>
        /// <returns>Configuration Result</returns>
        public ConfigurationResult Configure(IWin32Window owner)
        {
            using (OggVorbisConfigurationForm f = new OggVorbisConfigurationForm())
            {
                f.Quality = m_options.Quality;
                f.vBitrate.Value = m_options.Bitrate;
                f.rbtnABR.Checked = !(f.rbtnVBR.Checked = m_options.VBR);
                f.txtCLI.Text = m_options.CLI.Trim();
                if (f.ShowDialog(owner) == DialogResult.OK)
                {
                    m_options.Quality = f.Quality;
                    m_options.Bitrate = f.vBitrate.Value;
                    m_options.VBR = f.rbtnVBR.Checked;
                    m_options.CLI = f.txtCLI.Text.Trim();
                    return ConfigurationResult.OK;
                }
                else
                    return ConfigurationResult.Cancel;
            }
        }

        /// <summary>
        /// Must save configuration to XmlElement
        /// </summary>
        /// <returns>Persisted settings in XML form</returns>
        public XmlElement SaveConfiguration()
        {
            return Utility.SerializeObject(m_options);
        }

        /// <summary>
        /// Must load configuration from XmlElement
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public void LoadConfiguration(XmlElement configuration)
        {
            m_options = (Options)Utility.DeSerializeObject(m_options.GetType(), configuration);
        }

        /// <summary>
        ///  Must reset configuration to defaults
        /// </summary>
        public void ResetConfiguration()
        {
            m_options = new Options();
        }
    }
}
