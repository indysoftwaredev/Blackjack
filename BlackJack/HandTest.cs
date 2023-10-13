using Blackjack.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.UnitTests
{
    public class HandTest
    {
        [Fact]
        public void Tests_Are_Working()
        {
            Assert.False(false);
        }

        [Fact]
        public void HandOfAA_Equals_12()
        {
            Hand hand = new Hand(
                new List<Card>
                {
                    new Card(1, Suit.Spades),
                    new Card(1, Suit.Spades)
                });

            Assert.Equal(12, hand.Total);
        }

        [Fact]
        public void HandOfAAA_Equals_13()
        {
            Hand hand = new Hand(
                new List<Card>
                {
                    new Card(1, Suit.Spades),
                    new Card(1, Suit.Spades),
                    new Card(1, Suit.Spades)
                });

            Assert.Equal(13, hand.Total);
        }

        [Fact]
        public void HandOfAAAA_Equals_14()
        {
            Hand hand = new Hand(
                new List<Card>
                {
                    new Card(1, Suit.Spades),
                    new Card(1, Suit.Spades),
                    new Card(1, Suit.Spades),
                    new Card(1, Suit.Spades)
                });

            Assert.Equal(14, hand.Total);
        }

        [Fact]
        public void HandOfAA9_Equals_21()
        {
            Hand hand = new Hand(
                new List<Card>
                {
                    new Card(1, Suit.Spades),
                    new Card(1, Suit.Spades),
                    new Card(9, Suit.Spades)
                });

            Assert.Equal(21, hand.Total);
        }

        [Fact]
        public void HandOfAAJ_Equals_12()
        {
            Hand hand = new Hand(
                new List<Card>
                {
                    new Card(1, Suit.Spades),
                    new Card(1, Suit.Spades),
                    new Card(11, Suit.Spades)
                });

            Assert.Equal(12, hand.Total);
        }

        [Fact]
        public void HandOfAK_Equals_12()
        {
            Hand hand = new Hand(
                new List<Card>
                {
                    new Card(1, Suit.Spades),
                    new Card(13, Suit.Spades)
                });

            Assert.Equal(21, hand.Total);
        }

        [Fact]
        public void HandOf22_Equals_4()
        {
            Hand hand = new Hand(
                new List<Card>
                {
                    new Card(2, Suit.Spades),
                    new Card(2, Suit.Spades)
                });

            Assert.Equal(4, hand.Total);
        }

        [Fact]
        public void HandOfTQA_Equals_21()
        {
            Hand hand = new Hand(
                new List<Card>
                {
                    new Card(10, Suit.Spades),
                    new Card(12, Suit.Spades),
                    new Card(1, Suit.Spades)
                });

            Assert.Equal(21, hand.Total);
        }
    }
}
