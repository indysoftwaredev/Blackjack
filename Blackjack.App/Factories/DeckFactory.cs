using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.App.Factories
{
    public class DeckFactory
    {

        public static List<Card> DefaultDeck = new List<Card>() 
        {
            new Card(1, Suit.Spades),
            new Card(2, Suit.Spades),
            new Card(3, Suit.Spades),
            new Card(4, Suit.Spades),
            new Card(5, Suit.Spades),
            new Card(6, Suit.Spades),
            new Card(7, Suit.Spades),
            new Card(8, Suit.Spades),
            new Card(9, Suit.Spades),
            new Card(10, Suit.Spades),
            new Card(11, Suit.Spades),
            new Card(12, Suit.Spades),
            new Card(13, Suit.Spades),
            new Card(1, Suit.Hearts),
            new Card(2, Suit.Hearts),
            new Card(3, Suit.Hearts),
            new Card(4, Suit.Hearts),
            new Card(5, Suit.Hearts),
            new Card(6, Suit.Hearts),
            new Card(7, Suit.Hearts),
            new Card(8, Suit.Hearts),
            new Card(9, Suit.Hearts),
            new Card(10, Suit.Hearts),
            new Card(11, Suit.Hearts),
            new Card(12, Suit.Hearts),
            new Card(13, Suit.Hearts),
            new Card(1, Suit.Clubs),
            new Card(2, Suit.Clubs),
            new Card(3, Suit.Clubs),
            new Card(4, Suit.Clubs),
            new Card(5, Suit.Clubs),
            new Card(6, Suit.Clubs),
            new Card(7, Suit.Clubs),
            new Card(8, Suit.Clubs),
            new Card(9, Suit.Clubs),
            new Card(10, Suit.Clubs),
            new Card(11, Suit.Clubs),
            new Card(12, Suit.Clubs),
            new Card(13, Suit.Clubs),
            new Card(1, Suit.Diamonds),
            new Card(2, Suit.Diamonds),
            new Card(3, Suit.Diamonds),
            new Card(4, Suit.Diamonds),
            new Card(5, Suit.Diamonds),
            new Card(6, Suit.Diamonds),
            new Card(7, Suit.Diamonds),
            new Card(8, Suit.Diamonds),
            new Card(9, Suit.Diamonds),
            new Card(10, Suit.Diamonds),
            new Card(11, Suit.Diamonds),
            new Card(12, Suit.Diamonds),
            new Card(13, Suit.Diamonds)
        };

        public static Deck CreateDeck()
        {
            return CreateDeck(1);
        }

        public static Deck CreateDeck(int numberOfStandardDecks)
        {
            Deck deck = new Deck(DefaultDeck);
            for(int i = 0; i < numberOfStandardDecks; i++)
            {
                if(i > 0)
                {
                    deck.Add(new Deck(DefaultDeck));
                }
            }
            return deck;
        }
    }
}
