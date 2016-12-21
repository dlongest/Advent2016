using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem20
{
    public class Range
    {
        public static Range From(string range)
        {
            var tokens = range.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

            var lower = Int64.Parse(tokens[0]);
            var upper = Int64.Parse(tokens[1]);

            return new Range(lower, upper);
        }

        public Range(long lower, long upper)
        {
            if (lower > upper)
                throw new ArgumentException("lower must be less than upper");

            this.Lower = lower;
            this.Upper = upper;
        }

        public long Lower { get; private set; }

        public long Upper { get; private set; }

        public bool Overlaps(Range range)
        {
            if (this.Lower > range.Upper + 1 || range.Lower > this.Upper + 1)            
                return false;

            return true;
        }

        public bool Includes(long value)
        {
            return this.Lower <= value && value <= this.Upper;
        }

        public long HowManyInRange
        {
            get
            {
                return this.Upper - this.Lower + 1;
            }
        }

        public Range Merge(Range overlapping)
        {
            if (!this.Overlaps(overlapping))
                throw new ArgumentException("Provided range must overlap the current range in order to be merged");

            var minLower = Math.Min(this.Lower, overlapping.Lower);
            var maxUpper = Math.Max(this.Upper, overlapping.Upper);

            return new Range(minLower, maxUpper);
        }

        public Range Merge(IEnumerable<Range> overlapping)
        {
            var all = overlapping.Concat(new[] { this });

            var min = all.Min(a => a.Lower);
            var max = all.Max(a => a.Upper);

            return new Range(min, max);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var r = obj as Range;

            if (r == null) return false;

            return r.Lower == this.Lower && r.Upper == this.Upper;
        }

        public override int GetHashCode()
        {
            return this.Lower.GetHashCode() + 3 * this.Upper.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", this.Lower, this.Upper);
        }
    }
}
