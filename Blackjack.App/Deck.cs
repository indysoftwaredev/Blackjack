using Blackjack.App.Factories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.App
{
    public class Deck : List<Card>
    {

        public Deck() : this(DeckFactory.DefaultDeck)
        {
            
        }

        public Deck(IEnumerable<Card> collection) : base(collection)
        {
            
        }

        public int NumberOfCards { get { return Count; }  }

        public void Collect(Hand hand)
        {
            hand.ForEach(c => this.Add(c));
            hand.Clear();
        }

        public Card Deal()
        {
            Card card = this.Take(1).First();
            this.RemoveAt(0);
            return card;
        }

        public Card DealFaceUp()
        {
            Card card = Deal();
            card.IsFaceDown = false;
            return card;
        }

        public Card GetCardAt(int position)
        {
            return this[position];
        }

        /// <summary>
        /// Utilizes the Fisher-Yates Shuffle algorithm
        /// <see cref="" href="https://en.wikipedia.org/wiki/Fisher–Yates_shuffle"/>
        /// </summary>
        public void Shuffle()
        {
            Random rng = new Random();
            int lastCardIndex = this.Count;

            while (lastCardIndex > 1)
            {
                lastCardIndex--;
                int randomPosition = rng.Next(lastCardIndex + 1); //select from the range

                //swap with last card
                Card card = this[randomPosition];
                this[randomPosition] = this[lastCardIndex];
                this[lastCardIndex] = card;
            }

            this.ForEach(c => c.IsFaceDown = true);
        }

        internal void Add(Deck deck)
        {
            deck.ForEach(c => this.Add(c));
        }
    }
}
