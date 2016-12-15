using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem14
{
    public interface IKeyFinder
    {
        int[] Find(string salt, int initialKeyspace, int keyCount, int keyspaceRange);        
    }

    public class ParallelizingKeyFinder : IKeyFinder
    {
        private readonly IKeyFinder inner;              
        private int keyspaceRange = 50000;
        private int numberPartitions = 4;
        
        public ParallelizingKeyFinder(IKeyFinder inner)
        {
            this.inner = inner;             
        }

        public int[] Find(string salt, int initialKeyspace = 0, int keyCount = 64, int keyspaceRange = Int32.MaxValue)
        {
            var found = new ConcurrentDictionary<Tuple<int, int>, int[]>();

            var partitioned = Partition.Create(initialKeyspace, 25000, this.numberPartitions);

            var result = Parallel.ForEach(partitioned, partition =>
            {
                var keys = this.inner.Find(salt, initialKeyspace: partition.Item1, keyspaceRange: partition.Item2, keyCount: 64);

                if (!found.TryAdd(partition, keys))
                    throw new InvalidOperationException("Couldn't add keys to master dictionary");
            });

            if (!result.IsCompleted)
            {
                throw new InvalidOperationException("no clue - didn't finish");
            }

            return found.SelectMany(a => a.Value).OrderBy(a => a).Take(keyCount).ToArray();
        }

        private IEnumerable<int> GetFullKeyspace(int initialKey)
        {
            return Enumerable.Range(initialKey, keyspaceRange);
        }
    }
    
    public class StretchedKeyFinder : KeyFinder
    {
        private readonly int howManyStretches;

        public StretchedKeyFinder(int howManyStretches)
        {
            this.howManyStretches = howManyStretches;
        }

        /// <summary>
        /// Returns the stretched hash with the iniital hash based on the given salt and index. 
        /// </summary>
        /// <param name="salt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override HashedValue CreatePotentialKey(string salt, int index)
        {
            return new HashedValue(CreateValue(salt, index)).Rehash(this.howManyStretches);
        }
    }

    public class KeyFinder : IKeyFinder
    {
        private Regex tripletPattern = new Regex(@"([a-z0-9])\1\1");
        
        public int[] Find(string salt, int initialKeyspace = 0, int keyCount = 64, int keyspaceRangeExclusive = Int32.MaxValue)
        {
            var index = initialKeyspace;
            var keys = new List<int>();

            while (keys.Count < keyCount && index < keyspaceRangeExclusive)
            {
                var potentialKey = CreatePotentialKey(salt, index);

                if (IsKey(potentialKey, index, a => CreatePotentialKey(salt, a)))
                {
                    keys.Add(index);
                }

                ++index;
            }

            return keys.ToArray();
        }

        protected string CreateValue(string salt, int index)
        {
            return string.Format("{0}{1}", salt, index);
        }

        /// <summary>
        /// Returns the hashed value represented by the salt and the index.
        /// </summary>
        /// <param name="salt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected virtual HashedValue CreatePotentialKey(string salt, int index)
        {
            return new HashedValue(CreateValue(salt, index));
        }

        private bool IsKey(HashedValue potentialKey, int keyIndex, Func<int, HashedValue> hashGenerator)
        {
            var match = tripletPattern.Match(potentialKey.HexadecimalHash);

            if (match == Match.Empty)
                return false;

            var tripletCharacter = match.Value.First();

            var quintupletPattern = new Regex(string.Format("{0}{0}{0}{0}{0}", tripletCharacter));

            foreach (var index in Enumerable.Range(keyIndex + 1, 1000))
            {
                var hash = hashGenerator(index);

                if (quintupletPattern.IsMatch(hash.HexadecimalHash))
                    return true;
            }

            return false;
        }
    }

    

    public class HashedValue
    {
        public HashedValue(string value)
        {
            this.Value = value;
            this.HexadecimalHash = HashedValue.Hash(this.Value);
        }

        public string HexadecimalHash { get; set; }

        public string Value { get; private set; }

        public static string Hash(string s)
        {
            return Md5Hash.Compute(s).ToHexString();
        }

        public HashedValue Rehash(int count)
        {
            var value = this;

            foreach (var c in Enumerable.Range(0, count))
            {
                value = new HashedValue(value.HexadecimalHash);
            }

            return value;
        }
    }

    public static class BoolExtensions
    {
        internal static bool AnyTrue(this IEnumerable<bool> s)
        {
            return s.Any(a => a == true);
        }
    }

    public static class Partition
    {
        public static IEnumerable<Tuple<int, int>> Create(int minimum, int maximum, int partitions)
        {
            var valuesPerPartition = (maximum - minimum + 1) / partitions;
            var leftover = (maximum - minimum + 1) % partitions;

            var ranges = new List<Tuple<int, int>>();

            foreach(var partition in Enumerable.Range(0, partitions))
            {
                var begin = partition * valuesPerPartition + minimum;
                var endExclusive = (partition + 1) * valuesPerPartition + 1;

                // If we're on the last partition, we apply the leftover to it.
                if (partition == partitions - 1)
                {
                    endExclusive += leftover;
                }

                ranges.Add(Tuple.Create(begin, endExclusive));
            }

            return ranges;
        }

    }
}
