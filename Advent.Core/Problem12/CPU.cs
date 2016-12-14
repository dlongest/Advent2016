using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem12
{
    public class CPU
    {

        private IDictionary<string, int> registers = new Dictionary<string, int>()
        { { "a", 0 }, { "b", 0 }, { "c", 1 }, { "d", 0 } };

        public IEnumerable<string> Registers {  get { return this.registers.Keys;  } }

        public int Register(string label)
        {
            return this.registers[label];
        }

        public void Increment(string label)
        {
            this.registers[label]++;
        }
        public void Decrement(string label)
        {
            this.registers[label]--;
        }

        public void CopyValueIntoRegister(int value, string register)
        {
            this.registers[register] = value;
        }
    }
        

    internal static class SomeExtensions
    {
        internal static bool IsNumber(this string s)
        {
            try
            {
                Int32.Parse(s);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static int AsNumber(this string s)
        {
            try
            {
                return Int32.Parse(s);                
            }
            catch (Exception)
            {
                throw new ArgumentException(string.Format("String could not be parsed into an integer: {0}", s));
            }
        }
    }
}
