using Blackjack.App;
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
        public void ClearDisplay()
        {
            
        }

        public void Display(string message)
        {
            
        }

        public HandAction GetHandAction()
        {
            return HandAction.Stand;
        }

        public int GetNumberOfPlayers(int maximumNumberOfPlayers)
        {
            return 1;
        }
    }
}
