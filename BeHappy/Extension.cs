using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using BeHappy.DSP.ConfigurationForms;
using BeHappy.Extensibility;
using BeHappy.Extensions;

namespace BeHappy.Extensions
{

	/// <summary>
	/// Summary description for ExtensionBase.
	/// </summary>
	public class ExtensionItemBase : IExtensionItemCommon
	{
		private const string TypeNameAttribute = "Type";
		public string Documentation;
		[XmlAttribute("Name")]
		public string m_title;
        private string script = null;
		[XmlElement("Script")]
		public string Script
        {
            get
            {
                return this.script;
            }
            set
            {
                if (null != value && 0 != value.Length)
                    this.script = Utils.CleanUpString(value);
                else
                    this.script = null;
            }
        }
		[XmlAttribute("UniqueID")]
		public Guid UniqueID;
		[XmlIgnore]
		internal IExtensionItemCommon m_obj;
		[XmlElement("Plugin")]
		public XmlElement ExtensionObject
		{
			get
			{
				if(m_obj==null)
					return null;
				else
				{
					// Serialize m_obj to XML
					XmlElement element = Utility.SerializeObject(m_obj);
					element.SetAttribute(TypeNameAttribute, m_obj.GetType().AssemblyQualifiedName );
					return element;
				}
			}

			set
			{
				if(value==null)
					m_obj = null;
				else
				{
					string typeName = value.GetAttribute(TypeNameAttribute);
					Type type = Type.GetType(typeName, true);
					m_obj = (IExtensionItemCommon)Utility.DeSerializeObject(type, value);
				}
			}
		}

		public ExtensionItemBase()
		{
		}

		protected IExtensionItemCommon imp
		{
			get
			{
				return m_obj==null?this:m_obj;
			}
		}

		public bool IsSupportConfiguration
		{
			get
			{
				return imp is ISupportConfiguration;
			}
		}

		public ConfigurationResult Configure(IWin32Window owner)
		{
			return (imp as ISupportConfiguration).Configure(owner);
		}

        public void ResetConfiguration()
        {
            (imp as ISupportConfiguration).ResetConfiguration();
        }

		public string ScriptBlock
		{
			get
			{
				return imp.GetScript();
			}
		}

		public string Title
		{
			get
			{
				return imp.GetTitle();
			}
		}

		public override string ToString()
		{
			return Title;
		}

		string IExtensionItemCommon.GetTitle()
		{
			return m_title;
		}

		string IExtensionItemCommon.GetScript()
		{
			return Script;
		}

		public XmlElement SaveConfiguration()
		{
			return (imp as ISupportConfiguration).SaveConfiguration();
		}

		public void LoadConfiguration(XmlElement conf)
		{
			(imp as ISupportConfiguration).LoadConfiguration(conf);
		}
	}

	/// <summary>
	/// Summary description for FileRelatedExtensionBase.
	/// </summary>
	public class FileRelatedExtensionItemBase: ExtensionItemBase, IFileRelatedExtensionItemCommon
	{
		[XmlElement("SupportedFileExtension")]
		public string[] m_listOfSupportedFileExtensions;
		
		public string[] ListOfSupportedFileExtensions
		{
			get
			{
				return ((IFileRelatedExtensionItemCommon)imp).GetListOfSupportedExtensions();
			}
		}

		public FileRelatedExtensionItemBase()
		{
		}

		public string GetFilesMask()
		{
			return ListOfSupportedFileExtensions==null ? "*.*" :  "*." + string.Join("; *.",ListOfSupportedFileExtensions);
		}

		public string GetFirstExtension()
		{
			if(ListOfSupportedFileExtensions!=null)
				if(ListOfSupportedFileExtensions.Length!=0)
					return ListOfSupportedFileExtensions[0].ToString();
			return "dat";
		}

		public bool IsSupportedException(string s)
		{
			if(ListOfSupportedFileExtensions==null)
				return true;
			if(ListOfSupportedFileExtensions.Length==0)
				return true;
			if(s.StartsWith("."))
				s=s.Substring(1);
			foreach(string e in ListOfSupportedFileExtensions)
			{
				if(0 == string.Compare( s , e, true))
					return true;
			}
			return false;
		}

		string[] IFileRelatedExtensionItemCommon.GetListOfSupportedExtensions()
		{
			return m_listOfSupportedFileExtensions;
		}
	}

	/// <summary>
	/// Summary description for DigitalSignalProcessor.
	/// </summary>
	[XmlRoot(Namespace = Constants.DefaultXmlNamespace)]
	public sealed class DigitalSignalProcessor: ExtensionItemBase, IDigitalSignalProcessor
	{

