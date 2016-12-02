using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem1
{
    public class Coordinate
    {
        public static Coordinate Origin = new Coordinate(0, 0);

        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public Coordinate[] MoveNorth(int distance)
        {
            var ys = Enumerable.Range(this.Y + 1, distance);

            return ys.Select(y => new Coordinate(this.X, y)).ToArray();
        }

        public Coordinate[] MoveSouth(int distance)
        {
            var ys = Enumerable.Range(this.Y - distance, distance).Reverse();

            return ys.Select(y => new Coordinate(this.X, y)).ToArray();
        }

        public Coordinate[] MoveEast(int distance)
        {
            var xs = Enumerable.Range(this.X + 1, distance);

            return xs.Select(x => new Coordinate(x, this.Y)).ToArray();
        }

        public Coordinate[] MoveWest(int distance)
        {
            var xs = Enumerable.Range(this.X - distance, distance).Reverse();

            return xs.Select(x => new Coordinate(x, this.Y)).ToArray();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var c = obj as Coordinate;

            if (c == null)
                return false;

            return this.X == c.X & this.Y == c.Y;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() + 3 * this.Y.GetHashCode();
        }
    }
}
