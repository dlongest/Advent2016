using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem21
{
    public class MovePositionRule : ScrambleRuleBase
    {
        private readonly Regex numberPattern = new Regex(@"\d+");

        public MovePositionRule()
            :base(@"move position \d+ to position \d+")
        {
        }

        public override string Scramble(string s, string instruction)
        {
            if (!CanScramble(instruction))
                throw new ArgumentException("Unable to scramble this instruction: " + instruction);

            var positions = ParsePositions(instruction);

            var letter = s[positions.From].ToString();

            var removed = s.Remove(positions.From, 1);

            var inserted = removed.Insert(positions.To, letter);

            return inserted;
        }

        public override string Descramble(string s, string originalInstruction)
        {
            var positions = ParsePositions(originalInstruction);

            var instruction = string.Format("move position {0} to position {1}", positions.To, positions.From);

            return this.Scramble(s, instruction);
        }

        private Positions ParsePositions(string instruction)
        {
            var matches = this.numberPattern.Matches(instruction).Cast<Match>();

            var from = Int32.Parse(matches.First().Value);
            var to = Int32.Parse(matches.Last().Value);

            return new Positions
            {
                From = from,
                To = to
            };
        }

        private class Positions
        {
            public int From { get; set; }

            public int To { get; set; }

        }
    }
}
