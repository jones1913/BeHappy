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
            List<string> lst = new List<string>();
            for (int i = 0; i < arr.Length; ++i)
            {
                string str = arr[i].Trim(' ', '\t', '\r');
                if (str.Length != 0)
                    lst.Add(str);
            }
            return string.Join("\n", lst.ToArray());
        }
    }
}
