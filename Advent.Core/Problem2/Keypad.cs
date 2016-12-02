using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem2
{
    public class Keypad
    {
        private Key[] keys;
        private Key currentKey;

        public Keypad(string startingLabel, params Key[] keys)
        {
            this.keys = keys;
            Reset(startingLabel);
        }

        public void Reset(string label)
        {
            if (!keys.Any(k => k.Label.Equals(label)))
            {
                throw new ArgumentException(string.Format("No key with label {0} is in keypad", label));
            }

            this.currentKey = keys.Single(k => k.Label.Equals(label));
        }

        public string Current()
        {
            return this.currentKey.Label;
        }


        public Key[] Keys { get { return this.keys;  } }

        public Keypad Left()
        {
            this.currentKey = this.currentKey.Left;
            return this;
        }

        public Keypad Right()
        {
            this.currentKey = this.currentKey.Right;
            return this;
        }

        public Keypad Up()
        {
            this.currentKey = this.currentKey.Up;
            return this;
        }

        public Keypad Down()
        {
            this.currentKey = this.currentKey.Down;
            return this;
        }        
    }   
}
