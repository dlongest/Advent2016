using Advent.Core.Problem17;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem17
{
    public class PathTests
    {
        [Fact]
        public void New_CreatesInitialPath_AtGivenLocation()
        {
            var startingRoom = new Room(1, 2);
            var targetRoom = new Room(3, 3);

            var sut = Path.New(startingRoom, targetRoom);

            Assert.Equal(new Room(1, 2), sut.CurrentRoom);
            Assert.Equal(targetRoom, sut.TargetRoom);
        }   

        [Fact]
        public void AvailableMoves_IsSameAsUnderlyingCurrentRoom()
        {
            var startingRoom = new Room(1, 2);
            var other = new Room(0, 0);
            startingRoom.SetRoomForMove(Direction.Up, other);

            var sut = Path.New(startingRoom, new Room(3, 3));

            var actual = sut.AvailableMoves;

            Assert.Equal(1, actual.Count());
            Assert.Equal(Direction.Up, actual.First());
        }

        [Fact]
        public void NoMoreRoomsAvailable_WhenTargetIsReached()
        {
            var startingRoom = new Room(1, 2);
            var target = new Room(0, 0);
            startingRoom.SetRoomForMove(Direction.Up, target);
            target.SetRoomForMove(Direction.Down, startingRoom);

            var sut = Path.New(startingRoom, target).Move(Direction.Up);

            var actual = sut.AvailableMoves;

            Assert.Equal(0, actual.Count());
        }

        [Fact]
        public void AsString_ReturnsStringRepresentationOfDirectionCollection()
        {
            var expected = "UDDRL";
            var directions = new[] { Direction.Up, Direction.Down, Direction.Down, Direction.Right, Direction.Left };

            Assert.Equal(expected, directions.AsString());
        }

        [Fact]
        public void PathKeepsTrackOfMovesBetweenRooms()
        {
            var expected = "RLDU";

            var topLeft = new Room(0, 0);
            var rightOne = new Room(1, 0);
            var downOne = new Room(0, 1);

            topLeft.SetRoomForMove(Direction.Right, rightOne);
            topLeft.SetRoomForMove(Direction.Down, downOne);

            rightOne.SetRoomForMove(Direction.Left, topLeft);
            downOne.SetRoomForMove(Direction.Up, topLeft);

            var path = Path.New(topLeft, new Room(3, 3))
                           .Move(Direction.Right)
                           .Move(Direction.Left)
                           .Move(Direction.Down)
                           .Move(Direction.Up);

            var actual = path.MovesSoFar.AsString();

            Assert.Equal(expected, actual);
        }

    }
}
