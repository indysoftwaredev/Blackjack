using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.App.Services
{
    public class InteractionService : IInteractionService
    {
        public int GetNumberOfPlayers(int maximumNumberOfPlayers)
        {
            if(maximumNumberOfPlayers < 0) //invalid maximum
            {
                return 0;
            }

            int numOfPlayers = 0;
            Console.WriteLine($"How Many Players? (0 to {maximumNumberOfPlayers})");

            string? numberOfPlayersString = Console.ReadLine();

            if (numberOfPlayersString != null)
            {
                if (int.TryParse(numberOfPlayersString, out numOfPlayers))
                {
                    if (numOfPlayers >= 0 && numOfPlayers <= maximumNumberOfPlayers) { return numOfPlayers; }
                }
            }

            Console.WriteLine($"Please Enter a number between 0 and {maximumNumberOfPlayers}");
            return GetNumberOfPlayers(maximumNumberOfPlayers);
        }
    }
}
