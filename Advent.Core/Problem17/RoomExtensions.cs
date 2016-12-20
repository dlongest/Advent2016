using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem17
{
    public static class RoomExtensions
    {
        public static Room At(this IEnumerable<Room> rooms, int x, int y)
        {
            return rooms.First(a => a.X == x && a.Y == y);
        }

        public static IList<Path> Append(this IList<Path> paths, Path path)
        {
            paths.Add(path);

            return paths;
        }
    }
}
