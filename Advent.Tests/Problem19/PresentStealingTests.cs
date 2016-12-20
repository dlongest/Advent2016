using Advent.Core.Problem19;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem19
{
    public class PresentStealingTests
    {
        [Fact]
        public void WhichElf_ReturnsCorrectPositionOfFinalElf()
        {
            var expected = 3;

            var actual = PresentStealing.WhichElfGetsThePresents(5);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WhichSmartElf_ReturnsCorrectPositionOfFinalElf()
        {
            var expected = 3;

            var actual = PresentStealing.WhichSmartElfGetsThePresents(5);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WhichSmartElf_ReturnsCorrectPositionOfFinalElf_ForActualProblem()
        {
            var expected = 1830117;

            var actual = PresentStealing.WhichSmartElfGetsThePresents(3012210);

            Assert.Equal(expected, actual);       
        }

        [Fact]
        public void WhichCircularElf_ReturnsCorrectPositionOfFinalElf()
        {

            var expected = 2;

            var actual = PresentStealing.WhichCircularElfGetsThePresents(5);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("3" ,"3")]
        [InlineData("4", "1")]
        [InlineData("5", "2")]
        [InlineData("6", "3")]      
        [InlineData("9", "9")]
        [InlineData("10", "1")]
        [InlineData("11", "2")]
        [InlineData("12", "3")]
        [InlineData("13", "4")]
        [InlineData("14", "5")]
        [InlineData("15", "6")]
        [InlineData("16", "7")]
        [InlineData("17", "8")]
        [InlineData("18", "9")]
      
        public void WhichSmartCircularElf_ReturnsCorrectPositionOfFinalElf_InFirstHalf(int numberElves, int expected)
        {
            var actual = PresentStealing.WhichSmartCircularElfGetsThePresents(numberElves);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("7", "5")]
        [InlineData("8", "7")]
        [InlineData("19", "11")]
        [InlineData("20", "13")]
        [InlineData("57", "33")]
        [InlineData("58", "35")]
        public void WhichSmartCircularElf_ReturnsCorrectPositionOfFinalElf_InSecondHalf(int numberElves, int expected)
        {
            var actual = PresentStealing.WhichSmartCircularElfGetsThePresents(numberElves);

            Assert.Equal(expected, actual);
        }
    }
}
