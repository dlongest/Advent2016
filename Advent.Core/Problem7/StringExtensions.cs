using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem7
{
    public static class StringExtensions
    {
        public static IEnumerable<string> Window(this string s, int windowSize)
        {
            if (s.Length < windowSize)
                return new string[0];

            var numberWindows = (s.Length - windowSize) + 1;

            return Enumerable.Range(0, numberWindows)
                             .Select(i => s.Substring(i, windowSize));
        }

        public static bool IsPalindrome(this string s)
        {
            var split = (s.IsEvenLength()) ? s.SplitEvenLength() : s.SplitOddLength();

            return split.Item1.SequenceEqual(split.Item2.Reverse());
        }        

        private static Tuple<IEnumerable<char>, IEnumerable<char>> SplitOddLength(this string s)
        {
            if (s.IsEvenLength())
                throw new ArgumentException("string length must be odd");

            var middleCharacterIndex = (int)(s.Length / 2);  // 3 / 2 = 1.5 rounded up to 2
            var firstHalf = s.Take(middleCharacterIndex);
            var secondHalf = s.Skip(middleCharacterIndex + 1);

            return Tuple.Create(firstHalf, secondHalf);
        }

        private static bool IsEvenLength(this string s)
        {
            return s.Length % 2 == 0;
        }

        private static Tuple<IEnumerable<char>, IEnumerable<char>> SplitEvenLength(this string s)
        {
            if (!s.IsEvenLength())
                throw new ArgumentException("string length must be even");

            var firstHalf = s.Take(s.Length / 2);
            var secondHalf = s.Skip(s.Length / 2);

            return Tuple.Create(firstHalf, secondHalf);
        }     
    }   
}
