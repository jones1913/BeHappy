using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using BeHappy.Extensibility;

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
			using(Stream f= new FileStream(fileName, FileMode.Create))
			{
				Utility.GetXmlSerializer(this.GetType()).Serialize(f,this);
			}
		}
	}
}
