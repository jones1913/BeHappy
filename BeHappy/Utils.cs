using System;
using System.Collections.Generic;
using System.Text;

namespace BeHappy
{
    class Utils
    {
        public static string CleanUpString(string s)
        {
            string[] arr = s.Split('\n');
            List<string> a2 = new List<string>();
            for (int i = 0; i < arr.Length; ++i)
            {
                string a = arr[i].Trim(' ', '\t', '\r');
                if (0 != a.Length)
                    a2.Add(a);
            }
            return string.Join("\n", a2.ToArray());

        }
    }
}
