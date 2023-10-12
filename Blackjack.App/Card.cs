using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.App
{
    public class Card
    {

        public Suit Suit { get; set; }
        public int Value { get; set; }

        public Card(int value, Suit suit)
        {
            Value = value;
            Suit = suit;
        }

        public string Name
        {
            get
            {
                return $"{ConvertValueToName(Value)} of {ConvertSuitToName(Suit)}";
            }
        }

        private String ConvertValueToName(int value)
        {
            switch(value)
            {
                case 1: return "Ace";
                case 2: return "Two";
                case 3: return "Three";
                case 4: return "Four";
                case 5: return "Five";
                case 6: return "Six";
                case 7: return "Seven";
                case 8: return "Eight";
                case 9: return "Nine";
                case 10: return "Ten";
                case 11: return "Jack";
                case 12: return "Queen";
                case 13: return "King";
                default: throw new ArgumentException("Valid Values for cards are 1 thru 13 only");
            }
        }

        private string ConvertSuitToName(Suit suit)
        {
            switch(suit)
            {
                case Suit.Spades: return "Spades";
                case Suit.Hearts: return "Hearts";
                case Suit.Clubs: return "Clubs";
                default: return "Diamonds";
            }
        }
        

    }
}
