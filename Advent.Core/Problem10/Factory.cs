using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem10
{   
    public class Factory : IInstructable
    {
        private IDictionary<int, List<int>> bots = new Dictionary<int, List<int>>();
        private IDictionary<int, List<int>> outputBins = new Dictionary<int, List<int>>();


        public IEnumerable<int> Bots { get { return this.bots.Keys;  } }

        public IEnumerable<int> BotValues(int botLabel)
        {
            return this.bots[botLabel];
        }

        public IEnumerable<int> OutputBins { get { return this.outputBins.Keys; } }

        public IEnumerable<int> OutputBinValues(int binLabel)
        {
            return this.outputBins[binLabel];
        }


        public void LoadBot(LoadBotInstruction instruction)
        {
            CreateBotIfNotPresent(instruction.BotLabel);

            bots[instruction.BotLabel].Add(instruction.Value);
        }
      

        public void MoveChips(MoveChipInstruction instruction)
        {
            if (!this.bots.ContainsKey(instruction.Bot))
            {
                bool b = true;

                throw new InvalidOperationException(string.Format("Bot {0} doesn't exist", instruction.Bot));
            }


            var values = this.bots[instruction.Bot];

            if (values.Count != 2)
                throw new InvalidOperationException(string.Format("Cannot execute MoveChipsInstruction with only 2 microchips on bot: {0}"));

            instruction.Monitor(instruction.Bot, values);

            SendValue(instruction.Lower, instruction.SendLowerTo, values.Min());
            SendValue(instruction.Higher, instruction.SendHigherTo, values.Max());

            this.bots[instruction.Bot].Clear();
        }

        private void SendValue(MoveChipType outputType, int label, int value)
        {
            if (outputType == MoveChipType.Bot)
            {
                SendToBot(label, value);
            }
            else if (outputType == MoveChipType.Output)
            {
                AddToOutputBin(label, value);
            }
        }

        private void AddToOutputBin(int binLabel, int value)
        {
            if (!this.outputBins.ContainsKey(binLabel))
            {
                this.outputBins.Add(binLabel, new List<int>());
            }

            this.outputBins[binLabel].Add(value);
        }

        private void SendToBot(int botLabel, int value)
        {
            CreateBotIfNotPresent(botLabel);
            this.bots[botLabel].Add(value);
        }

        private void CreateBotIfNotPresent(int botLabel)
        {
            if (!bots.ContainsKey(botLabel))
            {
                bots.Add(botLabel, new List<int>());
            }
        }

        public IEnumerable<int> BotLabels { get { return this.bots.Keys; } }
    }
}


