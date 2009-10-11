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
    namespace Aften
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
            public TrackBar vQuality;
            public TextBox txtCLI;
//            internal ComboBox lstBandwidth;
            internal ComboBox lstDynRangeLevel;
            private Label label1;
            internal CheckBox cbxReadToEndOfFile;
            internal CheckBox cbxIndependentLR;
            private Label label2;
            internal ComboBox lstCenterMixLevel;
            private Label label3;
            internal ComboBox lstSurroundMixLevel;
            private Label label4;
            internal NumericUpDown munDiaNorm;
            private Label label5;
            internal ComboBox lstDPL;
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
//                this.lstBandwidth = new System.Windows.Forms.ComboBox();
                this.lstDynRangeLevel = new System.Windows.Forms.ComboBox();
                this.label1 = new System.Windows.Forms.Label();
                this.cbxReadToEndOfFile = new System.Windows.Forms.CheckBox();
                this.cbxIndependentLR = new System.Windows.Forms.CheckBox();
                this.label2 = new System.Windows.Forms.Label();
                this.lstCenterMixLevel = new System.Windows.Forms.ComboBox();
                this.label3 = new System.Windows.Forms.Label();
                this.lstSurroundMixLevel = new System.Windows.Forms.ComboBox();
                this.label4 = new System.Windows.Forms.Label();
                this.munDiaNorm = new System.Windows.Forms.NumericUpDown();
                this.label5 = new System.Windows.Forms.Label();
               this.groupBox3 = new System.Windows.Forms.GroupBox();
               this.txtCLI = new System.Windows.Forms.TextBox();
                this.lstDPL = new System.Windows.Forms.ComboBox();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
                this.groupBox1.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.vQuality)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.vBitrate)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.munDiaNorm)).BeginInit();
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
//              this.button1.Location = new System.Drawing.Point(336, 269);
                   this.button1.Location = new System.Drawing.Point(336, 318);
                this.button1.Name = "button1";
                this.button1.Size = new System.Drawing.Size(75, 23);
                this.button1.TabIndex = 1;
                this.button1.Text = "OK";
                //
                // button2
                //
                this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
                this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
//              this.button2.Location = new System.Drawing.Point(416, 269);
                   this.button2.Location = new System.Drawing.Point(416, 318);
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
                this.groupBox1.Location = new System.Drawing.Point(138, 4);
                this.groupBox1.Name = "groupBox1";
                this.groupBox1.Size = new System.Drawing.Size(353, 151);
                this.groupBox1.TabIndex = 9;
                this.groupBox1.TabStop = false;
                this.groupBox1.Text = "Bitrate management";
                //
                // vQuality
                //
                this.vQuality.Dock = System.Windows.Forms.DockStyle.Top;
                this.vQuality.Location = new System.Drawing.Point(3, 106);
                this.vQuality.Maximum = 1023;
                this.vQuality.Minimum = 1;
                this.vQuality.Name = "vQuality";
                this.vQuality.Size = new System.Drawing.Size(347, 42);
                this.vQuality.TabIndex = 17;
                this.vQuality.TickFrequency = 32;
                this.vQuality.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
                this.vQuality.Value = 1;
                this.vQuality.ValueChanged += new System.EventHandler(this.vQuality_ValueChanged);
                //
                // rbtnVBR
                //
                this.rbtnVBR.Dock = System.Windows.Forms.DockStyle.Top;
                this.rbtnVBR.Location = new System.Drawing.Point(3, 82);
                this.rbtnVBR.Name = "rbtnVBR";
                this.rbtnVBR.Size = new System.Drawing.Size(347, 24);
                this.rbtnVBR.TabIndex = 14;
                this.rbtnVBR.Text = "Variable bit rate";
                this.rbtnVBR.CheckedChanged += new System.EventHandler(this.rbtnCBR_CheckedChanged);
                //
                // vBitrate
                //
                this.vBitrate.Dock = System.Windows.Forms.DockStyle.Top;
                this.vBitrate.Location = new System.Drawing.Point(3, 40);
                this.vBitrate.Maximum = 640;
                this.vBitrate.Minimum = 64;
                this.vBitrate.Name = "vBitrate";
                this.vBitrate.Size = new System.Drawing.Size(347, 42);
                this.vBitrate.TabIndex = 15;
