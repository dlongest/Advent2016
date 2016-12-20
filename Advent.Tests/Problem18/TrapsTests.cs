using Advent.Core.Problem18;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem18
{
    public class TrapsTests
    {
        [Fact]
        public void Next_Creates_Correct_NextRow_FromInput()
        {
            var sut = new Traps();

            var next = sut.Next("..^^.");

            Assert.Equal(".^^^^", next);
        }

        [Fact]
        public void Next_CreatesCorrectRows_FromInitialInput()
        {
            var sut = new Traps();

            var rows = sut.Next("..^^.", 2);

            Assert.Equal(".^^^^", rows.First());
            Assert.Equal("^^..^", rows.Last());
        }

        [Fact]
        public void SafeSpots_CorrectlyCountsSafeSpotsInRow()
        {
            var sut = new Traps();

            var actual = sut.SafeSpots("..^^.");

            Assert.Equal(3, actual);
        }

        [Fact]
        public void SafeSpots_CorrectlyCountsSafeSpotsInAllRows()
        {
            var sut = new Traps();

            var rows = sut.Next("..^^.", 2, includefirstRow: true);

            var actual = sut.SafeSpots(rows);

            Assert.Equal(6, actual);
        }

        [Fact]
        public void Next_LongerExample_IsCorrect()
        {
            var expected = new string[] { ".^^.^.^^^^", "^^^...^..^", "^.^^.^.^^.", "..^^...^^^",
                ".^^^^.^^.^", "^^..^.^^..", "^^^^..^^^.", "^..^^^^.^^", ".^^^..^.^^", "^^.^^^..^^"  };

            var sut = new Traps();

            var actual = sut.Next(".^^.^.^^^^", 9, includefirstRow: true);

            Assert.Equal(expected, actual);          
        }

        [Fact]
        public void SafeSpots_LongerExample_IsCorrect()
        {
            var sut = new Traps();

            var rows = sut.Next(".^^.^.^^^^", 9, includefirstRow: true);

            var actual = sut.SafeSpots(rows);

            Assert.Equal(38, actual);
        }

        // This test runs correctly, but is very time-consuming.  And actually, the "test" is the
        // live test case to submit, not an actual test.
        //[Fact]
        public void ActualProblem()
        {
            var input = "^.^^^.^..^....^^....^^^^.^^.^...^^.^.^^.^^.^^..^.^...^.^..^.^^.^..^.....^^^.^.^^^..^^...^^^...^...^.";
            var sut = new Traps();

            var rows = sut.Next(input, 399999, includefirstRow: true);

            var safeSpots = sut.SafeSpots(rows);

        }
    }
}
