using Advent.Core.Problem4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem4
{
    public class RoomManagerTests
    {
        [Fact]
        public void ComputesCorrectlyTotalSectorIDs_BasedOnOnlyValidNames()
        {
            int expected = 1514;
            var input = new[] { "aaaaa-bbb-z-y-x-123[abxyz]", "a-b-c-d-e-f-g-h-987[abcde]",
                                "not-a-real-room-404[oarel]", "totally-real-room-200[decoy]" }
                        .Select(a => new RoomName(a));

            var validator = new RoomManager();

            var actual = validator.TotalSectorID(input);

            Assert.Equal(expected, actual);
        }
    }
}
