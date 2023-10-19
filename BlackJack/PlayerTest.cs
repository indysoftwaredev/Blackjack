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
        public void Construct_HasEmptyListOfHands()
        {
            Player player = new Player();
            Assert.Equal(0, player.HandCount);
        }

        [Fact]
        public void ResetHands_ForcesPlayerToZeroHands()
        {
            Player player = new Player();
            player.Add(new Hand(new List<Card>
            {
                new Card(1, Suit.Spades),
                new Card(8, Suit.Diamonds)
            }));
            player.Add(new Hand(new List<Card>
            {
                new Card(1, Suit.Spades),
                new Card(8, Suit.Diamonds)
            }));

            player.ResetHands();

            Assert.Equal(0, player.HandCount);
        }

        [Fact]
        public void Construct_PlayerNumberIsZeroByDefault()
        {
            Player player = new Player();

            Assert.Equal(0, player.PlayerNumber);
        }

        [Fact]
        public void Construct_PlayerNumber_AssignsThroughConstructor()
        {
            Player player = new Player(1);

            Assert.Equal(1, player.PlayerNumber);
        }

        [Fact]
        public void Add_SingleHand_NumbersHandAccordingly()
        {
            Player player = new Player();
            player.Add(new Hand());

            Assert.Equal(1, player.GetHand(0).HandNumber);
        }
    }
}
