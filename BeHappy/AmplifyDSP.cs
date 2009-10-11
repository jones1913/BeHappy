using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BeHappy.Extensibility;
using System.Xml.Serialization;

namespace BeHappy.Amplify
{
    internal class ConfigurationDialog : BeHappy.ConfigurationFormBase
    {
        private Label label1;
        internal CheckBox cbxDB;
        internal NumericUpDown numValue;
        private System.ComponentModel.IContainer components = null;

        public ConfigurationDialog()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbxDB = new System.Windows.Forms.CheckBox();
            this.numValue = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numValue)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(49, 46);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(125, 46);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Amplify by";
            // 
            // cbxDB
            // 
            this.cbxDB.AutoSize = true;
            this.cbxDB.Location = new System.Drawing.Point(158, 13);
            this.cbxDB.Name = "cbxDB";
            this.cbxDB.Size = new System.Drawing.Size(41, 17);
            this.cbxDB.TabIndex = 3;
            this.cbxDB.Text = "DB";
            this.cbxDB.UseVisualStyleBackColor = true;
            // 
            // numValue
            // 
            this.numValue.DecimalPlaces = 4;
            this.numValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numValue.Location = new System.Drawing.Point(72, 12);
            this.numValue.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numValue.Name = "numValue";
            this.numValue.Size = new System.Drawing.Size(80, 20);
            this.numValue.TabIndex = 7;
            // 
            // ConfigurationDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(201, 71);
            this.Controls.Add(this.numValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxDB);
            this.Name = "ConfigurationDialog";
            this.Text = "Amplify Configuration Dialog";
            this.Controls.SetChildIndex(this.cbxDB, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.numValue, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

    }

    [XmlRoot("AmplifyDSP", Namespace = Constants.DefaultXmlNamespace)]
    public sealed class DSP : IDigitalSignalProcessor, ISupportConfiguration
    {
        [XmlRoot("AmplifyDSP.Configuration", Namespace = Constants.DefaultXmlNamespace)]
        public sealed class Config
        {
            public float Amount = 0F;
            public bool Db = false;
        }

        private Config c;

        public DSP()
        {
            (this as ISupportConfiguration).ResetConfiguration();
        }

        #region IExtensionItemCommon Members

        string IExtensionItemCommon.GetTitle()
        {
            return "Amplify by " + this.c.Amount + " " + (this.c.Db ? "Db" : null);
        }

        string IExtensionItemCommon.GetScript()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}({1})", this.c.Db ? "AmplifyDb" : "Amplify", this.c.Amount);
        }

        #endregion

        #region ISupportConfiguration Members

        ConfigurationResult ISupportConfiguration.Configure(IWin32Window owner)
        {
            using (ConfigurationDialog f = new ConfigurationDialog())
            {
                f.numValue.Value = (decimal)this.c.Amount;
                f.cbxDB.Checked = this.c.Db;
                if (DialogResult.OK != f.ShowDialog(owner))
                    return ConfigurationResult.Cancel;
                this.c.Amount = (float)f.numValue.Value;
                this.c.Db = f.cbxDB.Checked;
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

