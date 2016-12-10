using Advent.Core.Problem9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem9
{
    public class StringDecompressorTests
    {
        [Theory]
        [InlineData("advent", "advent")]
        [InlineData("A(1x5)BC", "ABBBBBC")]
        [InlineData("(3x3)XYZ", "XYZXYZXYZ")]
        [InlineData("A(2x2)BCD(2x2)EFG", "ABCBCDEFEFG")]
        [InlineData("(6x1)(1x3)A", "(1x3)A")]
        [InlineData("X(8x2)(3x3)ABCY", "X(3x3)ABC(3x3)ABCY")]
        public void Decompresses_Correctly(string input, string expected)
        {
            var actual = new StringDecompressor().Decompress(input);

            Assert.Equal(expected, actual);
        }     
    }

}
