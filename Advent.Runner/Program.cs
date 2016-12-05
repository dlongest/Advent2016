using Advent.Core;
using Advent.Core.Problem1;
using Advent.Core.Problem2;
using Advent.Core.Problem3;
using Advent.Core.Problem4;
using Advent.Core.Problem5;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Problems.Problem5();

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }   


    public static class Problems
    {
        public static void Problem1()
        {
            var turns = TurnReader.FromFile("P1.txt").ToArray();

            var gps = new Gps();

            foreach (var turn in turns)
            {
                gps.Move(turn);
            }

            Console.WriteLine("How far away is first duplicate?  {0}", gps.HowFarIsFirstDuplicate());
        }

        public static void Problem2()
        {
            var keypad = new DiamondKeypadBuilder().Build("5");

            var finder = new BathroomCodeFinder();

            var code = finder.Code(keypad, () => InstructionReader.FromFile("P2.txt"));

            Console.WriteLine("Code = {0}", code);
        }

        public static void Problem3()
        {
            var count = new ColumnBasedTriangleEvaluator().HowManyValid("P3.txt");

            Console.WriteLine("Number Valid = {0}", count);
        }

        public static void Problem4()
        {
            var decryptor = new ShiftDecryptor();

            var rooms = RoomManager.FromFile("P4.txt");

            foreach (var room in rooms.Where(a => a.IsReal()))
            {
                var decryptedName = decryptor.Decrypt(room);
                
                if (decryptedName == "northpole object storage")
                    Console.WriteLine(string.Format("{0} ===> {1}", room.FullName, decryptedName));               
            }
        }

        public static void Problem5()
        {
            var finder = new PasswordFinder(new PasswordPrefixValidator("00000"), 
                                            new ResumableIntSequenceGenerator());

            var password = finder.Find("ugkcyxxp", 8);

            Console.WriteLine("Password = {0}", password);
        }
    }
}
