using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem17
{
    public abstract class PathFinderBase : IPathFinder
    {
        protected readonly IList<Room> rooms;
        protected char[] openCharacters = new char[] { 'b', 'c', 'd', 'e', 'f' };

        public PathFinderBase(IEnumerable<Room> rooms)
        {
            this.rooms = rooms.ToList();
        }

        public Room RoomAt(int x, int y)
        {
            return this.rooms.At(x, y);
        }

        public abstract Path Find(string passcode);

        protected IEnumerable<Direction> OpenDoors(Room current, string hash)
        {
            if (current.DoorsAvailable.Contains(Direction.Up) && openCharacters.Contains(hash[0]))
            {
                yield return Direction.Up;
            }

            if (current.DoorsAvailable.Contains(Direction.Down) && openCharacters.Contains(hash[1]))
            {
                yield return Direction.Down;
            }

            if (current.DoorsAvailable.Contains(Direction.Left) && openCharacters.Contains(hash[2]))
            {
                yield return Direction.Left;
            }

            if (current.DoorsAvailable.Contains(Direction.Right) && openCharacters.Contains(hash[3]))
            {
                yield return Direction.Right;
            }
        }
    }
}
