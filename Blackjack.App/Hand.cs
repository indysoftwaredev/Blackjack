using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.App
{
    public class Hand
    {
        private static int TARGET_MAX_TOTAL = 21;

        public Hand(List<Card> cards)
        {
            Cards = cards;
        }

        public List<Card> Cards { get; }

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
            Cards.ForEach(card => total += card.Value);
            return total;
        }

        private int CalculateTotal(int numberOfAces, int total)
        {
            if(numberOfAces == 0) { return total; }
            if(total <= TARGET_MAX_TOTAL) { return total; }

            total -= 10;
            if(total > TARGET_MAX_TOTAL)
            {
                return CalculateTotal(--numberOfAces, total);
            }

            return total;
            
        }

        private int NumberOfAces()
        {
            int count = 0;
            Cards.ForEach(card =>
            {
                if (card.Rank.Equals(1))
                {
                    count++;
                }
            });
            return count;
        }
    }
}
