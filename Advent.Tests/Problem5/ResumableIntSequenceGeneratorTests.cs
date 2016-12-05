using Advent.Core.Problem5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem5
{
    public class ResumableIntSequenceGeneratorTests
    {
        [Fact]
        public void Generate_ResumesGeneratingSequence_AtNextValueWhereItStopped()
        {
            var sut = new ResumableIntSequenceGenerator(0);

            var values = sut.Generate().Take(5).ToArray();

            Assert.Equal(5, sut.Generate().First());
        }
    }   
}
