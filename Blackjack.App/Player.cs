using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.App
{
    public class Player
    {
        private List<Hand> Hands = new List<Hand>();

        public int HandCount
        {
            get
            {
                return Hands.Count;
            }
        }

        public Hand GetHand(int index) { return Hands[index]; }

        public List<Hand> GetHands() { return Hands; }

        public Player()
        {
            //ResetHands();
        }

        public void Add(Hand hand)
        {
            hand.HandNumber = HandCount + 1;
            Hands.Add(hand);
        }

        public Player(int playerNumber) : this()
        {
            PlayerNumber = playerNumber;
        }

        public int PlayerNumber { get; set; }

        public void ResetHands()
        {
            Hands = new List<Hand>();
        }


    }
}
