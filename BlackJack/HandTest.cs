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
        public void Total_AandA_Equals_12()
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
        public void TotalAandAandA_Equals_13()
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
        public void Total_AandAandAandA_Equals_14()
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
        public void Total_AandAand9_Equals_21()
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
        public void Total_AandAandJ_Equals_12()
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
        public void Total_AandK_Equals_12()
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
        public void Total_2And2_Equals_4()
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
        public void Total_TandQandA_Equals_21()
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

        [Fact]
        public void Display_Equals_CardDisplayValues()
        {
            Hand hand = new Hand(
                new List<Card>
                {
                    new Card(1, Suit.Spades, false),
                    new Card(2, Suit.Clubs, false),
                    new Card(3, Suit.Diamonds, false),
                    new Card(4, Suit.Hearts, false)
                });

            Assert.Equal("A♠2♣3♦4♥", hand.Display);
        }

        [Fact]
        public void TurnFaceUp_TurnsAllCardsInHandFaceUp()
        {
            Hand hand = new Hand(
                new List<Card>
                {
                    new Card(1, Suit.Spades, true),
                    new Card(10, Suit.Diamonds, false)
                });

            hand.TurnFaceUp();

            hand.ForEach(c => Assert.True(c.IsFaceUp));
        }

        [Fact]
        public void HandResult_DefaultIsNone()
        {
            Hand hand = new Hand();

            Assert.Equal(HandResult.None, hand.Result);
        }

        [Fact]
        public void GetDisplayAndTotal_ReturnsDisplayAndTotal()
        {
            Hand hand = new Hand(
                new List<Card>
                {
                    new Card(1, Suit.Spades, false),
                    new Card(10, Suit.Diamonds, false)
                });

            Assert.Equal("A♠T♦ = 21", hand.DisplayWithTotal());
        }

        [Fact]
        public void ResultDisplay_None_EqualsEmptyString()
        {
            Hand hand = new Hand();
            hand.Result = HandResult.None;

            Assert.Equal("", hand.ResultDisplay);
        }

        [Fact]
        public void ResultDisplay_Blackjack_EqualsBlackJack()
        {
            Hand hand = new Hand();
            hand.Result = HandResult.Blackjack;

            Assert.Equal("Blackjack", hand.ResultDisplay);

        }

        [Fact]
        public void ResultDisplay_Bust_EqualsBust()
        {
            Hand hand = new Hand();
            hand.Result = HandResult.Bust;

            Assert.Equal("Bust", hand.ResultDisplay);

        }

        [Fact]
        public void ResultDisplay_Push_EqualsPush()
        {
            Hand hand = new Hand();
            hand.Result = HandResult.Push;

            Assert.Equal("Push", hand.ResultDisplay);
        }

        [Fact]
        public void ResultDisplay_Win_EqualsWin()
        {
            Hand hand = new Hand();
            hand.Result = HandResult.Win;

            Assert.Equal("Win", hand.ResultDisplay);
        }

        [Fact]
        public void ResultDisplay_Loss_EqualsLoss()
        {
            Hand hand = new Hand();
            hand.Result = HandResult.Loss;

            Assert.Equal("Loss", hand.ResultDisplay);
        }
    }
}
