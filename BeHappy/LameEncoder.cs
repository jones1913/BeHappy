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
    internal class EncoderConfigurationForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.RadioButton rbtnVBR;
        public System.Windows.Forms.RadioButton rbtnCBR;
        public TrackBar vBitrate;
        public RadioButton rbtnABR;
        public TrackBar vQuality;
//      public CheckBox cbxStrictISO;
        public CheckBox cbxUseNewVbr;
        public TextBox txtCLI;
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
            this.cbxUseNewVbr = new System.Windows.Forms.CheckBox();
            this.vQuality = new System.Windows.Forms.TrackBar();
            this.rbtnVBR = new System.Windows.Forms.RadioButton();
            this.vBitrate = new System.Windows.Forms.TrackBar();
            this.rbtnABR = new System.Windows.Forms.RadioButton();
            this.rbtnCBR = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCLI = new System.Windows.Forms.TextBox();
//          this.cbxStrictISO = new System.Windows.Forms.CheckBox();
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
            this.button1.Location = new System.Drawing.Point(336, 306);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            //
            // button2
            //
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(416, 306);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbxUseNewVbr);
            this.groupBox1.Controls.Add(this.vQuality);
            this.groupBox1.Controls.Add(this.rbtnVBR);
            this.groupBox1.Controls.Add(this.vBitrate);
            this.groupBox1.Controls.Add(this.rbtnABR);
            this.groupBox1.Controls.Add(this.rbtnCBR);
            this.groupBox1.Location = new System.Drawing.Point(138, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 208);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bitrate management";
            //
            // cbxUseNewVbr
            //
            this.cbxUseNewVbr.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbxUseNewVbr.Location = new System.Drawing.Point(3, 172);
            this.cbxUseNewVbr.Name = "cbxUseNewVbr";
            this.cbxUseNewVbr.Size = new System.Drawing.Size(347, 30);
            this.cbxUseNewVbr.TabIndex = 18;
            this.cbxUseNewVbr.Text = "use new VBR routine (else vbr-old)";
            //
            // vQuality
            //
            this.vQuality.Dock = System.Windows.Forms.DockStyle.Top;
            this.vQuality.Location = new System.Drawing.Point(3, 130);
            this.vQuality.Maximum = 9;
            this.vQuality.Name = "vQuality";
            this.vQuality.Size = new System.Drawing.Size(347, 42);
            this.vQuality.TabIndex = 17;
            this.vQuality.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.vQuality.Value = 6;
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
            this.vBitrate.Maximum = 320;
            this.vBitrate.Minimum = 8;
            this.vBitrate.Name = "vBitrate";
            this.vBitrate.Size = new System.Drawing.Size(347, 42);
            this.vBitrate.TabIndex = 15;
            this.vBitrate.TickFrequency = 8;
            this.vBitrate.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.vBitrate.Value = 12;
            this.vBitrate.ValueChanged += new System.EventHandler(this.vBitrate_ValueChanged);
            //
            // rbtnABR
            //
            this.rbtnABR.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbtnABR.Location = new System.Drawing.Point(3, 40);
            this.rbtnABR.Name = "rbtnABR";
            this.rbtnABR.Size = new System.Drawing.Size(347, 24);
            this.rbtnABR.TabIndex = 16;
            this.rbtnABR.Text = "Average bit rate";
            this.rbtnABR.CheckedChanged += new System.EventHandler(this.rbtnCBR_CheckedChanged);
            //
            // rbtnCBR
            //
            this.rbtnCBR.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbtnCBR.Location = new System.Drawing.Point(3, 16);
            this.rbtnCBR.Name = "rbtnCBR";
            this.rbtnCBR.Size = new System.Drawing.Size(347, 24);
            this.rbtnCBR.TabIndex = 13;
            this.rbtnCBR.Text = "Constant bit rate";
            this.rbtnCBR.CheckedChanged += new System.EventHandler(this.rbtnCBR_CheckedChanged);
            //
            // groupBox3
            //
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txtCLI);
            this.groupBox3.Location = new System.Drawing.Point(138, 255);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(353, 46);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Additional CLI arguments";
            //
            // txtCLI
            //
            this.txtCLI.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtCLI.Location = new System.Drawing.Point(3, 16);
            this.txtCLI.Name = "txtCLI";
            this.txtCLI.Size = new System.Drawing.Size(347, 20);
            this.txtCLI.TabIndex = 0;
            //
            // cbxStrictISO
            //
//          this.cbxStrictISO.Location = new System.Drawing.Point(141, 218);
//          this.cbxStrictISO.Name = "cbxStrictISO";
//          this.cbxStrictISO.Size = new System.Drawing.Size(284, 26);
//          this.cbxStrictISO.TabIndex = 14;
//          this.cbxStrictISO.Text = "comply as much as possible to ISO MPEG spec";
            //
            // EncoderConfigurationForm
            //
            this.AcceptButton = this.button1;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(494, 332);
//          this.Controls.Add(this.cbxStrictISO);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EncoderConfigurationForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lame MP3 Encoder Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vBitrate)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

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
            return m_config.GetCommandLine() + " -S --silent - \"{0}\"";
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
