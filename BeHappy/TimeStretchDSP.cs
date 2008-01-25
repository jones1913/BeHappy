using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BeHappy.Extensibility;
using System.Xml.Serialization;

namespace BeHappy.TimeStretch
{
    internal class ConfigurationDialog : BeHappy.ConfigurationFormBase
    {
        internal NumericUpDown numRateFrom;
        internal NumericUpDown numRateTo;
        private Label label1;
        public RadioButton rbtnFrameRate;
        public RadioButton rbtnCustom;
        internal NumericUpDown numCustom;
        private GroupBox gbRateControl;
        public RadioButton rdoCtlRate;
        public RadioButton rdoCtlTempo;
        public RadioButton rdoCtlPitch;
        private System.ComponentModel.IContainer components = null;
        public string m_strTimeStretchMethod;
        private ToolTip m_tt;

        public ConfigurationDialog()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
            // init default
            m_strTimeStretchMethod = "tempo";

            rbtnFrameRate_CheckedChanged(null, null);
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


        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            m_tt = new ToolTip();
            m_tt.AutoPopDelay = 10000; // show for 10 seconds while mouse is over item

            this.numRateFrom = new System.Windows.Forms.NumericUpDown();
            this.numRateTo = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtnFrameRate = new System.Windows.Forms.RadioButton();
            this.rbtnCustom = new System.Windows.Forms.RadioButton();
            this.numCustom = new System.Windows.Forms.NumericUpDown();
            this.gbRateControl = new System.Windows.Forms.GroupBox();
            this.rdoCtlRate = new System.Windows.Forms.RadioButton();
            this.rdoCtlPitch = new System.Windows.Forms.RadioButton();
            this.rdoCtlTempo = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numRateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCustom)).BeginInit();
            this.gbRateControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(113, 228);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(189, 228);
            // 
            // numRateFrom
            // 
            this.numRateFrom.DecimalPlaces = 3;
            this.numRateFrom.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numRateFrom.Location = new System.Drawing.Point(42, 34);
            this.numRateFrom.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numRateFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numRateFrom.Name = "numRateFrom";
            this.numRateFrom.Size = new System.Drawing.Size(79, 20);
            this.numRateFrom.TabIndex = 7;
            this.numRateFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numRateFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            // 
            // numRateTo
            // 
            this.numRateTo.DecimalPlaces = 3;
            this.numRateTo.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numRateTo.Location = new System.Drawing.Point(149, 34);
            this.numRateTo.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numRateTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numRateTo.Name = "numRateTo";
            this.numRateTo.Size = new System.Drawing.Size(79, 20);
            this.numRateTo.TabIndex = 8;
            this.numRateTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numRateTo.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "->";
            // 
            // rbtnFrameRate
            // 
            this.rbtnFrameRate.AutoSize = true;
            this.rbtnFrameRate.Checked = true;
            this.rbtnFrameRate.Location = new System.Drawing.Point(12, 11);
            this.rbtnFrameRate.Name = "rbtnFrameRate";
            this.rbtnFrameRate.Size = new System.Drawing.Size(155, 17);
            this.rbtnFrameRate.TabIndex = 10;
            this.rbtnFrameRate.TabStop = true;
            this.rbtnFrameRate.Text = "FrameRate based transform";
            this.rbtnFrameRate.UseVisualStyleBackColor = true;
            this.rbtnFrameRate.CheckedChanged += new System.EventHandler(this.rbtnFrameRate_CheckedChanged);
            // 
            // rbtnCustom
            // 
            this.rbtnCustom.AutoSize = true;
            this.rbtnCustom.Location = new System.Drawing.Point(12, 72);
            this.rbtnCustom.Name = "rbtnCustom";
            this.rbtnCustom.Size = new System.Drawing.Size(222, 17);
            this.rbtnCustom.TabIndex = 14;
            this.rbtnCustom.Text = "Custom (actual_time / desired_time) x 100";
            this.rbtnCustom.UseVisualStyleBackColor = true;
            this.rbtnCustom.CheckedChanged += new System.EventHandler(this.rbtnFrameRate_CheckedChanged);
            // 
            // numCustom
            // 
            this.numCustom.DecimalPlaces = 6;
            this.numCustom.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numCustom.Location = new System.Drawing.Point(42, 95);
            this.numCustom.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numCustom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            393216});
            this.numCustom.Name = "numCustom";
            this.numCustom.Size = new System.Drawing.Size(125, 20);
            this.numCustom.TabIndex = 11;
            this.numCustom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCustom.Value = new decimal(new int[] {
            1,
            0,
            0,
            393216});
            // 
            // gbRateControl
            // 
            this.gbRateControl.Controls.Add(this.rdoCtlTempo);
            this.gbRateControl.Controls.Add(this.rdoCtlPitch);
            this.gbRateControl.Controls.Add(this.rdoCtlRate);
            this.gbRateControl.ForeColor = System.Drawing.Color.MediumBlue;
            this.gbRateControl.Location = new System.Drawing.Point(12, 131);
            this.gbRateControl.Name = "gbRateControl";
            this.gbRateControl.Size = new System.Drawing.Size(241, 91);
            this.gbRateControl.TabIndex = 15;
            this.gbRateControl.TabStop = false;
            this.gbRateControl.Text = "Rate Control";
            // 
            // rdoCtlRate
            // 
            this.rdoCtlRate.AutoSize = true;
            this.rdoCtlRate.ForeColor = System.Drawing.SystemColors.WindowText;
            this.rdoCtlRate.Location = new System.Drawing.Point(7, 20);
            this.rdoCtlRate.Name = "rdoCtlRate";
            this.rdoCtlRate.Size = new System.Drawing.Size(134, 17);
            this.rdoCtlRate.TabIndex = 0;
            this.rdoCtlRate.Text = "&Rate, tempo and no pitch correction";
            this.m_tt.SetToolTip(this.rdoCtlRate, "Change playback RATE that affects both tempo and pitch at the same time");
            this.rdoCtlRate.UseVisualStyleBackColor = true;
            this.rdoCtlRate.CheckedChanged += new System.EventHandler(this.rdoCtlRate_CheckedChanged);
            // 
            // rdoCtlPitch
            // 
            this.rdoCtlPitch.AutoSize = true;
            this.rdoCtlPitch.ForeColor = System.Drawing.SystemColors.WindowText;
            this.rdoCtlPitch.Location = new System.Drawing.Point(7, 44);
            this.rdoCtlPitch.Name = "rdoCtlPitch";
            this.rdoCtlPitch.Size = new System.Drawing.Size(110, 17);
            this.rdoCtlPitch.TabIndex = 1;
            this.rdoCtlPitch.Text = "&Pitch changed preserving tempo";
            this.m_tt.SetToolTip(this.rdoCtlPitch, "Sound PITCH can be increased or decreased while maintaining the original tempo");
            this.rdoCtlPitch.UseVisualStyleBackColor = true;
            this.rdoCtlPitch.CheckedChanged += new System.EventHandler(this.rdoCtlRate_CheckedChanged);
            // 
            // rdoCtlTempo
            // 
            this.rdoCtlTempo.AutoSize = true;
            this.rdoCtlTempo.Checked = true;
            this.rdoCtlTempo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.rdoCtlTempo.Location = new System.Drawing.Point(7, 68);
            this.rdoCtlTempo.Name = "rdoCtlTempo";
            this.rdoCtlTempo.Size = new System.Drawing.Size(159, 17);
            this.rdoCtlTempo.TabIndex = 2;
            this.rdoCtlTempo.TabStop = true;
            this.rdoCtlTempo.Text = "&Tempo changed, pitch correction";
            this.m_tt.SetToolTip(this.rdoCtlTempo, "Sound TEMPO can be increased or decreased while maintaining the original pitch");
            this.rdoCtlTempo.UseVisualStyleBackColor = true;
            this.rdoCtlTempo.CheckedChanged += new System.EventHandler(this.rdoCtlRate_CheckedChanged);
            // 
            // ConfigurationDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(265, 253);
            this.Controls.Add(this.gbRateControl);
            this.Controls.Add(this.rbtnCustom);
            this.Controls.Add(this.numCustom);
            this.Controls.Add(this.rbtnFrameRate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numRateTo);
            this.Controls.Add(this.numRateFrom);
            this.Name = "ConfigurationDialog";
            this.ShowInTaskbar = true;
            this.Text = "TimeStretch Configuration Dialog";
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.numRateFrom, 0);
            this.Controls.SetChildIndex(this.numRateTo, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.rbtnFrameRate, 0);
            this.Controls.SetChildIndex(this.numCustom, 0);
            this.Controls.SetChildIndex(this.rbtnCustom, 0);
            this.Controls.SetChildIndex(this.gbRateControl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numRateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCustom)).EndInit();
            this.gbRateControl.ResumeLayout(false);
            this.gbRateControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void rbtnFrameRate_CheckedChanged(object sender, EventArgs e)
        {
            numCustom.Enabled = !(numRateFrom.Enabled=(numRateTo.Enabled=rbtnFrameRate.Checked));
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
            if (rdoCtlRate.Checked == true)
                m_strTimeStretchMethod = "rate";
            else if (rdoCtlPitch.Checked == true)
                m_strTimeStretchMethod = "pitch";
            else
                m_strTimeStretchMethod = "tempo";
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

            internal float ActualTempo
            {
                get
                {
                    return Custom ? Tempo : (100.0F * ToRate) / FromRate;
                }
            }

            internal string Title
            {
                get
                {
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
                "TimeStretch(last, {0}={1})", this.c.Control, this.c.ActualTempo);
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

                if (DialogResult.OK != f.ShowDialog(owner))
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

