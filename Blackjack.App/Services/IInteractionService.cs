using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.App.Services
{
    public interface IInteractionService
    {
        public int GetNumberOfPlayers(int maximumNumberOfPlayers);
        public void Display(string message);
    }
}
