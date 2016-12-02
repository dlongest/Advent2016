using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem2
{
    public class BathroomCodeFinder
    {
        public string Code(Keypad keypad, Func<IEnumerable<IEnumerable<char>>> instructions)
        {
            var code = string.Empty;

            foreach (var instructionSet in instructions())
            {
                foreach (var instruction in instructionSet)
                {
                    switch (instruction)
                    {
                        case 'U':
                            keypad.Up();
                            break;
                        case 'D':
                            keypad.Down();
                            break;
                        case 'L':
                            keypad.Left();
                            break;
                        case 'R':
                            keypad.Right();
                            break;
                    }
                }

                code = code + keypad.Current();
            }

            return code;
        }
    }
}
