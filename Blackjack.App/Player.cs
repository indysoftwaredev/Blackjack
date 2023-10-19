using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.App
{
    public class Player
    {
        public List<Hand> Hands = new List<Hand>();

        public Player()
        {
            ResetHands();
        }

        public Player(int playerNumber) : this()
        {
            PlayerNumber = playerNumber;
        }

        public int PlayerNumber { get; set; }

        public void ResetHands()
        {
            Hands = new List<Hand>
            {
                new Hand(new List<Card>())
            };
        }
    }
}
