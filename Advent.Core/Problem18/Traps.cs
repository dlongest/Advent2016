using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem18
{
    public class Traps
    {
        private string[] traps = new string[] { "^^.", ".^^", "^..", "..^" };

        public string Next(string row)
        {
            var withShadowTiles = string.Format(".{0}.", row);

            return string.Join("", Enumerable.Range(0, row.Length)
                                             .Select(i =>
                                                        new
                                                        {
                                                            Index = i,
                                                            PriorTiles = withShadowTiles.Substring(i, 3)
                                                        })
                                             .Select(a => ItsATrap(a.PriorTiles))
                                             .Select(b => b ? "^" : "."));
        }

        public int SafeSpots(string row)
        {
            return row.Count(a => a == '.');
        }

        public int SafeSpots(IEnumerable<string> rows)
        {
            return rows.Select(r => SafeSpots(r)).Sum();
        }


        private bool ItsATrap(string tiles)
        {
            return this.traps.Contains(tiles);
        }
    }

    public static class TrapsExtensions
    {
        public static IEnumerable<string> Next(this Traps traps, string firstRow, int rowsToCreate, bool includefirstRow = false)
        {        
            var row = firstRow;

            if (includefirstRow)
                yield return firstRow;

            foreach (var index in Enumerable.Range(0, rowsToCreate))
            {
                row = traps.Next(row);
                yield return row;
            }
        }        
    }
}
