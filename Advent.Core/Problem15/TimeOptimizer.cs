using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem15
{
    public class TimeOptimizer
    {       
        private IDictionary<int, Disc> orderedDiscs;
        private int howManyDiscs;

        public TimeOptimizer(Disc[] discs)
        {
            this.howManyDiscs = discs.Count();

            this.orderedDiscs = Enumerable.Range(1, discs.Count())
                                          .Zip(discs, (o, d) => new { Order = o, Disc = d })
                                          .ToDictionary(k => k.Order, v => v.Disc);
        }

        public int FindOptimalTime()
        {
            int time = 0;

            while (!IsOptimal(time))
            {
                ++time;
            }

            return time;
        }

        private bool IsOptimal(int time)
        {
            foreach (var key in this.orderedDiscs.Keys.OrderBy(a => a))
            {
                if (this.orderedDiscs[key].WhatPositionAfter(key + time) != 0)
                    return false;
            }

            return true;
        }

        private void RotateAllDiscs()
        {
            foreach (var key in this.orderedDiscs.Keys)
            {
                this.orderedDiscs[key].Rotate();
            }
        }
    }
    
    public class OrderedDisc
    {
        public OrderedDisc(Disc disc, int order)
        {
            this.Disc = disc;
            this.Order = order;
        }

        public int Order { get; private set; }

        public Disc Disc { get; private set; }
    }
}
