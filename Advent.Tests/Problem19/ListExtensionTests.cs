using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Advent.Core.Problem19;

namespace Advent.Tests.Problem19
{
    public class ListExtensionTests
    {
        [Fact]
        public void IndexAway_ReturnsTheIndexAGivenAmountFromIndex_IncludingWrapping()
        {
            var s = new int[] { 1, 2, 3, 4, 5 };

            Assert.Equal(2, s.IndexAway(0, 2));
            Assert.Equal(3, s.IndexAway(1, 2));
            Assert.Equal(4, s.IndexAway(2, 2));
            Assert.Equal(0, s.IndexAway(3, 2));
            Assert.Equal(1, s.IndexAway(4, 2));
            Assert.Equal(3, s.IndexAway(4, 4));
        }

        [Fact]
        public void ValueAway_ReturnsTheValueAGivenAmountFromIndex_IncludingWrapping()
        {
            var s = new int[] { 1, 2, 3, 4, 5 };

            Assert.Equal(3, s.ValueAway(0, 2));
            Assert.Equal(4, s.ValueAway(1, 2));
            Assert.Equal(5, s.ValueAway(2, 2));
            Assert.Equal(1, s.ValueAway(3, 2));
            Assert.Equal(2, s.ValueAway(4, 2));
            Assert.Equal(4, s.ValueAway(4, 4));
        }

    }
}
