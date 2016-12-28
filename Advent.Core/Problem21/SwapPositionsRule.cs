using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem21
{
    public class SwapPositionsRule : ScrambleRuleBase
    {        
        private Regex numberPattern = new Regex(@"\d+");

        public SwapPositionsRule()
            :base(@"swap position \d+ with position \d+")
        {
        }

        public override string Scramble(string s, string instruction)
        {
            if (!CanScramble(instruction))
                throw new ArgumentException("Unable to scramble this instruction: " + instruction);

            var positions = ParsePositions(instruction);

            var chars = s.ToArray();

            var t = chars[positions.Item1];
            chars[positions.Item1] = chars[positions.Item2];
            chars[positions.Item2] = t;

            return new string(chars);
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
