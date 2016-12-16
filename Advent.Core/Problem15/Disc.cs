using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem15
{
    public class Disc
    {
        private int[] positions;        

        public Disc(int howManyPositions, int startingPosition)
        {
            this.CurrentPosition = startingPosition;
            this.HowManyPositions = howManyPositions;
            this.positions = Enumerable.Range(0, howManyPositions).ToArray();
        }

        public Disc Rotate()
        {
            this.CurrentPosition = WhatIsNextPosition();
            return this;
        }

        public int WhatIsNextPosition()
        {
            return (this.CurrentPosition + 1) % this.HowManyPositions;
        }

        public int HowFarToNextOpening()
        {
            return this.HowManyPositions - this.CurrentPosition;
        }

        public int WhatPositionAfter(int numberRotations)
        {
            return (this.CurrentPosition + numberRotations) % this.HowManyPositions;
        }

        public int HowManyPositions { get; private set; }

        public int CurrentPosition { get; private set; }
    }
}
