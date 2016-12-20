using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem17
{
    public class LongestPathFinder : PathFinderBase
    {
        public LongestPathFinder(IEnumerable<Room> rooms)
            : base(rooms)
        {
        }

        public override Path Find(string passcode)
        {
            var starting = RoomAt(0, 0);
            var target = RoomAt(3, 3);

            var longestPath = FindPath(Path.New(starting, target), null, passcode);


            return longestPath;
        }

        private Path FindPath(Path path, Path longestPath, string passcode)
        {
            if (path.Status == PathStatus.Complete)
            {
                if (longestPath == null)
                    return path;

                return (path.MovesSoFar.Count() > longestPath.MovesSoFar.Count()) ? path : longestPath;
            }

            // If the path is Stuck, we're done. 
            if (path.Status == PathStatus.Stuck)
                return longestPath;

            // At this point, we're going to progress on our path.  We first compute the hash 
            // for the given passcode.  Then we find all the open doors from the current room
            // based on that hash.  Then, since any door that's open is equally likely, 
            // we traverse them both recursively.  
            var hash = Md5Hash.Compute(passcode).ToHexString().Substring(0, 4);
            var openDoors = OpenDoors(path.CurrentRoom, hash);

            var paths = new List<Path>();

            foreach (var door in openDoors)
            {
                var newPath = FindPath(path.Move(door), longestPath, passcode + door);
                if (newPath != null)
                    paths.Add(newPath);
            }

            var completed = paths.Where(p => p.Status == PathStatus.Complete);

            return completed.Any() ? completed.OrderByDescending(p => p.MovesSoFar.Count()).First() : longestPath;
        }
    }
}
