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
        public void Deal_ShouldDeliverAceOfSpades()
        {
            Deck deck = new Deck();
            Card card = deck.Deal();
            Assert.Equal("Ace of Spades", card.Name);
        }
    }
}