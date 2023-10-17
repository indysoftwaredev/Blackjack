using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.App
{
    public class Player : List<Hand>
    {
        //public List<Hand> Hands { get; set; } = new List<Hand>();

        public Player()
        {
            this.Add(new Hand(new List<Card>()));
        }
    }
}
