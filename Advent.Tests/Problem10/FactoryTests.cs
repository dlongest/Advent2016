using Advent.Core.Problem10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests.Problem10
{
    public class FactoryTests
    {
        [Fact]
        public void LoadBot_CreatesBotsWhenNeeded()
        {
            var sut = new Factory();

            sut.LoadBot(new LoadBotInstruction(1, 2));

            Assert.Equal(1, sut.BotLabels.Count());
            Assert.True(sut.BotLabels.Contains(1));
        }

        [Fact]
        public void MoveChips()
        {
            var sut = new Factory();

            sut.LoadBot(new LoadBotInstruction(2, 5));
            sut.LoadBot(new LoadBotInstruction(1, 3));
            sut.LoadBot(new LoadBotInstruction(2, 2));

            sut.MoveChips(new MoveChipInstruction
            {
                Bot = 2,
                Lower = MoveChipType.Bot,
                SendLowerTo = 1,
                Higher = MoveChipType.Bot,
                SendHigherTo = 0,
                Monitor = (label, vals) =>
                {
                    Assert.Equal(2, label);
                    Assert.True(vals.Contains(5));
                    Assert.True(vals.Contains(2));
                }
            });

            sut.MoveChips(new MoveChipInstruction
            {
                Bot = 1,
                Lower = MoveChipType.Output,
                SendLowerTo = 1,
                Higher = MoveChipType.Bot,
                SendHigherTo = 0,
                Monitor = (label, vals) =>
                {
                    Assert.Equal(1, label);
                    Assert.True(vals.Contains(3));
                    Assert.True(vals.Contains(2));
                }
            });

            sut.MoveChips(new MoveChipInstruction
            {
                Bot = 0,
                Lower = MoveChipType.Output,
                SendLowerTo = 2,
                Higher = MoveChipType.Output,
                SendHigherTo = 0,
                Monitor = (label, vals) =>
                {
                    Assert.Equal(0, label);
                    Assert.True(vals.Contains(3));
                    Assert.True(vals.Contains(5));
                }
            });
        }
    }
}
