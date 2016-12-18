using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem13
{
    public class Path
    {
        public static Path Empty = new Path();

        public static Path From(MazeCoordinate start, MazeCoordinate end, IEnumerable<MazeCoordinate> route)
        {
            if (!route.First().Equals(start) || !route.Last().Equals(end))
                return Path.Empty;

            return new Path(start, end, route);
        }

        private Path()
        {
            this.FullPath = Enumerable.Empty<MazeCoordinate>();
            this.Length = 0;
        }

        private Path(MazeCoordinate start, MazeCoordinate end, IEnumerable<MazeCoordinate> path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (!path.Any())
            {
                throw new ArgumentException("Path does not contain any coordinates");
            }

            this.Start = start;
            this.End = end;
            this.FullPath = path;
            this.Length = path.Count() - 1;
        }

        public MazeCoordinate Start { get; private set; }

        public MazeCoordinate End { get; private set; }

        public IEnumerable<MazeCoordinate> FullPath { get; private set; }

        public int Length { get; private set; }
    }
}
