using Advent.Core.Problem8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem8
{
    public class ScreenTests
    {
        [Fact]
        public void Rows_Returns_EachRow_InColumnOrder()
        {
            var expectedRow1 = new[] { new Pixel(0, 0, false), new Pixel(1, 0, false), new Pixel(2, 0, false) };
            var expectedRow2 = new[] { new Pixel(0, 1, false), new Pixel(1, 1, false), new Pixel(2, 1, false) };

            var screen = new Screen(3, 2);

            var rows = screen.Rows.ToArray();

            Assert.Equal(2, rows.Count());
            Assert.True(rows.First().SequenceEqual(expectedRow1));
            Assert.True(rows.Last().SequenceEqual(expectedRow2));
        }

        [Fact]
        public void Columns_Returns_EachColumn_InRowOrder()
        {
            var expectedColumn1 = new[] { new Pixel(0, 0, false), new Pixel(0, 1, false) };
            var expectedColumn2 = new[] { new Pixel(1, 0, false), new Pixel(1, 1, false) };
            var expectedColumn3 = new[] { new Pixel(2, 0, false), new Pixel(2, 1, false) };

            var screen = new Screen(3, 2);

            var columns = screen.Columns.ToArray();

            Assert.Equal(3, columns.Count());
            Assert.True(columns[0].SequenceEqual(expectedColumn1));
            Assert.True(columns[1].SequenceEqual(expectedColumn2));
            Assert.True(columns[2].SequenceEqual(expectedColumn3));
        }

        [Fact]
        public void Update_ReplacesPixelAtPosition_WithNewPixel()
        {
            var expected = new Pixel(0, 1, true);

            var sut = new Screen(2, 2);

            var current = sut.At(0, 1);
            sut.Update(expected);

            Assert.True(expected != current);
            Assert.True(expected == sut.At(0, 1));
        }

        [Theory]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        public void Update_IgnoresPixel_OutsideViewer(int x, int y)
        {
            var sut = new Screen(2, 2);

            sut.Update(new Pixel(x, y, isOn: true));

            Assert.True(true);
        }


        [Fact]
        public void RectangleScreenTest()
        {
            var screen = new Screen(7, 3);

            new TurnOnPixelsInRectangleCommand(3, 2).Update(screen);

            var expected = new[] { "###....", "###....", "......." };

            var actual = screen.Rows.Select(a => a.AsString()).ToArray();

            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void RectangleAndColumnRotateScreenTest()
        {
            var screen = new Screen(7, 3);

            new TurnOnPixelsInRectangleCommand(3, 2).Update(screen);
            new RotateColumnCommand(1, 1).Update(screen);

            var expected = new[] { "#.#....", "###....", ".#....." };

            var actual = screen.Rows.Select(a => a.AsString()).ToArray();

            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void RectangleAndColumnAndRowRotateScreenTest()
        {
            var screen = new Screen(7, 3);

            new TurnOnPixelsInRectangleCommand(3, 2).Update(screen);
            new RotateColumnCommand(1, 1).Update(screen);
            new RotateRowCommand(0, 4).Update(screen);

            var expected = new[] { "....#.#", "###....", ".#....." };
           
            var actual = screen.Rows.Select(a => a.AsString()).ToArray();

            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void RowRotateScreenTest()
        {
            var screen = new Screen(7, 3);

            screen.Update(new[] { new Pixel(1, 0, true), new Pixel(2, 0, true), new Pixel(4, 0, true), new Pixel(5, 0, true) });
           
            new RotateRowCommand(0, 2).Update(screen);

            var expected = new[] { "#..##.#" ,".......", "......." };

            var actual = screen.Rows.Select(a => a.AsString()).ToArray();

            Assert.True(expected.SequenceEqual(actual));
        }


        [Fact]
        public void CompleteSystemTest()
        {
            var screen = new Screen(7, 3);

            new TurnOnPixelsInRectangleCommand(3, 2).Update(screen);
            new RotateColumnCommand(1, 1).Update(screen);
            new RotateRowCommand(0, 4).Update(screen);
            new RotateColumnCommand(1, 1).Update(screen);

            var expected = new[] { ".#..#.#", "#.#....", ".#....." };

            var actual = screen.Rows.Select(a => a.AsString()).ToArray();

            Assert.True(expected.SequenceEqual(actual));
        }
              
    }
}
