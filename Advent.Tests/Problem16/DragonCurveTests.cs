using Advent.Core.Problem16;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem16
{
    public class DragonCurveTests
    {
        [Theory]
        [InlineData("1", "100")]
        [InlineData("0", "001")]
        [InlineData("11111", "11111000000")]
        [InlineData("111100001010", "1111000010100101011110000")]
        public void Iterate_ProducesCorrectNextString(string input, string expected)
        {
            var actual = DragonCurve.Iterate(input);

            Assert.Equal(expected, actual);
        }
    }

    public class DiskFillerTests
    {
        [Fact]
        public void Checksum_IsCorrect_ForGivenInput()
        {
            var expected = "01100";

            var sut = new DiskFiller();

            var actual = sut.Checksum("10000", 20);

            Assert.Equal(expected, actual);
        }
    }
}
