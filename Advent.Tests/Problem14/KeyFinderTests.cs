using Advent.Core.Problem14;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem14
{
    public class KeyFinderTests
    {
        [Fact]
        public void CorrectlyFindsFirstKey()
        {
            var sut = new KeyFinder();

            var keys = sut.Find("abc", initialKeyspace: 0, keyCount: 1);

            Assert.Equal(39, keys[0]);
        }

        [Fact]
        public void CorrectlyFindsFirstTwoKeys()
        {
            var sut = new KeyFinder();

            var keys = sut.Find("abc", initialKeyspace: 0, keyCount: 2);

            Assert.Equal(39, keys[0]);
            Assert.Equal(92, keys[1]);
        }

        [Fact]
        public void KeyspaceRange_IsExclusive()        
        {
            var sut = new KeyFinder();

            var keys = sut.Find("abc", initialKeyspace: 0, keyspaceRangeExclusive: 92);

            Assert.Equal(1, keys.Count());
            Assert.Equal(39, keys[0]);
        }

        [Fact]
        public void StopsLookingForKeys_WhenKeyspaceExhausted_RegardlessOfKeysFound()
        {
            var sut = new KeyFinder();

            var keys = sut.Find("abc", initialKeyspace: 0, keyCount: 64, keyspaceRangeExclusive: 93);

            Assert.Equal(2, keys.Count());
            Assert.Equal(39, keys[0]);
            Assert.Equal(92, keys[1]);
        }

        // This test runs and passes but it's very time-consuming.
        //[Fact]
        public void CorrectlyFindsLastKey()
        {
            var sut = new KeyFinder();

            var keys = sut.Find("abc", initialKeyspace: 0, keyCount: 64);

            Assert.Equal(22728, keys[63]);
        }
    }

    public class StretchedKeyFinderTests
    {
        [Fact]
        public void CorrectlyFindsFirstKey()
        {
            var sut = new StretchedKeyFinder(2016);

            var keys = sut.Find("abc", initialKeyspace: 0, keyCount: 1);

            Assert.Equal(10, keys[0]);
        }

        // This test runs and passes but it's very time-consuming.
        //[Fact]
        public void CorrectlyFindsLastKey()
        {            
            var sut = new StretchedKeyFinder(2016);

            var keys = sut.Find("abc", initialKeyspace: 918, keyCount: 62);

            Assert.Equal(10, keys[0]);
            Assert.Equal(22551, keys[63]);
        }
    }

    public class ParallelizingKeyFinderTests
    {
        //[Fact]
        public void CorrectlyFindsFirstKey_UsingStandardFinder()
        {
            var sut = new ParallelizingKeyFinder(new KeyFinder());

            var actual = sut.Find("abc", 0, 1);

            Assert.Equal(1, actual.Count());
            Assert.Equal(39, actual[0]);
        }

        //[Fact]
        public void CorrectlyFindsLastKey()
        {
            var sut = new ParallelizingKeyFinder(new KeyFinder());

            var keys = sut.Find("abc", initialKeyspace: 0, keyCount: 64, keyspaceRange: 25000);

            Assert.Equal(22728, keys[63]);
        }

        //[Fact]
        public void CorrectlyFindsLastStretchedKey()
        {
            var sut = new ParallelizingKeyFinder(new StretchedKeyFinder(2016));

            var keys = sut.Find("abc", initialKeyspace: 0, keyCount: 64, keyspaceRange: 25000);

            Assert.Equal(22551, keys[63]);
        }
    }
}
