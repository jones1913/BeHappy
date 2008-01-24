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
        [EnumTitle("aacPlus v2", "aacPlus v2")]
        PS,
        [EnumTitle("aacPlus", "aacPlus")]
        HE,
        [EnumTitle("AAC-LC", "AAC-LC")]
        LC,
        [EnumTitle("aacPlusHigh", "aacPlusHigh")]
        High
    }
    namespace NeroDigitalAAC
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
            private System.Windows.Forms.GroupBox groupBox3;
            public System.Windows.Forms.CheckBox cbxCreateHintTrack;
            public System.Windows.Forms.RadioButton rbtnVBR;
            public System.Windows.Forms.RadioButton rbtnCBR;
            public TrackBar vBitrate;
            public RadioButton rbtnABR;
            public TrackBar vQuality;
            public CheckBox cbxSSE2;
            private LinkLabel linkLabel1;
            public ComboBox lstProfile;
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

                lstProfile.Items.AddRange(EnumProxy.CreateArray(new AacProfile[] { AacProfile.Auto, AacProfile.PS, AacProfile.HE, AacProfile.LC }));

                vBitrate_ValueChanged(null, null);
                vQuality_ValueChanged(null, null);
                rbtnCBR_CheckedChanged(null, null);
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
                this.vQuality = new System.Windows.Forms.TrackBar();
                this.rbtnVBR = new System.Windows.Forms.RadioButton();
                this.vBitrate = new System.Windows.Forms.TrackBar();
                this.rbtnCBR = new System.Windows.Forms.RadioButton();
                this.rbtnABR = new System.Windows.Forms.RadioButton();
                this.groupBox3 = new System.Windows.Forms.GroupBox();
                this.lstProfile = new System.Windows.Forms.ComboBox();
                this.cbxCreateHintTrack = new System.Windows.Forms.CheckBox();
                this.cbxSSE2 = new System.Windows.Forms.CheckBox();
                this.linkLabel1 = new System.Windows.Forms.LinkLabel();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
                this.groupBox1.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.vQuality)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.vBitrate)).BeginInit();
                this.groupBox3.SuspendLayout();
                this.SuspendLayout();
                // 
                // pictureBox1
                // 
                this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
                this.pictureBox1.Location = new System.Drawing.Point(4, 8);
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
                this.button1.Location = new System.Drawing.Point(336, 247);
                this.button1.Name = "button1";
                this.button1.Size = new System.Drawing.Size(75, 23);
                this.button1.TabIndex = 1;
                this.button1.Text = "OK";
                // 
                // button2
                // 
                this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
                this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.button2.Location = new System.Drawing.Point(416, 247);
                this.button2.Name = "button2";
                this.button2.Size = new System.Drawing.Size(75, 23);
                this.button2.TabIndex = 2;
                this.button2.Text = "Cancel";
                // 
                // groupBox1
                // 
                this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                            | System.Windows.Forms.AnchorStyles.Right)));
                this.groupBox1.Controls.Add(this.vQuality);
                this.groupBox1.Controls.Add(this.rbtnVBR);
                this.groupBox1.Controls.Add(this.vBitrate);
                this.groupBox1.Controls.Add(this.rbtnCBR);
                this.groupBox1.Controls.Add(this.rbtnABR);
                this.groupBox1.Location = new System.Drawing.Point(138, 4);
                this.groupBox1.Name = "groupBox1";
                this.groupBox1.Size = new System.Drawing.Size(353, 182);
                this.groupBox1.TabIndex = 9;
                this.groupBox1.TabStop = false;
                this.groupBox1.Text = "Bitrate management";
                // 
                // vQuality
                // 
                this.vQuality.Dock = System.Windows.Forms.DockStyle.Top;
                this.vQuality.Location = new System.Drawing.Point(3, 130);
                this.vQuality.Maximum = 100;
                this.vQuality.Name = "vQuality";
                this.vQuality.Size = new System.Drawing.Size(347, 42);
                this.vQuality.TabIndex = 17;
                this.vQuality.TickFrequency = 5;
                this.vQuality.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
                this.vQuality.ValueChanged += new System.EventHandler(this.vQuality_ValueChanged);
                // 
                // rbtnVBR
                // 
                this.rbtnVBR.Dock = System.Windows.Forms.DockStyle.Top;
                this.rbtnVBR.Location = new System.Drawing.Point(3, 106);
                this.rbtnVBR.Name = "rbtnVBR";
                this.rbtnVBR.Size = new System.Drawing.Size(347, 24);
                this.rbtnVBR.TabIndex = 14;
                this.rbtnVBR.Text = "Variable bit rate";
                this.rbtnVBR.CheckedChanged += new System.EventHandler(this.rbtnCBR_CheckedChanged);
                // 
                // vBitrate
                // 
                this.vBitrate.Dock = System.Windows.Forms.DockStyle.Top;
                this.vBitrate.Location = new System.Drawing.Point(3, 64);
                this.vBitrate.Maximum = 3200;
                this.vBitrate.Minimum = 160;
                this.vBitrate.Name = "vBitrate";
                this.vBitrate.Size = new System.Drawing.Size(347, 42);
                this.vBitrate.TabIndex = 15;
                this.vBitrate.TickFrequency = 80;
                this.vBitrate.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
                this.vBitrate.Value = 3200;
                this.vBitrate.ValueChanged += new System.EventHandler(this.vBitrate_ValueChanged);
                // 
                // rbtnCBR
                // 
                this.rbtnCBR.Dock = System.Windows.Forms.DockStyle.Top;
                this.rbtnCBR.Location = new System.Drawing.Point(3, 40);
                this.rbtnCBR.Name = "rbtnCBR";
                this.rbtnCBR.Size = new System.Drawing.Size(347, 24);
                this.rbtnCBR.TabIndex = 13;
                this.rbtnCBR.Text = "Constant bit rate";
                this.rbtnCBR.CheckedChanged += new System.EventHandler(this.rbtnCBR_CheckedChanged);
                // 
                // rbtnABR
                // 
                this.rbtnABR.Dock = System.Windows.Forms.DockStyle.Top;
                this.rbtnABR.Location = new System.Drawing.Point(3, 16);
                this.rbtnABR.Name = "rbtnABR";
                this.rbtnABR.Size = new System.Drawing.Size(347, 24);
                this.rbtnABR.TabIndex = 16;
                this.rbtnABR.Text = "Average bit rate";
                this.rbtnABR.CheckedChanged += new System.EventHandler(this.rbtnCBR_CheckedChanged);
                // 
                // groupBox3
                // 
                this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                            | System.Windows.Forms.AnchorStyles.Right)));
                this.groupBox3.AutoSize = true;
                this.groupBox3.Controls.Add(this.lstProfile);
                this.groupBox3.Location = new System.Drawing.Point(138, 192);
                this.groupBox3.Name = "groupBox3";
                this.groupBox3.Size = new System.Drawing.Size(122, 40);
                this.groupBox3.TabIndex = 11;
                this.groupBox3.TabStop = false;
                this.groupBox3.Text = "AAC Profile";
                // 
                // lstProfile
                // 
                this.lstProfile.Dock = System.Windows.Forms.DockStyle.Top;
                this.lstProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.lstProfile.FormattingEnabled = true;
                this.lstProfile.Location = new System.Drawing.Point(3, 16);
                this.lstProfile.Name = "lstProfile";
                this.lstProfile.Size = new System.Drawing.Size(116, 21);
                this.lstProfile.TabIndex = 0;
                // 
                // cbxCreateHintTrack
                // 
                this.cbxCreateHintTrack.AutoSize = true;
                this.cbxCreateHintTrack.Location = new System.Drawing.Point(266, 192);
                this.cbxCreateHintTrack.Name = "cbxCreateHintTrack";
                this.cbxCreateHintTrack.Size = new System.Drawing.Size(205, 17);
                this.cbxCreateHintTrack.TabIndex = 13;
                this.cbxCreateHintTrack.Text = "Create hint track (for streaming server)";
                // 
                // cbxSSE2
                // 
                this.cbxSSE2.AutoSize = true;
                this.cbxSSE2.Location = new System.Drawing.Point(266, 215);
                this.cbxSSE2.Name = "cbxSSE2";
                this.cbxSSE2.Size = new System.Drawing.Size(156, 17);
                this.cbxSSE2.TabIndex = 14;
                this.cbxSSE2.Text = "Use SSE2 CPU instructions";
                // 
                // linkLabel1
                // 
                this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                this.linkLabel1.AutoSize = true;
                this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 34);
                this.linkLabel1.Location = new System.Drawing.Point(1, 252);
                this.linkLabel1.Name = "linkLabel1";
                this.linkLabel1.Size = new System.Drawing.Size(176, 13);
                this.linkLabel1.TabIndex = 15;
                this.linkLabel1.TabStop = true;
                this.linkLabel1.Tag = "http://www.nero.com/nerodigital/eng/Nero_Digital_Audio.html";
                this.linkLabel1.Text = "Get NeroDigital AAC encoder binary";
                this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
                // 
                // EncoderConfigurationForm
                // 
                this.AcceptButton = this.button1;
                this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
                this.CancelButton = this.button2;
                this.ClientSize = new System.Drawing.Size(494, 273);
                this.Controls.Add(this.linkLabel1);
                this.Controls.Add(this.cbxSSE2);
                this.Controls.Add(this.cbxCreateHintTrack);
                this.Controls.Add(this.groupBox3);
                this.Controls.Add(this.groupBox1);
                this.Controls.Add(this.button2);
                this.Controls.Add(this.button1);
                this.Controls.Add(this.pictureBox1);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                this.Name = "EncoderConfigurationForm";
                this.ShowInTaskbar = false;
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                this.Text = "NeroDigital Audio Encoder Configuration";
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
                this.groupBox1.ResumeLayout(false);
                this.groupBox1.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.vQuality)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.vBitrate)).EndInit();
                this.groupBox3.ResumeLayout(false);
                this.ResumeLayout(false);
                this.PerformLayout();

            }
            #endregion

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

                        m_config.SSE2 = f.cbxSSE2.Checked;

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
                return m_config.SSE2 ? "neroAacEnc_sse2.exe" : "neroAacEnc.exe";
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

            public static readonly string[] MP4Extensions;

            static Encoder()
            {
                if ("true" == System.Configuration.ConfigurationManager.AppSettings["preferMP4overM4A"])
                    MP4Extensions = new string[] { "mp4", "m4a" };
                else
                    MP4Extensions = new string[] { "m4a", "mp4" };
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
                public bool SSE2;

                public Config()
                {
                    Bitrate = 64000;
                    Quality = .3M;
                    Profile = AacProfile.Auto;
                    Mode = BitrateManagementMode.VBR;
                    CreateHintTrack = false;
                    SSE2 = false;
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
                    if (SSE2)
                        encoder += ", SSE2";
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
}
