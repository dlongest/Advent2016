using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem3
{
    public class RowBasedTriangleEvaluator
    {

        public int HowManyValid(string file)
        {
            var allNumbers = new List<int[]>();

            using (var reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    var threeNumbers = reader.ReadLine().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(a => Int32.Parse(a))
                                        .OrderBy(a => a)
                                        .ToArray();

                    allNumbers.Add(threeNumbers);
                }
            }

            return allNumbers.Where(a => a[0] + a[1] > a[2]).Count();
        }
    }

    public class ColumnBasedTriangleEvaluator
    {
        public int HowManyValid(string file)
        {
            var allNumbers = new List<int[]>();

            using (var reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    var first = ReadLine(reader);
                    var second = ReadLine(reader);
                    var third = ReadLine(reader);

                    allNumbers.Add(new[] { first[0], second[0], third[0] }.OrderBy(a => a).ToArray());
                    allNumbers.Add(new[] { first[1], second[1], third[1] }.OrderBy(a => a).ToArray());
                    allNumbers.Add(new[] { first[2], second[2], third[2] }.OrderBy(a => a).ToArray());
                }
            }

            return allNumbers.Where(a => a[0] + a[1] > a[2]).Count();
        }

        private int[] ReadLine(StreamReader reader)
        {
            return reader.ReadLine().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(a => Int32.Parse(a))
                                        .ToArray();
        }
    }
}
