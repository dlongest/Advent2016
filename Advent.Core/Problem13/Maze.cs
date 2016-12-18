using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem13
{
    public class Maze
    {
        private IDictionary<MazeCoordinate, MazeNode> graph;

        public Maze(IEnumerable<MazeNode> graph)
        {
            this.graph = graph.ToDictionary(m => m.Coordinate, m => m);
        }

        public IDictionary<MazeCoordinate, Path> StepsToAllSpaces(MazeCoordinate start)
        {
            var paths = new ConcurrentDictionary<MazeCoordinate, Path>();

            Parallel.ForEach(this.graph.Keys, node =>
            {
                var route = FindOptimalRoute(start, node);

                if (!paths.TryAdd(node, route))
                {
                    throw new InvalidOperationException("Bang");
                }
            });

            return paths.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public Path FindOptimalRoute(MazeCoordinate start, MazeCoordinate end)
        {
            var notVisited = new HashSet<MazeCoordinate>();
            var distanceToStart = new Dictionary<MazeCoordinate, int>();
            var previousInOptimalPath = new Dictionary<MazeCoordinate, MazeCoordinate>();

            foreach (var openSpace in this.graph.Keys)
            {
                distanceToStart[openSpace] = Int32.MaxValue;
                previousInOptimalPath[openSpace] = null;
                notVisited.Add(openSpace);
            }

            distanceToStart[start] = 0;

            while (notVisited.Any())
            {
                var u = SelectClosestUnvisited(notVisited, distanceToStart);

                notVisited.Remove(u);

                foreach (var neighbor in this.graph[u].Neighbors.Where(n => notVisited.Contains(n)))
                {
                    var a = (distanceToStart[u] == Int32.MaxValue) ? 1 : distanceToStart[u] + 1;
                    if (a < distanceToStart[neighbor])
                    {
                        distanceToStart[neighbor] = a;
                        previousInOptimalPath[neighbor] = u;
                    }
                }
            }

            var finalPath = FinalPath(start, end, previousInOptimalPath);

            return finalPath;
        }

        private MazeCoordinate SelectClosestUnvisited(HashSet<MazeCoordinate> unvisited,
                                                      IDictionary<MazeCoordinate, int> distances)
        {
            return unvisited.Select(a => new { Node = a, Distance = distances[a] })
                            .OrderBy(a => a.Distance)
                            .First().Node;
        }

        private Path FinalPath(MazeCoordinate start, 
                                                      MazeCoordinate target,
                                                      IDictionary<MazeCoordinate, MazeCoordinate> previous)
        {
            var final = new List<MazeCoordinate>();

            var u = target;

            if (!previous.ContainsKey(u))
                return Path.Empty;

            while (previous[u] != null)
            {
                if (final.Contains(u))
                {
                    throw new InvalidOperationException("How are we doing this?");
                }

                final.Insert(0, u);
                u = previous[u];
            }

            final.Insert(0, u);

            return Path.From(start, target, final);
        }

        private bool CoordinateNotReachable(MazeCoordinate start, MazeCoordinate end, IEnumerable<MazeCoordinate> route)
        {
            var reachable = route.Contains(start) && route.Contains(end);

            return reachable;
        }
        
        public IEnumerable<string[]> AsRowGrid(Func<bool, string> isOpenSpace)
        {
            var maxX = this.graph.Keys.Max(a => a.X);
            var maxY = this.graph.Keys.Max(a => a.Y);
            
            foreach (var y in Enumerable.Range(0, maxY + 1))
            {
                var xs = Enumerable.Range(0, maxX + 1);

                var inGraph = this.graph.Where(a => a.Key.Y == y);

                yield return xs.Select(x => isOpenSpace(inGraph.Any(m => m.Key.X == x))).ToArray();
            }
        }

        public string AsRow(int row, Func<bool, string> isOpenSpace)
        {
            var maxX = this.graph.Max(a => a.Key.X);

            var xs = Enumerable.Range(0, maxX + 1);

            var inGraph = this.graph.Where(a => a.Key.Y == row);

            return string.Join("", xs.Select(x => isOpenSpace(inGraph.Any(m => m.Key.X == x))));
        }

        public int ColumnCount { get; private set; }

        public int RowCount { get; private set; }


        public static Maze Create(int xs, int ys, int input)
        {
            var graph = new List<MazeNode>();

            foreach (var x in Enumerable.Range(0, xs))
            {
                foreach (var y in Enumerable.Range(0, ys))
                {
                    if (IsOpenSpace(x, y, input))
                        graph.Add(new MazeNode(x, y));
                }
            }

            foreach (var node in graph)
            {
                var neighbors = Neighbors(node.Coordinate, 0, xs - 1, 0, ys - 1, input);

                node.AddNeighbors(neighbors);
            }

            return new Maze(graph);
        }


        public IEnumerable<MazeCoordinate> OpenSpaces
        {
            get
            {
                return this.graph.Keys;
            }
        }


        private static IEnumerable<MazeCoordinate> Neighbors(MazeCoordinate current,           
                                                             int minX, int maxX, 
                                                             int minY, int maxY, 
                                                             int input)
        {
            var surrounding = new[]
            {
                new MazeCoordinate(current.X - 1, current.Y), new MazeCoordinate(current.X + 1, current.Y),
                new MazeCoordinate(current.X, current.Y - 1), new MazeCoordinate(current.X, current.Y + 1)
            };

            return surrounding.Where(s => IsOpenSpace(s.X, s.Y, input))
                              .Where(s => s.X >= minX && s.X <= maxX && s.Y >= minY && s.Y <= maxY);

        }

        public static bool IsOpenSpace(int x, int y, int input)
        {
            var z = x * x + 3 * x + 2 * x * y + y + y * y;
            z += input;

            var bitArray = new BitArray(new int[] { z });

            var howMany1s = bitArray.Cast<bool>().Count(b => b);

            return howMany1s % 2 == 0;
        }
    }
}
