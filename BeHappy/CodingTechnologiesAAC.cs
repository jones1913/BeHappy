using System;
using System.Collections;
using System.ComponentModel;
// using System.Diagnostics;
using System.Drawing;
using System.IO;              // permite Directory
// using System.Text;
// using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using BeHappy.Extensibility;
// using BeHappy.Extensions;


namespace BeHappy.CodingTechnologiesAAC
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

            lstProfile.Items.AddRange(EnumProxy.CreateArray(new AacProfile[] { AacProfile.PS, AacProfile.HE, AacProfile.LC, AacProfile.High }));
            lstChannelMode.Items.AddRange(EnumProxy.CreateArray(typeof(Encoder.Config.AacStereoMode)));

            vBitrate_ValueChanged(null, null);
        }

        private void vBitrate_ValueChanged(object sender, EventArgs e)
        {
            this.label1.Text = "CBR @ " + vBitrate.Value + " kbit/s";
        }

    }

    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    [XmlRoot("CodingTechnologiesAAC", Namespace = Constants.DefaultXmlNamespace)]
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
                f.cbxMPEG4AAC.Checked = m_config.MPEG4;
//              f.cbxMP4mux.Checked = m_config.MPMUX;
                f.cbxPNS.Checked = m_config.PNS;
                f.cbxSPEECH.Checked = m_config.SPEECH;
                f.lstProfile.SelectedItem = EnumProxy.Create(m_config.Profile);
                f.lstChannelMode.SelectedItem = EnumProxy.Create(m_config.ChannelMode);


                if (f.ShowDialog(owner) == DialogResult.OK)
                {
                    m_config.Bitrate = f.vBitrate.Value;
                    m_config.Profile = (AacProfile)(f.lstProfile.SelectedItem as EnumProxy).RealValue;
                    m_config.ChannelMode = (Config.AacStereoMode)(f.lstChannelMode.SelectedItem as EnumProxy).RealValue;
                    m_config.MPEG4 = f.cbxMPEG4AAC.Checked;
//                  m_config.MPMUX = f.cbxMP4mux.Checked;
                    m_config.PNS = f.cbxPNS.Checked;
                    m_config.SPEECH = f.cbxSPEECH.Checked;


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
            return "CT " + m_config.GetDescription(); //Coding Technologies
        }

        /// <summary>
        /// Return executable name
        /// example: oggenc.exe
        /// </summary>
        /// <returns>executable name</returns>
        public string GetExecutableName()
        {
            return "enc_aacPlus.exe";
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
//            bool aac = ".aac" == targetFileExtension;
//            return "- \"{0}\" --rawpcm {1} {3} {2} " + (aac?null:(m_config.MPMUX?"--mp4mux ":"--mp4box ")) + m_config.GetCommandLine();
            return "- \"{0}\" --rawpcm {1} {3} {2} " + m_config.GetCommandLine();
        }

        /// <summary>
        /// If true BeHappy will send RIFF WAV header to encoder's stdin
        /// otherwize BeHappy will send just raw pcm data
        /// </summary>
        /// <returns></returns>
        public bool MustSendRiffHeader()
        {
            return false;
        }

        private static readonly string[] extensions;

        static Encoder()
        {
            string[] arr = new string[BeHappy.NeroDigitalAAC.Encoder.MP4Extensions.Length + 4];
            int i = -1;
            foreach (string ext in BeHappy.NeroDigitalAAC.Encoder.MP4Extensions) arr[++i] = ext;
            arr[++i] = "3gp";
            arr[++i] = "3g2";
            arr[++i] = "3gpp";
            arr[++i] = "aac";
            extensions = arr;
        }

        /// <summary>
        /// List of supported file extensions
        /// </summary>
        /// <returns>array of strings</returns>
        public string[] GetListOfSupportedExtensions()
        {
            return extensions;
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
//          return this.m_config.Profile==AacProfile.High?"2==Audiochannels(last)?last:ConvertToMono(last)":null;
            return "16==Audiobits(last)?last:ConvertAudioTo16bit(last)";
        }

        /// <summary>
        ///  Must reset configuration to defaults
        /// </summary>
        public void ResetConfiguration()
        {
            m_config = new Config();
        }

        private Config m_config;

        [XmlRoot("CodingTechnologiesAAC.Configuration", Namespace = Constants.DefaultXmlNamespace)]
        public class Config
        {
            public enum AacStereoMode
            {
                [EnumTitle("Joint Stereo")]
                Joint,
                [EnumTitle("Independent Stereo")]
                Independent,
                [EnumTitle("Mono Channel")]
                Mono,
                [EnumTitle("Dual Channels")]
                Dual
            }

            public int Bitrate;
            public bool MPEG4;
//          public bool MPMUX;
            public bool PNS;
            public bool SPEECH;
            public AacProfile Profile;
            public AacStereoMode ChannelMode;

            public Config()
            {
                Bitrate = 128000;
                MPEG4 = false;
//              MPMUX = false;
                PNS = false;
                SPEECH = false;
                Profile = AacProfile.LC;
                ChannelMode = AacStereoMode.Joint;
            }

            internal string GetDescription()
            {
                string encoder = string.Format("{1}, CBR @ {0} kbit/s, {2}", Bitrate, EnumProxy.Create(this.Profile), EnumProxy.Create(this.ChannelMode));
                if (MPEG4) encoder += " (MPEG-4 AAC)";
                if (PNS) encoder += " (PNS)";
                if (SPEECH) encoder += " (SPEECH)";
                return encoder;
            }

            internal string GetCommandLine()
            {
                string s = "--br " + Bitrate + "000";
                if (MPEG4) s += " --mpeg4aac";
                if (PNS) s += " --pns";
                if (SPEECH) s += " --speech";
                switch (Profile)
                {
                    case AacProfile.PS:
                        s += " --ps";
                        break;
                    case AacProfile.HE:
                        s += " --he";
                        break;
                    case AacProfile.LC:
                        s += " --lc";
                        break;
                    case AacProfile.High:
                        s += " --high";
                        break;
                }
                switch (this.ChannelMode)
                {
                    case Config.AacStereoMode.Dual:
                        s += " --dc";
                        break;
                    case Config.AacStereoMode.Mono:
                        s += " --mono";
                        break;
                    case Config.AacStereoMode.Joint:
                        break;
                    case Config.AacStereoMode.Independent:
                        s += " --is";
                        break;

                }
                return s;
            }
        }
    }
}
