using Advent.Core.Problem22;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem22
{
    public class GridNodeTests
    {
        [Fact]
        public void From_CreatesGridNodeCorrectly()
        {
            var input = "/dev/grid/node-x1-y2     87T   71T    16T   81%";

            var node = GridNode.From(input);

            Assert.Equal(1, node.X);
            Assert.Equal(2, node.Y);
            Assert.Equal(87, node.Size);
            Assert.Equal(71, node.Used);
            Assert.Equal(16, node.Available);
        }

        [Fact]
        public void IsViable_ReturnsTrue_WhenAllCriteriaMatch()
        {
            var node = new GridNode(0, 0, 65, 10, 55);
            var other = new GridNode(0, 1, 75, 84, 11);

            Assert.True(node.IsViable(other));
        }

        [Fact]
        public void IsViable_ReturnsFalse_When_UsedIsZero()
        {
            var node = new GridNode(0, 0, 65, 0, 65);
            var other = new GridNode(0, 1, 85, 74, 11);

            Assert.False(node.IsViable(other));
        }

        [Fact]
        public void IsViable_ReturnsFalse_WhenSameNode()
        {
            var node = new GridNode(0, 0, 65, 10, 55);
            var other = new GridNode(0, 0, 85, 74, 11);

            Assert.False(node.IsViable(other));
        }

        [Fact]
        public void IsViable_ReturnsFalse_UsedIsNotLessThanEqualToOtherAvailable()
        {
            var node = new GridNode(0, 0, 65, 10, 55);
            var other = new GridNode(0, 1, 85, 76, 9);

            Assert.False(node.IsViable(other));
        }
    }

    public class GridNodeExtensionsTests
    {
        [Fact]
        public void PairAll_GeneratesCartesianProduct()
        {
            var node00 = new GridNode(0, 0, 65, 10, 55);
            var node01 = new GridNode(0, 1, 65, 10, 55);
            var node10 = new GridNode(1, 0, 65, 10, 55);
            
            var pairs = new[] { node00, node01, node10 }.PairAll();

            Assert.Equal(9, pairs.Count());

            Assert.Contains(pairs, pair => pair.A == node00 && pair.B == node00);
            Assert.Contains(pairs, pair => pair.A == node00 && pair.B == node01);
            Assert.Contains(pairs, pair => pair.A == node00 && pair.B == node10);
            Assert.Contains(pairs, pair => pair.A == node01 && pair.B == node00);
            Assert.Contains(pairs, pair => pair.A == node01 && pair.B == node01);
            Assert.Contains(pairs, pair => pair.A == node01 && pair.B == node10);
            Assert.Contains(pairs, pair => pair.A == node10 && pair.B == node00);
            Assert.Contains(pairs, pair => pair.A == node10 && pair.B == node01);
            Assert.Contains(pairs, pair => pair.A == node10 && pair.B == node10);
        }

        [Fact]
        public void WhereViable_ReturnsCollection_ContainingOnlyViablePairs()
        {
            var node00 = new GridNode(0, 0, size: 50, used: 10, available: 40);
            var node01 = new GridNode(0, 1, size: 25, used: 5, available: 20);
            var node10 = new GridNode(1, 0, size: 5, used: 0, available: 5);

            var viablePairs = new[] { node00, node01, node10 }.PairAll().WhereViable();

            Assert.Equal(3, viablePairs.Count());
            
            Assert.Contains(viablePairs, pair => pair.A == node00 && pair.B == node01);            
            Assert.Contains(viablePairs, pair => pair.A == node01 && pair.B == node00);            
            Assert.Contains(viablePairs, pair => pair.A == node01 && pair.B == node10);                 
        }
    }
}
