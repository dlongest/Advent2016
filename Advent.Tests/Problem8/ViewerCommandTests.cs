using Advent.Core.Problem8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Collections;
using Moq;

namespace Advent.Tests.Problem8
{
    public class ViewerCommandTests
    {
        [Theory]
        [ClassData(typeof(RectangleTestCases))]
        public void Rectangle_TurnsOnCorrect_Pixels(TurnOnPixelsInRectangleCommand sut, Pixel[] expected)
        {
            var viewer = new StubViewer(4, 4);

            sut.Update(viewer);

            Assert.Equal(4, viewer.Pixels.Count);
            Assert.True(viewer.Pixels.Contains(expected[0]));
            Assert.True(viewer.Pixels.Contains(expected[1]));
            Assert.True(viewer.Pixels.Contains(expected[2]));
            Assert.True(viewer.Pixels.Contains(expected[3]));
        }

        [Fact]
        public void Column_RotatesElements()
        {
            var expectedColumn = new[] { new Pixel(0, 0, false), new Pixel(0, 1, false), new Pixel(0, 2, true) };

            var viewer = new[]
            {
                new Pixel[0],
                new[] { new Pixel(0, 0, isOn: false), new Pixel(0, 1, isOn: true), new Pixel(0, 2, isOn: false) },
                new Pixel[0]
            };

            Pixel[] actual = null;

            var mockViewer = new Mock<IPixelViewer>();

            mockViewer.Setup(a => a.Columns).Returns(() => viewer);
            mockViewer.Setup(a => a.Update(It.IsAny<Pixel[]>())).Callback<Pixel[]>(pixels => actual = pixels);            

            var sut = new RotateColumnCommand(1, 1);

            sut.Update(mockViewer.Object);

            Assert.True(expectedColumn.SequenceEqual(actual));
        }

        [Fact]
        public void Row_RotatesElements()
        {
            var expectedRow = new[] { new Pixel(0, 1, true), new Pixel(1, 1, false), new Pixel(2, 1, false) };

            var viewer = new[]
            {
                new Pixel[0],
                new[] { new Pixel(0, 1, isOn: false), new Pixel(1, 1, isOn: true), new Pixel(2, 1, isOn: false) },
                new Pixel[0]
            };

            Pixel[] actual = null;

            var mockViewer = new Mock<IPixelViewer>();

            mockViewer.Setup(a => a.Rows).Returns(() => viewer);
            mockViewer.Setup(a => a.Update(It.IsAny<Pixel[]>())).Callback<Pixel[]>(pixels => actual = pixels);

            var sut = new RotateRowCommand(1, 2);

            sut.Update(mockViewer.Object);

            Assert.True(expectedRow.SequenceEqual(actual));
        }


        private class RectangleTestCases : IEnumerable<object[]>
        {
            private List<object[]> data = new List<object[]>();

            public RectangleTestCases()
            {
                this.data.Add(new object[] { new TurnOnPixelsInRectangleCommand(2, 2), new[]
                {
                    new Pixel(0, 0, true), new Pixel(0, 1, true), new Pixel(1, 0, true), new Pixel(1, 1, true)
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


    public class StubViewer : IPixelViewer
    {
        public StubViewer(int numberColumns, int numberRows)
        {
            this.NumberColumns = numberColumns;
            this.NumberRows = numberRows;
        }

        public List<Pixel> Pixels = new List<Pixel>();

        public int NumberColumns { get; private set; }

        public IEnumerable<Pixel[]> Columns
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int NumberRows { get; private set; }

        public IEnumerable<Pixel[]> Rows
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Pixel At(int row, int column)
        {
            throw new NotImplementedException();
        }

        public void Update(Pixel[] pixels)
        {
            this.Pixels.AddRange(pixels);
        }

        public void Update(Pixel pixel)
        {
            throw new NotImplementedException();
        }
    }
}
