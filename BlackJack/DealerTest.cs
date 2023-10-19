using Blackjack.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.UnitTests
{
    public class DealerTest
    {
        [Fact]
        public void Tests_Are_Working()
        {
            Assert.True(true);
        }

        [Fact]
        public void Construct_HasEmptyHand()
        {
            Dealer dealer = new Dealer();

            Assert.Empty(dealer.Hand);
        }

        [Fact]
        public void ResetHand_SetsHandToEmpty()
        {
            Dealer dealer = new Dealer();
            dealer.Hand = new Hand(new List<Card>
            {
                new Card(9, Suit.Diamonds),
                new Card(8, Suit.Hearts)
            });

            dealer.ResetHand();
            Assert.Empty(dealer.Hand);
        }
    }
}
