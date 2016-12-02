using Advent.Core;
using Advent.Core.Problem1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Collections;

namespace Advent.Tests
{
    public class GpsTests
    {
        [Fact]
        public void HowFar_IsZero_AfterNoMoves()
        {
            var gps = new Gps();

            Assert.Equal(0, gps.HowFar());
        }

        [Theory]
        [InlineData("R", 1)]
        [InlineData("R", 5)]
        [InlineData("R", 10)]
        [InlineData("L", 2)]
        [InlineData("L", 4)]
        [InlineData("R", 1)]
        public void HowFar_IsEqualToBlocks_AfterOneMove(string direction, int blocks)
        {
            var turn = new Turn(direction, blocks);

            var gps = new Gps();

            gps.Move(turn);

            Assert.Equal(blocks, gps.HowFar());
        }

        [Theory]
        [InlineData("R2", "R2")]
        public void HowFar_IsCorrect_After2Turns(string first, string second)
        {
            var gps = new Gps();

            gps.Move(new Turn(first));
            gps.Move(new Turn(second));

            Assert.Equal(4, gps.HowFar());
        }

        [Fact]
        public void HowFar_IsCorrect_After3Turns_WithALoopback()
        {
            var gps = new Gps();

            gps.Move(new Turn("R2"));
            gps.Move(new Turn("R2"));
            gps.Move(new Turn("R2"));

            Assert.Equal(2, gps.HowFar());
        }

        [Fact]
        public void HowFar_IsCorrect_AfterCombinationTurns()
        {
            var gps = new Gps();

            gps.Move(new Turn("R5"));
            gps.Move(new Turn("L5"));
            gps.Move(new Turn("R5"));
            gps.Move(new Turn("R3"));

            Assert.Equal(12, gps.HowFar());
        }

        [Theory]
        [ClassData(typeof(CoordinatePathTestCases))]
        public void CoordinatesMovedThrough_ContainsCorrectListOfCoordinatesInOrder(Coordinate[] expected,
                                                                                    Turn[] turns)
        {
            var gps = new Gps();

            foreach (var turn in turns)
            {
                gps.Move(turn);
            }

            Assert.Equal(expected, gps.CoordinatesMovedThrough());
        }

        [Fact]
        public void HowFarIsFirstDuplicate_IsCorrect()
        {
            var gps = new Gps();

            gps.Move(new Turn("R8"));
            gps.Move(new Turn("R4"));
            gps.Move(new Turn("R4"));
            gps.Move(new Turn("R8"));

            Assert.Equal(4, gps.HowFarIsFirstDuplicate());
        }

        private class CoordinatePathTestCases : IEnumerable<object[]>
        {
            private IList<object[]> data = new List<object[]>();

            public CoordinatePathTestCases()
            {
                data.Add(new object[]
                {
                    new[]
                    {
                    new Coordinate(0,0), new Coordinate(1,0), new Coordinate(2,0),new Coordinate(2,1),
                    new Coordinate(1, 1), new Coordinate(1,0)
                    },
                    new[]
                    {
                         new Turn("R2"), new Turn("L1"), new Turn("L1"), new Turn("L1")
                    }
                });
                data.Add(new object[]
                {
                    new[]
                    {
                        new Coordinate(0,0), new Coordinate(1,0), new Coordinate(2,0),new Coordinate(2,1),
                        new Coordinate(3,1), new Coordinate(4,1)

                    },
                    new[]
                    {
                         new Turn("R2"), new Turn("L1"), new Turn("R2")
                    }
                });
            }

            public IEnumerator<object[]> GetEnumerator()
            {
                return this.data.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}

    
