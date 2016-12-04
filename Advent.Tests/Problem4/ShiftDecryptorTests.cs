using Advent.Core.Problem4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem4
{
    public class ShiftDecryptorTests
    {
        [Fact]
        public void Decrypt_Produces_Correct_PlaintextOfName()
        {
            var expected = "very encrypted name";
            var input = new RoomName("qzmt-zixmtkozy-ivhz-343[zimtk]");

            var sut = new ShiftDecryptor();

            var actual = sut.Decrypt(input);

            Assert.Equal(expected, actual);
        }
    }
}
