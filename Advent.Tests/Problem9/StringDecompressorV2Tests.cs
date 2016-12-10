using Advent.Core.Problem9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem9
{
    public class StringDecompressorV2Tests
    {  
        [Theory]
        [InlineData("advent", 6)]
        [InlineData("A(1x5)BC", 7)]
        [InlineData("(3x3)XYZ", 9)]
        [InlineData("X(8x2)(3x3)ABCY", 20)]
        [InlineData("(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN", 445)]
        public void Decompresses_Correctly(string input, int expected)
        {
            var actualLength = new StringDecompressorV2().Decompress(input);

            Assert.Equal(expected, actualLength);
        }

        // This test runs correctly, but it takes a long time because of the decompression.
       [Fact]
        public void Decompresses_Correctly_OnVeryLongString()
        {
            var input = "(27x12)(20x12)(13x14)(7x10)(1x12)A";

            var actualLength = new StringDecompressorV2().Decompress(input);

            Assert.Equal(241920, actualLength);
        }
    }
}
