using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem22
{
    public class GridNode
    {
        public static GridNode From(string entry)
        {
            var tokens = entry.Replace("T", string.Empty).Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);

            return new GridNode(tokens[0], Int32.Parse(tokens[1]), Int32.Parse(tokens[2]), Int32.Parse(tokens[3]));
        }

        private Regex numberPattern = new Regex(@"\d+");

        public GridNode(string name, int size, int used, int available)
        {
            var nodeNumbers = this.numberPattern.Matches(name).Cast<Match>();

            this.X = Int32.Parse(nodeNumbers.First().Value);
            this.Y = Int32.Parse(nodeNumbers.Last().Value);

            this.Size = size;
            this.Used = used;
            this.Available = available;
        }


        public GridNode(int x, int y, int size, int used, int available)
        {
            this.X = x;
            this.Y = y;
            this.Size = size;
            this.Used = used;
            this.Available = available;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public int Size { get; private set; }

        public int Used { get; private set; }

        public int Available { get; private set; }
        
        /// <summary>
        /// Returns true when this.Used != 0, this != other, and this.Used <= other.Available
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsViable(GridNode other)
        {
            if (this.Used == 0)
                return false;

            if (this.Equals(other))
                return false;

            return this.Used <= other.Available;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var node = obj as GridNode;

            if (node == null) return false;

            return this.X == node.X && this.Y == node.Y;
        }        

        public override int GetHashCode()
        {
            return this.X.GetHashCode() + 3 * this.Y.GetHashCode();
        }
    }
}
