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
                new Card
                {
                    Name = "Ace of Spades"
                },
                new Card
                {
                    Name = "King of Spades"
                }
            };
        }

        public Card Deal()
        {
            Card card = _cards.Take(1).First();
            _cards.RemoveAt(0);
            return card;
        }
    }
}
