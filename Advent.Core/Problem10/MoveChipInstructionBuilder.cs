using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem10
{
    public class MoveChipInstructionBuilder
    {
        private MoveChipInstruction instruction;

        public MoveChipInstructionBuilder()
        {
        }

        public MoveChipInstructionBuilder New()
        {
            this.instruction = new MoveChipInstruction();
            this.instruction.Monitor = (label, values) => { };
            return this;
        }

        public MoveChipInstructionBuilder ForBot(int bot)
        {
            instruction.Bot = bot;
            return this;
        }

        public MoveChipInstructionBuilder SendLowerTo(int label)
        {
            instruction.SendLowerTo = label;
            return this;
        }

        public MoveChipInstructionBuilder ForLowerType(MoveChipType type)
        {
            instruction.Lower = type;
            return this;
        }

        public MoveChipInstructionBuilder SendHigherTo(int label)
        {
            instruction.SendHigherTo = label;
            return this;
        }

        public MoveChipInstructionBuilder ForHigherType(MoveChipType type)
        {
            instruction.Higher = type;
            return this;
        }

        public MoveChipInstructionBuilder WithMonitor(Action<int, IEnumerable<int>> monitor)
        {
            if (monitor != null)
            {
                this.instruction.Monitor = monitor;
            }

            return this;
        }

        public MoveChipInstruction Build()
        {
            return this.instruction;
        }
    }
}
