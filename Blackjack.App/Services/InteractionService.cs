using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.App.Services
{
    public class InteractionService : IInteractionService
    {
        public void ClearDisplay()
        {
            Console.Clear();
        }

        public void Display(string message)
        {
            Console.WriteLine(message);
        }

        public HandAction GetHandAction()
        {
            Display("1: Hit");
            Display("2: Stand");
            int actionNumber = ConversionService.ConvertStringToInt(Console.ReadLine());
            if(actionNumber == 1)
            {
                return HandAction.Hit;
            }
            if(actionNumber == 2)
            {
                return HandAction.Stand;
            }
            Display($"Please enter either 1 or 2.");
            return GetHandAction();

        }

        public int GetNumberOfPlayers(int maximumNumberOfPlayers)
        {
            if(maximumNumberOfPlayers < 0) //invalid maximum
            {
                return 0;
            }

            Display($"How Many Players? (0 to {maximumNumberOfPlayers})");

            int numOfPlayers = ConversionService.ConvertStringToInt(Console.ReadLine());
            if (numOfPlayers >= 0 && numOfPlayers <= maximumNumberOfPlayers) 
            { 
                return numOfPlayers; 
            }            

            Display($"Please Enter a number between 0 and {maximumNumberOfPlayers}");
            return GetNumberOfPlayers(maximumNumberOfPlayers);
        }
    }
}
