using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem9
{
    public class StringDecompressor
    {
        public string Decompress(string s)
        {
            var decompressed = new StringBuilder(s.Length * 2);
            var remaining = s;

            while (remaining.Length > 0)
            {
                if (Marker.AtBeginningOf(remaining))
                {
                    var marker = new Marker(remaining);

                    remaining = marker.Remaining;
                    decompressed.Append(marker.ToString());
                }
                else
                {
                    var normal = new NormalString(remaining);

                    remaining = normal.Remaining;
                    decompressed.Append(normal.ToString());
                }
            }

            return decompressed.ToString();
        }
    }
    
    public interface IToken
    {
        string Remaining { get; }
        string ToString();
    }

    public class NormalString : IToken
    {
        private string decompressed;

        public NormalString(string s)
        {
            var nextMarkerIndex = Marker.IndexOfNext(s);

            if (nextMarkerIndex == -1)
            {
                this.decompressed = s;
                this.Remaining = string.Empty;
            }
            else
            {
                this.decompressed = s.Substring(0, nextMarkerIndex);
                this.Remaining = s.Substring(nextMarkerIndex, s.Length - nextMarkerIndex);
            }
        }

        public string Remaining { get; private set; }

        public override string ToString()
        {
            return this.decompressed.ToString();
        }
    }

    public class Marker : IToken
    {
        private string decompressed;
        private static Regex markerAtBeginningPattern = new Regex(@"^\(\d+x\d+\)");
        private static Regex nextMarkerPattern = new Regex(@"\(\d+x\d+\)");
        private static Regex charactersToTakePattern = new Regex(@"^\d+");
        private static Regex repeatPattern = new Regex(@"\d+$");
        private int charactersToTake;
        private int repeat;

        public static int IndexOfNext(string s)
        {
            var match = nextMarkerPattern.Match(s);

            return match == Match.Empty ? -1 : match.Index;
        }

        public static bool AtBeginningOf(string s)
        {
            return markerAtBeginningPattern.IsMatch(s);
        }

        public static bool ContainsMarker(string s)
        {
            return IndexOfNext(s) != -1;
        }

        private string ChompMarker(string s)
        {
            var marker = markerAtBeginningPattern.Match(s).Value;

            var values = marker.Replace("(", "").Replace(")", "").Split(new string[] { "x" }, StringSplitOptions.RemoveEmptyEntries);

            this.charactersToTake = Int32.Parse(values[0]);
            this.repeat = Int32.Parse(values[1]);

            return s.Substring(marker.Length, s.Length - marker.Length);
        }

        public Marker(string s)
        {
            if (!AtBeginningOf(s))
                throw new ArgumentException("String does not start with a Marker");

            var remainingAfterMarkerRemoval = ChompMarker(s);

            var toRepeat = remainingAfterMarkerRemoval.Substring(0, this.charactersToTake);

            this.Remaining = new string(remainingAfterMarkerRemoval.Skip(this.charactersToTake).ToArray());
            this.decompressed = Repeat(toRepeat, repeat);
        }

        private static string Repeat(string toRepeat, int times)
        {
            return string.Join("", Enumerable.Repeat(0, times).Select(a => toRepeat));
        }

        public string Remaining { get; private set; }

        public override string ToString()
        {
            return decompressed.ToString();
        }
    }
}
