using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem17
{
    public class Path
    {
        public static Path NoPathAvailable = new Path();

        private IEnumerable<Direction> moves;
       
        public static Path New(Room startingRoom, Room targetRoom)
        {
            return new Path(startingRoom, targetRoom);
        }

        private Path()
        {
            this.Status = PathStatus.Stuck;
            this.moves = Enumerable.Empty<Direction>();
        }

        private Path(Room startingRoom, Room targetRoom)
        {
            this.CurrentRoom = startingRoom;
            this.TargetRoom = targetRoom;
            this.Status = PathStatus.InProgress;
            this.moves = new List<Direction>();
        }

        public Path(Room current, Room targetRoom, IEnumerable<Direction> moves)
        {
            this.moves = moves;
            this.CurrentRoom = current;
            this.TargetRoom = targetRoom;

            if (this.CurrentRoom.Equals(this.TargetRoom))
            {
                this.Status = PathStatus.Complete;
            }
            else if (!this.CurrentRoom.DoorsAvailable.Any())
            {
                this.Status = PathStatus.Stuck;
            }
            else
            {
                this.Status = PathStatus.InProgress;
            }
        }

        public IEnumerable<Direction> AvailableMoves
        {
            get
            {
                if (this.CurrentRoom.Equals(this.TargetRoom))
                    return Enumerable.Empty<Direction>();

                return this.CurrentRoom.DoorsAvailable;
            }
        }

        public Path Move(Direction direction)
        {
            var updatedMoves = moves.Concat(new[] { direction });
            var newRoom = this.CurrentRoom.Move(direction);

            return new Path(newRoom, this.TargetRoom, updatedMoves);
        }

        public Room CurrentRoom { get; private set; }

        public Room TargetRoom { get; private set;  }

        public IEnumerable<Direction> MovesSoFar { get { return this.moves; } }
        
        public PathStatus Status { get; private set; }
    }

    public enum PathStatus
    {
        InProgress, Complete, Stuck
    };

    public static class PathExtensions
    {
        public static string AsString(this IEnumerable<Direction> directions)
        {
            return string.Join("", directions.Select(d => d.ToString()));
        }
    }
}
