using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem21
{
    public class RotateLeftRule : ScrambleRuleBase
    {
        private readonly Regex numberPattern = new Regex(@"\d+");

        public RotateLeftRule()
            :base(@"rotate left \d+ steps?")
        {
        }

        public override string Scramble(string s, string instruction)
        {
            if (!CanScramble(instruction))
                throw new ArgumentException("Unable to scramble this instruction: " + instruction);

            var steps = ParseSteps(instruction);

            return s.RotateLeft(steps);
        }

        public override string Descramble(string s, string originalInstruction)
        {
            var number = Int32.Parse(Regex.Match(originalInstruction, @"\d+").Value);

            return s.RotateRight(number);
        }

        private int ParseSteps(string instruction)
        {
            return Int32.Parse(numberPattern.Match(instruction).Value);
        }
    }

    public class RotateRightRule : ScrambleRuleBase
    {
        private readonly Regex numberPattern = new Regex(@"\d+");
        
        public RotateRightRule()
            : base(@"rotate right \d+ steps?")
        {
        }

        public override string Scramble(string s, string instruction)
        {
            if (!CanScramble(instruction))
                throw new ArgumentException("Unable to scramble this instruction: " + instruction);

            var steps = ParseSteps(instruction);

            return s.RotateRight(steps);
        }

        public override string Descramble(string s, string originalInstruction)
        {
            var number = Int32.Parse(Regex.Match(originalInstruction, @"\d+").Value);

            return s.RotateLeft(number);
        }

        private int ParseSteps(string instruction)
        {
            return Int32.Parse(numberPattern.Match(instruction).Value);
        }
    }
}
