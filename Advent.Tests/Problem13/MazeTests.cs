using Advent.Core.Problem13;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Collections;

namespace Advent.Tests.Problem13
{
    public class MazeTests
    {
        [Fact]
        public void Create_BuildsCorrectMaze()
        {
            var maze = Maze.Create(10, 7, 10);

            Assert.Equal(".#.####.##", maze.AsRow(0, b => b ? "." : "#"));
            Assert.Equal("..#..#...#", maze.AsRow(1, b => b ? "." : "#"));
            Assert.Equal("#....##...", maze.AsRow(2, b => b ? "." : "#"));
            Assert.Equal("###.#.###.", maze.AsRow(3, b => b ? "." : "#"));
            Assert.Equal(".##..#..#.", maze.AsRow(4, b => b ? "." : "#"));
            Assert.Equal("..##....#.", maze.AsRow(5, b => b ? "." : "#"));
            Assert.Equal("#...##.###", maze.AsRow(6, b => b ? "." : "#"));
        }

        [Fact]
        public void Create_BuildsNodes_WithCorrectNeighbors()
        {
            var maze = Maze.Create(10, 7, 10);

            Assert.Equal(37, maze.OpenSpaces.Count());
        }

        [Fact]
        public void FindOptimalRoute_PlotsRouteWithFewestSteps()
        {
            var maze = Maze.Create(10, 7, 10);

            var route = maze.FindOptimalRoute(new MazeCoordinate(1, 1), new MazeCoordinate(7, 4));

        }


        private class NeighborTestCases : IEnumerable<object[]>
        {
            private List<object[]> data = new List<object[]>();

            public NeighborTestCases()
            {
                this.data.Add(new object[] { new MazeCoordinate(0, 0), new[] { new MazeCoordinate(0, 1) } });
                this.data.Add(new object[] { new MazeCoordinate(2, 0), new MazeCoordinate[0] });
                this.data.Add(new object[] { new MazeCoordinate(7, 0), new[] { new MazeCoordinate(7, 1) } });
                this.data.Add(new object[] { new MazeCoordinate(0, 1), new[] { new MazeCoordinate(0, 0), new MazeCoordinate(1, 1) } });
                this.data.Add(new object[] { new MazeCoordinate(1, 1), new[] { new MazeCoordinate(0, 1), new MazeCoordinate(1, 2) } });
            }

            public IEnumerator<object[]> GetEnumerator()
            {
                return this.data.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }

//  0123456789
//0 .#.####.##
//1 ..#..#...#
//2 #....##...
//3 ###.#.###.
//4 .##..#..#.
//5 ..##....#.
//6 #...##.###
}
