using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem8
{
    public static class PixelExtensions
    {
        public static IEnumerable<T> Shift<T>(this IEnumerable<T> source, int shift)
        {
            var splitAt = source.Count() - shift;

            var parts = source.Split(splitAt);

            return parts.Item2.Concat(parts.Item1);
        }

        public static Tuple<IEnumerable<T>, IEnumerable<T>> Split<T>(this IEnumerable<T> source,
                                                                     int firstCount)
        {
            var firstPart = source.Take(firstCount);

            var secondPart = source.Skip(firstCount);

            return Tuple.Create(firstPart, secondPart);
        }

        public static string AsString(this IEnumerable<Pixel> pixels)
        {
            return string.Join("", pixels.Select(a => a.ToString()));
        }
    }
}
