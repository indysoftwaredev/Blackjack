using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.App
{
    public class Deck
    {
        private List<Card> _cards;

        public Deck()
        {
            _cards = new List<Card>
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
        }

        public Card Deal()
        {
            Card card = _cards.Take(1).First();
            _cards.RemoveAt(0);
            return card;
        }

        public Card GetCardAt(int position)
        {
            return _cards[position];
        }

        /// <summary>
        /// Utilizes the Fisher-Yates Shuffle algorithm
        /// <see cref="" href="https://en.wikipedia.org/wiki/Fisher–Yates_shuffle"/>
        /// </summary>
        public void Shuffle()
        {
            Random rng = new Random();
            int lastCardIndex = _cards.Count;

            while (lastCardIndex > 1)
            {
                lastCardIndex--;
                int randomPosition = rng.Next(lastCardIndex + 1); //select from the range

                //swap with last card
                Card card = _cards[randomPosition];
                _cards[randomPosition] = _cards[lastCardIndex];
                _cards[lastCardIndex] = card;
            }
        }

        internal void ListContents()
        {
            int pos = 0;
            foreach (Card card in _cards)
            {
                Console.WriteLine($"Assert.Equal(\"{card.Name}\", deck.GetCardAt({pos}).Name);");
                pos++;
            }
        }
    }
}
