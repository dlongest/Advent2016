using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem21
{
    public class InstructionReverser
    {
        private IDictionary<Func<string, bool>, Func<string, IEnumerable<string>>> reversals = new Dictionary<Func<string, bool>, Func<string, IEnumerable<string>>>();

        public InstructionReverser()
        {
            //this.reversals.Add(s => IsSwapPositions(s), s => ReverseSwapPositions(s));
            //this.reversals.Add(s => IsSwapLetters(s), s => ReverseSwapLetters(s));
            //this.reversals.Add(s => IsRotateLeft(s), s => ReverseRotateLeft(s));
            //this.reversals.Add(s => IsRotateRight(s), s => ReverseRotateRight(s));
            //this.reversals.Add(s => IsRotateLetterBased(s), s => ReverseRotateLetterBased(s));
            //this.reversals.Add(s => IsReversePositions(s), s => ReverseReversePositions(s));
            //this.reversals.Add(s => IsMovePosition(s), s => ReverseMovePosition(s));
        }

        public IEnumerable<string> Reverse(IEnumerable<string> instructions)
        {
            var newInstructions = new List<string>();

            foreach (var instruction in instructions)
            {
                var r = this.reversals.Select(kvp => new { Applies = kvp.Key(instruction), Func = kvp.Value });

                if (!r.Any())
                    throw new ArgumentException("Unable to handle instruction");

                var reversed = r.First(a => a.Applies).Func(instruction);

                newInstructions.AddRange(reversed);                
            }

            return newInstructions;
        }

        //private static bool IsSwapPositions(string instruction)
        //{
        //    return Regex.IsMatch(instruction, @"swap position \d+ with position \d+");
        //}

        //private static IEnumerable<string> ReverseSwapPositions(string instruction)
        //{
        //    var positions = ParseBinaryNumbers(instruction);

        //    yield return string.Format("swap position {0} with position {1}", positions.Item2, positions.Item1);
        //}

        //private static bool IsSwapLetters(string instruction)
        //{
        //    return Regex.IsMatch(instruction, @"swap letter [a-z] with letter [a-z]");
        //}
        
        //private static IEnumerable<string> ReverseSwapLetters(string instruction)
        //{
        //    yield return instruction;
        //}

        //private static bool IsRotateLeft(string instruction)
        //{
        //    return Regex.IsMatch(instruction, @"rotate left \d+ steps?");
        //}

        //private static IEnumerable<string> ReverseRotateLeft(string instruction)
        //{
        //    var number = Int32.Parse(Regex.Match(instruction, @"\d+").Value);

        //    yield return string.Format(@"rotate right {0} steps", number);
        //}

        //private static bool IsRotateRight(string instruction)
        //{
        //    return Regex.IsMatch(instruction, @"rotate right \d+ steps?");
        //}

        //private static IEnumerable<string> ReverseRotateRight(string instruction)
        //{
        //    var number = Int32.Parse(Regex.Match(instruction, @"\d+").Value);

        //    yield return string.Format(@"rotate left {0} steps", number);
        //}

        private static bool IsRotateLetterBased(string instruction)
        {
            return Regex.IsMatch(instruction, @"rotate based on position of letter [a-z]");
        }

        private static IEnumerable<string> ReverseRotateLetterBased(string instruction)
        {
            var targetLetter = instruction.Last();

            yield return instruction;

        }

        //private static bool IsReversePositions(string instruction)
        //{
        //    return Regex.IsMatch(instruction, @"reverse positions \d+ through \d+");
        //}

        //private static IEnumerable<string> ReverseReversePositions(string instruction)
        //{
        //    yield return instruction;
        //}

        //private static bool IsMovePosition(string instruction)
        //{
        //    return Regex.IsMatch(instruction, @"move position \d+ to position \d+");
        //}

        //private static IEnumerable<string> ReverseMovePosition(string instruction)
        //{
        //    var positions = ParseBinaryNumbers(instruction);

        //    yield return string.Format("move position {0} to position {1}", positions.Item2, positions.Item1);
        //}

        private static Tuple<int, int> ParseBinaryNumbers(string instruction)
        {
            var matches = Regex.Matches(instruction, @"\d+").Cast<Match>();

            var first = Int32.Parse(matches.First().Value);
            var second = Int32.Parse(matches.Last().Value);

            return Tuple.Create(first, second);
        }

        private static Tuple<int, int> ParseBinaryStrings(string instruction)
        {
            var matches = Regex.Matches(instruction, @"\d+").Cast<Match>();

            var first = Int32.Parse(matches.First().Value);
            var second = Int32.Parse(matches.Last().Value);

            return Tuple.Create(first, second);
        }

    }
    
}
