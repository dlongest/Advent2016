using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem8
{    
    public class Pixel
    {
        public Pixel(int x, int y, bool isOn = false)
            :this(new Coordinate(x, y), isOn)
        {
        }

        public Pixel(Coordinate coordinate, bool isOn = false)
        {
            this.Coordinate = coordinate;
            this.IsOn = isOn;
        }

        public Coordinate Coordinate { get; private set; }

        public bool IsOn { get; private set; }

        public Pixel TurnOn()
        {
            return new Pixel(this.Coordinate, true);
        }

        public Pixel TurnOff()
        {
            return new Pixel(this.Coordinate, false);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var c = obj as Pixel;

            if (c == null)
                return false;

            return this.Coordinate.Equals(c.Coordinate) && this.IsOn == c.IsOn;
        }

        public override int GetHashCode()
        {
            return this.Coordinate.GetHashCode() + 3 * this.IsOn.GetHashCode();
        }
        
        
        public override string ToString()
        {
            return this.IsOn ? "#" : ".";
        }
        
        public bool Equals(Coordinate coordinate)
        {
            return this.Coordinate.X == coordinate.X && this.Coordinate.Y == coordinate.Y;
        }

        public bool CoordinateEquals(Pixel pixel)
        {
            return this.Coordinate.Equals(pixel.Coordinate);
        }
    }

    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() + 3 * this.Y.GetHashCode();
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

    }
}
