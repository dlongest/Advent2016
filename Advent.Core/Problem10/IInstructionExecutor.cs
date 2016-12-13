using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem10
{
    public interface IInstructionExecutor
    {
        bool CanExecute(IInstructable factory, string instruction);

        void Execute(IInstructable factory, string instruction);
    }
}
