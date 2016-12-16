using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem15
{
    public static class DiscFactory
    {
        public static IEnumerable<Disc> Create()
        {
            var discs = new List<Disc>();

            discs.Add(new Disc(13, 10));
            discs.Add(new Disc(17, 15));
            discs.Add(new Disc(19, 17));
            discs.Add(new Disc(7, 1));
            discs.Add(new Disc(5, 0));
            discs.Add(new Disc(3, 1));

            return discs;
        }

        public static IEnumerable<Disc> CreatePart2()
        {
            var discs = new List<Disc>();

            discs.Add(new Disc(13, 10));
            discs.Add(new Disc(17, 15));
            discs.Add(new Disc(19, 17));
            discs.Add(new Disc(7, 1));
            discs.Add(new Disc(5, 0));
            discs.Add(new Disc(3, 1));
            discs.Add(new Disc(11, 0));

            return discs;
        }
    }
}
