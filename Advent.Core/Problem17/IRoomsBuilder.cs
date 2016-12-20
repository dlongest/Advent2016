using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem17
{
    public interface IRoomsBuilder
    {
        IEnumerable<Room> Create();
    }
}
