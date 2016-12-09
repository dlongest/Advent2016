using Advent.Core.Problem8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem8
{
    public class PixelTests
    {
        [Fact]        
        public void ObjectEquals_AssessesEquality_OnAllProperties()
        {
            Assert.True(new Pixel(1, 1, true).Equals(new Pixel(1, 1, true)));
            Assert.True(new Pixel(1, 1, false).Equals(new Pixel(1, 1, false)));
            Assert.False(new Pixel(1, 1, true).Equals(new Pixel(1, 1, false)));
        }       

        [Fact]
        public void CoordinateEqual_AssessesEquality_BasedOnlyOnCoordinate()
        {
            Assert.True(new Pixel(1, 1, true).CoordinateEquals(new Pixel(1, 1, true)));
            Assert.True(new Pixel(1, 1, false).CoordinateEquals(new Pixel(1, 1, false)));
            Assert.True(new Pixel(1, 1, true).CoordinateEquals(new Pixel(1, 1, false)));
            Assert.False(new Pixel(1, 1, true).CoordinateEquals(new Pixel(1, 2, false)));
        }

        [Fact]
        public void EqualsCoordinate_AssessesEquality_BasedOnlyOnCoordinate()
        {
            Assert.True(new Pixel(1, 1, true).Equals(new Coordinate(1, 1)));
            Assert.False(new Pixel(1, 1, true).Equals(new Coordinate(1, 2)));            
        }

        [Fact]
        public void TurnOn_ReturnsPixel_AtSameCoordinate_WithIsOn_True()
        {
            var expected = new Pixel(1, 2, isOn: true);

            var sut = new Pixel(1, 2, isOn: false);
            var actual = sut.TurnOn();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TurnOn_DoesNotMutate_OriginalPixel()
        {
            var expected = new Pixel(1, 2, isOn: true);

            var sut = new Pixel(1, 2, isOn: false);
            var actual = sut.TurnOn();

            Assert.False(sut.IsOn);
        }

        [Fact]
        public void TurnOff_ReturnsPixel_AtSameCoordinate_WithIsOn_False()
        {
            var expected = new Pixel(1, 2, isOn: false);

            var sut = new Pixel(1, 2, isOn: true);
            var actual = sut.TurnOff();

            Assert.Equal(expected, actual);
        }
    }
}
