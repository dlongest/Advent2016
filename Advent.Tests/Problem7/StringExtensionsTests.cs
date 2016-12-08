using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Advent.Core.Problem7;

namespace Advent.Tests.Problem7
{
    public class StringExtensionsTests
    {
        [Theory]
        [ClassData(typeof(WindowTestCases))]
        public void Window_ReturnsAllSubstrings_OfGivenSize_InString(string input, 
                                                                     int windowSize, 
                                                                     string[] expected)
        {
            var actual  = input.Window(windowSize);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("abba", true)]
        [InlineData("abcba", true)]
        [InlineData("abbc", false)]
        [InlineData("abbcc", false)]
        public void IsPalindrome_IsCorrect(string input, bool expected)
        {
            var b = new char[] { 'a', 'b' }.Equals(
                new char[] { 'a', 'b' });

            Assert.Equal(expected, input.IsPalindrome());
        }


        private class WindowTestCases : IEnumerable<object[]>
        {
            private IList<object[]> data = new List<object[]>();

            public WindowTestCases()
            {
                this.data.Add(new object[] { "abcd", 4,  new string[] { "abcd" } });
                this.data.Add(new object[] { "abcde", 4, new string[] { "abcd", "bcde" } });
                this.data.Add(new object[] { "abcdef", 4, new string[] { "abcd", "bcde", "cdef" } });
            }

            public IEnumerator<object[]> GetEnumerator()
            {
                return this.data.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}
