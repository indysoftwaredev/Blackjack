using Blackjack.App;
using System.ComponentModel.DataAnnotations;

namespace BlackJack.UnitTests
{
    public class DeckTest
    {
        [Fact]
        public void Tests_Are_Working()
        {
            Assert.True(true); 
        }

        [Fact]
        public void Deal_NewDeck_FirstCardShouldBeAceOfSpades()
        {
            Deck deck = new Deck();
            Card card = deck.Deal();
            Assert.Equal("Ace of Spades", card.Name);
        }

        [Fact]
        public void Deal_NewDeck_SecondCardShouldBeTwoOfSpades()
        {
            Deck deck = new Deck();
            deck.Deal();
            Card card = deck.Deal();
            Assert.Equal("Two of Spades", card.Name);
        }

        [Fact]
        public void Deal_NewDeck_52ndCardShouldBeKingOfDiamonds()
        {
            Deck deck = new Deck();

            for (int i = 0; i < 51; i++)
            {
                deck.Deal();
            }
            Card card = deck.Deal();
            Assert.Equal("King of Diamonds", card.Name);
        }
    }
}