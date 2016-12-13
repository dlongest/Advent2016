using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem10
{
    public class LoadBotInstructionExecutor : IInstructionExecutor
    {
        private Regex numberPattern = new Regex(@"\d+");


        public bool CanExecute(IInstructable factory, string instruction)
        {
            return instruction.StartsWith("value");
        }

        public void Execute(IInstructable factory, string instruction)
        {
            if (!CanExecute(factory, instruction))
                throw new InvalidOperationException(string.Format("Unable to execute instruction as CanExecute returned false: {0}", instruction));

            var parts = this.numberPattern.Matches(instruction)
                                          .Cast<Match>()
                                          .Select(a => a.Value);

            var value = Int32.Parse(parts.First());
            var bot = Int32.Parse(parts.Last());

            factory.LoadBot(new LoadBotInstruction(bot, value));
        }
    }
}
