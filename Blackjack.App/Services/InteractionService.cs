using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.App.Services
{
    public class InteractionService : IInteractionService
    {
        public void display(string message)
        {
            Console.WriteLine(message);
        }

        public int GetNumberOfPlayers(int maximumNumberOfPlayers)
        {
            if(maximumNumberOfPlayers < 0) //invalid maximum
            {
                return 0;
            }

            display($"How Many Players? (0 to {maximumNumberOfPlayers})");

            int numOfPlayers = ConversionService.ConvertStringToInt(Console.ReadLine());
            if (numOfPlayers >= 0 && numOfPlayers <= maximumNumberOfPlayers) 
            { 
                return numOfPlayers; 
            }            

            display($"Please Enter a number between 0 and {maximumNumberOfPlayers}");
            return GetNumberOfPlayers(maximumNumberOfPlayers);
        }
    }
}
