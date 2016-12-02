using Advent.Core.Problem2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem2
{
    public class KeypadBuilderTests
    {
        [Fact]
        public void OneKey_IsCorrect()
        {
            var keypad = new NineButtonSquareKeypadBuilder().Build("1");

            keypad.Reset("1");

            Assert.Equal("1", keypad.Left().Current());

            keypad.Reset("1");
            Assert.Equal("1", keypad.Up().Current());

            keypad.Reset("1");
            Assert.Equal("2", keypad.Right().Current());

            keypad.Reset("1");
            Assert.Equal("4", keypad.Down().Current());            
        }
    }
}
