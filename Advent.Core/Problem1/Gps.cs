using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem1
{   
    public class Gps
    {           
        private int currentDirection = 0;           
        private IList<Coordinate> visited = new List<Coordinate>();
        private Coordinate currentPosition;

        public Gps(int direction = 0)
        {
            this.currentPosition = Coordinate.Origin;
            this.visited.Add(this.currentPosition);
        }

        public void Move(Turn turn)
        {
            this.currentDirection = WhatDirectionAfterTurn(turn);
            MoveInCurrentDirection(turn.Blocks);
        }

        private int WhatDirectionAfterTurn(Turn turn)
        {
            if (currentDirection == 0)
            {
                return (turn.Direction == Direction.Left) ? 270 : 90;
            }

            if (currentDirection == 180)
            {
                return (turn.Direction == Direction.Left) ? 90 : 270;
            }

            if (currentDirection == 90)
            {
                return (turn.Direction == Direction.Left) ? 0 : 180;
            }

            if (currentDirection == 270)
            {
                return (turn.Direction == Direction.Left) ? 180 : 0;
            }

            throw new ArgumentException("No idea which way to turn");
        }

        public Coordinate[] ComputeCoordinatePath(int distance)
        {
            switch (currentDirection)
            {
                case 0:
                    return this.currentPosition.MoveNorth(distance);
                case 90:
                    return this.currentPosition.MoveEast(distance);
                case 180:
                    return this.currentPosition.MoveSouth(distance);
                case 270:
                    return this.currentPosition.MoveWest(distance);
                default:
                    throw new InvalidOperationException("I can't figure out which direction to turn");
            }
        }

        private void MoveInCurrentDirection(int blocks)
        {
            var path = ComputeCoordinatePath(blocks);
            this.visited.AddRange(path);
            this.currentPosition = path.Last();
        }

        
        private int TaxicabDistanceToOrigin(Coordinate coordinate)
        {
            return Math.Abs(coordinate.X) + Math.Abs(coordinate.Y);
        }

        private int TaxicabDistanceToOrigin(Tuple<int, int> coordinate)
        {
            return Math.Abs(coordinate.Item1) + Math.Abs(coordinate.Item2);
        }
       
        public Coordinate[] CoordinatesMovedThrough()
        {
            return this.visited.ToArray();
        }        

        public int HowFar()
        {
            return TaxicabDistanceToOrigin(this.currentPosition);
        }

        public int HowFarIsFirstDuplicate()
        {
            var duplicates = FindDuplicatesInOrder();

            if (!duplicates.Any())
            {
                return 0;
            }

            return TaxicabDistanceToOrigin(duplicates.First());
        }

        public Coordinate[] FindDuplicatesInOrder()
        {
            var duplicates = new List<Coordinate>();

            for (int i = 0; i < this.visited.Count - 1; ++i)
            {
                for (int j = i + 1; j < this.visited.Count; ++j)
                {
                    if (this.visited[i].Equals(this.visited[j]))
                    {
                        duplicates.Add(this.visited[i]);
                    }
                }
            }

            return duplicates.ToArray();
        }
    }

    public static class ListExtensions
    {
        public static IList<T> AddRange<T>(this IList<T> collection, T[] ts)
        {
            foreach (var t in ts)
            {
                collection.Add(t);
            }

            return collection;
        }
    }    
}
