using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent.Core.Problem12
{
    public class InitialState
    {
        public IDictionary<string, int> Registers { get; set; }

        public int Address { get; set; }

        private static InitialState state = new InitialState();

        public static InitialState Default {  get { return state; } }
    }
        

    public class InstructionProcessor
    {
        private readonly CPU cpu;
        private Regex copyValuePattern = new Regex(@"cpy \d+ [abcd]");
        private Regex copyRegisterPattern = new Regex(@"cpy [abcd] [abcd]");
        private Regex incrementPattern = new Regex(@"inc [abcd]");
        private Regex decrementPattern = new Regex(@"dec [abcd]");        
        private int initialAddress;

        public InstructionProcessor(CPU cpu)
        {
            this.cpu = cpu;
            this.initialAddress = 0;
        }

        public InstructionProcessor(CPU cpu, InitialState state)
        {
            this.cpu = cpu;

            if (state != InitialState.Default)
            {
                
                foreach (var kvp in state.Registers)
                {
                    this.cpu.CopyValueIntoRegister(kvp.Value, kvp.Key);
                }

                this.initialAddress = state.Address;

            }
        }

        public void Process(IEnumerable<string> instructions, Action<int, IDictionary<string, int>> afterExecute)
        {
            var addressableInstructions = Create(instructions).ToArray();

            int address = this.initialAddress;

            while (address < addressableInstructions.Count())
            {
                var instruction = addressableInstructions[address];

                if (IsJumpInstruction(instruction.Value))
                {
                    var nextInstruction = ProcessJumpInstruction(instruction);

                    afterExecute(address, GetCurrentRegisterValues());
                    address = nextInstruction;
                    
                    continue;
                }

                if (IsIncrementInstruction(instruction.Value))
                {
                    ProcessIncrementInstruction(instruction.Value);
                }
                else if (IsDecrementInstruction(instruction.Value))
                {
                    ProcessDecrementInstruction(instruction.Value);
                }
                else if (IsCopyRegisterInstruction(instruction.Value))
                {
                    ProcessCopyRegisterInstruction(instruction.Value);
                }
                else if (IsCopyValueInstruction(instruction.Value))
                {
                    ProcessCopyValueInstruction(instruction.Value);
                }
                else if (IsAddValueInstruction(instruction.Value))
                {
                    ProcessAddValueInstruction(instruction.Value);
                }

                afterExecute(address, GetCurrentRegisterValues());
                address++;
            }
        }

        private IDictionary<string, int> GetCurrentRegisterValues()
        {
            var registers = this.cpu.Registers.OrderBy(a => a);

            return registers.ToDictionary(r => r, r => this.cpu.Register(r));
        }

        private IEnumerable<Instruction> Create(IEnumerable<string> instructions)
        {
            var count = instructions.Count();

            return instructions.Zip(Enumerable.Range(0, count), (instruction, address) =>
                                    new Instruction
                                    {
                                        Address = address,
                                        Value = instruction
                                    });
        }

        private bool IsJumpInstruction(string instruction)
        {
            return instruction.StartsWith("jnz");
        }

        private int ProcessJumpInstruction(Instruction instruction)
        {
            var arguments = ParseBinaryCommand(instruction.Value);

            var argumentValue = arguments.Item2;

            // The value we need to conditional against can be a value (a number) or a register.  
            // No matter what, the subsequent logic is the same. 
            int valueToCheck = argumentValue.IsNumber() ? argumentValue.AsNumber() : this.cpu.Register(argumentValue);
            
            var addressOffset = Int32.Parse(arguments.Item3);

            return valueToCheck == 0 ? instruction.Address + 1 : instruction.Address + addressOffset;
        }

        private bool IsCopyValueInstruction(string instruction)
        {
            return this.copyValuePattern.IsMatch(instruction);
        }

        private bool IsCopyRegisterInstruction(string instruction)
        {
            return this.copyRegisterPattern.IsMatch(instruction);
        }

        private bool IsIncrementInstruction(string instruction)
        {
            return this.incrementPattern.IsMatch(instruction);
        }
        private bool IsDecrementInstruction(string instruction)
        {
            return this.decrementPattern.IsMatch(instruction);
        }

        private bool IsAddValueInstruction(string instruction)
        {
            return instruction.StartsWith("add");
        }

        private void ProcessIncrementInstruction(string instruction)
        {
            var arguments = ParseUnaryCommand(instruction);

            var register = arguments.Item2;

            this.cpu.Increment(register);
        }

        private void ProcessDecrementInstruction(string instruction)
        {
            var arguments = ParseUnaryCommand(instruction);

            var register = arguments.Item2;

            this.cpu.Decrement(register);
        }

        private void ProcessCopyValueInstruction(string instruction)
        {
            var arguments = ParseBinaryCommand(instruction);

            var value = Int32.Parse(arguments.Item2);
            var register = arguments.Item3;

            this.cpu.CopyValueIntoRegister(value, register);
        }

        private void ProcessCopyRegisterInstruction(string instruction)
        {
            var arguments = ParseBinaryCommand(instruction);

            var source = arguments.Item2;
            var target = arguments.Item3;

            var sourceValue = this.cpu.Register(source);

            this.cpu.CopyValueIntoRegister(sourceValue, target);
        }

        private void ProcessAddValueInstruction(string instruction)
        {
            var arguments = ParseBinaryCommand(instruction);

            var sourceRegister = arguments.Item2;
            var targetRegister = arguments.Item3;

            var sourceValue = this.cpu.Register(sourceRegister);
            
            this.cpu.AddValueToRegister(sourceValue, targetRegister);
        }


        private Tuple<string, string, string> ParseBinaryCommand(string instruction)
        {
            var tokens = instruction.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            return Tuple.Create(tokens[0], tokens[1], tokens[2]);
        }

        private Tuple<string, string> ParseUnaryCommand(string instruction)
        {
            var tokens = instruction.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            return Tuple.Create(tokens[0], tokens[1]);
        }

        private class Instruction
        {

            public int Address { get; set; }

            public string Value { get; set; }
        }
    }
}
