using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem14
{
    public static class StringExtensions
    {
        public static string ToHexString(this byte[] bytes)
        {
            var hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                hex.AppendFormat("{0:x2}", b);

            return hex.ToString();
        }

        public static bool IsDigit(this string s)
        {
            try
            {
                Int32.Parse(s);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string CharAt(this string s, int index)
        {
            return s.Substring(index, 1);
        }
    }
}
