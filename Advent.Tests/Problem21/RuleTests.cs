using Advent.Core.Problem21;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem21
{
    public class RuleTests
    {
        [Fact]
        public void SwapPositions_ReturnsTrue_ForInstructionItCanHandle()
        {
            var x = new Random().Next();
            var y = new Random().Next();

            var sut = new SwapPositionsRule();

            Assert.True(sut.CanScramble(string.Format("swap position {0} with position {1}", x, y)));
            Assert.False(sut.CanScramble("swap position X with position Y"));
        }

        [Fact]
        public void SwapPositions_ReturnsStringWithGivenPositionsSwapped()
        {
            var expected = "ebcda";
            var sut = new SwapPositionsRule();

            var actual = sut.Scramble("abcde", "swap position 4 with position 0");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SwapLetters_ReturnsTrue_ForInstructionItCanHandle()
        {
            var sut = new SwapLettersRule();

            Assert.True(sut.CanScramble("swap letter d with letter a"));
            Assert.False(sut.CanScramble("swap letter xx with letter yy"));
            Assert.False(sut.CanScramble("swap letter 4 with letter 2"));
        }


        [Fact]
        public void SwapLetters_ReturnsStringWithGivenLettersSwapped()
        {            
            var expected = "edcba";
            var sut = new SwapLettersRule();

            var actual = sut.Scramble("ebcda", "swap letter d with letter b");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("reverse positions 0 through 4", "edcba", "abcde")]
        [InlineData("reverse positions 0 through 2", "abcde", "cbade" )]
        [InlineData("reverse positions 1 through 3", "abcde", "adcbe")]
        [InlineData("reverse positions 2 through 4", "abcde", "abedc")]
        public void ReversePositions_ReturnsString_WithGivenSections_Reversed(string instruction, string input, string expected)
        {
            var actual = new ReversePositionsRule().Scramble(input, instruction);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("rotate left 1 step", "abcde", "bcdea")]
        [InlineData("rotate left 2 steps", "abcde", "cdeab")]
        [InlineData("rotate left 3 steps", "abcde", "deabc")]
        [InlineData("rotate left 4 steps", "abcde", "eabcd")]
        [InlineData("rotate left 5 steps", "abcde", "abcde")]
        [InlineData("rotate left 6 steps", "abcde", "bcdea")]
        public void RotateLeft_ReturnsString_RotatedGivenPositions(string instruction, string input, string expected)
        {            
            var actual = new RotateLeftRule().Scramble(input, instruction);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("rotate right 1 step", "abcde", "eabcd")]
        [InlineData("rotate right 2 steps", "abcde", "deabc")]
        [InlineData("rotate right 3 steps", "abcde", "cdeab")]
        [InlineData("rotate right 4 steps", "abcde", "bcdea")]
        [InlineData("rotate right 5 steps", "abcde", "abcde")]
        [InlineData("rotate right 6 steps", "abcde", "eabcd")]
        public void RotateRight_ReturnsString_RotatedGivenPositions(string instruction, string input, string expected)
        {
            var actual = new RotateRightRule().Scramble(input, instruction);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("rotate based on position of letter a", "abcde", "eabcd")]        
        [InlineData("rotate based on position of letter b", "abcde", "deabc")]
        [InlineData("rotate based on position of letter c", "abcde", "cdeab")]
        [InlineData("rotate based on position of letter d", "abcde", "bcdea")]
        [InlineData("rotate based on position of letter e", "abcde", "eabcd")]
        public void RotateBasedOnLetter_CorrectlyRotatesBasedOnInstruction(string instruction, string input, string expected)
        {
            var actual = new RotateBasedOnLetterRule().Scramble(input, instruction);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("move position 1 to position 4", "bcdea", "bdeac")]
        [InlineData("move position 3 to position 0", "bdeac", "abdec")]
        public void MovePosition_CorrectlyRemovesAndReinsertsCharacter(string instruction, string input, string expected)
        {
            var actual = new MovePositionRule().Scramble(input, instruction);

            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData("rotate based on position of letter a", "habcdefg", "abcdefgh")]
        [InlineData("rotate based on position of letter b", "ghabcdef", "abcdefgh")]
        [InlineData("rotate based on position of letter c", "fghabcde", "abcdefgh")]
        [InlineData("rotate based on position of letter d", "efghabcd", "abcdefgh")]
        [InlineData("rotate based on position of letter e", "cdefghab", "abcdefgh")]
        [InlineData("rotate based on position of letter f", "bcdefgha", "abcdefgh")]
        [InlineData("rotate based on position of letter g", "abcdefgh", "abcdefgh")]
        [InlineData("rotate based on position of letter h", "habcdefg", "abcdefgh")]
        public void Descramble_CorrectlyReverses_RotateBasedOnLetter(string instruction, string input, string expected)
        {
            var actual = new RotateBasedOnLetterRule().Descramble(input, instruction);

            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData("reverse positions 0 through 4", "abcde", "edcba")]
        [InlineData("reverse positions 0 through 2", "cbade", "abcde")]
        [InlineData("reverse positions 1 through 3", "adcbe", "abcde")]
        [InlineData("reverse positions 2 through 4", "abedc", "abcde")]
        public void Descramble_Reverses_Reversal(string instruction, string input, string expected)
        {
            var actual = new ReversePositionsRule().Descramble(input, instruction);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Descramble_ReswapsLetters()
        {
            var expected = "edcba";
            var sut = new SwapLettersRule();

            var actual = sut.Descramble("ebcda", "swap letter d with letter b");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("rotate left 1 step",  "bcdea", "abcde")]
        [InlineData("rotate left 2 steps", "cdeab", "abcde")]
        [InlineData("rotate left 3 steps", "deabc", "abcde")]
        [InlineData("rotate left 4 steps", "eabcd", "abcde")]
        [InlineData("rotate left 5 steps", "abcde", "abcde")]
        [InlineData("rotate left 6 steps", "bcdea", "abcde")]
        public void Descramble_Reverses_RotateLeft(string instruction, string input, string expected)
        {
            var actual = new RotateLeftRule().Descramble(input, instruction);

            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData("move position 1 to position 4", "bdeac", "bcdea")]
        [InlineData("move position 3 to position 0", "abdec", "bdeac")]
        public void Descramble_ReversesMovePosition(string instruction, string input, string expected)
        {
            var actual = new MovePositionRule().Descramble(input, instruction);

            Assert.Equal(expected, actual);
        }

    }
}
