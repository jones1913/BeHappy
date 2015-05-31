using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace BeHappy.Extensibility
{
	public class Constants
	{
		/// <summary>
		/// Default namespace used by BeHappy for object persistance
		/// </summary>
		public const string DefaultXmlNamespace = "http://workspaces.gotdotnet.com/behappy";
	}

	public sealed class Utility
	{

		private static IDictionary m_dict = new HybridDictionary(16);


		public static XmlElement SerializeObject(object o)
		{
			if(o == null)
				return null;
			using(Stream x = new MemoryStream())
			{
				GetXmlSerializer(o.GetType()).Serialize(x,o);
				x.Position = 0;
				XmlDocument doc = new XmlDocument();
				doc.Load(x);
				return doc.DocumentElement;
			}
		}

		public static XmlSerializer GetXmlSerializer(System.Type type)
		{
			lock(m_dict.SyncRoot)
			{
				if(m_dict.Contains(type))
				{
					return (XmlSerializer) m_dict[type];
				}
				else
				{
					XmlSerializer s = new XmlSerializer(type);
					m_dict.Add(type, s);
					return s;
				}
			}
		}

		public static object DeSerializeObject(System.Type type, XmlElement e)
		{
			return e == null ? null : GetXmlSerializer(type).Deserialize(new XmlNodeReader(e));
		}
	}

	/// <summary>
	/// Configuration Result
	/// </summary>
	public enum ConfigurationResult
	{
		/// <summary>
		/// Item was not configured
		/// </summary>
		Cancel,
		/// <summary>
		/// Item was succesfuly configured
		/// </summary>
		OK
	}

	/// <summary>
	/// Extension can implement this interface to allow
	/// configuration via GUI 
	/// </summary>
	public interface ISupportConfiguration
	{
		/// <summary>
		/// Show configuration GUI
		/// </summary>
		/// <returns>Configuration Result</returns>
		ConfigurationResult Configure( IWin32Window owner );

		/// <summary>
		/// Must save configuration to XmlElement
		/// </summary>
		/// <returns>Persisted settings in XML form</returns>
		XmlElement SaveConfiguration();

		/// <summary>
		/// Must load configuration from XmlElement
		/// </summary>
		/// <param name="configuration">Configuration</param>
		void LoadConfiguration(XmlElement configuration);

		/// <summary>
		///  Must reset configuration to defaults
		/// </summary>
		void ResetConfiguration();
	}

	/// <summary>
	/// Basic extensibility interface, common for each type of plugin
	/// </summary>
	public interface IExtensionItemCommon
	{
		/// <summary>
		/// String used for representation in GUI
		/// </summary>
		/// <returns>Title</returns>
		string GetTitle();

		/// <summary>
		/// A part of AviSynth script
		/// {0} means input file name
		/// {1} means output file name
		/// {2} means unique string (to use as part of identifier)
		/// {3} means '{' character (to allow '{' to be used)
		/// {4} means '}' character (to allow '}' to be used)
		/// </summary>
		/// <returns>AviSynth script block</returns>
		string GetScript();
		
		/// <summary>
		/// AviSynth plugin needed to run the script.
		/// e.g. "somePlugin.dll"
		/// </summary>
		/// <returns>String with filename</returns>
		string GetAvsPlugin();
	}

	/// <summary>
	/// Basic extensibility interface, common for Source and Encoder plugins
	/// </summary>
	public interface IFileRelatedExtensionItemCommon: IExtensionItemCommon
	{
		/// <summary>
		/// List of supported file extensions
		/// </summary>
		/// <returns>array of strings</returns>
		string[] GetListOfSupportedExtensions();
	}

	/// <summary>
	/// Each DSP must implement this interface
	/// </summary>
	public interface IDigitalSignalProcessor: IExtensionItemCommon
	{
		
	}

	/// <summary>
	/// Each AudioSource must implement this interface
	/// </summary>
	public interface IAudioSource: IFileRelatedExtensionItemCommon
	{
		
	}

	/// <summary>
	/// Each AudioEncoder must implement this interface
	/// </summary>
	public interface IAudioEncoder: IFileRelatedExtensionItemCommon
	{
		/// <summary>
		/// Return executable name
		/// example: oggenc.exe
		/// </summary>
		/// <returns>executable name</returns>
		string GetExecutableName();
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
		string GetCommandLineArguments(string targetFileExtension);
		/// <summary>
		/// If true BeHappy will send RIFF WAV header to encoder's stdin
		/// otherwize BeHappy will send just raw pcm data
		/// </summary>
		/// <returns></returns>
		bool MustSendRiffHeader();
		/// <summary>
		/// Header Type written to encoder.
		/// 0 = WAV; 1 = W64; 2 = RF64
		/// </summary>
		int HeaderType();
		/// <summary>
		/// Encodes to lossless audio format or not (used for GUI filter).
		/// </summary>
		bool IsLossless();
	}
	

}
