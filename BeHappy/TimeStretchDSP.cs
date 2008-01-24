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
        private System.ComponentModel.IContainer components = null;

        public ConfigurationDialog()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
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
            this.numRateFrom = new System.Windows.Forms.NumericUpDown();
            this.numRateTo = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtnFrameRate = new System.Windows.Forms.RadioButton();
            this.rbtnCustom = new System.Windows.Forms.RadioButton();
            this.numCustom = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numRateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCustom)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(90, 131);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(166, 131);
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
            this.rbtnCustom.Size = new System.Drawing.Size(106, 17);
            this.rbtnCustom.TabIndex = 14;
            this.rbtnCustom.Text = "Custom transform";
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
            // ConfigurationDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(242, 156);
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
            ((System.ComponentModel.ISupportInitialize)(this.numRateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCustom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void rbtnFrameRate_CheckedChanged(object sender, EventArgs e)
        {
            numCustom.Enabled = !(numRateFrom.Enabled=(numRateTo.Enabled=rbtnFrameRate.Checked));
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
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "TimeStretch(last, tempo={0})", this.c.ActualTempo);
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
                f.rbtnFrameRate.Checked = !(f.rbtnCustom.Checked = this.c.Custom);
                if (DialogResult.OK != f.ShowDialog(owner))
                    return ConfigurationResult.Cancel;
                this.c.Tempo = (float)f.numCustom.Value;
                this.c.FromRate = (float)f.numRateFrom.Value;
                this.c.ToRate = (float)f.numRateTo.Value;
                this.c.Custom = f.rbtnCustom.Checked;
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

