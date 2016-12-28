using Advent.Core.Problem21;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem21
{
    public class ScramblerTests
    {
        private static string[] instructions = new []
            {
                "swap position 4 with position 0", 
                "swap letter d with letter b",
                "reverse positions 0 through 4",
                "rotate left 1 step",
                "move position 1 to position 4",
                "move position 3 to position 0",
                "rotate based on position of letter b",
                "rotate based on position of letter d"
            };

        [Fact]
        public void Scramble_ReturnsCorrectString_BasedOnContainedRules()
        {
            var input = "abcde";
            var expected = "decab";

            var sut = new Scrambler();

            var actual = sut.Scramble(input, instructions);

            Assert.Equal(expected, actual);
        }
    }
}
