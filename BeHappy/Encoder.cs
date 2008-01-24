using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace BeHappy
{
	internal delegate void EncoderStatusCallbackDelegate(EncoderCallbackEventArgs s);


	internal sealed class EncoderCallbackEventArgs
	{
		internal enum EventType
		{
			Error,
			Terminated,
			Done,
			Notify,
			Progress,
			StdErr,
			StdOut
		}

		internal string Message = null;
		internal double Progress = -1;
		internal EventType Type = EventType.Error;


		internal EncoderCallbackEventArgs(EventType t)
		{
			this.Type=t;
		}

		internal EncoderCallbackEventArgs(Exception e)
		{
			this.Type=EventType.Error;
			this.Message=e.ToString();
		}

		internal EncoderCallbackEventArgs(string s)
		{
			this.Type=EventType.Notify;
			this.Message=s;
		}


	}
	internal sealed class Encoder
	{

		private Process m_process;
		private string	m_script;
		private string	m_output;
		private string	m_encoder;
		private string	m_commandLine;
		private bool	m_sendHeader;

		private long m_nSampleCount;
		private long m_nSizeInBytes;

		private Thread m_encoderThread = null;
		private Thread m_readFromStdOutThread = null;
		private Thread m_readFromStdErrThread = null;

		internal event EncoderStatusCallbackDelegate EncoderCallback;

		private void raiseEvent(EncoderCallbackEventArgs e)
		{
			if(EncoderCallback!=null)
				EncoderCallback(e);
		}

		private void setProgress(double n)
		{
			EncoderCallbackEventArgs e = new EncoderCallbackEventArgs(EncoderCallbackEventArgs.EventType.Progress);
			e.Progress=n;
			raiseEvent(e);
		}

		private void audioStreamFound()
		{
			raiseEvent(new EncoderCallbackEventArgs("Found Audio Stream"));
		}

		private void audioStreamInfo(AviSynthClip x)
		{
			raiseEvent(new EncoderCallbackEventArgs(string.Format("Channels={0}, BitsPerSample={1}, SampleRate={2}Hz", x.ChannelsCount, x.BitsPerSample, x.AudioSampleRate)));
		}



		internal Encoder(string script, string output, string encoder, string commandLine, bool sendHeader)
		{
			this.m_script = script;
			this.m_output = output;
			this.m_encoder = encoder;
			this.m_commandLine = commandLine;
			this.m_sendHeader = sendHeader;
			m_process = null;
			if(null!=m_encoder)
				if(0==m_encoder.Length)
					m_encoder=null;
		}

		internal Encoder(
			Job job):this(
			job.AviSynthScript, 
			job.TargetFile, 
			job.EncoderExecutable, 
			job.CommandLine, 
			job.SendRiffHeader)
		{
		}

		private void readStdOut()
		{
			readStdStream(true);
		}

		private void readStdErr()
		{
			readStdStream(false);
		}

		private void readStdStream(bool bStdOut)
		{
			EncoderCallbackEventArgs e = new EncoderCallbackEventArgs(bStdOut?EncoderCallbackEventArgs.EventType.StdOut:EncoderCallbackEventArgs.EventType.StdErr);
			using(StreamReader r = bStdOut?m_process.StandardOutput:m_process.StandardError)
			{
				while (!m_process.HasExited)
				{
					Thread.Sleep(0);
					string text1 = r.ReadToEnd(); //r.ReadLine();
					if (text1 != null)
					{
						if(text1.Length>0)
						{
							e.Message = text1;
							raiseEvent(e);
						}
					}
					Thread.Sleep(0);
				}
			}
		}

		private void encode()
		{
			try
			{
				string sTempFileName = saveScriptToTempFile();
				try
				{
					using(AviSynthScriptEnvironment env = new AviSynthScriptEnvironment())
					{
						using(AviSynthClip x = env.ParseScript(m_script)) //.OpenScriptFile(sTempFileName))
						{
							if(0==x.SamplesCount)
								throw new ApplicationException("Can't find audio stream!");
							audioStreamFound();
							readAudioStreamInfo(x);
							audioStreamInfo(x);
							const int MAX_SAMPLES_PER_ONCE=4096;
							int frameSample=0;
							int samplesRead=0;
							int frameBufferTotalSize = MAX_SAMPLES_PER_ONCE*x.ChannelsCount*x.BitsPerSample/8;
							int bytesRead=0; //frameBufferTotalSize;
							byte[] frameBuffer = new byte[frameBufferTotalSize];
							if(m_encoder!=null)
							{
								createEncoderProcess(x);
							}
							try
							{
								using(Stream target = getOutputStream())
								{


									// let's write WAV Header
									if(m_sendHeader)
									{

										raiseEvent(new EncoderCallbackEventArgs("Writing RIFF header to encoder's StdIn"));
										writeHeader(target, x);
									}

									raiseEvent(new EncoderCallbackEventArgs("Writing PCM data to encoder's StdIn"));
									GCHandle h = GCHandle.Alloc(frameBuffer, GCHandleType.Pinned);
									try
									{
										while (frameSample < x.SamplesCount)
										{
											if(m_process!=null)
												if(m_process.HasExited)
													throw new ApplicationException("Abnormal encoder termination " + m_process.ExitCode.ToString());
											samplesRead=0;
											bytesRead=0;

											int nHowMany = Math.Min((int) (x.SamplesCount-frameSample), MAX_SAMPLES_PER_ONCE) ;

											x.ReadAudio(h.AddrOfPinnedObject(),frameSample, nHowMany);
											bytesRead = nHowMany * x.BytesPerSample * x.ChannelsCount;
											samplesRead = nHowMany;
											setProgress((100*(double)frameSample/x.SamplesCount));

											target.Write(frameBuffer,0,bytesRead);
											if(m_process!=null) target.Flush();

											frameSample += samplesRead;

											Thread.Sleep(0);

										}
									}
									finally
									{
										h.Free();
									}

									setProgress(100);
									if(m_sendHeader && (x.SampleType==AudioSampleType.INT24 || x.SampleType==AudioSampleType.INT8))
										target.WriteByte(0);

								}
								if(m_process!=null)
								{
									raiseEvent(new EncoderCallbackEventArgs("Finalizing encoder"));
									m_process.WaitForExit();
									m_readFromStdErrThread.Join();
									m_readFromStdOutThread.Join();
									if(0!=m_process.ExitCode)
										throw new ApplicationException("Abnormal encoder termination " + m_process.ExitCode.ToString());
								}
							}
							finally
							{
								if(m_process!=null)
									if(!m_process.HasExited)
									{
										m_process.Kill();
										m_process.WaitForExit();
										m_readFromStdErrThread.Join();
										m_readFromStdOutThread.Join();
									}
								m_readFromStdErrThread=null;
								m_readFromStdOutThread=null;
							}

						}
					}
				}
				finally
				{
					File.Delete(sTempFileName);
				}
			}
			catch(Exception e)
			{
				clearOutput();
				if(e is ThreadAbortException)
				{
					raiseEvent(new EncoderCallbackEventArgs(EncoderCallbackEventArgs.EventType.Terminated));
				}
				else
				{
					raiseEvent(new EncoderCallbackEventArgs(e));
				}
				return;
			}
			raiseEvent(new EncoderCallbackEventArgs(EncoderCallbackEventArgs.EventType.Done));
		}

		private void clearOutput()
		{
			try
			{
				File.Delete(m_output);
			}
			catch(Exception e2)
			{
				raiseEvent(new EncoderCallbackEventArgs(e2));
			}
		}

		private Stream getOutputStream()
		{
			return (m_process==null)?( new FileStream(m_output, FileMode.Create)):m_process.StandardInput.BaseStream;
		}


		private void createEncoderProcess(AviSynthClip x)
		{
			try
			{
				m_process = new Process();
				ProcessStartInfo info = new ProcessStartInfo();
				// Command line arguments, to be passed to encoder
				// {0} means output file name
				// {1} means samplerate in Hz
				// {2} means bits per sample
				// {3} means channel count
				// {4} means samplecount
				// {5} means size in bytes
				info.Arguments = string.Format(m_commandLine, 
					m_output, x.AudioSampleRate, x.BitsPerSample, x.ChannelsCount,m_nSampleCount,m_nSizeInBytes);
				info.FileName = m_encoder;
				raiseEvent(new EncoderCallbackEventArgs(string.Format("{0} {1}", m_encoder, info.Arguments)));
				info.UseShellExecute = false;
				info.RedirectStandardInput = true;
				info.RedirectStandardOutput = true;
				info.RedirectStandardError = true;
				info.CreateNoWindow = true;
				m_process.StartInfo = info;
				m_process.Start();
				m_process.PriorityClass=ProcessPriorityClass.Idle;
				m_readFromStdOutThread = new Thread(new ThreadStart( readStdOut));
				m_readFromStdErrThread = new Thread(new ThreadStart( readStdErr));
				m_readFromStdOutThread.Start();
				m_readFromStdOutThread.Priority = ThreadPriority.Normal;
				m_readFromStdErrThread.Start();
				m_readFromStdErrThread.Priority = ThreadPriority.Normal;
			}
			catch(Exception e)
			{
				throw new ApplicationException("Can't start encoder: " + e.Message, e);
			}
		}

		private string saveScriptToTempFile()
		{
			string sTempFileName = System.IO.Path.GetTempPath() + "encode-" + Guid.NewGuid().ToString("n") + ".avs";
	
			using( Stream tempAVS = new FileStream(sTempFileName, System.IO.FileMode.CreateNew))
			{
				using( TextWriter avswr = new StreamWriter(tempAVS, System.Text.Encoding.Default))
				{
					avswr.WriteLine(m_script);
				}
			}
			return sTempFileName;
		}

		private void writeHeader(Stream target, AviSynthClip x )
		{
			const uint FAAD_MAGIC_VALUE = 0xFFFFFF00;
			const uint WAV_HEADER_SIZE = 36;
			bool useFaadTrick =  m_nSizeInBytes>=(uint.MaxValue-WAV_HEADER_SIZE);
			target.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"),0,4);
			target.Write(BitConverter.GetBytes(useFaadTrick?FAAD_MAGIC_VALUE:(uint)(m_nSizeInBytes + WAV_HEADER_SIZE)),0,4);
			target.Write(System.Text.Encoding.ASCII.GetBytes("WAVEfmt "),0,8);
			target.Write(BitConverter.GetBytes((uint)0x10),0,4);
#warning Format Tag
			target.Write(BitConverter.GetBytes((short)0x01),0,2);
			target.Write(BitConverter.GetBytes(x.ChannelsCount),0,2);
			target.Write(BitConverter.GetBytes(x.AudioSampleRate),0,4);
			target.Write(BitConverter.GetBytes(x.BitsPerSample*x.AudioSampleRate*x.ChannelsCount/8),0,4);
			target.Write(BitConverter.GetBytes( x.ChannelsCount * x.BitsPerSample/8),0,2);
			target.Write(BitConverter.GetBytes(x.BitsPerSample),0,2);
			target.Write(System.Text.Encoding.ASCII.GetBytes("data"),0,4);
            target.Write(BitConverter.GetBytes(useFaadTrick ? (FAAD_MAGIC_VALUE - WAV_HEADER_SIZE) : (uint)m_nSizeInBytes), 0, 4);
		}


		internal void Start()
		{
			m_encoderThread = new Thread(new ThreadStart(this.encode));
			m_encoderThread.Priority = ThreadPriority.Lowest;
			m_encoderThread.Start();
		}

		internal void Abort()
		{
			m_encoderThread.Abort();
			m_encoderThread = null;
		}


		private void readAudioStreamInfo(AviSynthClip x)
		{
			m_nSampleCount = x.SamplesCount;
			m_nSizeInBytes = m_nSampleCount;
			m_nSizeInBytes*=x.BytesPerSample;
			m_nSizeInBytes*=x.ChannelsCount;
		}

		public bool IsBusy
		{
			get
			{
				return m_encoderThread!=null && m_encoderThread.IsAlive;
			}
		}
	}
}
/*
	internal sealed class Encoder
	{
		#region Some VfW Staff
		[StructLayout(LayoutKind.Sequential)]
		private	struct WAVEFORMATEX
		{
			public ushort	wFormatTag;
			public ushort	nChannels;
			public uint		nSamplesPerSec;
			public uint		nAvgBytesPerSec;
			public ushort	nBlockAlign;
			public ushort	wBitsPerSample;
			public ushort	cbSize;
		}

		[StructLayout(LayoutKind.Sequential)]
		private	struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
		}
 
		[StructLayout(LayoutKind.Sequential)]
		private	struct AVISTREAMINFO
		{
			public uint fccType;
			public uint fccHandler;
			public uint dwFlags;
			public uint dwCaps;
			public ushort wPriority;
			public ushort wLanguage;
			public uint dwScale;
			public uint dwRate;
			public uint dwStart;
			public uint dwLength;
			public uint dwInitialFrames;
			public uint dwSuggestedBufferSize;
			public uint dwQuality;
			public uint dwSampleSize;
			public RECT rcFrame;
			public uint dwEditCount;
			public uint dwFormatChangeCount;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=0x40)]
			public byte[] szName;
		}
 

		[DllImport("avifil32.dll")]
		private static extern void AVIFileExit();
		[DllImport("avifil32.dll")]
		private static extern int AVIFileGetStream(IntPtr pfile, out IntPtr ppavi, uint fccType, int lParam);
		[DllImport("avifil32.dll")]
		private static extern void AVIFileInit();
		[DllImport("avifil32.dll")]
		private static extern int AVIFileOpen(out IntPtr ppfile, string szFile, uint mode, int pclsidHandler);
		[DllImport("avifil32.dll")]
		private static extern uint AVIFileRelease(IntPtr pfile);
		[DllImport("avifil32.dll")]
		private static extern int AVIStreamInfo(IntPtr pavi, out AVISTREAMINFO psi, int lSize);
		[DllImport("avifil32.dll")]
		private static extern int AVIStreamRelease(IntPtr pavi);
		[DllImport("avifil32.dll")]
		private static extern int AVIStreamReadFormat(IntPtr pavi, int lPos, out WAVEFORMATEX lpFormat, out int lpcbFormat);
		[DllImport("avifil32.dll")]
		private static extern uint AVIStreamLength(IntPtr pavi);
		[DllImport("avifil32.dll")]
		private static extern int AVIStreamRead(IntPtr pavi, int lStart, int lSamples, IntPtr lpBuffer, int cbBuffer, out int plBytes, out int plSamples);
		#endregion

		private const uint STREAMTYPE_AUDIO=0x73647561;

		private Process m_process;
		private string	m_script;
		private string	m_output;
		private string	m_encoder;
		private string	m_commandLine;
		private bool	m_sendHeader;

		private uint m_nSampleCount;
		private long m_nSizeInBytes;
		private IntPtr m_aviStream;
		private AVISTREAMINFO m_aviStreamInfo  = new AVISTREAMINFO();
		private WAVEFORMATEX m_wavHeader = new WAVEFORMATEX();
		private IntPtr m_aviFile;

		private Thread m_encoderThread = null;
		private Thread m_readFromStdOutThread = null;
		private Thread m_readFromStdErrThread = null;

		internal event EncoderStatusCallbackDelegate EncoderCallback;

		private void raiseEvent(EncoderCallbackEventArgs e)
		{
			if(EncoderCallback!=null)
				EncoderCallback(e);
		}

		private void setProgress(double n)
		{
			EncoderCallbackEventArgs e = new EncoderCallbackEventArgs(EncoderCallbackEventArgs.EventType.Progress);
			e.Progress=n;
			raiseEvent(e);
		}

		private void audioStreamFound()
		{
			raiseEvent(new EncoderCallbackEventArgs("Found Audio Stream"));
		}

		private void audioStreamInfo()
		{
			raiseEvent(new EncoderCallbackEventArgs(string.Format("Channels={0}, BitsPerSample={1}, SampleRate={2}Hz", m_wavHeader.nChannels, m_wavHeader.wBitsPerSample, m_wavHeader.nSamplesPerSec)));
		}



		internal Encoder(string script, string output, string encoder, string commandLine, bool sendHeader)
		{
			this.m_script = script;
			this.m_output = output;
			this.m_encoder = encoder;
			this.m_commandLine = commandLine;
			this.m_sendHeader = sendHeader;
			m_process = null;
			if(null!=m_encoder)
				if(0==m_encoder.Length)
					m_encoder=null;
		}

		internal Encoder(
			Job job):this(
				job.AviSynthScript, 
				job.TargetFile, 
				job.EncoderExecutable, 
				job.CommandLine, 
				job.SendRiffHeader)
		{
		}

		private void readStdOut()
		{
			readStdStream(true);
		}

		private void readStdErr()
		{
			readStdStream(false);
		}

		private void readStdStream(bool bStdOut)
		{
			EncoderCallbackEventArgs e = new EncoderCallbackEventArgs(bStdOut?EncoderCallbackEventArgs.EventType.StdOut:EncoderCallbackEventArgs.EventType.StdErr);
			using(StreamReader r = bStdOut?m_process.StandardOutput:m_process.StandardError)
			{
				while (!m_process.HasExited)
				{
					Thread.Sleep(0);
					string text1 = r.ReadToEnd(); //r.ReadLine();
					if (text1 != null)
					{
						if(text1.Length>0)
						{
							e.Message = text1;
							raiseEvent(e);
						}
					}
					Thread.Sleep(0);
				}
			}
		}

		private void encode()
		{
			try
			{
				string sTempFileName = saveScriptToTempFile();

				try
				{
					AVIFileInit();
					if(0!=AVIFileOpen(out m_aviFile, sTempFileName, 0x20, 0))
						throw new ApplicationException("Can't open script!");
					try
					{
						int currentAVIStream = 0;
						for(;;)
						{		
							// Go through the streams
							if (0 != AVIFileGetStream(m_aviFile, out m_aviStream, 0, currentAVIStream)) 
							{
								if (currentAVIStream == 0) 
									throw new ApplicationException("AVIFileGetStream failed, unable to get any streams!");
								throw new ApplicationException("Can't find audio stream!");
							}		
							if (0 != AVIStreamInfo(m_aviStream, out m_aviStreamInfo, Marshal.SizeOf(m_aviStreamInfo)))
								throw new ApplicationException("AVIStreamInfo failed, unable to get stream info!");

							if (m_aviStreamInfo.fccType == STREAMTYPE_AUDIO) 
							{
								audioStreamFound();
								readAudioStreamInfo();
								audioStreamInfo();
								break;
							}
							else
							{
								AVIStreamRelease(m_aviStream);
							}
							currentAVIStream++;
						}
						try
						{
							const int MAX_SAMPLES_PER_ONCE=4096;
							int frameSample=0;
							int samplesRead=0;
							int frameBufferTotalSize = MAX_SAMPLES_PER_ONCE*m_wavHeader.nChannels*m_wavHeader.wBitsPerSample/8;
							int bytesRead=0; //frameBufferTotalSize;
							byte[] frameBuffer = new byte[frameBufferTotalSize];
							if(m_encoder!=null)
							{
								createEncoderProcess();
							}
							try
							{
								using(Stream target = getOutputStream())
								{


									// let's write WAV Header
									if(m_sendHeader)
									{

										raiseEvent(new EncoderCallbackEventArgs("Writing RIFF header to encoder's StdIn"));
										writeHeader(target);
									}

									raiseEvent(new EncoderCallbackEventArgs("Writing PCM data to encoder's StdIn"));
									GCHandle h = GCHandle.Alloc(frameBuffer, GCHandleType.Pinned);
									try
									{
										while (frameSample < m_aviStreamInfo.dwLength)
										{
											if(m_process!=null)
												if(m_process.HasExited)
													throw new ApplicationException("Abnormal encoder termination " + m_process.ExitCode.ToString());
											samplesRead=0;
											bytesRead=0;

											int nHowMany = Math.Min((int) (m_aviStreamInfo.dwLength-frameSample), MAX_SAMPLES_PER_ONCE) ;

											if (0!=AVIStreamRead(m_aviStream, frameSample, nHowMany, h.AddrOfPinnedObject() , frameBufferTotalSize, out bytesRead, out samplesRead)) 
												throw new ApplicationException("AVIStreamRead returned error.");
											if (0>=bytesRead)
												throw new ApplicationException("Unexpected EOF!");

											setProgress((100*(double)frameSample/m_aviStreamInfo.dwLength));

											target.Write(frameBuffer,0,bytesRead);
											if(m_process!=null) target.Flush();

											frameSample += samplesRead;

											Thread.Sleep(0);

										}
									}
									finally
									{
										h.Free();
									}

									setProgress(100);
									if(m_sendHeader && m_wavHeader.nBlockAlign%2==1)
										target.WriteByte(0);

								}
								if(m_process!=null)
								{
									raiseEvent(new EncoderCallbackEventArgs("Finalizing encoder"));
									m_process.WaitForExit();
									m_readFromStdErrThread.Join();
									m_readFromStdOutThread.Join();
									if(0!=m_process.ExitCode)
										throw new ApplicationException("Abnormal encoder termination " + m_process.ExitCode.ToString());
								}
							}
							finally
							{
								if(m_process!=null)
									if(!m_process.HasExited)
									{
										m_process.Kill();
										m_process.WaitForExit();
										m_readFromStdErrThread.Join();
										m_readFromStdOutThread.Join();
									}
								m_readFromStdErrThread=null;
								m_readFromStdOutThread=null;
							}
						}
						finally
						{
							AVIStreamRelease(m_aviStream);
						}
					}
					finally
					{
						AVIFileRelease(m_aviFile);
					}
				}
				finally
				{
					AVIFileExit();
					File.Delete(sTempFileName);
				}
			}
			catch(Exception e)
			{
				clearOutput();
				if(e is ThreadAbortException)
				{
					raiseEvent(new EncoderCallbackEventArgs(EncoderCallbackEventArgs.EventType.Terminated));
				}
				else
				{
					raiseEvent(new EncoderCallbackEventArgs(e));
				}
				return;
			}
			raiseEvent(new EncoderCallbackEventArgs(EncoderCallbackEventArgs.EventType.Done));
		}

		private void clearOutput()
		{
			try
			{
				File.Delete(m_output);
			}
			catch(Exception e2)
			{
				raiseEvent(new EncoderCallbackEventArgs(e2));
			}
		}

		private Stream getOutputStream()
		{
			return (m_process==null)?( new FileStream(m_output, FileMode.Create)):m_process.StandardInput.BaseStream;
		}


		private void createEncoderProcess()
		{
			try
			{
				m_process = new Process();
				ProcessStartInfo info = new ProcessStartInfo();
				// Command line arguments, to be passed to encoder
				// {0} means output file name
				// {1} means samplerate in Hz
				// {2} means bits per sample
				// {3} means channel count
				// {4} means samplecount
				// {5} means size in bytes
				info.Arguments = string.Format(m_commandLine, 
					m_output, m_wavHeader.nSamplesPerSec, m_wavHeader.wBitsPerSample, m_wavHeader.nChannels,m_nSampleCount,m_nSizeInBytes);
				info.FileName = m_encoder;
				raiseEvent(new EncoderCallbackEventArgs(string.Format("{0} {1}", m_encoder, info.Arguments)));
				info.UseShellExecute = false;
				info.RedirectStandardInput = true;
				info.RedirectStandardOutput = true;
				info.RedirectStandardError = true;
				info.CreateNoWindow = true;
				m_process.StartInfo = info;
				m_process.Start();
				m_process.PriorityClass=ProcessPriorityClass.Idle;
				m_readFromStdOutThread = new Thread(new ThreadStart( readStdOut));
				m_readFromStdErrThread = new Thread(new ThreadStart( readStdErr));
				m_readFromStdOutThread.Start();
				m_readFromStdOutThread.Priority = ThreadPriority.Normal;
				m_readFromStdErrThread.Start();
				m_readFromStdErrThread.Priority = ThreadPriority.Normal;
			}
			catch(Exception e)
			{
				throw new ApplicationException("Can't start encoder: " + e.Message, e);
			}
		}

		private string saveScriptToTempFile()
		{
			string sTempFileName = System.IO.Path.GetTempPath() + "encode-" + Guid.NewGuid().ToString("n") + ".avs";
	
			using( Stream tempAVS = new FileStream(sTempFileName, System.IO.FileMode.CreateNew))
			{
				using( TextWriter avswr = new StreamWriter(tempAVS, System.Text.Encoding.Default))
				{
					avswr.WriteLine(m_script);
				}
			}
			return sTempFileName;
		}

		private void writeHeader(Stream target )
		{
			const uint FAAD_MAGIC_VALUE = 0xFFFFFF00;
			const uint WAV_HEADER_SIZE = 36;
			bool useFaadTrick =  m_nSizeInBytes>=(uint.MaxValue-WAV_HEADER_SIZE);
			target.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"),0,4);
			target.Write(BitConverter.GetBytes(useFaadTrick?FAAD_MAGIC_VALUE:(uint)(m_nSizeInBytes + WAV_HEADER_SIZE)),0,4);
			target.Write(System.Text.Encoding.ASCII.GetBytes("WAVEfmt "),0,8);
			target.Write(BitConverter.GetBytes((uint)0x10),0,4);
			target.Write(BitConverter.GetBytes(m_wavHeader.wFormatTag),0,2);
			target.Write(BitConverter.GetBytes(m_wavHeader.nChannels),0,2);
			target.Write(BitConverter.GetBytes(m_wavHeader.nSamplesPerSec),0,4);
			target.Write(BitConverter.GetBytes(m_wavHeader.nAvgBytesPerSec),0,4);
			target.Write(BitConverter.GetBytes(m_wavHeader.nBlockAlign),0,2);
			target.Write(BitConverter.GetBytes(m_wavHeader.wBitsPerSample),0,2);
			target.Write(System.Text.Encoding.ASCII.GetBytes("data"),0,4);
			target.Write(BitConverter.GetBytes(useFaadTrick?FAAD_MAGIC_VALUE:(uint)m_nSizeInBytes),0,4);
		}


		internal void Start()
		{
			m_encoderThread = new Thread(new ThreadStart(this.encode));
			m_encoderThread.Priority = ThreadPriority.Lowest;
			m_encoderThread.Start();
		}

		internal void Abort()
		{
			m_encoderThread.Abort();
			m_encoderThread = null;
		}


		private void readAudioStreamInfo()
		{
			int cbACM = Marshal.SizeOf(m_wavHeader);
			m_wavHeader.cbSize = 0;
			if(0!=AVIStreamReadFormat(m_aviStream, 0, out m_wavHeader, out cbACM)) 
				throw new ApplicationException("AVIStreamReadFormat failed!");
			m_nSampleCount = AVIStreamLength(m_aviStream);
			m_nSizeInBytes = m_nSampleCount;
			m_nSizeInBytes*=m_wavHeader.nBlockAlign;
		}

		public bool IsBusy
		{
			get
			{
				return m_encoderThread!=null && m_encoderThread.IsAlive;
			}
		}
	}
}
*/