using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BeHappy.Extensibility;
using System.Xml;
using System.Xml.Serialization;


namespace BeHappy.CodingTechnologiesAAC
{
    /// <summary>
    /// Summary description for EncoderConfigurationForm.
    /// </summary>
    internal class EncoderConfigurationForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        public TrackBar vBitrate;
        private Label label1;
        public CheckBox cbxMPEG2AAC;
        private Label label2;
        public ComboBox lstProfile;
        public ComboBox lstChannelMode;
        private Label label3;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

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

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EncoderConfigurationForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.vBitrate = new System.Windows.Forms.TrackBar();
            this.cbxMPEG2AAC = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstProfile = new System.Windows.Forms.ComboBox();
            this.lstChannelMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vBitrate)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 96);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(336, 135);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(416, 135);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.vBitrate);
            this.groupBox1.Location = new System.Drawing.Point(136, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 82);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bitrate management";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(352, 21);
            this.label1.TabIndex = 19;
            this.label1.Text = "label1";
            // 
            // vBitrate
            // 
            this.vBitrate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.vBitrate.Location = new System.Drawing.Point(3, 37);
            this.vBitrate.Maximum = 320000;
            this.vBitrate.Minimum = 8000;
            this.vBitrate.Name = "vBitrate";
            this.vBitrate.Size = new System.Drawing.Size(352, 42);
            this.vBitrate.TabIndex = 15;
            this.vBitrate.TickFrequency = 8000;
            this.vBitrate.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.vBitrate.Value = 8000;
            this.vBitrate.ValueChanged += new System.EventHandler(this.vBitrate_ValueChanged);
            // 
            // cbxMPEG2AAC
            // 
            this.cbxMPEG2AAC.AutoSize = true;
            this.cbxMPEG2AAC.Location = new System.Drawing.Point(181, 115);
            this.cbxMPEG2AAC.Name = "cbxMPEG2AAC";
            this.cbxMPEG2AAC.Size = new System.Drawing.Size(132, 17);
            this.cbxMPEG2AAC.TabIndex = 19;
            this.cbxMPEG2AAC.Text = "produce MPEG-2 AAC";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Profile";
            // 
            // lstProfile
            // 
            this.lstProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstProfile.FormattingEnabled = true;
            this.lstProfile.Location = new System.Drawing.Point(181, 88);
            this.lstProfile.Name = "lstProfile";
            this.lstProfile.Size = new System.Drawing.Size(100, 21);
            this.lstProfile.TabIndex = 21;
            // 
            // lstChannelMode
            // 
            this.lstChannelMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstChannelMode.FormattingEnabled = true;
            this.lstChannelMode.Location = new System.Drawing.Point(381, 88);
            this.lstChannelMode.Name = "lstChannelMode";
            this.lstChannelMode.Size = new System.Drawing.Size(110, 21);
            this.lstChannelMode.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(299, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Channel Mode";
            // 
            // EncoderConfigurationForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(494, 161);
            this.Controls.Add(this.lstChannelMode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstProfile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbxMPEG2AAC);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EncoderConfigurationForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Coding Technologies AAC Encoder Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vBitrate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void vBitrate_ValueChanged(object sender, EventArgs e)
        {
            this.label1.Text = "CBR @ " + ((decimal)vBitrate.Value) / 1000.0M + " kbit/s";
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
                f.cbxMPEG2AAC.Checked = m_config.MPEG2;
                f.lstProfile.SelectedItem = EnumProxy.Create(m_config.Profile);
                f.lstChannelMode.SelectedItem = EnumProxy.Create(m_config.ChannelMode);


                if (f.ShowDialog(owner) == DialogResult.OK)
                {
                    m_config.Bitrate = f.vBitrate.Value;
                    m_config.Profile = (AacProfile)(f.lstProfile.SelectedItem as EnumProxy).RealValue;
                    m_config.ChannelMode = (Config.AacStereoMode)(f.lstChannelMode.SelectedItem as EnumProxy).RealValue;
                    m_config.MPEG2 = f.cbxMPEG2AAC.Checked;


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
            bool raw = ".aac" == targetFileExtension;
            return "- \"{0}\" --rawpcm {1} {3} {2} " + (raw?null:"--mp4box ") + m_config.GetCommandLine();
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
            foreach (string ext in BeHappy.NeroDigitalAAC.Encoder.MP4Extensions)
                arr[++i] = ext;
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
            return this.m_config.Profile==AacProfile.High?"2==Audiochannels(last)?last:ConvertToMono(last)":null;
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
                [EnumTitle("Stereo")]
                Independent,
                [EnumTitle("Dual Channels")]
                Dual
            }

            public int Bitrate;
            public bool MPEG2;
            public AacProfile Profile;
            public AacStereoMode ChannelMode;

            public Config()
            {
                Bitrate = 64000;
                MPEG2 = false;
                Profile = AacProfile.PS;
                ChannelMode = AacStereoMode.Joint;
            }

            internal string GetDescription()
            {
                string encoder = string.Format("{1}, CBR @ {0} kbit/s, {2}", ((decimal)Bitrate) / 1000.0M, EnumProxy.Create(this.Profile), EnumProxy.Create(this.ChannelMode));
                if (MPEG2) encoder += " (MPEG-2 AAC)";
                return encoder;
            }

            internal string GetCommandLine()
            {
                string s = "--cbr " + Bitrate;
                if (MPEG2) s += " --mpeg2aac";
                switch (Profile)
                {
                    case AacProfile.PS:
                        break;
                    case AacProfile.HE:
                        s += " --nops";
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
