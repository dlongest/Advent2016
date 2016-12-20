using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem19
{
    public static class PresentStealing
    {

        public static int WhichElfGetsThePresents(int numberElves)
        {
            var elves = new List<int>(Enumerable.Range(1, numberElves));

            while (elves.Count > 1)
            {
                var taker = elves.First();
                elves.RemoveRange(0, 2);

                elves.Add(taker);
            }

            return elves.First();
        }

        private static IDictionary<int, int> powersOf2;
        private static IDictionary<int, int> powersOf3;

        static PresentStealing()
        {            
            powersOf2 = Enumerable.Range(0, 32).ToDictionary(i => i, i => (int)Math.Pow(2, i));
            powersOf3 = Enumerable.Range(0, 32).ToDictionary(i => i, i => (int)Math.Pow(3, i));
        }

        public static int WhichSmartElfGetsThePresents(int numberElves)
        {
            // Given a value z that lies within the range [2^m, 2^n] where 2^m is the 
            // greatest power of 2 below z, then the elf receiving the presents can be
            // computed as:
            //      (z - 2^m) * 2  + 1

            var closestPowerOf2 = ClosestLesserPowerOf2(numberElves);

            return (numberElves - closestPowerOf2) * 2 + 1;
        }

        private static int ClosestLesserPowerOf2(int numberElves)
        {
            return powersOf2.Values.Where(i => i <= numberElves).Max();           
        }

        public static int WhichCircularElfGetsThePresents(int numberElves)
        {
            var elves = new List<int>(Enumerable.Range(1, numberElves));

            while (elves.Count > 1)
            {
                var elvesAway = (int)(elves.Count / 2);

                elves.RemoveAt(elvesAway);

                elves = elves.Rotate(1);
            }

            return elves.First();
        }

        private static KeyValuePair<int, int> ClosestLesserPowerOf3(int numberElves)
        {
            var max = powersOf3.Values.Where(i => i <= numberElves).Max();

            return powersOf3.First(kvp => kvp.Value == max);
        }

        private static KeyValuePair<int, int> ClosestGreaterPowerOf3(int numberElves)
        {
            var min = powersOf3.Values.Where(i => i > numberElves).Min();

            return powersOf3.First(kvp => kvp.Value == min);
        }

        public static int WhichSmartCircularElfGetsThePresents(int numberElves)
        {
            var closestLesserPowerOf3 = ClosestLesserPowerOf3(numberElves);

            if (numberElves == closestLesserPowerOf3.Value)
                return numberElves;

            var difference = numberElves - closestLesserPowerOf3.Value;

            if (difference <= closestLesserPowerOf3.Value)
                return difference;
             

            // 7 - 3 = 4    # 5
            // 8 - 3 = 5    # 7
            // 19 - 9 = 10  # 11
            // 20 - 9 = 11  # 13
            // 55 - 27 = 28 # 29
            // 56 - 27 = 29 # 31
            // 57 - 27 = 30 # 33
            // 58 - 27 = 31 # 35
             
            var closestGreaterPowerOf3 = ClosestGreaterPowerOf3(numberElves);

            var values = Enumerable.Range(closestLesserPowerOf3.Value * 2 + 1, closestLesserPowerOf3.Value - 1);

            var adds = Enumerable.Range(0, closestLesserPowerOf3.Value - 1).Select(i => i * 2 + 1 - i);

            var zipped = values.Zip(adds, (v, a) => new { Value = v, Add = a, ElfPosition = v - closestLesserPowerOf3.Value + a });

            return zipped.Where(a => a.Value == numberElves).First().ElfPosition;
        }        

    }

    public static class ListExtensions
    {
        public static T ValueAway<T>(this IEnumerable<T> source, int index, int distance)
        {
            var newIndex = source.IndexAway(index, distance);

            return source.ElementAt(newIndex);
        }

        public static int IndexAway<T>(this IEnumerable<T> source, int index, int distance)
        {
            return (index + distance) % source.Count();
        }

        public static List<T> Rotate<T>(this List<T> source, int distance)
        {
            var first = source.Take(distance);

            return source.Skip(distance).Concat(first).ToList();
        }
    }
}
