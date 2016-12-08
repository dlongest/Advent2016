using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem7
{
    public class IPv7
    {
        private string address;
        private static Regex ipv7AddressPattern = new Regex(@"^[a-z]+(\[[a-z]+\][a-z]+)+$");
        private static Regex sequencePattern = new Regex(@"[a-z]+");

        public IPv7(string address)
        {
            if (!IPv7.IsValid(address))
                throw new ArgumentException("address is not in the IP V7 format");

            var sequences = FindSequences(address);

            var indexed = Enumerable.Range(0, sequences.Count())
                                    .Zip(sequences, (i, s) => new { Index = i, Sequence = s });

            this.SupernetSequences = indexed.Where(a => a.Index % 2 == 0).Select(a => a.Sequence);
            this.HypernetSequences = indexed.Where(a => a.Index % 2 == 1).Select(a => a.Sequence);
        }

        private static IEnumerable<string> FindSequences(string address)
        {
            var matches = sequencePattern.Matches(address);

            return matches.Cast<Match>().Select(m => m.Value);
        }

        public IEnumerable<string> SupernetSequences { get; private set; }

        public IEnumerable<string> HypernetSequences { get; private set; }

        public bool SupportsTls
        {
            get
            {
                var supernetAbbas = this.SupernetSequences.Select(a => AnyABBAsInSequence(a));

                var hypernetAbbas = this.HypernetSequences.Select(a => AnyABBAsInSequence(a));

                var anyStandardAbbas = supernetAbbas.Where(b => b).Any();

                var anyHypernetAbbas = hypernetAbbas.Where(b => b).Any();

                var result = !anyHypernetAbbas && anyStandardAbbas;

                return result;
            }
        }

        public bool SupportsSsl
        {
            get
            {
                var supernetABAs = Combine(this.SupernetSequences, 3).Where(c => IsAreaBroadcastAccessor(c));

                var expectedBABs = supernetABAs.Select(aba => MakeByteAllocationBlock(aba));

                var allHypernets = Combine(this.HypernetSequences, 3);

                return expectedBABs.Any(bab => allHypernets.Contains(bab));
            }
        }

        private bool AnyABBAsInSequence(string sequence)
        {
            var windows = sequence.Window(4);

            var palindromes = windows.Where(a => a.IsPalindrome());

            if (!palindromes.Any())
                return false;

            var anyWithDistinctCharacters = palindromes.Any(a => ContainsDistinctCharacters(a));

            return anyWithDistinctCharacters;
        }

        private bool IsAreaBroadcastAccessor(string s)
        {
            if (s.Length != 3)
                return false;

            return s[0] == s[2] && s[0] != s[1];
        }

        private string MakeByteAllocationBlock(string areaBroadcastAccessor)
        {
            if (!IsAreaBroadcastAccessor(areaBroadcastAccessor))
            {
                throw new ArgumentException("areaBroadcastAccessor must be 3 characters in length and in correct format");
            }

            return new string(new char[] { areaBroadcastAccessor[1], areaBroadcastAccessor[0], areaBroadcastAccessor[1] });
        }

        private IEnumerable<string> Combine(IEnumerable<string> sequence, int windowSize)
        {
            return sequence.Select(a => a.Window(windowSize)).SelectMany(c => c);
        }

        private bool ContainsDistinctCharacters(string abba)
        {
            return abba.Distinct().Count() > 1;
        }

        public static bool IsValid(string address)
        {
            return ipv7AddressPattern.IsMatch(address);
        }
    }
}
