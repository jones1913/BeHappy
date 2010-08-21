using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using BeHappy.Extensibility;
using System.Windows.Forms;

namespace BeHappy
{
	/// <summary>
	/// Summary description for Configuration.
	/// </summary>
	[XmlRoot(Namespace = Constants.DefaultXmlNamespace, ElementName = "BeHappy.Configuration")]
	public class Configuration
	{


		public sealed class __pluginPersistance
		{
			[XmlAttribute("UniqueID")]
			public Guid UniqueID;
			public XmlElement Xml;

			public __pluginPersistance()
			{
			}

			public __pluginPersistance(Guid uniqueId, XmlElement configuration)
			{
				UniqueID = uniqueId;
				Xml = configuration;
			}
		}

		protected IDictionary m_dict = new Hashtable();

		[XmlIgnore]
		public IDictionary Settings
		{
			get{return m_dict;}
		}

		[XmlElement("PluginConfig")]
		public __pluginPersistance[] __plugins
		{
			get
			{
				ArrayList temp = new ArrayList();
				foreach(Guid g in m_dict.Keys)
				{
					temp.Add(new __pluginPersistance(g,m_dict[g] as XmlElement));
				}
				return (__pluginPersistance[]) temp.ToArray(typeof(__pluginPersistance));
			}

			set
			{
				m_dict.Clear();
				foreach(__pluginPersistance pp in value)
					if(pp.Xml!=null)
						m_dict.Add(pp.UniqueID, pp.Xml);
			}
		}

		public JobList JobList;
		public Guid[] DspOrder;
		public Guid CurrentEncoder;
        public GuiPosition GuiPosition;
        public MiscSettings MiscSettings;

		public Configuration()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static Configuration LoadFromFile(string fileName)
		{
			using(Stream f= new FileStream(fileName, FileMode.Open))
			{
				return Utility.GetXmlSerializer(typeof(Configuration)).Deserialize(f) as Configuration;
			}
		}

		public void SaveToFile(string fileName)
		{
            try
            {
                using (Stream f = new FileStream(fileName, FileMode.Create))
                {
                    Utility.GetXmlSerializer(this.GetType()).Serialize(f, this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.InnerException, "Save To File Exception");
            }
		}
	}
	[XmlRoot(Namespace = Constants.DefaultXmlNamespace, ElementName = "BeHappy.GUIPosition")]
    public sealed class GuiPosition
    {
        [XmlElement("Top")]
        public int iTop;
        [XmlElement("Left")]
        public int iLeft;
        [XmlElement("Width")]
        public int iWidth;
        [XmlElement("Height")]
        public int iHeight;
    }
    [XmlRoot(Namespace = Constants.DefaultXmlNamespace, ElementName = "BeHappy.MiscSettings")]
    public sealed class MiscSettings
    {
        [XmlElement("DirectShowPlayer")]
        public string directShowPlayer;
        [XmlElement("PreferMP4overM4A")]
        public bool preferMP4overM4A;

    }
}
