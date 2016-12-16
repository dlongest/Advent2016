using Advent.Core.Problem15;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem15
{
    public class DiscTests
    {
        [Theory]
        [InlineData(0, 3, 1)]
        [InlineData(1, 3, 2)]
        [InlineData(2, 3, 0)]
        public void Rotate_Moves_ToNextPosition(int startingPosition, int totalPositions, int expected)
        {
            var sut = new Disc(totalPositions, startingPosition);
            sut.Rotate();

            Assert.Equal(expected, sut.CurrentPosition);
        }

        [Theory]
        [InlineData(0, 4, 1, 1)]
        [InlineData(0, 4, 2, 2)]
        [InlineData(0, 4, 3, 3)]
        [InlineData(0, 4, 4, 0)]
        public void WhatPositionAfter_ReturnsPositionFollowingGivenRotations(int startingPosition, 
                                                                             int totalPositions,
                                                                             int numberRotations,
                                                                             int expected)
        {
            var sut = new Disc(totalPositions, startingPosition);

            var actual = sut.WhatPositionAfter(numberRotations);

            Assert.Equal(expected, actual);
        }
    }

    public class TimeOptimizerTests
    {
        [Fact]
        public void FindOptimalTime_ReturnsEarliestTime_ToDropCapsule()
        {
            var disc1 = new Disc(5, 4);
            var disc2 = new Disc(2, 1);

            var sut = new TimeOptimizer(new[] { disc1, disc2 });

            var actual = sut.FindOptimalTime();

            Assert.Equal(5, actual);
        }
    }
}
