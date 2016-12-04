using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem4
{
    public class RoomName
    {
        private Regex sectorIDPattern = new Regex(@"[0-9]*");
        private Regex checksumPattern = new Regex(@"[a-z]+");

        public RoomName(string fullName)
        {
            this.FullName = fullName;

            var dashAfterNameIndex = fullName.LastIndexOf('-');

            this.EncryptedName = fullName.Substring(0, dashAfterNameIndex);

            var sectorAndChecksum = fullName.Substring(dashAfterNameIndex + 1, fullName.Length - dashAfterNameIndex - 1);

            var checksumMatches = this.checksumPattern.Match(sectorAndChecksum);
            var sectorIDMatches = this.sectorIDPattern.Match(sectorAndChecksum);

            this.SectorID = Int32.Parse(sectorIDMatches.Value);
            this.Checksum = checksumMatches.Value;
        }

        public string FullName { get; private set;  }

        public string EncryptedName { get; private set; }

        public string Checksum { get; private set; }

        public int SectorID { get; private set; }

        public bool IsReal()
        {
            var characterCounts = this.EncryptedName
                                      .Where(c => c != '-')
                                      .GroupBy(c => c)
                                      .Select(g => new { Letter = g.Key, Count = g.Count() })
                                      .OrderByDescending(a => a.Count)
                                      .ThenBy(a => a.Letter);

            var expectedChecksum = new string(characterCounts.Take(5).Select(c => c.Letter).ToArray());

            return this.Checksum.Equals(expectedChecksum);
        }
    }
}
