using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
		public string Script {
			get {
				return this.script;
			}
			set {
				this.script = String.IsNullOrEmpty(value) ? null : Utils.CleanUpString(value);
			}
		}
		public string LoadAvsPlugin;
		[XmlAttribute("UniqueID")]
		public Guid UniqueID;
		[XmlIgnore]
		internal IExtensionItemCommon m_obj;
		[XmlElement("Plugin")]
		public XmlElement ExtensionObject {
			get	{
				if (m_obj == null)
					return null;
				else {
					// Serialize m_obj to XML
					XmlElement element = Utility.SerializeObject(m_obj);
					element.SetAttribute(TypeNameAttribute, m_obj.GetType().AssemblyQualifiedName);
					return element;
				}
			}
			set	{
				if (value == null)
					m_obj = null;
				else {
					string typeName = value.GetAttribute(TypeNameAttribute);
					Type type = Type.GetType(typeName, true);
					m_obj = (IExtensionItemCommon)Utility.DeSerializeObject(type, value);
				}
			}
		}

		public ExtensionItemBase()
		{
		}

		protected IExtensionItemCommon imp {
			get	{
				return m_obj == null ? this : m_obj;
			}
		}

		public bool IsSupportConfiguration {
			get	{
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

		public string ScriptBlock {
			get	{
				return imp.GetScript();
			}
		}
		
		public string AvsPlugin {
			get {
				return imp.GetAvsPlugin();
			}
		}

		public string Title	{
			get	{
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
		
		string IExtensionItemCommon.GetAvsPlugin()
		{
			return LoadAvsPlugin;
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
		
		public string[] ListOfSupportedFileExtensions {
			get	{
				return ((IFileRelatedExtensionItemCommon)imp).GetListOfSupportedExtensions();
			}
		}

		public FileRelatedExtensionItemBase()
		{
		}

		public string GetFilesMask()
		{
			return ListOfSupportedFileExtensions == null ? "*.*" :  "*." + String.Join("; *.", ListOfSupportedFileExtensions);
		}

		public string GetFirstExtension()
		{
			if (ListOfSupportedFileExtensions == null || ListOfSupportedFileExtensions.Length == 0)
				return "dat";
			else return ListOfSupportedFileExtensions[0].ToString();
		}

		public bool IsSupportedException(string s)
		{
			if (ListOfSupportedFileExtensions == null || ListOfSupportedFileExtensions.Length == 0)
				return true;
			if (s.StartsWith("."))
				s = s.Substring(1);
			
			foreach (string e in ListOfSupportedFileExtensions)
			{
				if (String.Compare(s, e, true) == 0)
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
			private int val;

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
					f.value = val;
					f.Text = "Configure normalization factor";
					if (f.ShowDialog(owner) == DialogResult.OK)
					{
						val = f.value;
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
				return Utility.SerializeObject(val);
			}

			/// <summary>
			/// Must load configuration from XmlElement
			/// </summary>
			/// <param name="configuration">Configuration</param>
			public void LoadConfiguration(XmlElement configuration)
			{
				val = (int)Utility.DeSerializeObject(val.GetType(), configuration);
			}

			/// <summary>
			/// String used for representation in GUI
			/// </summary>
			/// <returns>Title</returns>
			public string GetTitle()
			{
				return String.Format("Normalize to {0}%", val);
			}

			/// <summary>
			/// A part of AviSynth script
			/// {0} means input file name
			/// {1} means output file name
			/// {2} means unique string (to use as part of identifier)
			/// {3} means '{' character (to allow '{' to be used)
			/// {4} means '}' character (to allow '}' to be used)
			/// </summary>
			/// <returns>AviSynth script block</returns>
			public string GetScript()
			{
				return "Normalize(" + val + ".0/100.0)";
			}
			
			public string GetAvsPlugin()
			{
				return null;
			}

			/// <summary>
			///  Must reset configuration to defaults
			/// </summary>
			public void ResetConfiguration()
			{
				val = 100;
			}
		}

		public DigitalSignalProcessor()
		{
		}

		public static DigitalSignalProcessor TimeStretch {
			get {
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

		public static DigitalSignalProcessor Normalize {
			get {
				DigitalSignalProcessor processor = new DigitalSignalProcessor();
				processor.UniqueID = new Guid("{6158F79F-D8A0-4021-89AE-B77B37C04C55}");
				processor.m_obj = new NormalizeDSP();
				return processor;
			}
		}

		public static DigitalSignalProcessor ToMono {
			get {
				DigitalSignalProcessor processor = new DigitalSignalProcessor();
				processor.Script = "ConvertToMono()";
				processor.m_title = "Convert to mono";
				processor.UniqueID = new Guid("{DB5415DD-D524-4e91-94DF-7EFF1F921B56}");
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
		}

		public static AudioSource AVS {
			get {
				AudioSource source = new AudioSource();
				source.Script = "Import(\"{0}\")";
				source.m_listOfSupportedFileExtensions = new string[]{"avs"};
				source.m_title = "AviSynth";
				source.UniqueID = new Guid("{15df59c0-dc7e-11da-8ad9-0800200c9a66}");
				return source;
			}
		}

		public static AudioSource WAV {
			get {
				AudioSource source = new AudioSource();
				source.Script = "WavSource(\"{0}\")";
				source.m_listOfSupportedFileExtensions = new string[]{"wav"};
				source.m_title = "WavSource";
				source.UniqueID = new Guid("{98D5F3C4-4D6F-48b2-B0BE-3895796144C3}");
				return source;
			}
		}

		public static AudioSource DirectShow {
			get {
				AudioSource source = new AudioSource();
				source.Script = "DirectShowSource(\"{0}\", video=false)";
				source.m_listOfSupportedFileExtensions = new string[]{"*"};
				source.m_title = "DirectShowSource";
				source.UniqueID = new Guid("{7457AF8E-600F-49fe-8AAE-710227D68DE6}");
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
		public string m_executableFileName;
		[XmlElement("ExecutableArguments")]
		public string m_executableArguments;
		[XmlElement("WriteRiffHeader")]
		public string WriteRiffHeaderStr {
			get	{
				return m_writeRiffHeader ? null : "false";
			}
			set	{
				m_writeRiffHeader = !String.Equals("false", value);
			}
		}
		[XmlIgnore]
		public bool	m_writeRiffHeader = true;

		[XmlElement("HeaderType")]
		public int Header;
		[XmlElement("IsLossless")]
		public bool Lossless = false;
		
		public string ExecutableFileName {
			get	{
				return ((IAudioEncoder)imp).GetExecutableName();
			}
		}

		public string GetExecutableArguments(string targetFileExtension)
		{
			return ((IAudioEncoder)imp).GetCommandLineArguments(targetFileExtension);
		}

		public bool WriteRiffHeader	{
			get	{
				return ((IAudioEncoder)imp).MustSendRiffHeader();
			}
		}
		
		public int HeaderType {
			get {
				return ((IAudioEncoder)imp).HeaderType();
			}
		}
		
		public bool IsLossless {
			get {
				return ((IAudioEncoder)imp).IsLossless();
			}
		}

		public AudioEncoder()
		{

		}

		public static AudioEncoder WAV {
			get {
				AudioEncoder encoder = new AudioEncoder();
				encoder.m_title = "WAV Writer";
				encoder.m_writeRiffHeader = true;
				encoder.m_listOfSupportedFileExtensions = new string[]{"wav"};
				encoder.Lossless = true;
				encoder.UniqueID = new Guid("{E6F05427-EF21-467c-AD28-0E43B28F27BB}");
				return encoder;
			}
		}

		public static AudioEncoder RAW {
			get {
				AudioEncoder encoder = new AudioEncoder();
				encoder.m_title = "PCM Writer";
				encoder.m_writeRiffHeader = false;
				encoder.m_listOfSupportedFileExtensions = new string[]{"dat","pcm","raw","*"};
				encoder.Lossless = true;
				encoder.UniqueID = new Guid("{983AB413-134B-4852-A42E-C7A52AE13855}");
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
		
		int IAudioEncoder.HeaderType()
		{
			return Header;
		}
		
		bool IAudioEncoder.IsLossless()
		{
			return Lossless;
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
		}

		public static Extension Default {
			get {
				Extension configuration = new Extension();
				configuration.AudioSources = new AudioSource[] {
					AudioSource.WAV,
					AudioSource.DirectShow,
					AudioSource.AVS
				};
				configuration.AudioEncoders = new AudioEncoder[] {
					AudioEncoder.WAV,
					AudioEncoder.RAW,
				};
				configuration.DigitalSignalProcessors = new DigitalSignalProcessor[] {
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
			using(Stream f = new FileStream(fileName, FileMode.Open))
			{
				return Utility.GetXmlSerializer(typeof(Extension)).Deserialize(f) as Extension;
			}
		}

		public void SaveToFile(string fileName)
		{
			using(Stream f = new FileStream(fileName, FileMode.Create))
			{
				Utility.GetXmlSerializer(this.GetType()).Serialize(f,this);
			}
		}

	}


	public abstract class MultiOptionBase: ISupportConfiguration
	{
		public sealed class RadioButtonOption
		{
			public RadioButtonOption()
			{
			}

			public override string ToString()
			{
				return String.Format(Name, string.Empty);
			}
			
			[XmlAttribute("Name")]
			public string Name;
			
			private string value = null;
			public string Value {
				get {
					return this.value;
				}
				set {
					this.value = String.IsNullOrEmpty(value) ? null : Utils.CleanUpString(value);
				}
			}
			
			private string toolTip;
			public string ToolTip {
				get {
					return toolTip;
				}
				set {
					toolTip = String.IsNullOrEmpty(value) ? null : Utils.CleanUpString(value);
				}
			}
			
			[XmlAttribute("Default")]
			public bool Default = false;
			public TrackBarOption Trackbar { get; set; }
			public bool HasTrackbar {
				get {
					return this.Trackbar != null; }
			}
		}

		public sealed  class CheckBoxOption
		{
			public CheckBoxOption()
			{
			}
			
			public override string ToString()
			{
				return Name;
			}
			
			[XmlAttribute("Name")]
			public string Name;
			
			private string valueChecked;
			public string ValueChecked {
				get {
					return valueChecked;
				}
				set {
					valueChecked = String.IsNullOrEmpty(value) ? null : Utils.CleanUpString(value);
				}
			}
			
			private string valueUnChecked;
			public string ValueUnChecked {
				get {
					return valueUnChecked;
				}
				set {
					valueUnChecked = String.IsNullOrEmpty(value) ? null : Utils.CleanUpString(value);
				}
			}
			
			private string toolTip;
			public string ToolTip {
				get {
					return toolTip;
				}
				set {
					toolTip = String.IsNullOrEmpty(value) ? null : Utils.CleanUpString(value);
				}
			}
			
			[XmlAttribute("DefaultChecked")]
			public bool DefaultChecked = false;
		}
		
		public sealed class DropDownOption
		{
			public class DropDownItem
			{
				public DropDownItem() {
				}
				
				public override string ToString()
				{
					return Name;
				}
				
				[XmlAttribute("Name")]
				public string  Name;
				
				private string value = null;
				public string Value {
					get {
						return this.value;
					}
					set {
						this.value = String.IsNullOrEmpty(value) ? null : Utils.CleanUpString(value);
					}
				}				
			}
			
			public DropDownOption()
			{
			}
			
			public override string ToString()
			{
				return Name;
			}
			
			[XmlAttribute("Name")]
			public string Name;
			
			[XmlElement("Item")]
			public DropDownItem[] Item;
			
			private string toolTip;
			public string ToolTip {
				get {
					return toolTip;
				}
				set {
					toolTip = String.IsNullOrEmpty(value) ? null : Utils.CleanUpString(value);
				}
			}
			
			[XmlAttribute("DefaultIndex")]
			public int DefaultIndex;
		}
		
		public sealed class NumericUpDownOption
		{
			public NumericUpDownOption() {
			}
			
			[XmlAttribute("Name")]
			public string Name;
			
			private string value = null;
			public string Value {
				get {
					return this.value;
				}
				set {
					this.value = String.IsNullOrEmpty(value) ? null : Utils.CleanUpString(value);
				}
			}
			
			public float Min;
			public float Max;
			public int DecimalPlaces;
			public float Increment = 1;
			[XmlAttribute("DefaultValue")]
			public float DefaultValue = 0;
			
			private string toolTip;
			public string ToolTip {
				get {
					return toolTip;
				}
				set {
					toolTip = String.IsNullOrEmpty(value) ? null : Utils.CleanUpString(value);
				}
			}
		}
		
		public sealed class TrackBarOption
		{
			public TrackBarOption() {
			}
			
			public int Min;
			public int Max;
			public int TickFrequency = 1;
			[XmlAttribute("DefaultValue")]
			public int DefaultValue;
			[XmlAttribute("FixedValues")]
			public bool FixedValues = false;
			[XmlElement("Value")]
			public string[] Values;
			public float Multiplier = 1;
		}
		
		public int DialogWidth = 400;
		public string TitleFormatString;
		public byte[] LogoBitmap;
		public string LoadAvsPlugin;
		public string Url;
		public string UrlToolTip;
		public bool ShowCommandTextbox = false;
		private string info;
		public string Info {
			get {
				return info;
			}
			set {
				info = String.IsNullOrEmpty(value) ? null : Utils.CleanUpString(value);
			}
		}

		private RadioButtonOption[] radiobuttons;

		[XmlElement("Radiobutton")]
		public RadioButtonOption[] Radiobuttons {
			get {
				return this.radiobuttons;
			}
			set {
				this.radiobuttons = value;
			}
		}

		[XmlElement("Checkbox")]
		public CheckBoxOption[] Checkboxes { get; set; }
		
		[XmlElement("Dropdown")]
		public DropDownOption[] Dropdowns {	get; set; }
		
		[XmlElement("NumericUpdown")]
		public NumericUpDownOption[] NumericUpdowns { get; set; }

		[XmlRoot("MultiOption.Configuration", Namespace = Constants.DefaultXmlNamespace)]
		public class MultiOptionConfig
		{
			public class ConfigIndices
			{
				public ConfigIndices()
				{}
			
			[XmlElement("SelectedIndex")]
			public int[] SelectedIndex { get; set; }
			[XmlElement("Checked")]
			public bool[] Checked { get; set; }
			}
			
			public MultiOptionConfig() {
			}
			
			public int? RadioButtonIndex { get; set; }
			public bool RadioButtonIndexSpecified {
				get { return RadioButtonIndex.HasValue; }
			}
			
			public float[] NumericUpDownValues { get; set; }
			public int[] TrackBarValues { get; set; }
			
			public ConfigIndices DropDownConfig;
			public ConfigIndices CheckBoxConfig;
			public string CustomArgs;			
		}

		private MultiOptionConfig config = new MultiOptionConfig();

		private MultiOptionConfig Config {
			get {
				return config;
			}
			set {
				config = value;
			}
		}

		public MultiOptionBase()
		{
			ResetConfiguration();
		}

		protected string GetCurrentOptionString {
			get {
				if (Radiobuttons != null)
				{
					int index = GetCurrentRadiobuttonIndex;
					int tbVal = GetCurrentTrackbarValue;
					
					if (tbVal != -999999)
					{
						if (Radiobuttons[index].Trackbar.FixedValues == true)
							return String.Format(Radiobuttons[index].Name, Radiobuttons[index].Trackbar.Values[tbVal]);
						else
							return String.Format(CultureInfo.CurrentUICulture, Radiobuttons[index].Name, tbVal * Radiobuttons[index].Trackbar.Multiplier);
					}
					else return String.Format(Radiobuttons[index].Name);
				}
				else return String.Empty;
			}
		}
		
		public int GetCurrentRadiobuttonIndex {
			get {				
				if (Radiobuttons != null)
				{
					if (Config.RadioButtonIndex.HasValue)
						return Config.RadioButtonIndex.Value;
					else
					{
						int index =  Radiobuttons.ToList().FindIndex(r => r.Default == true);
						return index < 0 ? 0 : index;
					}
				}
				else return -1;
			}
		}
		
		public int GetCurrentTrackbarValue {
			get {
				int idx = GetCurrentRadiobuttonIndex;
				
				if (Radiobuttons[idx].Trackbar != null)
				{
					if (Config.TrackBarValues != null && (idx < Config.TrackBarValues.Length))
					{
						return Config.TrackBarValues[idx];
					}
					else return Radiobuttons[idx].Trackbar.DefaultValue;
					
				}
				else return -999999;
			}
		}
		
		public int[] GetCurrentDropdownIndices {
			get {
				if (Dropdowns != null)
				{
					int[] idx = new int[Dropdowns.Length];
					
					for (int i = 0; i < idx.Length; i++)
					{
						if (Config.DropDownConfig != null && Config.DropDownConfig.SelectedIndex != null && (idx.Length == Config.DropDownConfig.SelectedIndex.Length))
						{
							idx[i] = Config.DropDownConfig.SelectedIndex[i];
						}
						else idx[i] = Dropdowns[i].DefaultIndex;
					}
					return idx;
				}
				else return null;
			}
		}
		
		public bool[] GetCurrentCheckboxStates {
			get {
				if (Checkboxes != null)
				{
					bool[] sts = new bool[Checkboxes.Length];
					
					for (int i = 0; i < sts.Length; i++)
					{
						if (Config.CheckBoxConfig != null && Config.CheckBoxConfig.Checked != null && (sts.Length == Config.CheckBoxConfig.Checked.Length))
						{
							sts[i] = Config.CheckBoxConfig.Checked[i];
						}
						else sts[i] = Checkboxes[i].DefaultChecked;
					}
					return sts;
				}
				else return null;
			}
		}
		
		public float[] GetCurrentNumericUpDownValues {
			get {
				if (NumericUpdowns != null)
				{
					float[] vals = new float[NumericUpdowns.Length];
					
					for (int i = 0; i < vals.Length; i++)
					{
						if (Config.NumericUpDownValues != null && (vals.Length == Config.NumericUpDownValues.Length))
						{
							vals[i] = Config.NumericUpDownValues[i];
						}
						else vals[i] = NumericUpdowns[i].DefaultValue;
					}
					return vals;
				}
				else return null;
			}
		}
		
		public string GetDropDownOptions(char separator, bool forGui)
		{
			if (Dropdowns != null)
			{
				var sb = new StringBuilder();
				int[] indices = GetCurrentDropdownIndices;
				
				for (int i = 0; i < Dropdowns.Length; i++)
				{
					if (forGui)
						sb.AppendFormat(" {0}:{1}{2}", Dropdowns[i].Name, Dropdowns[i].Item[indices[i]].Name, separator);
					else sb.Append(String.IsNullOrEmpty(Dropdowns[i].Item[indices[i]].Value) ? String.Empty :
					               Dropdowns[i].Item[indices[i]].Value + separator);
				}
				return sb.ToString();
			}
			else return String.Empty;
		}
		
		public string GetCheckBoxOptions(char separator, bool forGui)
		{
			if (Checkboxes != null)
			{
				var sb = new StringBuilder();
				bool[] states = GetCurrentCheckboxStates;
				
				for (int i = 0; i < Checkboxes.Length; i++)
				{
					if (states[i] == true)
					{
						if (forGui)	sb.Append(" " + Checkboxes[i].Name + separator);
						else sb.Append(String.IsNullOrEmpty(Checkboxes[i].ValueChecked) ? String.Empty : Checkboxes[i].ValueChecked + separator);
					}
					else if (!forGui)
						sb.Append(String.IsNullOrEmpty(Checkboxes[i].ValueUnChecked) ? String.Empty : Checkboxes[i].ValueUnChecked + separator);
				}
				return sb.ToString();
			}
			else return String.Empty;
		}
		
		public string GetRadioButtonOptions(char separator)
		{
			if (Radiobuttons != null)
			{
				int index = GetCurrentRadiobuttonIndex;
				int tbVal = GetCurrentTrackbarValue;

				if (tbVal != -999999)
				{
					if (Radiobuttons[index].Trackbar.FixedValues == true)
						return String.Format(Radiobuttons[index].Value + separator, Radiobuttons[index].Trackbar.Values[tbVal]);
					else
						return String.Format(CultureInfo.InvariantCulture, Radiobuttons[index].Value + separator, tbVal * Radiobuttons[index].Trackbar.Multiplier);
				}
				else return Radiobuttons[index].Value + separator;
			}
			else return String.Empty;
		}
		
		public string GetNumericUpdownOptions(char separator, bool forGui)
		{
			if (NumericUpdowns != null)
			{
				var sb = new StringBuilder();
				float[] nvals = GetCurrentNumericUpDownValues;
				
				for (int i = 0; i < NumericUpdowns.Length; i++)
				{
					if (forGui)
						sb.AppendFormat(" {0}:{1}{2}", NumericUpdowns[i].Name, nvals[i], separator);
					else
						sb.AppendFormat(NumericUpdowns[i].Value + separator, nvals[i]);
				}
				
				return sb.ToString();
			}
			else return String.Empty;
		}
		
		public string GetCustomCmdArgs()
		{
			if (ShowCommandTextbox == true && !String.IsNullOrEmpty(Config.CustomArgs))
			{
				return Config.CustomArgs;
			}
			else return String.Empty;
		}

		/// <summary>
		/// Show configuration GUI
		/// </summary>
		/// <returns>Configuration Result</returns>
		public ConfigurationResult Configure(IWin32Window owner)
		{
			using(ConfigurationFormForMultiOption f = new ConfigurationFormForMultiOption())
			{
				MultiOptionBase mo = this;
				f.Init(mo, Config);
				if (f.ShowDialog(owner) == DialogResult.OK)
				{
					Config = f.GetConfig();
					return ConfigurationResult.OK;
				}
				else return ConfigurationResult.Cancel;
			}
		}

		/// <summary>
		/// Must save configuration to XmlElement
		/// </summary>
		/// <returns>Persisted settings in XML form</returns>
		public XmlElement SaveConfiguration()
		{
			return Utility.SerializeObject(config);
		}

		/// <summary>
		/// Must load configuration from XmlElement
		/// </summary>
		/// <param name="configuration">Configuration</param>
		public void LoadConfiguration(XmlElement configuration)
		{
			config = (MultiOptionConfig)Utility.DeSerializeObject(config.GetType(), configuration);
		}

		/// <summary>
		///  Must reset configuration to defaults
		/// </summary>
		public void ResetConfiguration()
		{
			this.config = new MultiOptionBase.MultiOptionConfig();
		}

		public override string ToString()
		{
			string s = GetCurrentOptionString + ";" + GetDropDownOptions(';', true) + GetNumericUpdownOptions(';', true) + GetCheckBoxOptions(';', true);
			return String.Format(TitleFormatString, s.Trim(';'));
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

		public string ScriptPrologue {
			get {
				return this.scriptPrologue;
			}
			set {
				this.scriptPrologue = String.IsNullOrEmpty(value) ? null : Utils.CleanUpString(value);
			}
		}
		public string ScriptEpilogue {
			get {
				return this.scriptEpilogue;
			}
			set {
				this.scriptEpilogue = String.IsNullOrEmpty(value) ? null : Utils.CleanUpString(value);
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
		/// AviSynth plugin needed to run the script.
		/// e.g. "somePlugin.dll"
		/// </summary>
		/// <returns>String with filename</returns>
		public string GetAvsPlugin()
		{
			return this.LoadAvsPlugin;
		}

		/// <summary>
		/// A part of AviSynth script
		/// {0} means input file name
		/// {1} means output file name
		/// {2} means unique string (to use as part of identifier)
		/// {3} means '{' character (to allow '{' to be used)
		/// {4} means '}' character (to allow '}' to be used)
		/// </summary>
		/// <returns>AviSynth script block</returns>
		public string GetScript()
		{
			StringBuilder sb = new StringBuilder();
			string s = GetRadioButtonOptions(',') + GetDropDownOptions(',', false) + GetCheckBoxOptions(',', false) + GetNumericUpdownOptions(',', false) + GetCustomCmdArgs();
			
			if (!String.IsNullOrEmpty(ScriptPrologue))
			{
				sb.Append(ScriptPrologue);
				sb.Append(Environment.NewLine);
			}
			sb.Append(s.Trim(',', ' '));
			
			if (!String.IsNullOrEmpty(ScriptEpilogue))
			{
				sb.Append(ScriptEpilogue);
				sb.Append(Environment.NewLine);
			}
			return sb.ToString();
		}
	}

	[XmlRoot(Namespace = Constants.DefaultXmlNamespace)]
	public sealed class MultiOptionEncoder: MultiOptionBase, IAudioEncoder
	{
		public string ExecutableFileName;
		[XmlElement("SupportedFileExtension")]
		public string[] m_listOfSupportedFileExtensions;
		public bool UseRawPCM;
		public string Script;
		public string ExecutableCommandline;
		[XmlElement("HeaderType")]
		public int Header;
		[XmlElement("IsLossless")]
		public bool Lossless = false;

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
		/// AviSynth plugin needed to run the script.
		/// e.g. "somePlugin.dll"
		/// </summary>
		/// <returns>String with filename</returns>
		public string GetAvsPlugin()
		{
			return this.LoadAvsPlugin;
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
			var sb = new StringBuilder(GetRadioButtonOptions(' '));
			sb.Append(GetDropDownOptions(' ', false));
			sb.Append(GetCheckBoxOptions(' ', false));
			sb.Append(GetNumericUpdownOptions(' ', false));
			sb.Append(GetCustomCmdArgs());
			return ExecutableCommandline.Replace("%options%", sb.ToString().Trim());
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
		/// Header Type written to encoder.
		/// 0 = WAV; 1 = W64; 2 = RF64
		/// </summary>
		public int HeaderType()
		{
			return Header;
		}
		/// <summary>
		/// Encodes to lossless audio format or not (used for GUI filter).
		/// </summary>
		public bool IsLossless()
		{
			return Lossless;
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
		/// {4} means '}' character (to allow '}' to be used)
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
		[XmlElement("SupportedFileExtension")]
		public string[] m_listOfSupportedFileExtensions;
		public string Script;

		public MultiOptionSource(): base()
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
		/// AviSynth plugin needed to run the script.
		/// e.g. "somePlugin.dll"
		/// </summary>
		/// <returns>String with filename</returns>
		public string GetAvsPlugin()
		{
			return this.LoadAvsPlugin;
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
		/// {4} means '}' character (to allow '}' to be used)
		/// </summary>
		/// <returns>AviSynth script block</returns>
		public string GetScript()
		{
			var sb = new StringBuilder(GetRadioButtonOptions(','));
			sb.Append(GetDropDownOptions(',', false));
			sb.Append(GetCheckBoxOptions(',', false));
			sb.Append(GetNumericUpdownOptions(',', false));
			sb.Append(GetCustomCmdArgs());
			return Script.Replace("%options%", sb.ToString().Trim(',', ' '));
		}
	}
}
