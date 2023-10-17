using Blackjack.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.App
{
    public class Game
    {
        private static int MAX_PLAYERS = 7;
        private readonly IInteractionService _interactionService;

        public Game(IInteractionService interactionService)
        {
            _interactionService = interactionService;
        }

        public List<Player> Players { get; set; } = new List<Player>();

        public Dealer Dealer { get; set; } = new Dealer();

        public void Run()
        {
            Setup();

            
        }


        public void Setup()
        {
            int numberOfPlayers = _interactionService.GetNumberOfPlayers(MAX_PLAYERS);

            for(int i = 0; i < numberOfPlayers; i++)
            {
                Players.Add(new Player());
            }
        }
    }
}
