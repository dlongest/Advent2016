using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem10
{
    public class LoadBotInstruction
    {
        private Action<int, IEnumerable<int>> monitor;

        public LoadBotInstruction(int botLabel, int value)
        {
            this.monitor = (label, values) => { };
            this.BotLabel = botLabel;
            this.Value = value;
        }

        public int BotLabel { get; set; }

        public int Value { get; set; }
        
        public Action<int, IEnumerable<int>> Monitor
        {
            get { return this.monitor; }
            set
            {
                if (value != null)
                {
                    this.monitor = value;
                }
            }
        }
    }
}
