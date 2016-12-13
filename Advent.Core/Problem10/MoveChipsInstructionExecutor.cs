using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem10
{
    public class MoveChipsInstructionExecutor : IInstructionExecutor
    {
        private MoveChipInstructionBuilder builder = new MoveChipInstructionBuilder();

        private Regex numberPattern = new Regex(@"\d+");

        private Regex lowToBotPattern = new Regex(@"low to bot \d+");
        private Regex lowToOutputPattern = new Regex(@"low to output \d+");

        private Regex highToBotPattern = new Regex(@"high to bot \d+");
        private Regex highToOutputPattern = new Regex(@"high to output \d+");

        private Action<int, IEnumerable<int>> instructionMonitor;

        public MoveChipsInstructionExecutor()
            :this((label, values) => { })
        {
        }

        public MoveChipsInstructionExecutor(Action<int, IEnumerable<int>> instructionMonitor)
        {
            if (instructionMonitor != null)
            {
                this.instructionMonitor = instructionMonitor;
            }
        }

        public bool CanExecute(IInstructable factory, string instruction)
        {
            var isValidInstruction = IsValidInstruction(instruction);

            var haveTwoChips = DoesBotHaveTwoChips(factory, instruction);

            return isValidInstruction && haveTwoChips;
        }


        private bool IsValidInstruction(string instruction)
        {
            return instruction.StartsWith("bot");
        }

        private bool DoesBotHaveTwoChips(IInstructable factory, string instruction)
        {
            var bot = GetBotForInstruction(instruction);

            if (!factory.Bots.Any(a => a == bot))
            {
                return false;
            }

            var haveTwoChips = factory.BotValues(bot).Count() == 2;

            return haveTwoChips;
        }

        private int GetBotForInstruction(string instruction)
        {
            return Int32.Parse(this.numberPattern.Match(instruction).Value);
        }

        public void Execute(IInstructable factory, string instruction)
        {
            if (!CanExecute(factory, instruction))
                throw new InvalidOperationException(string.Format("Executor cannot run - CanExecute returned false for this input: {0}", instruction));

            StartInstructionForBot(instruction);
            SetLowerMoveInstruction(instruction);
            SetHigherMoveInstruction(instruction);            
            ExecuteInstruction(factory);
        }

        private void StartInstructionForBot(string instruction)
        {
            var bot = GetBotForInstruction(instruction);

            this.builder = builder.New().ForBot(bot).WithMonitor(this.instructionMonitor);
        }

        private void SetLowerMoveInstruction(string instruction)
        {
            if (this.lowToBotPattern.IsMatch(instruction))
            {
                var match = this.numberPattern.Match(this.lowToBotPattern.Match(instruction).Value);

                this.builder.ForLowerType(MoveChipType.Bot).SendLowerTo(Int32.Parse(match.Value));
            }
            else if (lowToOutputPattern.IsMatch(instruction))
            {
                var match = this.numberPattern.Match(this.lowToOutputPattern.Match(instruction).Value);

                this.builder.ForLowerType(MoveChipType.Output).SendLowerTo(Int32.Parse(match.Value));
            }
        }

        private void SetHigherMoveInstruction(string instruction)
        {
            if (this.highToBotPattern.IsMatch(instruction))
            {
                var match = this.numberPattern.Match(this.highToBotPattern.Match(instruction).Value);

                this.builder.ForHigherType(MoveChipType.Bot).SendHigherTo(Int32.Parse(match.Value));
            }
            else if (highToOutputPattern.IsMatch(instruction))
            {
                var match = this.numberPattern.Match(this.highToOutputPattern.Match(instruction).Value);

                this.builder.ForHigherType(MoveChipType.Output).SendHigherTo(Int32.Parse(match.Value));
            }
        }

        private void ExecuteInstruction(IInstructable factory)
        {
            var instruction = this.builder.Build();

            factory.MoveChips(instruction);
        }
    }
}
