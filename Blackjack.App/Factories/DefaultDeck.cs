using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.App.Factories
{
    public class DefaultDeck : List<Card>
    {
        public DefaultDeck()
        {
            Add(new Card(1, Suit.Spades));
            Add(new Card(2, Suit.Spades));
            Add(new Card(3, Suit.Spades));
            Add(new Card(4, Suit.Spades));
            Add(new Card(5, Suit.Spades));
            Add(new Card(6, Suit.Spades));
            Add(new Card(7, Suit.Spades));
            Add(new Card(8, Suit.Spades));
            Add(new Card(9, Suit.Spades));
            Add(new Card(10, Suit.Spades));
            Add(new Card(11, Suit.Spades));
            Add(new Card(12, Suit.Spades));
            Add(new Card(13, Suit.Spades));
            Add(new Card(1, Suit.Hearts));
            Add(new Card(2, Suit.Hearts));
            Add(new Card(3, Suit.Hearts));
            Add(new Card(4, Suit.Hearts));
            Add(new Card(5, Suit.Hearts));
            Add(new Card(6, Suit.Hearts));
            Add(new Card(7, Suit.Hearts));
            Add(new Card(8, Suit.Hearts));
            Add(new Card(9, Suit.Hearts));
            Add(new Card(10, Suit.Hearts));
            Add(new Card(11, Suit.Hearts));
            Add(new Card(12, Suit.Hearts));
            Add(new Card(13, Suit.Hearts));
            Add(new Card(1, Suit.Clubs));
            Add(new Card(2, Suit.Clubs));
            Add(new Card(3, Suit.Clubs));
            Add(new Card(4, Suit.Clubs));
            Add(new Card(5, Suit.Clubs));
            Add(new Card(6, Suit.Clubs));
            Add(new Card(7, Suit.Clubs));
            Add(new Card(8, Suit.Clubs));
            Add(new Card(9, Suit.Clubs));
            Add(new Card(10, Suit.Clubs));
            Add(new Card(11, Suit.Clubs));
            Add(new Card(12, Suit.Clubs));
            Add(new Card(13, Suit.Clubs));
            Add(new Card(1, Suit.Diamonds));
            Add(new Card(2, Suit.Diamonds));
            Add(new Card(3, Suit.Diamonds));
            Add(new Card(4, Suit.Diamonds));
            Add(new Card(5, Suit.Diamonds));
            Add(new Card(6, Suit.Diamonds));
            Add(new Card(7, Suit.Diamonds));
            Add(new Card(8, Suit.Diamonds));
            Add(new Card(9, Suit.Diamonds));
            Add(new Card(10, Suit.Diamonds));
            Add(new Card(11, Suit.Diamonds));
            Add(new Card(12, Suit.Diamonds));
            Add(new Card(13, Suit.Diamonds));
        }
    }
}
