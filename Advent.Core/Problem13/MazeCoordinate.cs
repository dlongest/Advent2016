using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem13
{
    public class MazeCoordinate
    {
        public MazeCoordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var mc = obj as MazeCoordinate;

            if (mc == null) return false;

            return mc.X == this.X && mc.Y == this.Y;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() + 3 * this.Y.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", this.X, this.Y);
        }
    }
}
