using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem21
{
    public interface IScrambleRule
    {
        bool CanScramble(string instruction);

        string Scramble(string s, string instruction);

        string Descramble(string s, string originalInstruction);
    }
}
