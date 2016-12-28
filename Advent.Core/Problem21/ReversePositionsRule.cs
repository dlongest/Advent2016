using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem21
{
    public class ReversePositionsRule : ScrambleRuleBase
    {        
        private Regex numberPattern = new Regex(@"\d+");

        public ReversePositionsRule()
            :base(@"reverse positions \d+ through \d+")
        {
        }

        public  override string Scramble(string s, string instruction)
        {
            var positions = ParsePositions(instruction);

            var scrambled = new StringBuilder();

            if (positions.Item1 > 0)
            {
                var firstPart = s.Substring(0, positions.Item1);
                scrambled.Append(firstPart);
            }
            
            var reversibleSection = s.Substring(positions.Item1, positions.Item2 - positions.Item1 + 1);

            scrambled.Append(new string(reversibleSection.Reverse().ToArray()));

            if (positions.Item2 < s.Length - 1)
            {
                var lastPart = s.Substring(positions.Item2 + 1, s.Length - positions.Item2 - 1);
                scrambled.Append(lastPart);
            }

            return scrambled.ToString();
        }

        public override string Descramble(string s, string originalInstruction)
        {
            return this.Scramble(s, originalInstruction);
        }

        private Tuple<int, int> ParsePositions(string instruction)
        {
            var matches = this.numberPattern.Matches(instruction).Cast<Match>();

            var x = Int32.Parse(matches.First().Value);
            var y = Int32.Parse(matches.Last().Value);

            return Tuple.Create(x, y);
        }
    }
}