		private sealed class NormalizeDSP: IDigitalSignalProcessor, ISupportConfiguration
		{

			private int v;

			public NormalizeDSP()
			{
				ResetConfiguration();
			}

			/// <summary>
			/// Show configuration GUI
			/// </summary>
			/// <returns>Configuration Result</returns>
			public ConfigurationResult Configure(IWin32Window owner)
			{
				using(ConfigureFormForNormalizeDSP f = new ConfigureFormForNormalizeDSP())
				{
					f.value = v;
					f.Text = "Configure normalization factor";
					if(DialogResult.OK==f.ShowDialog(owner))
					{
						v = f.value;
						return ConfigurationResult.OK;
					}
					else
					{
						return ConfigurationResult.Cancel;
					}
				}
			}

			/// <summary>
			/// Must save configuration to XmlElement
			/// </summary>
			/// <returns>Persisted settings in XML form</returns>
			public XmlElement SaveConfiguration()
			{
				return Utility.SerializeObject(v);
			}

			/// <summary>
			/// Must load configuration from XmlElement
			/// </summary>
			/// <param name="configuration">Configuration</param>
			public void LoadConfiguration(XmlElement configuration)
			{
				v = (int)Utility.DeSerializeObject(v.GetType(), configuration);
			}

			/// <summary>
			/// String used for representation in GUI
			/// </summary>
			/// <returns>Title</returns>
			public string GetTitle()
			{
				return string.Format("Normalize to {0}%", v);
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
				//return "Normalize(" + v/100 + "." + v%100 + ")";
				return "Normalize(" + v + ".0/100.0)";
			}

			/// <summary>
			///  Must reset configuration to defaults
			/// </summary>
			public void ResetConfiguration()
			{
				v=100;
			}
		}

		public DigitalSignalProcessor()
		{
		}

        public static DigitalSignalProcessor TimeStretch
        {
            get
            {
                DigitalSignalProcessor processor = new DigitalSignalProcessor();
                processor.UniqueID = new Guid("{5B88ABCE-A424-4E6D-8DFF-299AB7D09FB3}");
                processor.m_obj = new BeHappy.TimeStretch.DSP();
                return processor;
            }

        }

		public static DigitalSignalProcessor Amplify
		{
			get
			{
				DigitalSignalProcessor processor = new DigitalSignalProcessor();
                processor.UniqueID = new Guid("{649A8AC7-45A5-4F46-BD50-9CF03C00D821}");
				processor.m_obj = new BeHappy.Amplify.DSP();
				return processor;
			}
		}

        public static DigitalSignalProcessor Normalize
        {
            get
            {
                DigitalSignalProcessor processor = new DigitalSignalProcessor();
                processor.UniqueID = new Guid("{6158F79F-D8A0-4021-89AE-B77B37C04C55}");
                processor.m_obj = new NormalizeDSP();
                return processor;
            }
        }

		public static DigitalSignalProcessor ToMono
		{
			get
			{
				DigitalSignalProcessor processor = new DigitalSignalProcessor();
				processor.Script="ConvertToMono()";
				processor.m_title="Convert to mono";
				processor.UniqueID=new Guid("{DB5415DD-D524-4e91-94DF-7EFF1F921B56}");
				return processor;
			}
		}
	}


	/// <summary>
	/// Summary description for AudioSource.
	/// </summary>
	[XmlRoot(Namespace = Constants.DefaultXmlNamespace)]
	public sealed class AudioSource: FileRelatedExtensionItemBase, IAudioSource
	{

		public AudioSource()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        public static AudioSource AVS
        {
            get
            {
                AudioSource source = new AudioSource();
                source.Script = "Import(\"{0}\")";
                source.m_listOfSupportedFileExtensions = new string[] { "avs" };
                source.m_title = "AviSynth";
                source.UniqueID = new Guid("{15df59c0-dc7e-11da-8ad9-0800200c9a66}");
                return source;
            }
        }

		public static AudioSource WAV
		{
			get
			{
				AudioSource source = new AudioSource();
				source.Script="WavSource(\"{0}\")";
				source.m_listOfSupportedFileExtensions = new string[]{"wav"};
				source.m_title="WavSource";
				source.UniqueID=new Guid("{98D5F3C4-4D6F-48b2-B0BE-3895796144C3}");
				return source;
			}
		}

