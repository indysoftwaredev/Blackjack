using Blackjack.App.Factories;
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
            _interactionService = interactionService ?? throw new ArgumentException(nameof(interactionService));
        }

        public List<Player> Players { get; set; } = new List<Player>();

        public Dealer Dealer { get; set; } = new Dealer();
        public Deck Deck { get; set; } = DeckFactory.CreateDeck(6);

        public void Run()
        {
            Setup();

            while(Players.Count > 0) //main game loop
            {
                Deal();
            }
        }

        public void Setup()
        {
            int numberOfPlayers = _interactionService.GetNumberOfPlayers(MAX_PLAYERS);

            for(int i = 0; i < numberOfPlayers; i++)
            {
                Players.Add(new Player());
            }
        }

        public void Deal()
        {
            //deal each player a card face up
            Players.ForEach(p => p.Hands[0].Add(Deck.DealFaceUp()));

            //deal the dealer a card face up
            Dealer.Hands[0].Add(Deck.DealFaceUp());

            //deal each player a card face up
            Players.ForEach(p => p.Hands[0].Add(Deck.DealFaceUp()));

            //deal the dealer a card face down
            Dealer.Hands[0].Add(Deck.Deal());
        }
    }
}
