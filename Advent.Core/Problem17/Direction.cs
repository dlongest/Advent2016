using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem17
{
    public class Direction
    {
        public static Direction Up = new Direction("U");
        public static Direction Down = new Direction("D");
        public static Direction Left = new Direction("L");
        public static Direction Right = new Direction("R");

        private Direction(string label)
        {
            this.Label = label;
        }

        public string Label { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var d = obj as Direction;

            if (d == null) return false;

            return d.Label.Equals(this.Label);
        }

        public override int GetHashCode()
        {
            return this.Label.GetHashCode();
        }

        public override string ToString()
        {
            return this.Label;
        }
    }
}
