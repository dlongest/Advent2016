using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem21
{
    public abstract class ScrambleRuleBase : IScrambleRule
    {
        protected Regex rulePattern;

        public ScrambleRuleBase(string rulePattern)
        {
            this.rulePattern = new Regex(rulePattern);
        }

        public bool CanScramble(string instruction)
        {
            return rulePattern.IsMatch(instruction);
        }

        public abstract string Descramble(string s, string originalInstruction);

        public abstract string Scramble(string s, string instruction);
    }
}
