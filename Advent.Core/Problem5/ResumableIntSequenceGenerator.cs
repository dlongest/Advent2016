using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem5
{
    public class ResumableIntSequenceGenerator : ISequenceGenerator<int>
    {
        private int start;

        public ResumableIntSequenceGenerator(int start = 0)
        {
            this.start = start;
        }

        public IEnumerable<int> Generate()
        {
            while (true)
            {
                yield return this.start++;
            }
        }
    }
}
