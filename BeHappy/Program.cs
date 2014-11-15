using System;
using System.Windows.Forms;

namespace BeHappy
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			// To avoid ugly BadImageFormatException let's check current mode
			int pointerSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(IntPtr));
			if(pointerSize!=4)
			{
				// Can't run in non-32-bit mode!
				int processBits = pointerSize*8;
				string text = string.Format("BeHappy is designed to run as 32-bit process.\nUnfortunally this build (v{1}) is invalid and started as {0}-bit process. Please inform developers.\nBeHappy will be aborted.", processBits, Application.ProductVersion);
				string caption = string.Format("Can't run as {0}-bit process!", processBits);
				MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				return;
			}
			
			Application.Run(new MainForm());
		}
	}
}


