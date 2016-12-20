using Advent.Core.Problem17;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem17
{
    public class RoomTests
    {
        [Fact]
        public void Room_MovesToAvailableDirection_OrThrows()
        {
            var sut = new Room(0, 0);
            var alt = new Room(2, 2);

            sut.SetRoomForMove(Direction.Down, alt);
            sut.SetRoomForMove(Direction.Right, alt);

            Assert.Equal(alt, sut.Move(Direction.Down));
            Assert.Equal(alt, sut.Move(Direction.Right));
            Assert.Throws(typeof(ArgumentException), () => sut.Move(Direction.Up));
            Assert.Throws(typeof(ArgumentException), () => sut.Move(Direction.Left));
        }

        [Fact]
        public void DoorsAvailable_ReturnsDirectionsThatItCanMoveTo()
        {
            var sut = new Room(0, 0);
            var alt = new Room(2, 2);

            sut.SetRoomForMove(Direction.Down, alt);
            sut.SetRoomForMove(Direction.Right, alt);

            var actual = sut.DoorsAvailable;

            Assert.Equal(2, actual.Count());
            Assert.True(actual.Contains(Direction.Down));
            Assert.True(actual.Contains(Direction.Right));            
        }

        [Fact]
        public void Equals_IsBasedOn_CoordinateEquality()
        {
            var sut = new Room(1, 1);
            var sameAsSut = new Room(1, 1);
            sameAsSut.SetRoomForMove(Direction.Up, new Room(3, 3));
            var different = new Room(1, 2);

            Assert.Equal(sameAsSut, sut);
            Assert.NotEqual(sut, different);
        }
    }
}
