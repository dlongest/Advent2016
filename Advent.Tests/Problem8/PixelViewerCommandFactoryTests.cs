using Advent.Core.Problem8;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem8
{
    public class PixelViewerCommandFactoryTests
    {
        [Fact]
        public void RectangleCommand_CreatedCorrectly()
        {
            var expected = new TurnOnPixelsInRectangleCommand(3, 2);

            var mockViewer = new Mock<IPixelViewer>();

            var commandStrings = new[] { "rect 3x2" };

            var commands = new PixelViewerCommandFactory().Create(() => commandStrings);

            Assert.Equal(1, commands.Count());
            Assert.Equal(expected, commands.First());
        }

        [Fact]
        public void RotateColumnCommand_CreatedCorrectly()
        {
            var expected = new RotateColumnCommand(1, 1);

            var mockViewer = new Mock<IPixelViewer>();

            var commandStrings = new[] { "rotate column x=1 by 1" };

            var commands = new PixelViewerCommandFactory().Create(() => commandStrings);

            Assert.Equal(1, commands.Count());
            Assert.Equal(expected, commands.First());
        }

        [Fact]
        public void RotateRowCommand_CreatedCorrectly()
        {
            var expected = new RotateRowCommand(0, 4);

            var mockViewer = new Mock<IPixelViewer>();

            var commandStrings = new[] { "rotate row y=0 by 4" };

            var commands = new PixelViewerCommandFactory().Create(() => commandStrings);

            Assert.Equal(1, commands.Count());
            Assert.Equal(expected, commands.First());
        }
    }
}
