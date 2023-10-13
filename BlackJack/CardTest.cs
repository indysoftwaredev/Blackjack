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
    }
}