		public static AudioSource DirectShow
		{
			get
			{
				AudioSource source = new AudioSource();
				source.Script="DirectShowSource(\"{0}\", video=false)";
				source.m_listOfSupportedFileExtensions = new string[]{"*"};
				source.m_title="DirectShowSource";
				source.UniqueID=new Guid("{7457AF8E-600F-49fe-8AAE-710227D68DE6}");
				return source;
			}
		}
	}

	/// <summary>
	/// Summary description for CommandLineEncoder.
	/// </summary>
	[XmlRoot(Namespace = Constants.DefaultXmlNamespace)]
	public sealed class AudioEncoder: FileRelatedExtensionItemBase, IAudioEncoder
	{

		[XmlElement("ExecutableFileName")]
		public string	m_executableFileName;
		[XmlElement("ExecutableArguments")]
		public string	m_executableArguments;
		[XmlElement("WriteRiffHeader")]
		public string WriteRiffHeaderStr
		{
			get
			{
				return m_writeRiffHeader?null:"false";	
			}
			set
			{
				m_writeRiffHeader=!string.Equals("false", value);
			}
		}
		[XmlIgnore]
		public bool	m_writeRiffHeader=true;

		public string ExecutableFileName
		{
			get
			{
				return ((IAudioEncoder)imp).GetExecutableName();
			}
		}

		public string GetExecutableArguments(string targetFileExtension)
		{
			return ((IAudioEncoder)imp).GetCommandLineArguments(targetFileExtension);
		}

		public bool WriteRiffHeader
		{
			get
			{
				return ((IAudioEncoder)imp).MustSendRiffHeader();
			}
		}


		public AudioEncoder()
		{

		}

        public static AudioEncoder AftenAC3
        {
            get
            {
                AudioEncoder encoder = new AudioEncoder();
                encoder.m_obj = new BeHappy.Aften.Encoder();
                encoder.UniqueID = new Guid("{fffa1491-a3f9-b317-9fff-66676142efff}");
                return encoder;
            }
        }

        public static AudioEncoder CTAAC
        {
            get
            {
                AudioEncoder encoder = new AudioEncoder();
                encoder.m_obj = new BeHappy.CodingTechnologiesAAC.Encoder();
                encoder.UniqueID = new Guid("{be7a1491-a3f9-4314-9fff-6fa76142e9e2}");
                return encoder;
            }
        }



        public static AudioEncoder LameMP3
        {
            get
            {
                AudioEncoder encoder = new AudioEncoder();
                encoder.m_obj = new BeHappy.LameMP3.Encoder();
                encoder.UniqueID = new Guid("{d400e825-e472-4b5d-9788-956b696a0c86}");
                return encoder;
            }
        }

        public static AudioEncoder NeroDigitalAAC
        {
            get
            {
                AudioEncoder encoder = new AudioEncoder();
                encoder.m_obj = new BeHappy.NeroDigitalAAC.Encoder();
                encoder.UniqueID = new Guid("{2a9264e0-dc81-11da-8ad9-0800200c9a66}");
                return encoder;
            }
        }

        public static AudioEncoder OggVorbis
        {
            get
            {
                AudioEncoder encoder = new AudioEncoder();
                encoder.m_obj = new BeHappy.OggVorbis.Encoder();
                encoder.UniqueID = new Guid("{970151FD-EB11-4c93-BA3D-481C5976CF66}");
                return encoder;
            }
        }

		public static AudioEncoder WAV
		{
			get
			{
				AudioEncoder encoder = new AudioEncoder();
				encoder.m_title = "Wav Writer";
				encoder.m_writeRiffHeader= true;
				encoder.m_listOfSupportedFileExtensions = new string[]{"wav"};
				encoder.UniqueID=new Guid("{E6F05427-EF21-467c-AD28-0E43B28F27BB}");

				return encoder;
			}
		}

		public static AudioEncoder RAW
		{
			get
			{
				AudioEncoder encoder = new AudioEncoder();
				encoder.m_title = "Raw PCM Writer";
				encoder.m_writeRiffHeader= false;
				encoder.m_listOfSupportedFileExtensions = new string[]{"dat","pcm","raw","*"};
				encoder.UniqueID=new Guid("{983AB413-134B-4852-A42E-C7A52AE13855}");
				return encoder;
			}
		}

		string IAudioEncoder.GetExecutableName()
		{
			return m_executableFileName;
		}

		string IAudioEncoder.GetCommandLineArguments(string targetFileExtension)
		{
			return m_executableArguments;
		}

		bool IAudioEncoder.MustSendRiffHeader()
		{
			return m_writeRiffHeader;
		}

	}

