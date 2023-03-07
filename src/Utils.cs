using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BeHappy
{
    class Utils
    {
        public static string CleanUpString(string s)
        {
            string[] arr = s.Split('\n');
            List<string> lst = new List<string>();
            for (int i = 0; i < arr.Length; ++i)
            {
                string str = arr[i].Trim(' ', '\t', '\r');
                if (str.Length != 0)
                    lst.Add(str);
            }
            return string.Join("\n", lst.ToArray());
        }

        /// <summary>
		/// Change Font in all given controls and its child controls recursively
		/// </summary>
        public static void ChangeFontRecursive(Control[] controls, System.Drawing.Font font)
        {
            foreach (Control ct in controls)
            {
                ct.Font = font;

                if (ct.HasChildren)
                {
                    foreach (Control child in ct.Controls)
                    {
                        child.Font = font;
                    }
                }
            }
        }

        /// <summary>
		/// Replace all line endings with 'Environment.NewLine'
		/// </summary>
        public static string ChangeLineEndings(string text)
        {
            Regex rex = new Regex(@"\r\n|\n|\r", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return rex.Replace(text, Environment.NewLine);
        }

        public static bool HasMono { get; } = Type.GetType("Mono.Runtime") != null;

    }
}
