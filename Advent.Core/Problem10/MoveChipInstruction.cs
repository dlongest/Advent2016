using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem10
{
    public class MoveChipInstruction
    {
        public int Bot { get; set; }

        public MoveChipType Lower;

        public int SendLowerTo { get; set; }

        public MoveChipType Higher;

        public int SendHigherTo { get; set; }

        public Action<int, IEnumerable<int>> Monitor { get; set; }
    }

    public enum MoveChipType
    {
        Bot, Output
    };
}
