using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem21
{
    public class Scrambler
    {
        private readonly IScrambleRule[] rules;

        public Scrambler()
            :this(new SwapPositionsRule(), 
                 new SwapLettersRule(), 
                 new ReversePositionsRule(), 
                 new RotateLeftRule(), 
                 new RotateRightRule(),
                 new RotateBasedOnLetterRule(),
                 new MovePositionRule())
        {
        }

        public Scrambler(params IScrambleRule[] rules)
        {
            this.rules = rules;
        }

        public string Scramble(string s, IEnumerable<string> instructions)
        {
            return Execute(s, instructions, (rule, str, ins) => rule.Scramble(str, ins));
        }

        public string Descramble(string s, IEnumerable<string> originalInstructions)
        {
            return Execute(s, originalInstructions, (rule, str, ins) => rule.Descramble(str, ins));
        }

        private string Execute(string s, 
                               IEnumerable<string> instructions, 
                               Func<IScrambleRule, string, string, string> executeRule)
        {
            var scrambled = s;

            foreach (var instruction in instructions)
            {
                var rule = this.rules.Pick(instruction);

                scrambled = executeRule(rule, scrambled, instruction);
            }

            return scrambled;
        }
    }   

    

    internal static class RuleExtensions
    {
        public static IScrambleRule Pick(this IEnumerable<IScrambleRule> rules, string instruction)
        {
            return rules.First(a => a.CanScramble(instruction));
        }

        public static string RotateLeft(this string s, int steps)
        {
            var actualSteps = steps % s.Length;

            var toAppend = s.Substring(0, actualSteps);
            var remainder = s.Substring(actualSteps, s.Length - actualSteps);

            return string.Format("{0}{1}", remainder, toAppend);
        }

        public static string RotateRight(this string s, int steps)
        {
            var actualSteps = steps % s.Length;

            var toPrepend = s.Substring(s.Length - actualSteps, actualSteps);
            var remainder = s.Substring(0, s.Length - actualSteps);

            return string.Format("{0}{1}", toPrepend, remainder);
        }
    }
}