	/// <summary>
	/// Summary description for ExtensionsCollection.
	/// </summary>
	[XmlRoot(Namespace = Constants.DefaultXmlNamespace, ElementName = "BeHappy.Extension")]
	public class Extension
	{

		[XmlElement("AudioSource")]
		public AudioSource[] AudioSources;
		[XmlElement("AudioDSP")]
		public DigitalSignalProcessor[] DigitalSignalProcessors;
		[XmlElement("AudioEncoder")]
		public AudioEncoder[] AudioEncoders;

		public Extension()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static Extension Default
		{
			get
			{
				Extension configuration = new Extension();
				configuration.AudioSources = new AudioSource[]
				{
					AudioSource.WAV,
					AudioSource.DirectShow,
                    AudioSource.AVS
				};
				configuration.AudioEncoders = new AudioEncoder[]
				{
					AudioEncoder.WAV,
					AudioEncoder.RAW,
                    AudioEncoder.OggVorbis,
                    AudioEncoder.NeroDigitalAAC,
                    AudioEncoder.LameMP3,
                    AudioEncoder.CTAAC,
                    AudioEncoder.AftenAC3
				};
				configuration.DigitalSignalProcessors = new DigitalSignalProcessor[]
				{
					DigitalSignalProcessor.Normalize,
					DigitalSignalProcessor.ToMono,
                    DigitalSignalProcessor.Amplify,
                    DigitalSignalProcessor.TimeStretch
				};
				return configuration;
			}
		}

