using Advent.Core.Problem4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem4
{
    public class RoomNameTests
    {
        [Fact]
        public void RoomName_ExtractsEncryptedName_From_FullName()
        {
            var input = "aaaaa-bbb-z-y-x-123[abxyz]";
            var expected = "aaaaa-bbb-z-y-x";

            var sut = new RoomName(input);

            Assert.Equal(expected, sut.EncryptedName);
        }

        [Theory]
        [InlineData("aaaaa-bbb-z-y-x-123[abxyz]", "abxyz")]
        [InlineData("a-b-c-d-e-f-g-h-987[abcde]", "abcde")]
        [InlineData("not-a-real-room-404[oarel]", "oarel")]
        [InlineData("totally-real-room-200[decoy]", "decoy")]
        public void RoomName_ExtractsChecksum_From_FullName(string fullName, string expected)
        {
            var sut = new RoomName(fullName);

            Assert.Equal(expected, sut.Checksum);
        }

        [Theory]
        [InlineData("aaaaa-bbb-z-y-x-123[abxyz]",123)]
        [InlineData("a-b-c-d-e-f-g-h-987[abcde]", 987)]
        [InlineData("not-a-real-room-404[oarel]", 404)]
        [InlineData("totally-real-room-200[decoy]", 200)]
        public void RoomName_ExtractsSectorID_From_FullName(string fullName, int expected)
        {
            var sut = new RoomName(fullName);

            Assert.Equal(expected, sut.SectorID);
        }

        [Theory]
        [InlineData("aaaaa-bbb-z-y-x-123[abxyz]", true)]
        [InlineData("a-b-c-d-e-f-g-h-987[abcde]", true)]
        [InlineData("not-a-real-room-404[oarel]", true)]
        [InlineData("totally-real-room-200[decoy]", false)]
        public void IsReal_VerifiesThatChecksum_IsCorrect_BasedOnEncryptedName(string fullName, bool expected)
        {
            var sut = new RoomName(fullName);

            Assert.Equal(expected, sut.IsReal());
        }
    }   
}
