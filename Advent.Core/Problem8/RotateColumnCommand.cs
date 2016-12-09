using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem8
{
    public class RotateColumnCommand : RotateCommand
    {

        public RotateColumnCommand(int columnIndex, int shiftDown)
        {
            this.ColumnIndex = columnIndex;
            this.ShiftPixelsDown = shiftDown;
        }

        public int ColumnIndex { get; private set; }

        public int ShiftPixelsDown { get; private set; }

        public override void Update(IPixelViewer viewer)
        {
            var column = viewer.Columns.ElementAt(this.ColumnIndex);

            var newPixels = CreateUpdatedPixels(column, this.ShiftPixelsDown);

            viewer.Update(newPixels.ToArray());
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var command = obj as RotateColumnCommand;

            if (command == null)
                return false;

            return command.ColumnIndex == this.ColumnIndex && command.ShiftPixelsDown == this.ShiftPixelsDown;
        }

        public override int GetHashCode()
        {
            return this.ColumnIndex.GetHashCode() + 3 * this.ShiftPixelsDown.GetHashCode();
        }
    }
}