		public static Extension LoadFromFile(string fileName)
		{
			using(Stream f= new FileStream(fileName, FileMode.Open))
			{
				return Utility.GetXmlSerializer(typeof(Extension)).Deserialize(f) as Extension;
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


	public abstract class MultiOptionBase: ISupportConfiguration
	{

		public sealed class Option
		{
			/// <summary>
			/// Name
			/// </summary>
			public string Name;
            private string value = null;
			public string Value
            {
                get
                {
                    return this.value;
                }
                set
                {
                    if (null != value && 0 != value.Length)
                        this.value = Utils.CleanUpString(value);
                    else
                        this.value = null;
                }
            }
            private string toolTip = null;
            public string ToolTip
            {
                get
                {
                    return this.toolTip;
                }
                set
                {
                    if (null != value && 0 != value.Length)
                        this.toolTip = Utils.CleanUpString(value);
                    else
                        this.toolTip = null;
                }
            }
            [XmlAttribute("default")]
            public bool Default = false;
			public Option()
			{}

			public override string ToString()
			{
				return Name;
			}
		}

        public int DialogWidth = 400;
		public string TitleFormatString;
		public byte[] LogoBitmap;

        private Option[] options;

        [XmlElement("Option")]
        public Option[] Options
        {
            get
            {
                return this.options;
            }
            set
            {
                this.options = value;
                if (value == null) return;
                int i = 0;
                foreach (Option o in value)
                {
                    if (o.Default)
                    {
                        this.m_selectedIndex = i;
                        return;
                    }
                    ++i;
                }
            }
        }


        [XmlRoot("MultiOption.Configuration", Namespace = Constants.DefaultXmlNamespace)]
        public class MultiOptionConfig
        {
            [XmlAttribute("IndexOfSelectedOption")]
            public int Index;

            public MultiOptionConfig():this(0)
            {
            }

            public MultiOptionConfig(int n)
            {
                Index = n;
            }

        }

        private MultiOptionConfig c = new MultiOptionConfig();


        private int m_selectedIndex
        {
            get
            {
                return c.Index;
            }
            set
            {
                c.Index = value;
            }

        }

		public MultiOptionBase()
		{
			ResetConfiguration();
		}

		protected Option currentOption
		{
			get { return Options[m_selectedIndex]; }
		}

		/// <summary>
		/// Show configuration GUI
		/// </summary>
		/// <returns>Configuration Result</returns>
		public ConfigurationResult Configure(IWin32Window owner)
		{
			using(ConfigurationFormForMultiOption f = new ConfigurationFormForMultiOption())
			{
				f.Init(this.ToString(),this.LogoBitmap,  Options, m_selectedIndex, this.DialogWidth);
				if(DialogResult.OK==f.ShowDialog(owner))
				{
					m_selectedIndex = f.GetSelectedIndex();
					return ConfigurationResult.OK;
				}
				else
					return ConfigurationResult.Cancel;
			}
		}

		/// <summary>
		/// Must save configuration to XmlElement
		/// </summary>
		/// <returns>Persisted settings in XML form</returns>
		public XmlElement SaveConfiguration()
		{
			return Utility.SerializeObject(c);
		}

		/// <summary>
		/// Must load configuration from XmlElement
		/// </summary>
		/// <param name="configuration">Configuration</param>
		public void LoadConfiguration(XmlElement configuration)
		{
			c = (MultiOptionConfig)Utility.DeSerializeObject(c.GetType(),configuration);
		}

		/// <summary>
		///  Must reset configuration to defaults
		/// </summary>
		public void ResetConfiguration()
		{
            this.Options = this.Options;
		}

		public override string ToString()
		{
			return string.Format(TitleFormatString, currentOption.Name);
		}

	}


	
	[XmlRoot(Namespace=Constants.DefaultXmlNamespace)]
	public sealed class MultiOptionDSP : MultiOptionBase, IDigitalSignalProcessor 
	{


		public MultiOptionDSP():base()
		{
		}

        private string scriptPrologue = null;
        private string scriptEpilogue = null;

		public string ScriptPrologue
        {
            get
            {
                return this.scriptPrologue;
            }
            set
            {
                if (null != value && 0 != value.Length)
                    this.scriptPrologue = Utils.CleanUpString(value);
                else
                    this.scriptPrologue = null;
            }
        }
		public string ScriptEpilogue
        {
            get
            {
                return this.scriptEpilogue;
            }
            set
            {
                if (null != value && 0 != value.Length)
                    this.scriptEpilogue = Utils.CleanUpString(value);
                else
                    this.scriptEpilogue = null;
            }
        }


		/// <summary>
		/// String used for representation in GUI
		/// </summary>
		/// <returns>Title</returns>
		public string GetTitle()
		{
			return this.ToString();
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
			StringBuilder sb = new StringBuilder();
			if(ScriptPrologue!=null)
				if(ScriptPrologue.Length!=0)
				{
					sb.Append(ScriptPrologue);
					sb.Append(Environment.NewLine);
				}
			sb.Append(currentOption.Value);
		if(ScriptEpilogue!=null)
				if(ScriptEpilogue.Length!=0)
				{
					sb.Append(ScriptEpilogue);
					sb.Append(Environment.NewLine);
				}
			return sb.ToString();
		}
	}

	[XmlRoot(Namespace=Constants.DefaultXmlNamespace)]
	public sealed class MultiOptionEncoder: MultiOptionBase, IAudioEncoder
	{

		public string ExecutableFileName;
		[XmlElement("SupportedFileExtension")]
		public string[] m_listOfSupportedFileExtensions;
		public bool UseRawPCM;
		public string Script;

		public MultiOptionEncoder():base()
		{
			UseRawPCM = false;
		}

		/// <summary>
		/// String used for representation in GUI
		/// </summary>
		/// <returns>Title</returns>
		public string GetTitle()
		{
			return this.ToString();
		}

		/// <summary>
		/// Return executable name
		/// example: oggenc.exe
		/// </summary>
		/// <returns>executable name</returns>
		public string GetExecutableName()
		{
			return ExecutableFileName;
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
			return currentOption.Value;
		}

		/// <summary>
		/// If true BeHappy will send RIFF WAV header to encoder's stdin
		/// otherwize BeHappy will send just raw pcm data
		/// </summary>
		/// <returns></returns>
		public bool MustSendRiffHeader()
		{
			return !UseRawPCM;
		}

		/// <summary>
		/// List of supported file extensions
		/// </summary>
		/// <returns>array of strings</returns>
		public string[] GetListOfSupportedExtensions()
		{
			return m_listOfSupportedFileExtensions;
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
			return Script;
		}
	}

    [XmlRoot(Namespace = Constants.DefaultXmlNamespace)]
    public sealed class MultiOptionSource : MultiOptionBase, IAudioSource
    {

        public string ExecutableFileName;
        [XmlElement("SupportedFileExtension")]
        public string[] m_listOfSupportedFileExtensions;

        public MultiOptionSource()
            : base()
        {
        }

        /// <summary>
        /// String used for representation in GUI
        /// </summary>
        /// <returns>Title</returns>
        public string GetTitle()
        {
            return this.ToString();
        }

        /// <summary>
        /// List of supported file extensions
        /// </summary>
        /// <returns>array of strings</returns>
        public string[] GetListOfSupportedExtensions()
        {
            return m_listOfSupportedFileExtensions;
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
            return currentOption.Value;
        }
    }

}
