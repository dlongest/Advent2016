using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem20
{
    public class IpRanges
    {
        private long maximumValue;
        private HashSet<Range> ranges = new HashSet<Range>();

        public IpRanges()
            :this(4294967295)
        {
        }

        public IpRanges(long maximum)
        {
            this.maximumValue = maximum;
        }

        public void Block(Range range)
        {
            var overlappingRanges = ranges.Where(a => a.Overlaps(range));
           
            if (!overlappingRanges.Any())
            {
                AddNewRange(range);
                
            }
            else
            {
                AdjustOverlappingRanges(range, overlappingRanges);
            }           
        }
        
        private void AddNewRange(Range newRange)
        {
            ranges.Add(newRange);
        }

        private void AdjustOverlappingRanges(Range newRange, IEnumerable<Range> overlapping)
        {
            if (overlapping.Count() == 1)
            {
                var singleOverlap = overlapping.First();

                var updatedRange = singleOverlap.Merge(newRange);

                ranges.Remove(singleOverlap);
                ranges.Add(updatedRange);
            }
            else
            {
                var updatedRange = newRange.Merge(overlapping);

                ranges.RemoveAll(overlapping);
                ranges.Add(updatedRange);
            }
        }

       

        public Range Lowest()
        {
            var sortedRanges = this.ranges.OrderBy(a => a.Lower).ToArray();

            if (!sortedRanges.Any())
            {
                return new Range(0, this.maximumValue);
            }

            for (int i = 0; i< sortedRanges.Count()- 1; ++i)
            {
                var rangeDifference = sortedRanges[i + 1].Lower - sortedRanges[i].Upper;

                if (rangeDifference > 1)
                {
                    var lower = sortedRanges[i].Upper + 1;
                    var upper = sortedRanges[i + 1].Lower - 1;

                    return new Range(lower, upper);
                }
            }

            return new Range(sortedRanges.Last().Upper + 1, this.maximumValue);            
        }

        public IEnumerable<Range> AvailableRanges()
        {
            var ranges = new List<Range>();
            var sortedRanges = this.ranges.OrderBy(a => a.Lower).ToArray();

            if (!sortedRanges.Any())
            {
                return new[] { new Range(0, this.maximumValue) };
            }

            if (sortedRanges.First().Lower > 0)
            {
                ranges.Add(new Range(0, sortedRanges.First().Lower - 1));
            }

            for (int i = 0; i < sortedRanges.Count() - 1; ++i)
            {
                var rangeDifference = sortedRanges[i + 1].Lower - sortedRanges[i].Upper;

                if (rangeDifference > 1)
                {
                    var lower = sortedRanges[i].Upper + 1;
                    var upper = sortedRanges[i + 1].Lower - 1;

                    ranges.Add(new Range(lower, upper));
                }
            }

            if (sortedRanges.Last().Upper == this.maximumValue)
            {
                ranges.Add(sortedRanges.Last());
            }
            else 
            {
                var lower = sortedRanges.Last().Upper + 1;
                ranges.Add(new Range(lower, this.maximumValue));
            }           

            return ranges;
        }

        public long HowManyBlocked
        {
            get
            {
                return this.ranges.Sum(r => r.HowManyInRange);
            }
        }

        public long HowManyAvailable
        {
            get
            {
                var totalAvailable = this.AvailableRanges().Sum(r => r.HowManyInRange);

                return totalAvailable;
            }
        }
    }

    public static class RangeExtensions
    {
        public static bool None(this IEnumerable<Range> ranges, int value)
        {
            return !ranges.Any(r => r.Includes(value));
        }

        public static void RemoveAll<T>(this HashSet<T> set, IEnumerable<T> values)
        {
            // Have to force enumeration here or else you'll get exception about
            // source collection being modified during enumeration. 
            foreach (var value in values.ToList())
            {
                set.Remove(value);
            }
        }
            
    }
}
