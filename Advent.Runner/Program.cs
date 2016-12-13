using Advent.Core;
using Advent.Core.Problem1;
using Advent.Core.Problem10;
using Advent.Core.Problem2;
using Advent.Core.Problem3;
using Advent.Core.Problem4;
using Advent.Core.Problem5;
using Advent.Core.Problem6;
using Advent.Core.Problem7;
using Advent.Core.Problem8;
using Advent.Core.Problem9;
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
            Problems.Problem10();

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

        public static void Problem6()
        {
            var s = LetterFrequency.StringifyLeastFrequent(() => FileStringReader.Read("P6.txt"));

            Console.WriteLine("Frequented string = {0}", s);
        }

        public static void Problem7()
        {
            var supportsSSL = FileStringReader.Read("P7.txt")
                                              .Select(a => new IPv7(a))
                                              .Where(a => a.SupportsSsl)
                                              .Count();

            Console.WriteLine("How Many Support SSL?  {0}", supportsSSL);
        }

        public static void Problem8()
        {
            var screen = new Screen();

            var commands = new PixelViewerCommandFactory().Create(() => FileStringReader.Read("P8.txt"));

            commands.Process(screen);

            var display = new RowBasedPixelViewerDisplay(screen, s => Console.WriteLine(s));

            display.Refresh();

            var on = screen.Rows.Select(a => a.Count(b => b.IsOn)).Sum();

            Console.WriteLine("\nNumber On = {0}", on);
        }

        public static void Problem9()
        {
            var strings = FileStringReader.Read("P9.txt");

            var d = new StringDecompressorV2();

            var lengths = strings.Select(a => d.Decompress(a));

            lengths.ToList().ForEach(s => Console.WriteLine("Length = {0}", s));
        }

        public static void Problem10()
        {
            //var instructions = new[]
            //{
            //    "value 5 goes to bot 2",
            //    "bot 2 gives low to bot 1 and high to bot 0",
            //    "value 3 goes to bot 1",
            //    "bot 1 gives low to output 1 and high to bot 0",
            //    "bot 0 gives low to output 2 and high to output 0",
            //    "value 2 goes to bot 2"
            //};

            var instructions = FileStringReader.Read("P10.txt");

            var factory = new Factory();

            var executors = new IInstructionExecutor[]
            {
                new LoadBotInstructionExecutor(),
                new MoveChipsInstructionExecutor(
                    (label, values) =>
                    {
                        if (values.Contains(61) && values.Contains(17))
                        {
                            Console.WriteLine("Bot {0} is comparing values {1} and {2}", label, values.First(), values.Last());
                        }
                    })
            };

            new Foreman().Execute(executors.First(), executors.Last(), factory, instructions);

            //foreach (var bot in factory.Bots)
            //{
            //    Console.WriteLine("Bot {0} contains values [{1}]", bot, factory.BotValues(bot).Format());
            //}

            foreach (var bin in factory.OutputBins.OrderBy(a => a))
            {
                Console.WriteLine("Bin {0} contains values [{1}]", bin, factory.OutputBinValues(bin).Format());
            }
        }
    }

    public static class ArrayPrintExtensions
    {
        public static string Format(this IEnumerable<int> values)
        {
            var s = string.Join("", values);

            return s;
        }
    }
}
