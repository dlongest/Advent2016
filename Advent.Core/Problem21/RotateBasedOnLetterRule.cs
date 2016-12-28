using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem21
{
    public class RotateBasedOnLetterRule : ScrambleRuleBase
    {
        public RotateBasedOnLetterRule()
            :base(@"rotate based on position of letter [a-z]")
        {
        }

        public override string Scramble(string s, string instruction)
        {
            var letter = instruction.Last();

            var index = s.IndexOf(letter);

            var rotations = index + 1;

            if (index >= 4)
                ++rotations;

            return s.RotateRight(rotations);
        }

        public override string Descramble(string s, string originalInstruction)
        {
            var targetLetter = originalInstruction.Last();

            var index = s.IndexOf(targetLetter);

            return GetRotation(index, s.Length)(s);
        }

        private Func<string, string> GetRotation(int currentIndex, int length)
        {
            var targetIndex = GetTargetIndexForCharacterAt(currentIndex);

            if (currentIndex > targetIndex)
                return s => s.RotateLeft(currentIndex - targetIndex);

            return s => s.RotateRight(targetIndex - currentIndex);
        }

        private int GetTargetIndexForCharacterAt(int index)
        {
            switch (index)
            {
                case 0:
                    return 7;
                case 1:
                    return 0;
                case 2:
                    return 4;
                case 3:
                    return 1;
                case 4:
                    return 5;
                case 5:
                    return 2;
                case 6:
                    return 6;
                case 7:
                    return 3;
                default:
                    throw new ArgumentException("Yikes");
            }
        }
    }
}
