using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.App
{
    public class Card
    {

        public Suit Suit { get; }
        public int Rank { get; }

        public Card(int rank, Suit suit)
        {
            if(rank > 13 || rank < 1) { throw new ArgumentException("Card rank must be between 1 and 13"); }
            Rank = rank;
            Suit = suit;
        }

        public string Name
        {
            get
            {
                return $"{ConvertRankToName()} of {ConvertSuitToName(Suit)}";
            }
        }

        public int Value
        {
            get
            {
                switch(Rank)
                {
                    case 1: return 11;
                    case 11: return 10;
                    case 12: return 10;
                    case 13: return 10;
                    default: return Rank;
                }
            }
        }

        public bool IsFaceDown { get; set; } = true;
        public bool IsFaceUp { get { return !IsFaceDown; } }

        private String ConvertRankToName()
        {
            switch(Rank)
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
                default: throw new ArgumentException("Card rank must be between 1 and 13");
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
