using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Advent.Core.Problem14;

namespace Advent.Tests.Problem14
{
    public class ExtensionsTests
    {
        [Fact]
        public void Partition_CreatesCorrectRange_ForSinglePartition()
        {
            var expected = new[] { Tuple.Create(1, 21) };

            var actual = Partition.Create(1, 20, 1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Partition_CreatesCorrectRange_ForTwoEvenPartitions()
        {
            var expected = new[] { Tuple.Create(1, 11), Tuple.Create(11, 21) };

            var actual = Partition.Create(1, 20, 2);

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void Partition_CreatesCorrectRange_ForTwoUnevenPartitions()
        {
            var expected = new[] { Tuple.Create(1, 11), Tuple.Create(11, 22) };

            var actual = Partition.Create(1, 21, 2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Partition_CreatesCorrectRange_ForFourUnevenPartitions()
        {
            var expected = new[] { Tuple.Create(1, 6), Tuple.Create(6, 11),
            Tuple.Create(11, 16), Tuple.Create(16, 24) };

            var actual = Partition.Create(1, 23, 4);

            Assert.Equal(expected, actual);
        }
    }
}