//                this.vBitrate.TickFrequency = 8;
                this.vBitrate.TickFrequency = 32;
                this.vBitrate.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
                this.vBitrate.Value = 448;
                this.vBitrate.ValueChanged += new System.EventHandler(this.vBitrate_ValueChanged);
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
//                //
//                // lstBandwidth
//                //
//                this.lstBandwidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//                this.lstBandwidth.FormattingEnabled = true;
//                this.lstBandwidth.Location = new System.Drawing.Point(194, 241);
//                this.lstBandwidth.Name = "lstBandwidth";
//                this.lstBandwidth.Size = new System.Drawing.Size(81, 21);
//                this.lstBandwidth.TabIndex = 10;
                //
                // lstDynRangeLevel
                //
                this.lstDynRangeLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.lstDynRangeLevel.FormattingEnabled = true;
                this.lstDynRangeLevel.Location = new System.Drawing.Point(194, 243);
                this.lstDynRangeLevel.Name = "lstDynRangeLevel";
                this.lstDynRangeLevel.Size = new System.Drawing.Size(81, 21);
                this.lstDynRangeLevel.TabIndex = 10;
                //
                // label1
                //
                this.label1.AutoSize = true;
                this.label1.Location = new System.Drawing.Point(42, 246);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(146, 13);
                this.label1.TabIndex = 11;
                this.label1.Text = "Dynamic Range Compression";
