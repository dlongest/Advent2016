using Advent.Core.Problem17;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem17
{
    public class PathFinderTests
    {
        [Fact]
        public void RoomsAreConstructedCorrectly()
        {
            var rooms = new SquareShapedRoomsBuilder().Create();

            var room = rooms.At(0, 0)
                            .Move(Direction.Right).Move(Direction.Right).Move(Direction.Right)
                            .Move(Direction.Down)
                            .Move(Direction.Left).Move(Direction.Left).Move(Direction.Left)
                            .Move(Direction.Down)
                            .Move(Direction.Right).Move(Direction.Right).Move(Direction.Right)
                            .Move(Direction.Down);

            Assert.Equal(new Room(3, 3), room);
        }

        [Theory]
        [InlineData("ihgpwlah", "DDRRRD")]
        [InlineData("kglvqrro", "DDUDRLRRUDRD")]
        [InlineData("ulqzkmiv", "DRURDRUDDLLDLUURRDULRLDUUDDDRR")]
        [InlineData("hhhxzeay", "DDRUDLRRRD")]
        public void FindPath_FindsTheShortestPath(string passcode, string expected)
        {
            var pathFinder = new ShortestPathFinder(new SquareShapedRoomsBuilder().Create());

            var actual = pathFinder.Find(passcode);

            Assert.Equal(expected, actual.MovesSoFar.AsString());
        }

        // This test runs correctly but is very time-consuming.
        //[Theory]
        [InlineData("ihgpwlah", 370)]
        [InlineData("kglvqrro", 492)]
        [InlineData("ulqzkmiv", 830)]
        [InlineData("hhhxzeay", 398)]
        public void FindPath_FindsTheLongestPath(string passcode, int expected)
        {
            var pathFinder = new LongestPathFinder(new SquareShapedRoomsBuilder().Create());

            var actual = pathFinder.Find(passcode).MovesSoFar.Count();

            Assert.Equal(expected, actual);
        }
    }
}
