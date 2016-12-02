using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem2
{
    public class DiamondKeypadBuilder : IKeypadBuilder
    {
        public Keypad Build(string startingLabel)
        {
            var key1 = new Key("1");
            var key2 = new Key("2");
            var key3 = new Key("3");
            var key4 = new Key("4");
            var key5 = new Key("5");
            var key6 = new Key("6");
            var key7 = new Key("7");
            var key8 = new Key("8");
            var key9 = new Key("9");
            var keyA = new Key("A");
            var keyB = new Key("B");
            var keyC = new Key("C");
            var keyD = new Key("D");

            key1.Left = key1;
            key1.Up = key1;
            key1.Right = key1;
            key1.Down = key3; 

            key2.Left = key2;
            key2.Up = key2;
            key2.Right = key3;
            key2.Down = key6;

            key3.Up = key1;
            key3.Left = key2;
            key3.Right = key4;
            key3.Down = key7;

            key4.Up = key4;
            key4.Right = key4;
            key4.Left = key3;
            key4.Down = key8;

            key5.Left = key5;
            key5.Up = key5;
            key5.Down = key5;
            key5.Right = key6;

            key6.Up = key2;
            key6.Left = key5;
            key6.Down = keyA;
            key6.Right = key7;

            key7.Up = key3;
            key7.Left = key6;
            key7.Right = key8;
            key7.Down = keyB;

            key8.Up = key4;
            key8.Left = key7;
            key8.Right = key9;
            key8.Down = keyC;

            key9.Up = key9;
            key9.Right = key9;
            key9.Down = key9;
            key9.Left = key8;

            keyA.Left = keyA;
            keyA.Up = key6;
            keyA.Down = keyA;
            keyA.Right = keyB;

            keyB.Left = keyA;
            keyB.Up = key7;
            keyB.Down = keyD;
            keyB.Right = keyC;

            keyC.Left = keyB;
            keyC.Up = key8;
            keyC.Right = keyC;
            keyC.Down = keyC;

            keyD.Left = keyD;
            keyD.Right = keyD;
            keyD.Down = keyD;
            keyD.Up = keyB;

            return new Keypad(startingLabel, key1, key2, key3, key4, key5, key6, key7, key8, key9, keyA, keyB, keyC, keyD);
        }
    }
}
