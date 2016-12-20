using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem17
{
    public class ShortestPathFinder : PathFinderBase
    {
        public ShortestPathFinder(IEnumerable<Room> rooms)
            : base(rooms)
        {
        }


        public override Path Find(string passcode)
        {
            var starting = RoomAt(0, 0);
            var target = RoomAt(3, 3);

            var completedPaths = new HashSet<Path>();

            FindPath(Path.New(starting, target), completedPaths, passcode);

            var complete = completedPaths.Where(p => p.Status == PathStatus.Complete).ToArray();

            var inOrder = complete.OrderBy(p => p.MovesSoFar.Count());

            return inOrder.First();
        }

        private void FindPath(Path path, HashSet<Path> completedPaths, string passcode)
        {
            // If the path is complete, add it to completedPaths, but only if:
            // It's shorter than the completed paths if there are any, if we want the shortest path
            // It's longer than the completed paths if there are any, if we want the longest path
            if (path.Status == PathStatus.Complete)
            {
                completedPaths.Add(path);
                return;
            }

            // If the path is Stuck, we're done. 
            if (path.Status == PathStatus.Stuck)
                return;

            // If we want the shortest Path and the current path is already longer the shortest 
            // completed path, we don't need to continue.  
            if (completedPaths.Any() && IsCurrentPathLongerThanCompletedPaths(completedPaths, path))
            {
                return;
            }

            // At this point, we're going to progress on our path.  We first compute the hash 
            // for the given passcode.  Then we find all the open doors from the current room
            // based on that hash.  Then, since any door that's open is equally likely, 
            // we traverse them both recursively.  
            var hash = Md5Hash.Compute(passcode).ToHexString().Substring(0, 4);

            var openDoors = OpenDoors(path.CurrentRoom, hash);

            foreach (var door in openDoors)
            {
                FindPath(path.Move(door), completedPaths, passcode + door);
            }
        }

        private bool IsCurrentPathLongerThanCompletedPaths(HashSet<Path> completed, Path current)
        {
            var lengthOfCurrentPath = current.MovesSoFar.Count();

            var shortestCompletedPath = completed.Min(a => a.MovesSoFar.Count());

            return lengthOfCurrentPath >= shortestCompletedPath;
        }
    }
}
