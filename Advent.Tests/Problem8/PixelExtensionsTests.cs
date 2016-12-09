using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Advent.Core.Problem8;
using System.Collections;

namespace Advent.Tests.Problem8
{
    public class PixelExtensionsTests
    {
        [Fact]
        public void Shift_MovesElements_ByGivenAmount()
        {
            var elements = new[] { "a", "b", "c", "d", "e", "f" };

            var expected = new[] {"e", "f", "a", "b", "c", "d" };

            var actual = elements.Shift(2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Shift_By0_Returns_MatchingNewCollection()
        {
            var elements = new[] { "a", "b", "c", "d", "e", "f" };            

            var actual = elements.Shift(0);

            Assert.NotSame(elements, actual);
            Assert.True(elements.SequenceEqual(actual));
        }

        [Fact]
        public void Shift_ReturnsDifferentInstance_ForEmptyCollection()
        {
            var expected = new string[0];

            var actual = expected.Shift(4).ToArray();

            Assert.Equal(0, actual.Length);
            Assert.NotSame(expected, actual);            
        }

        [Theory]
        [ClassData(typeof(SplitTestCases))]
        public void Split_ReturnsTuple_WithCorrectElementsInEachPart(int[] collection, int splitCount,
                                                                     int[] firstPart, int[] secondPart)
        {
            var actual = collection.Split(splitCount);

            Assert.True(actual.Item1.SequenceEqual(firstPart));
            Assert.True(actual.Item2.SequenceEqual(secondPart));
        }

        [Fact]
        public void AsString_ReturnsPixelRepresentation_BasedOn_ToStringOfEachPixel()
        {
            var pixels = new[] { new Pixel(0, 0, true), new Pixel(0, 1, false), new Pixel(0, 2, true) };

            Assert.Equal("#.#", pixels.AsString());
        }

        private class SplitTestCases : IEnumerable<object[]>
        {
            private List<object[]> data = new List<object[]>();

            public SplitTestCases()
            {
                data.Add(new object[] { new[] { 0, 1, 2, 3, 4 }, 2, new[] { 0, 1 }, new[] { 2, 3, 4 } });
                data.Add(new object[] { new[] { 0, 1, 2, 3, 4 }, 0, new int[0], new[] { 0, 1, 2, 3, 4 }  });
                data.Add(new object[] { new[] { 0, 1, 2, 3, 4 }, 5, new[] { 0, 1, 2, 3, 4 }, new int[0] });
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
