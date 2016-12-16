using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem16
{
    public class DiskFiller
    {
        public string Checksum(string input, int diskLength)
        {
            var data = IterateUntilFilled(input, diskLength).Substring(0, diskLength);

            var checksum = ComputeChecksum(data);

            return checksum;
        }


        private string IterateUntilFilled(string input, int diskLength)
        {
            var output = input;

            while (output.Length < diskLength)
            {
                output = DragonCurve.Iterate(output);
            }

            return output;
        }

        private string ComputeChecksum(string data)
        {
            var pairs = Pairs(data);

            var checksum = GetChecksumForPairs(pairs);

            return IsEven(checksum.Length) ? ComputeChecksum(checksum) : checksum;
        }

        private IEnumerable<string> Pairs(string s)
        {
            var pairs = s.Length / 2;

            return Enumerable.Range(0, pairs)
                             .Select(p => string.Format("{0}{1}", s[p * 2], s[p * 2 + 1]));
        }

        private string GetChecksumForPairs(IEnumerable<string> pairs)
        {
            return string.Join("", pairs.Select(a => GetChecksumDigitForPair(a)));
        }

        private string GetChecksumDigitForPair(string pair)
        {
            if (pair == "00" || pair == "11")
                return "1";

            if (pair == "01" || pair == "10")
                return "0";

            throw new ArgumentException(string.Format("Unrecognized pair: {0}", pair));
        }

        private bool IsEven(int n)
        {
            return n % 2 == 0;
        }
    }

    public static class DragonCurve
    {
        public static string Iterate(string input)
        {
            var a = input;
            var b = string.Join("", input.Reverse().Select(c => FlipCharacter(c)));

            return string.Format("{0}0{1}", a, b);
        }

        private static string FlipCharacter(char c)
        {
            if (c == '0')
                return "1";

            if (c == '1')
                return "0";

            throw new ArgumentException(string.Format("Unrecognized character '{0}'", c));
        }
    }
}
