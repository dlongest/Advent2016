using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core
{
    public static class FileStringReader
    {
        public static IEnumerable<string> Read(string filename)
        {
            using (var reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
            }
        }

        public static IEnumerable<T> Read<T>(string filename, Func<string, T> projector)
        {
            return FileStringReader.Read(filename).Select(s => projector(s));
        }
    }
}
