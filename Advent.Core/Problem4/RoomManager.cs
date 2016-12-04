using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem4
{
    public class RoomManager
    {
        public int TotalSectorID(string file)
        {
            var names = RoomManager.FromFile(file);

            return TotalSectorID(names);
        }

        public static IEnumerable<RoomName> FromFile(string file)
        {
            var names = new List<RoomName>();

            using (var reader = new StreamReader("P4.txt"))
            {
                while (!reader.EndOfStream)
                {
                    names.Add(new RoomName(reader.ReadLine()));
                }
            }

            return names;
        }

        public int TotalSectorID(IEnumerable<RoomName> names)
        {
            var validNames = names.Where(a => a.IsReal());

            return validNames.Sum(a => a.SectorID);
        }
    }   
}
