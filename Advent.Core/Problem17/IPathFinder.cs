using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem17
{
    public interface IPathFinder
    {
        Path Find(string code);

        Room RoomAt(int x, int y);
    }
}
