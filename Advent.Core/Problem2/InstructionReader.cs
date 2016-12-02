using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem2
{
    public static class InstructionReader
    {
        public static IEnumerable<IEnumerable<char>> FromFile(string file)
        {
            using (var reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    yield return line.Select(c => c);
                }
            }
        }
    }
}
