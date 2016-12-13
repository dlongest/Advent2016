using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem10
{
    public interface IInstructable
    {
        void LoadBot(LoadBotInstruction instruction);

        void MoveChips(MoveChipInstruction instruction);

        IEnumerable<int> Bots { get; }

        IEnumerable<int> BotValues(int botLabel);

        IEnumerable<int> OutputBins { get; }

        IEnumerable<int> OutputBinValues(int binLabel);
    }
}
