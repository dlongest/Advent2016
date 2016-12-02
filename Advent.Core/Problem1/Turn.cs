using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem1
{
    public class Turn
    {
        public Turn(string direction, int blocks)
        {
            if (direction != "L" && direction != "R")
                throw new ArgumentException(string.Format("Direction not recognized: {0}", direction));

            if (direction == "L")
                this.Direction = Direction.Left;

            if (direction == "R")
                this.Direction = Direction.Right;

            if (blocks <= 0)
                throw new ArgumentException("Blocks must be greater than 0");

            this.Blocks = blocks;
        }

        public Turn(string turnAndBlocks)
        {
            var direction = turnAndBlocks.ElementAt(0);

            if (direction != 'L' && direction != 'R')
                throw new ArgumentException(string.Format("Direction not recognized: {0}", turnAndBlocks.ElementAt(0)));

            if (turnAndBlocks.ElementAt(0) == 'L')
                this.Direction = Direction.Left;

            if (turnAndBlocks.ElementAt(0) == 'R')
                this.Direction = Direction.Right;

            this.Blocks = Int32.Parse(turnAndBlocks.Substring(1, turnAndBlocks.Length - 1));
        }

        public Direction Direction { get; set; }

        public int Blocks { get; set; }
    }

    public enum Direction { Left, Right };
}
