using Blackjack.App;
using Blackjack.App.Factories;
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

        [Fact]
        public void NewDeck_AllCardsShouldBeInPosition()
        {
            Deck deck = new Deck();
            Assert.Equal("Ace of Spades", deck.GetCardAt(0).Name);
            Assert.Equal("Two of Spades", deck.GetCardAt(1).Name);
            Assert.Equal("Three of Spades", deck.GetCardAt(2).Name);
            Assert.Equal("Four of Spades", deck.GetCardAt(3).Name);
            Assert.Equal("Five of Spades", deck.GetCardAt(4).Name);
            Assert.Equal("Six of Spades", deck.GetCardAt(5).Name);
            Assert.Equal("Seven of Spades", deck.GetCardAt(6).Name);
            Assert.Equal("Eight of Spades", deck.GetCardAt(7).Name);
            Assert.Equal("Nine of Spades", deck.GetCardAt(8).Name);
            Assert.Equal("Ten of Spades", deck.GetCardAt(9).Name);
            Assert.Equal("Jack of Spades", deck.GetCardAt(10).Name);
            Assert.Equal("Queen of Spades", deck.GetCardAt(11).Name);
            Assert.Equal("King of Spades", deck.GetCardAt(12).Name);
            Assert.Equal("Ace of Hearts", deck.GetCardAt(13).Name);
            Assert.Equal("Two of Hearts", deck.GetCardAt(14).Name);
            Assert.Equal("Three of Hearts", deck.GetCardAt(15).Name);
            Assert.Equal("Four of Hearts", deck.GetCardAt(16).Name);
            Assert.Equal("Five of Hearts", deck.GetCardAt(17).Name);
            Assert.Equal("Six of Hearts", deck.GetCardAt(18).Name);
            Assert.Equal("Seven of Hearts", deck.GetCardAt(19).Name);
            Assert.Equal("Eight of Hearts", deck.GetCardAt(20).Name);
            Assert.Equal("Nine of Hearts", deck.GetCardAt(21).Name);
            Assert.Equal("Ten of Hearts", deck.GetCardAt(22).Name);
            Assert.Equal("Jack of Hearts", deck.GetCardAt(23).Name);
            Assert.Equal("Queen of Hearts", deck.GetCardAt(24).Name);
            Assert.Equal("King of Hearts", deck.GetCardAt(25).Name);
            Assert.Equal("Ace of Clubs", deck.GetCardAt(26).Name);
            Assert.Equal("Two of Clubs", deck.GetCardAt(27).Name);
            Assert.Equal("Three of Clubs", deck.GetCardAt(28).Name);
            Assert.Equal("Four of Clubs", deck.GetCardAt(29).Name);
            Assert.Equal("Five of Clubs", deck.GetCardAt(30).Name);
            Assert.Equal("Six of Clubs", deck.GetCardAt(31).Name);
            Assert.Equal("Seven of Clubs", deck.GetCardAt(32).Name);
            Assert.Equal("Eight of Clubs", deck.GetCardAt(33).Name);
            Assert.Equal("Nine of Clubs", deck.GetCardAt(34).Name);
            Assert.Equal("Ten of Clubs", deck.GetCardAt(35).Name);
            Assert.Equal("Jack of Clubs", deck.GetCardAt(36).Name);
            Assert.Equal("Queen of Clubs", deck.GetCardAt(37).Name);
            Assert.Equal("King of Clubs", deck.GetCardAt(38).Name);
            Assert.Equal("Ace of Diamonds", deck.GetCardAt(39).Name);
            Assert.Equal("Two of Diamonds", deck.GetCardAt(40).Name);
            Assert.Equal("Three of Diamonds", deck.GetCardAt(41).Name);
            Assert.Equal("Four of Diamonds", deck.GetCardAt(42).Name);
            Assert.Equal("Five of Diamonds", deck.GetCardAt(43).Name);
            Assert.Equal("Six of Diamonds", deck.GetCardAt(44).Name);
            Assert.Equal("Seven of Diamonds", deck.GetCardAt(45).Name);
            Assert.Equal("Eight of Diamonds", deck.GetCardAt(46).Name);
            Assert.Equal("Nine of Diamonds", deck.GetCardAt(47).Name);
            Assert.Equal("Ten of Diamonds", deck.GetCardAt(48).Name);
            Assert.Equal("Jack of Diamonds", deck.GetCardAt(49).Name);
            Assert.Equal("Queen of Diamonds", deck.GetCardAt(50).Name);
            Assert.Equal("King of Diamonds", deck.GetCardAt(51).Name);
        }

        [Fact]
        public void NewDeck_Shuffle_MostCardsShouldNotBeInPosition()
        {
            Deck deck = new Deck();

            //get list of unshuffled
            List<string> unshuffledCardNames = new List<string>();
            for (int i = 0; i < 52; i++)
            {
                unshuffledCardNames.Add(deck.GetCardAt(i).Name);
            }

            deck.Shuffle();

            List<string> shuffledCardNames = new List<String>();
            for (int i = 0; i < 52; i++)
            {
                shuffledCardNames.Add(deck.GetCardAt(i).Name);
            }

            int postShuffleDifferenceCount = 0;
            for (int i = 0; i < 52; i++)
            {
                if (!unshuffledCardNames[i].Equals(shuffledCardNames[i]))
                {
                    postShuffleDifferenceCount++;
                }
            }
            
            //Ran 1 million times without failure, 
            //though it is technically still possible.
            Assert.True(postShuffleDifferenceCount > 39, 
                "Random sequencde test, chance of failure is estimated < .0001% - rerun test.");

        }

        [Fact]
        public void Deal_NewDeck_CardIsFaceDown()
        {
            Deck deck = new Deck();
            Card card = deck.Deal();

            Assert.True(card.IsFaceDown);
        }

        [Fact]
        public void DealFaceDown_NewDeck_CardIsFaceUp()
        {
            Deck deck = new Deck();
            Card card = deck.DealFaceUp();

            Assert.False(card.IsFaceDown);
        }

        [Fact]
        public void Collect_Hand_AppendsHandToBackOfDeck()
        {
            Deck deck = new Deck();
            Hand hand = new Hand();

            Card card = deck.Deal();

            hand.Add(card);

            deck.Collect(hand);

            Assert.Equal(card, deck[deck.Count - 1]);
        }

        [Fact]
        public void Collect_Hand_ClearsHand()
        {
            Deck deck = new Deck();
            Hand hand = new Hand();
            Card card = deck.Deal();
            hand.Add(card);
            deck.Collect(hand);
            Assert.Empty(hand);
        }

        [Fact]
        public void Collect_DoesNotTurnFaceUpCardsDown()
        {

            Deck deck = new Deck();
            Hand hand = new Hand
            {
                deck.DealFaceUp(),
                deck.DealFaceUp()
            };

            deck.Collect(hand);

            Assert.True(deck[deck.Count - 1].IsFaceUp);
            Assert.True(deck[deck.Count - 2].IsFaceUp);
        }

        [Fact]
        public void Shuffle_TurnsAllCardsInDeckFaceDown()
        {
            Deck deck = new Deck();
            Hand hand = new Hand
            {
                deck.DealFaceUp(),
                deck.DealFaceUp()
            };

            deck.Collect(hand);
            deck.Shuffle();

            deck.ForEach(c => Assert.True(c.IsFaceDown));
        }

        [Fact]
        public void Deal_AllCards_WillBeFaceDownByDefault()
        {
            Deck deck = new Deck();
            
            List<Card> discards = new List<Card>();
            while(deck.Count > 0)
            {
                discards.Add(deck.Deal());
            }

            discards.ForEach(c => Assert.True(c.IsFaceDown));
        }

        [Fact]
        public void Deal_AllCardsButLastFaceUp_LastCardIsFaceDown()
        {
            /* Test and fix bug where if a card is dealt face up, then all
             * matching cards are also face up. 
             */

            Deck deck = DeckFactory.CreateDeck(2);
            while(deck.Count > 1)
            {
                deck.DealFaceUp();
            }

            Card card = deck.Deal();

            Assert.True(card.IsFaceDown);

        }

        
    }
}