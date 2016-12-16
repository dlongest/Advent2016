using Advent.Core;
using Advent.Core.Problem1;
using Advent.Core.Problem10;
using Advent.Core.Problem12;
using Advent.Core.Problem14;
using Advent.Core.Problem15;
using Advent.Core.Problem16;
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
            Problems.Problem16();

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

        public static void Problem12()
        {
            int a = 1;
            int b = 1;
            int d = 26;
            int c = 1;

            c = 7;
            while (c != 0)
            {
                ++d;
                --c;
            }

            while (d != 0)
            {
                c = a;

                while (b != 0)
                {
                    ++a;
                    --b;
                }
                b = c;
                --d;

            } 

            c = 19;
            
            while (c != 0)
            {
                d = 14;
                while (d != 0)
                {
                    a++;

                    d--;

                }
                --c;
            }


            Console.WriteLine("\nFinal Register State\n------------------------------------");
           
              Console.WriteLine("Register {0} == {1}", "a", a);
            Console.WriteLine("Register {0} == {1}", "b", b);
            Console.WriteLine("Register {0} == {1}", "c", c);
            Console.WriteLine("Register {0} == {1}", "d", d);



            //var instructions = new[]
            //{
            //    "cpy 41 a", "inc a", "inc a", "dec a", "jnz a 2", "dec a"
            //};

            //var realInstructions = FileStringReader.Read("P12_2.txt");

            //var cpu = new CPU();

            //var preservedPart2State = new InitialState
            //{
            //    Address = 11,
            //    Registers = new Dictionary<string, int>()
            //     {
            //         { "a", 566471  }, { "b", 265570 }, { "c", 514229 }, { "d", 6 }
            //     }
            //};

            //var processor = new InstructionProcessor(cpu); //, preservedPart2State);

            //Console.WriteLine("ADDR:\ta\tb\tc\td");
            //Console.WriteLine("---------------------------------");

            //int i = 0;

            //processor.Process(realInstructions,
            //    (address, dict) =>
            //    {
            //        //if (++i == 10000)
            //        if (true)
            //        {
            //            dict.PrintDictionary(address);
            //            i = 0;                       
            //        }
            //    });

            //Console.WriteLine("\nFinal Register State\n------------------------------------");
            //foreach (var register in cpu.Registers)
            //{
            //    Console.WriteLine("Register {0} == {1}", register, cpu.Register(register));
            //}

        }

        public static void Problem14()
        {
            var salt = "ahsbgdzn";

            var keys = new StretchedKeyFinder(2016).Find(salt);

            foreach (var index in Enumerable.Range(0, keys.Count()))
            {
                Console.WriteLine("key[{0}] = {1}", index, keys[index]);
            }
            
        }

        public static void Problem15()
        {
            var discs = DiscFactory.CreatePart2();

            var optimizer = new TimeOptimizer(discs.ToArray());

            var time = optimizer.FindOptimalTime();

            Console.WriteLine("Optimal Time = {0}", time);
        }

        public static void Problem16()
        {
            var filler = new DiskFiller();

            var checksum = filler.Checksum("10001110011110000", 35651584);

            Console.WriteLine("Checksum = {0}", checksum);
        }
    }

    public static class ArrayPrintExtensions
    {
        public static string Format(this IEnumerable<int> values)
        {
            var s = string.Join("", values);

            return s;
        }

        public static void PrintDictionary(this IDictionary<string, int> dict, int address)
        {
            var dictValues = string.Join("\t", dict.Keys.OrderBy(a => a).Select(a => dict[a]));

            var output = string.Format("{0}\t{1}", address, dictValues);

            Console.WriteLine(output);
        }
    }
}
