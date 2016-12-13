using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem10
{
    public class Foreman
    {
        public void Execute(IInstructionExecutor valueExecutor,
                            IInstructionExecutor moveChipsExecutor,
                            IInstructable factory,
                            IEnumerable<string> instructions)
        {

            var valueInstructions = instructions.Where(a => valueExecutor.CanExecute(factory, a));

            foreach (var valueInstruction in valueInstructions)
            {
                valueExecutor.Execute(factory, valueInstruction);
            }

            var remaining = instructions.Except(valueInstructions);

            while (remaining.Any())
            {
                //  -Initially, bot 1 starts with a value-3 chip, and bot 2 starts with a value-2 chip and a value-5 chip.
                //- Because bot 2 has two microchips, it gives its lower one(2) to bot 1 and its higher one (5) to bot 0.
                //- Then, bot 1 has two microchips; it puts the value-2 chip in output 1 and gives the value-3 chip to bot 0.
                //- Finally, bot 0 has two microchips; it puts the 3 in output 2 and the 5 in output 0
                var nextToExecute = NextInstruction(remaining,
                                                     i => moveChipsExecutor.CanExecute(factory, i));

               moveChipsExecutor.Execute(factory, nextToExecute);

                remaining = Remove(remaining, nextToExecute);
            }
        }

        private string NextInstruction(IEnumerable<string> instructions, Func<string, bool> canExecute)
        {
            var executable = instructions.Where(a => canExecute(a));

            if (!executable.Any())
            {
                throw new InvalidOperationException("No instructions are executable -- program will be deadlocked");
            }

            return executable.First();
        }

        private IEnumerable<string> Remove(IEnumerable<string> source, string item)
        {
            return source.Where(a => !a.Equals(item));
        }
    }    
}
