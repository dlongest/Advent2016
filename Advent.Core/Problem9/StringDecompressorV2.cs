using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem9
{
    public class StringDecompressorV2
    {
        public long Decompress(string s)
        {
            var remaining = s;
            var tokens = new List<ITokenV2>();

            while (remaining.Length > 0)
            {
                var token = CreateNextToken(remaining);
                tokens.Add(token);
                remaining = token.Remaining;
            }

            return tokens.Sum(a => a.Count);
        }

        private ITokenV2 CreateNextToken(string s)
        {
            if (MarkerTokenV2.AtBeginningOf(s))
                return new MarkerTokenV2(s);

            return new NormalTokenV2(s);
        }

    }
    public interface ITokenV2
    {
        long Count { get; }

        string Remaining { get; }
    }

    public class NormalTokenV2 : ITokenV2
    {
        public NormalTokenV2(string s)
        {
            var index = MarkerTokenV2.FirstMarkerIndex(s);

            if (index == -1)
            {
                this.Count = s.Length;
                this.Remaining = string.Empty;
            }
            else
            {
                this.Count = index;
                this.Remaining = s.Substring(index, s.Length - index);
            }
        }

        public string Remaining { get; private set; }

        public long Count { get; private set; }
    }

    public class MarkerTokenV2 : ITokenV2
    {
        private static Regex markerPattern = new Regex(@"\(\d+x\d+\)");

        public static int FirstMarkerIndex(string s)
        {
            var match = markerPattern.Match(s);

            return (match == Match.Empty) ? -1 : match.Index;
        }

        public static bool ContainsMarker(string s)
        {
            return markerPattern.IsMatch(s);
        }

        public static MarkerTokenV2[] Create(string s)
        {
            var matches = markerPattern.Matches(s);

            if (matches.Count == 0)
                return new MarkerTokenV2[0];

            return matches.Cast<Match>()
                          .Select(m => new MarkerTokenV2(m.Value))
                          .ToArray();
        }

        public static bool AtBeginningOf(string s)
        {
            var match = markerPattern.Match(s);

            if (match == Match.Empty)
                return false;

            return match.Index == 0;
        }

        public MarkerTokenV2(string s)
        {
            if (!MarkerTokenV2.AtBeginningOf(s))
                throw new ArgumentException("Marker must be at the beginning of the string");

            var marker = markerPattern.Match(s);         
           
            var parts = marker.Value.Replace("(", "").Replace(")", "").Split(new string[] { "x" }, StringSplitOptions.RemoveEmptyEntries);
            this.Take = Int32.Parse(parts[0]);
            this.Repeat = Int32.Parse(parts[1]);

            this.InnerString = s.Substring(marker.Length, this.Take);

            this.Remaining = s.Substring(marker.Index + marker.Length + this.Take,
                                         s.Length - (marker.Index + marker.Length + this.Take));
        }

        public string InnerString { get; private set; }

        private ITokenV2[] CreateInner(string s)
        {
            var innerRemaining = this.InnerString;
            var innerTokens = new List<ITokenV2>();     
            
            while (innerRemaining.Length > 0)
            {
                if (!MarkerTokenV2.AtBeginningOf(innerRemaining))
                {
                    var normal = new NormalTokenV2(innerRemaining);
                    innerTokens.Add(normal);                   

                    innerRemaining = normal.Remaining;
                }
                else
                {
                    var marker = new MarkerTokenV2(innerRemaining);
                    innerTokens.Add(marker);

                    innerRemaining = marker.Remaining;            
                }
            }

            return innerTokens.ToArray();
        }

        public ITokenV2[] Inner { get; private set; }

        public string Remaining { get; private set; }

        public int Take { get; set; }

        public int Repeat { get; set; }

        public long Count
        {
            get
            {
                var innerTokens = CreateInner(this.InnerString);

                return this.Repeat * innerTokens.Sum(c => c.Count);
            }
        }
    }
}
