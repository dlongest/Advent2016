using Advent.Core.Problem20;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem20
{
    public class RangeTests
    {
        [Theory]
        [InlineData("5", "8", "6", "7", true)]
        [InlineData("5", "8", "4", "5", true)]
        [InlineData("5", "8", "4", "6", true)]
        [InlineData("5", "8", "4", "7", true)]
        [InlineData("5", "8", "7", "9", true)]
        [InlineData("5", "8", "2", "4", true)]
        [InlineData("4", "6", "7", "8", true)]
        [InlineData("5", "8", "1", "3", false)]
        public void Overlaps_ReturnsCorrect_WhenAnyBoundsOverlap(int firstLower,
                                                                 int firstUpper,
                                                                 int secondLower,
                                                                 int secondUpper,
                                                                 bool expected)
        {
            var first = new Range(firstLower, firstUpper);
            var second = new Range(secondLower, secondUpper);

            Assert.Equal(expected, first.Overlaps(second));
            Assert.Equal(expected, second.Overlaps(first));
        }

        [Theory]
        [InlineData("5", "8", "6", "7", "5", "8")]
        [InlineData("5", "8", "4", "5", "4", "8")]
        [InlineData("5", "8", "4", "6", "4", "8")]
        [InlineData("5", "8", "4", "7", "4", "8")]
        [InlineData("5", "8", "7", "9", "5", "9")]
        [InlineData("5", "8", "2", "4", "2", "8")]
        [InlineData("4", "6", "7", "8", "4", "8")]
        public void Merge_ReturnsCorrectlyMergedRange(int firstLower, int firstUpper,
                                                      int secondLower, int secondUpper,
                                                      int expectedLower, int expectedUpper)
        {
            var first = new Range(firstLower, firstUpper);
            var second = new Range(secondLower, secondUpper);

            var sut = first.Merge(second);

            Assert.Equal(expectedLower, sut.Lower);
            Assert.Equal(expectedUpper, sut.Upper);
        }

        [Fact]
        public void Merge_CombinesDistinctRanges_WithOneThatOverlapsBoth()
        {
            var expected = new Range(394757734, 422306605);

            var first = new Range(410425445, 412372582);
            var second = new Range(412372583, 422306605);

            var beingAdded = new Range(394757734, 421655845);

            var actual = beingAdded.Merge(new[] { first, second });

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HowMany_ReturnsCorrectCount_ForSingleValueRange()
        {
            var expected = 1;

            Assert.Equal(expected, new Range(2, 2).HowManyInRange);
        }

        [Fact]
        public void HowMany_ReturnsCorrectCount_ForMultipleValueRange()
        {
            var expected = 13;

            Assert.Equal(expected, new Range(8, 20).HowManyInRange);
        }
    }
}
