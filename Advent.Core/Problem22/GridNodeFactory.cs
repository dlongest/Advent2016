using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem22
{
    public class GridNodeFactory
    {
        public IEnumerable<GridNode> Create(IEnumerable<string> entries)
        {
            foreach (var entry in entries)
            {
                yield return GridNode.From(entry);
            }
        }       
    }   


    public static class GridNodeExtensions
    {
        public static IEnumerable<Pair> PairAll(this IEnumerable<GridNode> nodes)
        {
            return nodes.SelectMany(n => nodes, (a, b) => new Pair(a, b));
        }

        public static IEnumerable<Pair> WhereViable(this IEnumerable<Pair> pairs)
        {
            return pairs.Where(pair => pair.IsViable());
        }
    }

    public class Pair
    {
        public Pair(GridNode first, GridNode second)
        {
            this.A = first;
            this.B = second;
        }

        public GridNode A { get; private set; }

        public GridNode B { get; private set; }

        public bool IsViable()
        {
            return this.A.IsViable(this.B);
        }
    }
}
