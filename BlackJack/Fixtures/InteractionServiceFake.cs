using Blackjack.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.UnitTests.Fixtures
{
    internal class InteractionServiceFake : IInteractionService
    {
        public void Display(string message)
        {
            
        }

        public int GetNumberOfPlayers(int maximumNumberOfPlayers)
        {
            return 1;
        }
    }
}
