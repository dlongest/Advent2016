using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Advent.Core.Problem6;

namespace Advent.Tests.Problem6
{
    public class ExtensionsTests
    {
        [Fact]
        public void ToCharArray_ConvertsCollectionOfStrings_IntoMultiDimCharArrays()
        {
            var data = new List<string>() { "abc", "abc", "abc", "abc" };

            var charArray = data.ToCharArray();

            Assert.Equal(4, charArray.Length);
            Assert.Equal(3, charArray[0].Length);
        }

        [Fact]
        public void Slice_ReturnsTheGivenColumnAcrossAllArrays()
        {
            var expected = new char[] { 'b', 'b', 'b', 'b', };

            var data = new List<string>() { "abc", "abc", "abc", "abc" };

            var bs = data.ToCharArray().Slice(1);

            Assert.Equal(bs, expected);
        }

        [Fact]
        public void StringifyMostFrequent_BuildsString_BasedOnFrequencies_InColumns()
        {
            var expected = "easter";

            var actual = LetterFrequency.StringifyMostFrequent(() => TestCase.Data);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void StringifyLeastFrequent_BuildsString_BasedOnFrequencies_InColumns()
        {
            var expected = "advent";

            var actual = LetterFrequency.StringifyLeastFrequent(() => TestCase.Data);

            Assert.Equal(expected, actual);
        }
    }

    public static class TestCase
    {
        public static IEnumerable<string> Data
        {
            get
            {
                return new[]
                {
                    "eedadn",
                    "drvtee",
                    "eandsr",
                    "raavrd",
                    "atevrs",
                    "tsrnev",
                    "sdttsa",
                    "rasrtv",
                    "nssdts",
                    "ntnada",
                    "svetve",
                    "tesnvt",
                    "vntsnd",
                    "vrdear",
                    "dvrsen",
                    "enarar"
                };
            }
        }
    }
}
