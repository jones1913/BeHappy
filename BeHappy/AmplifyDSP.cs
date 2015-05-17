using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BeHappy.Extensibility;
using System.Xml.Serialization;

namespace BeHappy.Amplify
{
    internal partial class ConfigurationDialog : BeHappy.ConfigurationFormBase
    {
        public ConfigurationDialog()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
        }
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
        
        public string GetAvsPlugin()
		{
			return null;
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

