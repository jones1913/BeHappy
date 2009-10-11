using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace BeHappy {
    internal delegate void EncoderStatusCallbackDelegate(EncoderCallbackEventArgs s);


    internal sealed class EncoderCallbackEventArgs {
        internal enum EventType {
            Error,
            Terminated,
            Done,
            Notify,
            Progress,
            StdErr,
            StdOut,
            KeepOutput
        }

        internal string Message = null;
        internal double Progress = -1;
        internal EventType Type = EventType.Error;


        internal EncoderCallbackEventArgs(EventType t) {
            this.Type=t;
        }

        internal EncoderCallbackEventArgs(Exception e) {
            this.Type=EventType.Error;
            this.Message=e.ToString();
        }

        internal EncoderCallbackEventArgs(string s) {
            this.Type=EventType.Notify;
            this.Message=s;
        }
    }

    internal sealed class Encoder {

        private Process m_process;
        private string  m_script;
        private string  m_output;
        private string  m_encoder;
        private string  m_commandLine;
        private bool    m_sendHeader;
        private int     m_HeaderType;
        private int     m_ChannelMask;
        private bool    m_bKeepOutput;

        private long m_nSampleCount;
        private long m_nSizeInBytes;
        private uint m_nFormatTag;     // 1 for int, 3 for float

        private Thread m_encoderThread = null;
        private Thread m_readFromStdOutThread = null;
        private Thread m_readFromStdErrThread = null;

        internal event EncoderStatusCallbackDelegate EncoderCallback;

        private void raiseEvent(EncoderCallbackEventArgs e) {
            if(EncoderCallback!=null)
                EncoderCallback(e);
        }

        private void setProgress(double n) {
            EncoderCallbackEventArgs e = new EncoderCallbackEventArgs(EncoderCallbackEventArgs.EventType.Progress);
            e.Progress=n;
            raiseEvent(e);
        }

        private void audioStreamFound() {
            raiseEvent(new EncoderCallbackEventArgs("Found Audio Stream"));
        }

        private void audioStreamInfo(AviSynthClip x) {
            if(x.SampleType==AudioSampleType.FLOAT) {                 // New for float output
                raiseEvent(new EncoderCallbackEventArgs(string.Format("Channels={0}, BitsPerSample={1} float, SampleRate={2}Hz", x.ChannelsCount, x.BitsPerSample, x.AudioSampleRate)));
            }
            else {
                raiseEvent(new EncoderCallbackEventArgs(string.Format("Channels={0}, BitsPerSample={1} int, SampleRate={2}Hz", x.ChannelsCount, x.BitsPerSample, x.AudioSampleRate)));
            }
        }

        internal Encoder(string script, string output, string encoder, string commandLine, bool sendHeader, int HeaderType, int ChannelMask, bool bKeepOutput) {
            this.m_script = script;
            this.m_output = output;
            this.m_encoder = encoder;
            this.m_commandLine = commandLine;
            this.m_sendHeader = sendHeader;
            this.m_HeaderType = HeaderType;
            this.m_ChannelMask = ChannelMask;
            this.m_bKeepOutput = bKeepOutput;
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
            job.SendRiffHeader,
            job.HeaderType,
            job.ChannelMask,
            job.bKeepOutput) {
        }

        private void readStdOut() {
            readStdStream(true);
        }

        private void readStdErr() {
            readStdStream(false);
        }

        private void readStdStream(bool bStdOut) {
            EncoderCallbackEventArgs e = new EncoderCallbackEventArgs(bStdOut?EncoderCallbackEventArgs.EventType.StdOut:EncoderCallbackEventArgs.EventType.StdErr);
            using(StreamReader r = bStdOut?m_process.StandardOutput:m_process.StandardError) {
                while (!m_process.HasExited) {
                    Thread.Sleep(0);
                    string text1 = r.ReadToEnd(); //r.ReadLine();
                    if (text1 != null) {
                        if(text1.Length>0) {
                            e.Message = text1;
                            raiseEvent(e);
                        }
                    }
                    Thread.Sleep(0);
                }
            }
        }

        private void encode() {
            try {
                string sTempFileName = saveScriptToTempFile();
                try {
                    using(AviSynthScriptEnvironment env = new AviSynthScriptEnvironment()) {
                        using(AviSynthClip x = env.ParseScript(m_script)) { //.OpenScriptFile(sTempFileName))
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
                            bool WExtHeader =  m_ChannelMask >= 0;
                            bool Greater4GB =  m_nSizeInBytes>=(uint.MaxValue-68);
                            if ((Greater4GB) && (m_HeaderType > 2)) m_HeaderType -=2;
                            if (m_encoder!=null) {
                                createEncoderProcess(x);
                            }
                            try {
                                using(Stream target = getOutputStream())
                                {
                                    // let's write WAV Header
                                    if(m_sendHeader) {

                                        if (m_HeaderType == 1) {                      // Useful for debug
                                            if (WExtHeader)
                                                raiseEvent(new EncoderCallbackEventArgs("Writing W64_EXT header to encoder's StdIn"));
                                            else
                                                raiseEvent(new EncoderCallbackEventArgs("Writing W64 header to encoder's StdIn"));
                                        } else if (m_HeaderType == 2) {
                                            if (WExtHeader)
                                                raiseEvent(new EncoderCallbackEventArgs("Writing RF64_EXT header to encoder's StdIn"));
                                            else
                                                raiseEvent(new EncoderCallbackEventArgs("Writing RF64 header to encoder's StdIn"));
                                        } else {
                                            if (WExtHeader)
                                                raiseEvent(new EncoderCallbackEventArgs("Writing RIFF_EXT header to encoder's StdIn"));
                                            else
                                                raiseEvent(new EncoderCallbackEventArgs("Writing RIFF header to encoder's StdIn"));
                                        }
                                        writeHeader(target, x);
                                    }

                                    raiseEvent(new EncoderCallbackEventArgs("Writing PCM data to encoder's StdIn"));
                                    GCHandle h = GCHandle.Alloc(frameBuffer, GCHandleType.Pinned);
                                    try {
                                        while (frameSample < x.SamplesCount) {
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
                                    finally {
                                        h.Free();
                                    }

                                    setProgress(100);

                                }
                                if(m_process!=null) {
                                    raiseEvent(new EncoderCallbackEventArgs("Finalizing encoder"));
                                    m_process.WaitForExit();
                                    m_readFromStdErrThread.Join();
                                    m_readFromStdOutThread.Join();
                                    if(0!=m_process.ExitCode)
                                        throw new ApplicationException("Abnormal encoder termination " + m_process.ExitCode.ToString());
                                }
                            }
                            finally {
                                if(m_process!=null)
                                    if(!m_process.HasExited) {
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
                finally {
                    File.Delete(sTempFileName);
                }
            }
            catch(Exception e) {
                clearOutput();
                if(e is ThreadAbortException) {
                    raiseEvent(new EncoderCallbackEventArgs(EncoderCallbackEventArgs.EventType.Terminated));
                }
                else {
                    raiseEvent(new EncoderCallbackEventArgs(e));
                }
                return;
            }
            raiseEvent(new EncoderCallbackEventArgs(EncoderCallbackEventArgs.EventType.Done));
        }

        private void clearOutput() {
            try {
                if (m_bKeepOutput) {
                    raiseEvent(new EncoderCallbackEventArgs("Any output was kept for you to inspect."));
                }
                else {
                    File.Delete(m_output);
                }
            }
            catch(Exception e2) {
                raiseEvent(new EncoderCallbackEventArgs(e2));
            }
        }

        private Stream getOutputStream() {
            return (m_process==null)?( new FileStream(m_output, FileMode.Create)):m_process.StandardInput.BaseStream;
        }

        private void createEncoderProcess(AviSynthClip x) {
            try {
                m_process = new Process();
                ProcessStartInfo info = new ProcessStartInfo();
                // Command line arguments, to be passed to encoder
                // {0} means output file name
                // {1} means samplerate in Hz
                // {2} means bits per sample
                // {3} means channel count
                // {4} means samplecount
                // {5} means size in bytes
                // {6} means format (1 int, 3 float)
                info.Arguments = string.Format(m_commandLine,
                    m_output, x.AudioSampleRate, x.BitsPerSample, x.ChannelsCount,m_nSampleCount,m_nSizeInBytes,m_nFormatTag);
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
            catch(Exception e) {
                throw new ApplicationException("Can't start encoder: " + e.Message, e);
            }
        }

        private string saveScriptToTempFile() {
            string sTempFileName = System.IO.Path.GetTempPath() + "encode-" + Guid.NewGuid().ToString("n") + ".avs";

            using( Stream tempAVS = new FileStream(sTempFileName, System.IO.FileMode.CreateNew)) {
                using( TextWriter avswr = new StreamWriter(tempAVS, System.Text.Encoding.Default)) {
                    avswr.WriteLine(m_script);
                }
            }
            return sTempFileName;
        }

        private void writeHeader(Stream target, AviSynthClip x ) {
            const uint FAAD_MAGIC_VALUE = 0xFFFFFF00;
            bool Greater4GB =  m_nSizeInBytes>=(uint.MaxValue-68);
            bool WExtHeader =  m_ChannelMask >= 0;
            uint HeaderSize = (uint)(WExtHeader ? 60 : 36);
            int[] defmask = {0, 4, 3, 7, 51, 55, 63, 319, 1599, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            if (m_HeaderType == 1) {                                          // W64
                HeaderSize = (uint)(WExtHeader ? 128 : 112);
                target.Write(System.Text.Encoding.ASCII.GetBytes("riff"),0,4);
                target.Write(BitConverter.GetBytes((uint)0x11CF912E),0,4);  // GUID
                target.Write(BitConverter.GetBytes((uint)0xDB28D6A5),0,4);
                target.Write(BitConverter.GetBytes((uint)0x0000C104),0,4);
                target.Write(BitConverter.GetBytes((m_nSizeInBytes + HeaderSize)),0,8);
                target.Write(System.Text.Encoding.ASCII.GetBytes("wave"),0,4);
                target.Write(BitConverter.GetBytes((uint)0x11D3ACF3),0,4);  // GUID
                target.Write(BitConverter.GetBytes((uint)0xC000D18C),0,4);
                target.Write(BitConverter.GetBytes((uint)0x8ADB8E4F),0,4);
                target.Write(System.Text.Encoding.ASCII.GetBytes("fmt "),0,4);
                target.Write(BitConverter.GetBytes((uint)0x11D3ACF3),0,4);  // GUID
                target.Write(BitConverter.GetBytes((uint)0xC000D18C),0,4);
                target.Write(BitConverter.GetBytes((uint)0x8ADB8E4F),0,4);
                target.Write(BitConverter.GetBytes(WExtHeader ? (ulong)64 : (ulong)48),0,8);
            } else if (m_HeaderType == 2) {                                   // RF64
                HeaderSize += 36;
                target.Write(System.Text.Encoding.ASCII.GetBytes("RF64"),0,4);
                target.Write(BitConverter.GetBytes((uint)0xFFFFFFFF),0,4);
                target.Write(System.Text.Encoding.ASCII.GetBytes("WAVE"),0,4);
                target.Write(System.Text.Encoding.ASCII.GetBytes("ds64"),0,4);  // new ds64 chunk 36 bytes
                target.Write(BitConverter.GetBytes((uint)28),0,4);
                target.Write(BitConverter.GetBytes((m_nSizeInBytes + HeaderSize)),0,8);
                target.Write(BitConverter.GetBytes((m_nSizeInBytes)),0,8);
                target.Write(BitConverter.GetBytes((m_nSampleCount)),0,8);
                target.Write(BitConverter.GetBytes((uint)0x0000),0,4);
                target.Write(System.Text.Encoding.ASCII.GetBytes("fmt "),0,4);
                target.Write(BitConverter.GetBytes(WExtHeader ? (uint)40 : (uint)16),0,4);
            } else {                                                          //WAV
                target.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"),0,4);
                target.Write(BitConverter.GetBytes(Greater4GB ? (FAAD_MAGIC_VALUE + HeaderSize):(uint)(m_nSizeInBytes + HeaderSize)),0,4);
                target.Write(System.Text.Encoding.ASCII.GetBytes("WAVE"),0,4);
                target.Write(System.Text.Encoding.ASCII.GetBytes("fmt "),0,4);
                target.Write(BitConverter.GetBytes(WExtHeader ? (uint)40 : (uint)16),0,4);
            }
            // fmt chunk common
            target.Write(BitConverter.GetBytes(WExtHeader ? (uint)0xFFFE : m_nFormatTag),0,2);
            target.Write(BitConverter.GetBytes(x.ChannelsCount),0,2);
            target.Write(BitConverter.GetBytes(x.AudioSampleRate),0,4);
            target.Write(BitConverter.GetBytes(x.BitsPerSample*x.AudioSampleRate*x.ChannelsCount/8),0,4);
            target.Write(BitConverter.GetBytes(x.ChannelsCount * x.BitsPerSample/8),0,2);
            target.Write(BitConverter.GetBytes(x.BitsPerSample),0,2);

            // if WAVE_FORMAT_EXTENSIBLE continue fmt chunk
            if (WExtHeader) {
                if (m_ChannelMask == 0) m_ChannelMask = defmask[x.ChannelsCount];
                target.Write(BitConverter.GetBytes((uint)0x16),0,2);
                target.Write(BitConverter.GetBytes(x.BitsPerSample),0,2);
                target.Write(BitConverter.GetBytes(m_ChannelMask),0,4);
                target.Write(BitConverter.GetBytes(m_nFormatTag),0,4);
                target.Write(BitConverter.GetBytes((uint)0x00100000),0,4); // GUID
                target.Write(BitConverter.GetBytes((uint)0xAA000080),0,4);
                target.Write(BitConverter.GetBytes((uint)0x719B3800),0,4);
            }
            // data chunk
            if (m_HeaderType == 1) {                                                // W64
                if (!WExtHeader) {
                    target.Write(BitConverter.GetBytes((uint)0x0000D000),0,4); // pad
                    target.Write(BitConverter.GetBytes((uint)0x0000D000),0,4);
                }
                target.Write(System.Text.Encoding.ASCII.GetBytes("data"),0,4);
                target.Write(BitConverter.GetBytes((uint)0x11D3ACF3),0,4);  // GUID
                target.Write(BitConverter.GetBytes((uint)0xC000D18C),0,4);
                target.Write(BitConverter.GetBytes((uint)0x8ADB8E4F),0,4);
                target.Write(BitConverter.GetBytes(m_nSizeInBytes + 24), 0, 8);
            } else if (m_HeaderType == 2) {                                         // RF64
                target.Write(System.Text.Encoding.ASCII.GetBytes("data"),0,4);
                target.Write(BitConverter.GetBytes((uint)0xFFFFFFFF),0,4);
            } else {                                                                // WAV
                target.Write(System.Text.Encoding.ASCII.GetBytes("data"),0,4);
                target.Write(BitConverter.GetBytes(Greater4GB ? FAAD_MAGIC_VALUE : (uint)m_nSizeInBytes), 0, 4);
            }
        }

        internal void Start() {
            m_encoderThread = new Thread(new ThreadStart(this.encode));
            m_encoderThread.Priority = ThreadPriority.Lowest;
            m_encoderThread.Start();
        }

        internal void Abort() {
            m_encoderThread.Abort();
            m_encoderThread = null;
        }

        private void readAudioStreamInfo(AviSynthClip x) {
            m_nSampleCount = x.SamplesCount;
            m_nSizeInBytes = m_nSampleCount;
            m_nSizeInBytes*=x.BytesPerSample;
            m_nSizeInBytes*=x.ChannelsCount;
            m_nFormatTag = 1;            // 1 for int, 3 for float
            if (x.SampleType==AudioSampleType.FLOAT) m_nFormatTag = 3;
        }

        public bool IsBusy {
            get {
                return m_encoderThread!=null && m_encoderThread.IsAlive;
            }
        }

        internal void SetKeepOutput(bool bKeepOutput) {
            m_bKeepOutput = bKeepOutput;
        }
    }
}
