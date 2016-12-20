using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem17
{
    public class Room
    {
        private IDictionary<Direction, Room> moves = new Dictionary<Direction, Room>();

        public Room(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Room Move(Direction direction)
        {
            if (!this.moves.ContainsKey(direction))
                throw new ArgumentException(string.Format("Cannot move {0} from this Room", direction));

            return this.moves[direction];
        }

        public void SetRoomForMove(Direction direction, Room room)
        {
            this.moves[direction] = room;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public IEnumerable<Direction> DoorsAvailable { get { return this.moves.Keys; } }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var r = obj as Room;

            if (r == null) return false;

            return r.X == this.X && r.Y == this.Y;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() + 3 * this.Y.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", this.X, this.Y);
        }
    } 


    public static class Md5Hash
    {
        public static byte[] Compute(string data)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var bytes = System.Text.Encoding.Default.GetBytes(data);

                return md5.ComputeHash(bytes);
            }
        }

        public static T Compute<T>(string data, Func<byte[], T> output)
        {
            var bytes = Md5Hash.Compute(data);

            return output(bytes);
        }

        public static string ToHexString(this byte[] bytes)
        {
            var hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                hex.AppendFormat("{0:x2}", b);

            return hex.ToString();
        }
    }
}