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

        [Fact]
        public void ResetHands_ForcesPlayerToHaveSingleEmptyHand()
        {
            Player player = new Player();
            player.Hands[0] = new Hand(new List<Card>
            {
                new Card(1, Suit.Spades),
                new Card(8, Suit.Diamonds)
            });
            player.Hands.Add(new Hand(new List<Card>
            {
                new Card(1, Suit.Spades),
                new Card(8, Suit.Diamonds)
            }));

            player.ResetHands();

            Assert.Single(player.Hands);
            Assert.Empty(player.Hands[0]);
        }

        [Fact]
        public void ResetHands_ChangesHandResultToNone()
        {
            Player player = new Player();
            player.Hands[0].Result = HandResult.Blackjack;

            player.ResetHands();

            Assert.Equal(HandResult.None, player.Hands[0].Result);
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
    }
}
