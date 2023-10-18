using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.App.Factories
{
    public class DeckFactory
    {
        public static Deck CreateDeck()
        {
            return CreateDeck(1);
        }

        public static Deck CreateDeck(int numberOfStandardDecks)
        {
            Deck deck = new Deck(new DefaultDeck());
            for(int i = 0; i < numberOfStandardDecks; i++)
            {
                if(i > 0)
                {
                    deck.Add(new Deck(new DefaultDeck()));
                }
            }
            return deck;
        }
    }
}
