using Advent.Core.Problem2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem2
{
    public class BathroomCodeFinderTests
    {
        [Fact]
        public void OneLineInstruction_IsCorrect()
        {
            var instructions = new[] { "ULL" };

            var keypad = new NineButtonSquareKeypadBuilder().Build("5");

            var sut = new BathroomCodeFinder();

            var code = sut.Code(keypad, () => instructions);

            Assert.Equal("1", code);
        }

        [Fact]
        public void TwoLineInstruction_IsCorrect()
        {
            var instructions = new[] { "ULL", "RRDDD" };

            var keypad = new NineButtonSquareKeypadBuilder().Build("5");

            var sut = new BathroomCodeFinder();

            var code = sut.Code(keypad, () => instructions);

            Assert.Equal("19", code);
        }

        [Fact]
        public void ThreeLineInstruction_IsCorrect()
        {
            var instructions = new[] { "ULL", "RRDDD", "LURDL" };

            var keypad = new NineButtonSquareKeypadBuilder().Build("5");

            var sut = new BathroomCodeFinder();

            var code = sut.Code(keypad, () => instructions);

            Assert.Equal("198", code);
        }

        [Fact]
        public void FourLineInstruction_IsCorrect()
        {
            var instructions = new[] { "ULL", "RRDDD", "LURDL", "UUUUD" };

            var keypad = new NineButtonSquareKeypadBuilder().Build("5");

            var sut = new BathroomCodeFinder();

            var code = sut.Code(keypad, () => instructions);

            Assert.Equal("1985", code);
        }

        [Fact]
        public void OneLineInstruction_ForDiamondKeypad_IsCorrect()
        {
            var instructions = new[] { "UUUUD" };

            var keypad = new DiamondKeypadBuilder().Build("B");

            var sut = new BathroomCodeFinder();

            var code = sut.Code(keypad, () => instructions);

            Assert.Equal("3", code);
        }


        [Fact]
        public void FourLineInstruction_ForDiamondKeypad_IsCorrect()
        {
            var instructions = new[] { "ULL", "RRDDD", "LURDL", "UUUUD" };

            var keypad = new DiamondKeypadBuilder().Build("5");

            var sut = new BathroomCodeFinder();

            var code = sut.Code(keypad, () => instructions);

            Assert.Equal("5DB3", code);
        }
    }
}
