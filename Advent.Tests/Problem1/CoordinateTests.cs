using Advent.Core.Problem1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem1
{
    public class CoordinateTests
    {
        [Fact]
        public void Equals_ReturnsTrue_WhenPropertiesAreEqual()
        {
            var coord1 = new Coordinate(2, 5);
            var coord2 = new Coordinate(2, 5);

            Assert.True(coord1.Equals(coord2));
        }

        [Fact]
        public void GetHashCodes_AreEqual_WhenPropertiesAreEqual()
        {
            var coord1 = new Coordinate(2, 5);
            var coord2 = new Coordinate(2, 5);

            Assert.True(coord1.GetHashCode() == coord2.GetHashCode());
        }

        [Fact]
        public void MoveNorth_ReturnsSingleCorrectCoordinate_ForDistance1()
        {
            var coord = Coordinate.Origin;
            var target = new Coordinate(0, 1);

            var walkedThrough = coord.MoveNorth(1);

            Assert.Equal(1, walkedThrough.Length);
            Assert.Equal(target, walkedThrough[0]);
        }

        [Fact]
        public void MoveNorth_ReturnsAllCoordinatesInPath_FromStartingPoint_InOrder()
        {
            var coord = Coordinate.Origin;
            var targets = new[] { new Coordinate(0, 1), new Coordinate(0, 2), new Coordinate(0, 3), new Coordinate(0, 4) };

            var walkedThrough = coord.MoveNorth(4);

            Assert.Equal(4, walkedThrough.Length);
            Assert.Equal(targets, walkedThrough);
        }

        [Fact]
        public void MoveSouth_ReturnsAllCoordinatesInPath_FromStartingPoint_InOrder()
        {
            var coord = Coordinate.Origin;
            var targets = new[] { new Coordinate(0, -1), new Coordinate(0, -2), new Coordinate(0, -3), new Coordinate(0, -4) };

            var walkedThrough = coord.MoveSouth(4);

            Assert.Equal(4, walkedThrough.Length);
            Assert.Equal(targets, walkedThrough);
        }

        [Fact]
        public void MoveEast_ReturnsAllCoordinatesInPath_FromStartingPoint_InOrder()
        {
            var coord = Coordinate.Origin;
            var targets = new[] { new Coordinate(1, 0), new Coordinate(2, 0), new Coordinate(3, 0), new Coordinate(4, 0) };

            var walkedThrough = coord.MoveEast(4);

            Assert.Equal(4, walkedThrough.Length);
            Assert.Equal(targets, walkedThrough);
        }

        [Fact]
        public void MoveWest_ReturnsAllCoordinatesInPath_FromStartingPoint_InOrder()
        {
            var coord = Coordinate.Origin;
            var targets = new[] { new Coordinate(-1, 0), new Coordinate(-2, 0), new Coordinate(-3, 0), new Coordinate(-4, 0) };

            var walkedThrough = coord.MoveWest(4);

            Assert.Equal(4, walkedThrough.Length);
            Assert.Equal(targets, walkedThrough);
        }
    }
}