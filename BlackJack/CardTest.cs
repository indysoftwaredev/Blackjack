using Blackjack.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.UnitTests
{
    public class CardTest
    {
        [Fact]
        public void Tests_Are_Working()
        {
            Assert.True(true);
        }

        [Fact]
        public void ValueOfAce_IsEqualToEleven()
        {
            Card card = new Card(1, Suit.Spades);
            Assert.Equal(11, card.Value);
        }

        [Fact]
        public void ValueOfKing_IsEqualToTen()
        {
            Card card = new Card(13, Suit.Spades);
            Assert.Equal(10, card.Value);
        }

        [Fact]
        public void ValueOfQueen_IsEqualToTen()
        {
            Card card = new Card(12, Suit.Spades);
            Assert.Equal(10, card.Value);
        }

        [Fact]
        public void ValueOfJack_IsEqualToTen()
        {
            Card card = new Card(11, Suit.Spades);
            Assert.Equal(10, card.Value);
        }

        [Fact]
        public void ValueOfNonAceNonFaceCards_AreEqualToTheirRank()
        {
            IEnumerable<int> ranks = Enumerable.Range(2, 9);
            foreach (int n in Enumerable.Range(2, 9))
            {
                Card card = new Card(n, Suit.Spades);
                Assert.Equal(n, card.Value);
            }
        }

        [Fact]
        public void Construct_CardRankGreaterThan13_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Card(14, Suit.Spades));
        }

        [Fact]
        public void Construct_CardRankLessThanOne_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Card(0, Suit.Spades));
        }

        [Fact]
        public void Construct_IsFaceDown_ByDefault()
        {
            Card card = new Card(1, Suit.Hearts);
            Assert.True(card.IsFaceDown);
        }

        [Fact]
        public void Display_AceOfSpades_DisplaysGraphicalRepresentationOfCard()
        {
            Card card = new Card(1, Suit.Spades);
            card.IsFaceDown = false;
            Assert.Equal("A♠", card.Display);
        }

        [Fact]
        public void Display_VariousCards_HaveTheCorrectDisplay()
        {
            Assert.Equal("A♥", new Card(1, Suit.Hearts, false).Display);
            Assert.Equal("2♦", new Card(2, Suit.Diamonds, false).Display);
            Assert.Equal("3♣", new Card(3, Suit.Clubs, false).Display);
            Assert.Equal("4♠", new Card(4, Suit.Spades, false).Display);
            Assert.Equal("T♥", new Card(10, Suit.Hearts, false).Display);
            Assert.Equal("J♦", new Card(11, Suit.Diamonds, false).Display);
            Assert.Equal("Q♣", new Card(12, Suit.Clubs, false).Display);
            Assert.Equal("K♠", new Card(13, Suit.Spades, false).Display);
        }

        [Fact]
        public void Display_FaceDownCards_DisplayCardBack()
        {
            Card card = new Card(1, Suit.Spades);
            card.IsFaceDown = true;

            Assert.Equal("🂠", card.Display);
        }
    }
}
