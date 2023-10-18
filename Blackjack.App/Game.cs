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
            Shuffle();

            while(Players.Count > 0) //main game loop
            {
                Deal();
                DisplayHands();


                //temporary stop for testing purposes
                Console.ReadLine();
            
            }
        }

        private void Shuffle()
        {
            Deck.Shuffle();
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
            Dealer.Hand.Add(Deck.DealFaceUp());

            //deal each player a card face up
            Players.ForEach(p => p.Hands[0].Add(Deck.DealFaceUp()));

            //deal the dealer a card face down
            Dealer.Hand.Add(Deck.Deal());
        }

        private void DisplayHands()
        {
            //display all hands should be:
            //Player #{} 
            //Hand #{}: Hand.Display (Hand.Total)

            int playerCount = 0;
            foreach(Player player in Players)
            {
                playerCount++;
                _interactionService.Display($"Player #{playerCount}");
                int handCount = 0;
                foreach(Hand hand in player.Hands)
                {
                    handCount++;
                    _interactionService.Display($"    Hand #{handCount}: {hand.Display} = {hand.Total}");
                    //hand.ForEach(c => _interactionService.Display($"{c.Name} "));
                }
            }


            //add card back to display for facedown cards
            //Add Dealer.Hand and delegate to Hands[0] for readability (check against Liskov Substitution Principle)
            _interactionService.Display($"Dealer: {Dealer.Hand.Display}");
            //Dealer.Hand.ForEach(c => _interactionService.Display($"{c.Name} "));

        }
    }
}
