using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem2
{
    public class Key
    {
        public Key(string label)
        {
            this.Label = label;
        }

        public string Label { get; private set; }

        public Key Up { get; set; }

        public Key Down { get; set; }

        public Key Left { get; set; }

        public Key Right { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var k = obj as Key;

            if (k == null)
                return false;

            return this.Label.Equals(k.Label);
        }

        public override int GetHashCode()
        {
            return this.Label.GetHashCode();
        }
    }
}
