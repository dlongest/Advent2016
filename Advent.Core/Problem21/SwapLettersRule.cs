using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem21
{
    public class SwapLettersRule : ScrambleRuleBase
    {       

        public SwapLettersRule()
            :base(@"swap letter [a-z] with letter [a-z]")
        {
        }

        public override string Scramble(string s, string instruction)
        {
            if (!CanScramble(instruction))
                throw new ArgumentException("Unable to scramble this instruction: " + instruction);

            var swaps = ParseLetters(instruction);

            var sb = new StringBuilder();

            foreach (var letter in s)
            {
                sb.Append(Replace(letter, swaps));
            }

            return sb.ToString();
        }

        public override string Descramble(string s, string originalInstruction)
        {
            return this.Scramble(s, originalInstruction);
        }

        private char Replace(char current, Tuple<char, char> swaps)
        {
            if (current != swaps.Item1 && current != swaps.Item2)
                return current;

            return (current == swaps.Item1) ? swaps.Item2 : swaps.Item1;
        }

        private Tuple<char, char> ParseLetters(string instruction)
        {
            var tokens = instruction.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            var firstLetter = tokens[2].First();
            var secondLetter = tokens[5].First();

            return Tuple.Create(firstLetter, secondLetter);
        }
    }
}
