using System;
using System.Xml.Serialization;
using BeHappy.Extensibility;

namespace BeHappy
{
	public enum JobState
	{
		Waiting,
		Postponed,
		Processing,
		Error,
		Aborted,
		Done

	}

	/// <summary>
	/// Summary description for Job.
	/// </summary>
	[XmlRoot(Namespace = Constants.DefaultXmlNamespace, ElementName = "BeHappy.Job")]
	public sealed class Job
	{

		public string AviSynthScript;
		public string EncoderExecutable;
		public string CommandLine;
		private string _log;
		public string Log
		{
			get
			{
				return _log;	
			}
			set
			{
				_log = normalizeString(value);
			}
		}

		internal static string normalizeString(string v)
		{
			return ("" + v).Replace(Environment.NewLine, "\n").Replace('\r','\n').Replace("\n", Environment.NewLine);
		}

		[XmlIgnore]
		public JobState State;
		[XmlElement("State")]
		public JobState __stateSpecialWorkaround
		{
			get{ return State==JobState.Processing?JobState.Waiting : State;}
			set{ State =value==JobState.Processing?JobState.Waiting : value;} 
		}
		public string SourceFile;
		public string TargetFile;
		public bool SendRiffHeader;
		public DateTime StartAt;
		public DateTime StopAt;
        public bool bKeepOutput;

		public Job()
		{
		}

		public string Name
		{
			get { return string.Format("{0}->{1}", System.IO.Path.GetFileName(this.SourceFile), System.IO.Path.GetFileName(this.TargetFile)) ;}
		}

	}

    [XmlRoot(Namespace = Constants.DefaultXmlNamespace, ElementName = "BeHappy.JobList")]
    public sealed class JobList
    {
        [XmlElement("Job")]
        public Job[] Jobs;
    }


}
