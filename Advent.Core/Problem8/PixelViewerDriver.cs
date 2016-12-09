using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent.Core.Problem8
{
    public static class PixelViewerDriver
    {      
       public static void Process(this IEnumerable<IPixelViewerCommand> commands, IPixelViewer viewer)
        {
            foreach (var command in commands)
            {
                command.Update(viewer);
            }
        }
    }

    public class NullCommand : IPixelViewerCommand
    {
        public void Update(IPixelViewer viewer)
        {            
        }
    }

    public interface IPixelViewerCommandFactory
    {
        IEnumerable<IPixelViewerCommand> Create(Func<IEnumerable<string>> commandReader);
    }

    public class PixelViewerCommandFactory : IPixelViewerCommandFactory
    {
        private Regex rectangleWidthPattern = new Regex(@"\s\d+");
        private Regex rectangleHeightPattern = new Regex(@"\d+$");
        private Regex rotateIndexPattern = new Regex(@"\d+\s");
        private Regex rotateShiftPattern = new Regex(@"\d+$");

        public IEnumerable<IPixelViewerCommand> Create(Func<IEnumerable<string>> commandReader)
        {
            foreach (var command in commandReader())
            {
                yield return CreateCommand(command);
            }
        }

        private IPixelViewerCommand CreateCommand(string s)
        {
            if (s.StartsWith("rect"))
            {
                var width = Int32.Parse(this.rectangleWidthPattern.Match(s).Value);
                var height = Int32.Parse(this.rectangleHeightPattern.Match(s).Value);

                return new TurnOnPixelsInRectangleCommand(width, height);
            }

            if (s.StartsWith("rotate column"))
            {
                var columnIndex = Int32.Parse(this.rotateIndexPattern.Match(s).Value);
                var shiftDown = Int32.Parse(this.rotateShiftPattern.Match(s).Value);

                return new RotateColumnCommand(columnIndex, shiftDown);
            }

            if (s.StartsWith("rotate row"))
            {
                var rowIndex = Int32.Parse(this.rotateIndexPattern.Match(s).Value);
                var shiftRight = Int32.Parse(this.rotateShiftPattern.Match(s).Value);

                return new RotateRowCommand(rowIndex, shiftRight);
            }

            return new NullCommand();
        }

    }
}
