using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem17
{
    public class SquareShapedRoomsBuilder : IRoomsBuilder
    {
        public IEnumerable<Room> Create()
        {
            var rooms = new List<Room>();

            foreach (var x in Enumerable.Range(0, 4))
            {
                foreach (var y in Enumerable.Range(0, 4))
                {
                    rooms.Add(new Room(x, y));
                }
            }

            foreach (var x in Enumerable.Range(0, 4))
            {
                foreach (var y in Enumerable.Range(0, 4))
                {
                    var room = rooms.At(x, y);

                    if (x >= 0 && x <= 2)
                    {
                        room.SetRoomForMove(Direction.Right, rooms.At(x + 1, y));
                    }

                    if (x >= 1 && x <= 3)
                    {
                        room.SetRoomForMove(Direction.Left, rooms.At(x - 1, y));
                    }

                    if (y >= 0 && y <= 2)
                    {
                        room.SetRoomForMove(Direction.Down, rooms.At(x, y + 1));
                    }

                    if (y >= 1 && y <= 3)
                    {
                        room.SetRoomForMove(Direction.Up, rooms.At(x, y - 1));
                    }
                }
            }

            return rooms;
        }
    }      
}
