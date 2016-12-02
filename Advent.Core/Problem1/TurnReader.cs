using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem1
{
    public static class TurnReader
    {
        public static IEnumerable<Turn> FromFile(string text)
        {
            using (var reader = new StreamReader(text))
            {
                var line = reader.ReadLine();

                return line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => new Turn(t.Trim()));
            }
        }
    }
}
