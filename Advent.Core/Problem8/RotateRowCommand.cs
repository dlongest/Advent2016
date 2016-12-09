using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem8
{
    public class RotateRowCommand : RotateCommand
    {
        public RotateRowCommand(int rowIndex, int shiftRight)
        {
            this.RowIndex = rowIndex;
            this.ShiftPixelsRight = shiftRight;
        }

        public int RowIndex { get; private set; }

        public int ShiftPixelsRight { get; private set; }

        public override void Update(IPixelViewer viewer)
        {
            var row = viewer.Rows.ElementAt(this.RowIndex);

            var newPixels = CreateUpdatedPixels(row, this.ShiftPixelsRight);

            viewer.Update(newPixels.ToArray());
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var command = obj as RotateRowCommand;

            if (command == null)
                return false;

            return command.RowIndex == this.RowIndex && command.ShiftPixelsRight == this.ShiftPixelsRight;
        }

        public override int GetHashCode()
        {
            return this.RowIndex.GetHashCode() + 3 * this.ShiftPixelsRight.GetHashCode();
        }
    }

    public abstract class RotateCommand : IPixelViewerCommand
    {
        public abstract void Update(IPixelViewer viewer);       

        protected Pixel[] CreateUpdatedPixels(Pixel[] currentPixels, int shift)
        {          
            var shiftedValues = currentPixels.Select(a => a.IsOn).Shift(shift).ToArray();

            var newPixels = currentPixels.Zip(shiftedValues,
                                              (pixel, newOnValue) => new Pixel(pixel.Coordinate, newOnValue));

            return newPixels.ToArray();
        }
    }
}