//                //
//                // label1
//                //
//                this.label1.AutoSize = true;
//                this.label1.Location = new System.Drawing.Point(131, 244);
//                this.label1.Name = "label1";
//                this.label1.Size = new System.Drawing.Size(57, 13);
//                this.label1.TabIndex = 11;
//                this.label1.Text = "Bandwidth";
               //
               // groupBox3
               //
               this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                           | System.Windows.Forms.AnchorStyles.Right)));
               this.groupBox3.Controls.Add(this.txtCLI);
               this.groupBox3.Location = new System.Drawing.Point(138, 255);
               this.groupBox3.Location = new System.Drawing.Point(42, 267);
               this.groupBox3.Name = "groupBox3";
               this.groupBox3.Size = new System.Drawing.Size(449, 46);
               this.groupBox3.TabIndex = 11;
               this.groupBox3.TabStop = false;
               this.groupBox3.Text = "Additional CLI arguments";
               //
               // txtCLI
               //
               this.txtCLI.Dock = System.Windows.Forms.DockStyle.Top;
               this.txtCLI.Location = new System.Drawing.Point(3, 16);
               this.txtCLI.Name = "txtCLI";
               this.txtCLI.Size = new System.Drawing.Size(443, 20);
               this.txtCLI.TabIndex = 0;
                //
                // cbxReadToEndOfFile
                //
                this.cbxReadToEndOfFile.AutoSize = true;
                this.cbxReadToEndOfFile.Location = new System.Drawing.Point(284, 242);
                this.cbxReadToEndOfFile.Name = "cbxReadToEndOfFile";
                this.cbxReadToEndOfFile.Size = new System.Drawing.Size(178, 17);
                this.cbxReadToEndOfFile.TabIndex = 12;
                this.cbxReadToEndOfFile.Text = "Read to End of File";
                this.cbxReadToEndOfFile.UseVisualStyleBackColor = true;
                //
                // cbxIndependentLR
                //
                this.cbxIndependentLR.AutoSize = true;
                this.cbxIndependentLR.Location = new System.Drawing.Point(284, 218);
                this.cbxIndependentLR.Name = "cbxIndependentLR";
                this.cbxIndependentLR.Size = new System.Drawing.Size(155, 17);
                this.cbxIndependentLR.TabIndex = 13;
                this.cbxIndependentLR.Text = "Independent L+R channels";
                this.cbxIndependentLR.UseVisualStyleBackColor = true;
                //
                // label2
                //
                this.label2.AutoSize = true;
                this.label2.Location = new System.Drawing.Point(102, 163);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(86, 13);
                this.label2.TabIndex = 15;
                this.label2.Text = "Center Mix Level";
                //
                // lstCenterMixLevel
                //
                this.lstCenterMixLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.lstCenterMixLevel.FormattingEnabled = true;
                this.lstCenterMixLevel.Location = new System.Drawing.Point(194, 162);
                this.lstCenterMixLevel.Name = "lstCenterMixLevel";
                this.lstCenterMixLevel.Size = new System.Drawing.Size(81, 21);
                this.lstCenterMixLevel.TabIndex = 14;
                //
                // label3
                //
                this.label3.AutoSize = true;
                this.label3.Location = new System.Drawing.Point(90, 192);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(98, 13);
                this.label3.TabIndex = 17;
                this.label3.Text = "Surround Mix Level";
                //
                // lstSurroundMixLevel
                //
                this.lstSurroundMixLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.lstSurroundMixLevel.FormattingEnabled = true;
                this.lstSurroundMixLevel.Location = new System.Drawing.Point(194, 189);
                this.lstSurroundMixLevel.Name = "lstSurroundMixLevel";
                this.lstSurroundMixLevel.Size = new System.Drawing.Size(81, 21);
                this.lstSurroundMixLevel.TabIndex = 16;
                //
                // label4
                //
                this.label4.AutoSize = true;
                this.label4.Location = new System.Drawing.Point(85, 219);
                this.label4.Name = "label4";
                this.label4.Size = new System.Drawing.Size(103, 13);
                this.label4.TabIndex = 19;
                this.label4.Text = "Dialog Normalization";
                //
                // munDiaNorm
                //
                this.munDiaNorm.Location = new System.Drawing.Point(194, 217);
                this.munDiaNorm.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
                this.munDiaNorm.Name = "munDiaNorm";
                this.munDiaNorm.Size = new System.Drawing.Size(81, 20);
                this.munDiaNorm.TabIndex = 20;
                //
                // label5
                //
                this.label5.AutoSize = true;
                this.label5.Location = new System.Drawing.Point(281, 165);
                this.label5.Name = "label5";
                this.label5.Size = new System.Drawing.Size(58, 13);
                this.label5.TabIndex = 22;
                this.label5.Text = "DPL Mode";
                //
                // lstDPL
                //
                this.lstDPL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.lstDPL.FormattingEnabled = true;
                this.lstDPL.Location = new System.Drawing.Point(344, 162);
                this.lstDPL.Name = "lstDPL";
                this.lstDPL.Size = new System.Drawing.Size(147, 21);
                this.lstDPL.TabIndex = 21;
                //
                // EncoderConfigurationForm
                //
                this.AcceptButton = this.button1;
                this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
                this.CancelButton = this.button2;
                this.ClientSize = new System.Drawing.Size(494, 344);
                this.Controls.Add(this.label5);
                this.Controls.Add(this.lstDPL);
                this.Controls.Add(this.munDiaNorm);
                this.Controls.Add(this.label4);
                this.Controls.Add(this.label3);
                this.Controls.Add(this.lstSurroundMixLevel);
                this.Controls.Add(this.label2);
                this.Controls.Add(this.lstCenterMixLevel);
                this.Controls.Add(this.cbxIndependentLR);
                this.Controls.Add(this.cbxReadToEndOfFile);
                this.Controls.Add(this.label1);
//                this.Controls.Add(this.lstBandwidth);
                this.Controls.Add(this.lstDynRangeLevel);
                this.Controls.Add(this.groupBox1);
               this.Controls.Add(this.groupBox3);
                this.Controls.Add(this.button2);
                this.Controls.Add(this.button1);
                this.Controls.Add(this.pictureBox1);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                this.Name = "EncoderConfigurationForm";
                this.ShowInTaskbar = false;
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                this.Text = "Aften AC3 Encoder Configuration";
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
                this.groupBox1.ResumeLayout(false);
                this.groupBox1.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.vQuality)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.vBitrate)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.munDiaNorm)).EndInit();
               this.groupBox3.ResumeLayout(false);
               this.groupBox3.PerformLayout();
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
}
