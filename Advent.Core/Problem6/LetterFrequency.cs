using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem6
{
    public class LetterFrequency
    {
        public static string StringifyMostFrequent(Func<IEnumerable<string>> strings)
        {
            return LetterFrequency.Stringify(strings, letterCounts => letterCounts.OrderByDescending(a => a.Count));
        }

        public static string StringifyLeastFrequent(Func<IEnumerable<string>> strings)
        {
            return LetterFrequency.Stringify(strings, letterCounts => letterCounts.OrderBy(a => a.Count));
        }

        private static string Stringify(Func<IEnumerable<string>> strings, 
                                        Func<IEnumerable<LetterCount>, IOrderedEnumerable<LetterCount>> sorter)
        {
            var xs = strings();

            var sb = new StringBuilder();

            foreach (var columnIndex in Enumerable.Range(0, xs.First().Length))
            {
                var grouped = xs.ToCharArray()
                                            .Slice(columnIndex)
                                            .GroupBy(ch => ch)
                                            .Select(g => new LetterCount { Letter = g.Key, Count = g.Count() });

                var mostCommonCharacter = sorter(grouped).Take(1).First().Letter;

                sb.Append(mostCommonCharacter);
            }

            return sb.ToString();
        }
    }

    internal class LetterCount
    {
        public char Letter { get; set; }

        public int Count { get; set; }
    }

    public static class Extensions
    {
        


        public static char[][] ToCharArray(this IEnumerable<string> s)
        {
            var stringLength = s.ValidateAllStringSameLength();

            return s.Select(a => a.ToCharArray()).ToArray();        
        }

        private static int ValidateAllStringSameLength(this IEnumerable<string> s)
        {
            var lengths = s.Select(a => a.Length).Distinct();

            if (lengths.Count() > 1)
                throw new ArgumentException("All strings must be the same length");

            if (lengths.Count() == 0)
                return 0;

            return lengths.First();
        }


        public static char[] Slice(this char[][] chars, int columnIndex)
        {
            return chars.Select(a => a[columnIndex]).ToArray();
        }
    }
}