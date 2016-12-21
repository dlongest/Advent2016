using Advent.Core.Problem20;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem20
{
    public class IpRangeTests
    {
        [Fact]
        public void ReturnsNumbersStartingAt0_WhenIpRange_IsEmpty()
        {
            var expected = new Range(0, 4294967295);

            var sut = new IpRanges();

            var actual = sut.Lowest();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReturnsValues_Exclusive_OfRange()
        {
            var expected = new[] { new Range(0, 1), new Range(5, 4294967295) };

            var sut = new IpRanges();
            sut.Block(new Range(2, 4));

            var actual = sut.AvailableRanges();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReturnsValues_Exclusive_OfMultipleRanges()
        {
            var expected = new[] { new Range(0, 1), new Range(5, 5), new Range(8, 4294967295) };            

            var sut = new IpRanges();
            sut.Block(new Range(2, 4));
            sut.Block(new Range(6, 7));

            var actual = sut.AvailableRanges();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReturnsValues_AfterMultipleOverlappedHaveToMerge()
        {
            var expected = new[] { new Range(0, 0), new Range(9, 4294967295) };

            var sut = new IpRanges();
            sut.Block(new Range(2, 4));
            sut.Block(new Range(5, 8));
            sut.Block(new Range(1, 7));

            var actual = sut.AvailableRanges();

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void HowManyAvailable_ReturnsMaximumPlusOne_ForNoBlocks()
        {
            Assert.Equal(4294967296, new IpRanges().HowManyAvailable);
        }

        [Fact]
        public void HowManyAvailable_ReturnsCountInRange_LessBlocks()
        {
            var sut = new IpRanges();
            sut.Block(new Range(2, 5));

            Assert.Equal(4294967292, sut.HowManyAvailable);
        }


        [Fact]
        public void HowManyAvailable_ReturnsCountInRange_LessAllBlocks()
        {            
            var expectedAvailable = new[] { new Range(3, 3), new Range(9, 9) };
           
            var sut = new IpRanges(9);
            sut.Block(new Range(5, 8));
            sut.Block(new Range(0, 2));
            sut.Block(new Range(4, 7));

            Assert.Equal(2, sut.HowManyAvailable);
            Assert.Equal(8, sut.HowManyBlocked);
            Assert.Equal(expectedAvailable, sut.AvailableRanges());
        }

        
    }
}
