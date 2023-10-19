using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.App
{
    public class Hand : List<Card>
    {
        public static int TARGET_MAX_TOTAL = 21;

        public Hand(List<Card> cards)
        {
            cards.ForEach(c => this.Add(c));
        }

        public Hand() : this(new List<Card>())
        {

        }

        public int Total 
        { 
            get
            {
                return CalculateTotal(NumberOfAces(), BasicTotal());
            }
        }

        private int BasicTotal()
        {
            int total = 0;
            this.ForEach(card => total += card.Value);
            return total;
        }

        private int CalculateTotal(int numberOfAces, int total)
        {
            if(numberOfAces == 0) { return total; }
            if(total <= TARGET_MAX_TOTAL) { return total; }

            return CalculateTotal(--numberOfAces, total - 10);
        }

        private int NumberOfAces()
        {
            int count = 0;
            this.ForEach(card =>
            {
                if (card.Rank.Equals(1))
                {
                    count++;
                }
            });
            return count;
        }

        public void TurnFaceUp()
        {
            this.ForEach(card => card.IsFaceDown = false);
        }

        public string DisplayWithTotal()
        {
            return $"{Display} = {Total}";
        }

        public string Display
        {
            get 
            {
                return string.Join("", this.Select(c => c.Display));
            }
        }

        public HandResult Result { get; set; }
    }
}
