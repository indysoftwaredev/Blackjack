using Blackjack.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.UnitTests
{
    public class PlayerTest
    {
        [Fact]
        public void Tests_Are_Working()
        {
            Assert.True(true);
        }

        [Fact]
        public void Construct_HasOneEmptyHandInListOfHands()
        {
            Player player = new Player();
            Assert.Single(player.Hands);
        }
    }
}
