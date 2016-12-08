using Advent.Core.Problem7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Collections;

namespace Advent.Tests.Problem7
{
    public class Ipv7Tests
    {
        [Theory]
        [ClassData(typeof(SequenceTestCases))]
        public void Segments_Contain_CorrectValues(string address, string[] standardSequences, string[] hypernetSequences)
        {
            var sut = new IPv7(address);

            Assert.Equal(standardSequences, sut.SupernetSequences.ToArray());
            Assert.Equal(hypernetSequences, sut.HypernetSequences.ToArray());
        }

        [Theory]
        [InlineData("abba[mnop]qrst", true)]
        [InlineData("abcd[bddb]xyyx", false)]
        [InlineData("aaaa[qwer]tyui", false)]
        [InlineData("ioxxoj[asdfgh]zxcvbn", true)]
        [InlineData("acca[defg]abd[elop]zarc", true)]
        [InlineData("acca[deed]abd[elop]zarc", false)]
        public void SupportsTls_ReturnsCorrect_BasedOnTlsParsingRules(string address, bool expected)
        {
            var sut = new IPv7(address);

            Assert.Equal(expected, sut.SupportsTls);
        }

        [Theory]
        [InlineData("aba[bab]xyz", true)]
        [InlineData("xyx[xyx]xyx", false)]
        [InlineData("aaa[kek]eke", true)]
        [InlineData("zazbz[bzb]cdb", true)]        
        public void SupportsSsl_ReturnsCorrect_BasedOnSslRules(string address, bool expected)
        {
            var sut = new IPv7(address);

            Assert.Equal(expected, sut.SupportsSsl);
        }

        private class SequenceTestCases : IEnumerable<object[]>
        {
            private IList<object[]> data = new List<object[]>();

            public SequenceTestCases()
            {
                this.data.Add(new object[] { "aaa[bbb]ccc", new string[] { "aaa", "ccc" }, new string[] { "bbb" } });
                this.data.Add(new object[] { "aaa[bbb]ccc[ddd]eee", new string[] { "aaa", "ccc", "eee" }, new string[] { "bbb", "ddd" } });
                this.data.Add(new object[] { "aaa[bbb]ccc[ddd]eee[fff]ggg", new string[] { "aaa", "ccc", "eee", "ggg" }, new string[] { "bbb", "ddd", "fff" } });
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
