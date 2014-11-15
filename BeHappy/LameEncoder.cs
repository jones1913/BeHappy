using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BeHappy.Extensibility;
using System.Xml;
using System.Xml.Serialization;


namespace BeHappy.LameMP3
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

            vBitrate_ValueChanged(null, null);
            vQuality_ValueChanged(null, null);
            rbtnCBR_CheckedChanged(null, null);
        }

        private void rbtnCBR_CheckedChanged(object sender, System.EventArgs e)
        {
            vQuality.Enabled = !(vBitrate.Enabled = !rbtnVBR.Checked);
            cbxUseNewVbr.Enabled = rbtnVBR.Checked;
        }

        private void vBitrate_ValueChanged(object sender, EventArgs e)
        {
            rbtnABR.Text = String.Format("Average Bitrate @ {0} kbit/s", vBitrate.Value);
            rbtnCBR.Text = String.Format("Constant Bitrate @ {0} kbit/s", Encoder.Config.NormalizeBitrate( vBitrate.Value));
        }

        private void vQuality_ValueChanged(object sender, EventArgs e)
        {
            rbtnVBR.Text = String.Format("Variable Bitrate (Q={0}) ", 9 - vQuality.Value);
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
    [XmlRoot("Lame", Namespace = Constants.DefaultXmlNamespace)]
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
                f.vQuality.Value = 9-m_config.Quality;

                f.rbtnVBR.Checked = m_config.Mode == BitrateManagementMode.VBR;
                f.rbtnCBR.Checked = m_config.Mode == BitrateManagementMode.CBR;
                f.rbtnABR.Checked = m_config.Mode == BitrateManagementMode.ABR;

                f.cbxUseNewVbr.Checked = m_config.UseVbrNew;

//              f.cbxStrictISO.Checked = m_config.StrictISO;

                f.txtCLI.Text = m_config.CLI;

                if (f.ShowDialog(owner) == DialogResult.OK)
                {
                    m_config.Bitrate = f.vBitrate.Value;
                    m_config.Quality = 9-f.vQuality.Value;

                    m_config.UseVbrNew = f.cbxUseNewVbr.Checked;

                    if (f.rbtnABR.Checked) m_config.Mode = BitrateManagementMode.ABR;
                    if (f.rbtnCBR.Checked) m_config.Mode = BitrateManagementMode.CBR;
                    if (f.rbtnVBR.Checked) m_config.Mode = BitrateManagementMode.VBR;

//                  m_config.StrictISO = f.cbxStrictISO.Checked;

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
            return "Lame MP3 " + m_config.GetDescription();
        }

        /// <summary>
        /// Return executable name
        /// example: oggenc.exe
        /// </summary>
        /// <returns>executable name</returns>
        public string GetExecutableName()
        {
            return "lame.exe";
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
            return m_config.GetCommandLine() + " --brief - \"{0}\"";
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

        private static readonly string[] MP3Extensions = new string[] { "mp3" };

        /// <summary>
        /// List of supported file extensions
        /// </summary>
        /// <returns>array of strings</returns>
        public string[] GetListOfSupportedExtensions()
        {
            return MP3Extensions;
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
// Better a error than a wrong result when multichannel
//            return "2==AudioChannels(last)?last:ConvertToMono(last)";
            return "32==Audiobits(last)?ConvertAudioTo32bit(last):last";
        }

        /// <summary>
        ///  Must reset configuration to defaults
        /// </summary>
        public void ResetConfiguration()
        {
            m_config = new Config();
        }

        private Config m_config;

        [XmlRoot("Lame.Configuration", Namespace = Constants.DefaultXmlNamespace)]
        public class Config
        {
            private static readonly int[] bitrates = new int[] { 8, 16, 24, 32, 40, 48, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320 };

            internal static int NormalizeBitrate(int n)
            {
                int delta = int.MaxValue;
                int last = n;
                foreach (int i in bitrates)
                {
                    int thisDelta = Math.Abs(n - i);
                    if (thisDelta < delta)
                    {
                        delta = thisDelta;
                        last = i;
                    }
                }
                return last;
            }

            public int Bitrate;
            public BitrateManagementMode Mode;
            public int Quality;
            public bool UseVbrNew;
//          public bool StrictISO;
            public string CLI;

            public Config()
            {
                Bitrate = 128;
                Quality = 4;
                Mode = BitrateManagementMode.VBR;
                UseVbrNew = true;
//              StrictISO = true;
                CLI = "-h";
            }

            internal string GetDescription()
            {
                string encoder = ", ";
                switch (Mode)
                {
                    case BitrateManagementMode.VBR:
                        encoder += string.Format("VBR {0}", Quality);
                        if (UseVbrNew) encoder += " (vbr-new)";
                        else encoder += " (vbr-old)";
//                      if (StrictISO)
//                          encoder += ", ISO";
                        break;
                    case BitrateManagementMode.ABR:
                        encoder += string.Format("ABR @ {0} kbit/s", Bitrate);
                        break;
                    case BitrateManagementMode.CBR:
                        encoder += string.Format("CBR @ {0} kbit/s", NormalizeBitrate(Bitrate));
                        break;
                }
                return encoder;
            }

            internal string GetCommandLine()
            {

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                switch (this.Mode)
                {
                    case BitrateManagementMode.ABR:
                        sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "--abr {0}  --nohist", this.Bitrate);
                        break;
                    case BitrateManagementMode.CBR:
                        sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "-b {0} ", NormalizeBitrate(this.Bitrate));
                        break;
                    case BitrateManagementMode.VBR:
                        sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, " -v -V {0} --nohist", this.Quality);
                        if (this.UseVbrNew) sb.Append(" --vbr-new");
                        else sb.Append(" --vbr-old");
                        break;
                }

                sb.Append(" ");
                sb.Append(this.CLI.Trim());

                return sb.ToString().Trim();
            }
        }
    }
}
