using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem13
{
    public class MazeNode
    {
        private readonly IList<MazeCoordinate> neighbors = new List<MazeCoordinate>();

        public MazeNode(int x, int y)
        {
            this.Coordinate = new MazeCoordinate(x, y);
        }

        public MazeNode(MazeCoordinate coordinate)
        {
            this.Coordinate = coordinate;
        }

        public MazeCoordinate Coordinate { get; private set; }

        public IEnumerable<MazeCoordinate> Neighbors { get { return this.neighbors; } }

        public MazeNode AddNeighbors(IEnumerable<MazeCoordinate> neighbors)
        {
            foreach (var neighbor in neighbors)
            {
                this.AddNeighbor(neighbor);
            }

            return this;
        }

        public MazeNode AddNeighbor(MazeCoordinate neighbor)
        {
            this.neighbors.Add(neighbor);
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var n = obj as MazeNode;

            if (n == null) return false;

            var coordsEqual = n.Coordinate.Equals(this.Coordinate);

            var neighborsEqual = this.neighbors.OrderBy(a => a.X).ThenBy(a => a.Y)
                                    .SequenceEqual(n.Neighbors.OrderBy(a => a.X).ThenBy(a => a.Y));

            return coordsEqual && neighborsEqual;
        }

        public override int GetHashCode()
        {
            return this.Coordinate.GetHashCode() + 3 * this.neighbors.GetHashCode();
        }
    }
}
