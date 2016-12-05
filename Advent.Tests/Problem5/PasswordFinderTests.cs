using Advent.Core.Problem5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem5
{
    public class PasswordFinderTests
    {     
        // This test passes but it's very time-consuming to run.  Uncomment the Theory attribute
        // if you want to run it.   
        //[Theory]
        [InlineData("abc", "05ace8e3")]             
        public void Find_FindsCorrectNumber_ForSecretKey(string secretKey, string expected)
        {
            var sut = new PasswordFinder(new PasswordPrefixValidator("00000"),
                                                new ResumableIntSequenceGenerator(start: 3231929));
            
            var actual = sut.Find(secretKey, 8);

            Assert.Equal(expected, actual);
        }
    }

    public class StubIntGenerator : ISequenceGenerator<int>
    {
        private int[] sequence = new int[] { 3231929, 5017308, 5357525 };
        private int lastIndex = -1;

        public IEnumerable<int> Generate()
        {            
            while (true)
            {
                lastIndex = lastIndex + 1 % sequence.Length;
                yield return sequence[lastIndex];               
            }
        }
    }
}
