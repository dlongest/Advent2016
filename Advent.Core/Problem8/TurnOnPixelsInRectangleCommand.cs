using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem8
{
    public class TurnOnPixelsInRectangleCommand : IPixelViewerCommand
    {
        public TurnOnPixelsInRectangleCommand(int rectangleWidth, int rectangleHeight)
        {
            this.RectangleWidth = rectangleWidth;
            this.RectangleHeight = rectangleHeight;
        }

        public int RectangleWidth { get; private set; }

        public int RectangleHeight { get; private set; }

        public void Update(IPixelViewer viewer)
        {
            var pixels = CreatePixels();

            viewer.Update(pixels);
        }

        public Coordinate[] CreateRectangleCoordinates()
        {
            var coords = new List<Coordinate>();

            foreach (var row in Enumerable.Range(0, this.RectangleWidth))
            {
                foreach (var column in Enumerable.Range(0, this.RectangleHeight))
                {
                    coords.Add(new Coordinate(row, column));
                }
            }

            return coords.ToArray();
        }

        private Pixel[] CreatePixels()
        {
            return CreateRectangleCoordinates().Select(b => new Pixel(b, isOn: true)).ToArray();
        }
    }
}
