using Blackjack.App.Factories;
using Blackjack.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
                EvaluateHands();
                if(ProcessDealerBlackjack())
                {
                    CollectHands();
                    continue;
                };

                //each player can hit until bust or stand

                //dealer hits until total > 17

                //evaluate hands
                //  remaining player hands win

                //temporary stop for testing purposes
                Console.ReadLine();
            
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

        private void Shuffle()
        {
            Deck.Shuffle();
        }

        public void Deal()
        {
            //deal each player a card face up
            Players.ForEach(p => p.Hands[0].Add(Deck.DealFaceUp()));

            //deal the dealer a card face up
            Dealer.Hand.Add(Deck.DealFaceUp());
            //Dealer.Hand.Add(new Card(1, Suit.Spades, false));

            //deal each player a card face up
            Players.ForEach(p => p.Hands[0].Add(Deck.DealFaceUp()));

            //deal the dealer a card face down
            Dealer.Hand.Add(Deck.Deal());
            //Dealer.Hand.Add(new Card(10, Suit.Hearts, true));
        }

        private void DisplayHands()
        {
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

            _interactionService.Display($"Dealer: {Dealer.Hand.Display}");
            //Dealer.Hand.ForEach(c => _interactionService.Display($"{c.Name} "));
        }

        public void EvaluateHands()
        {
            if (Dealer.Hand.Total == Hand.TARGET_MAX_TOTAL)
            {
                Dealer.Hand.Result = HandResult.Blackjack;
            }

            EvaluatePlayerHands();
        }

        private void EvaluatePlayerHands()
        {
            foreach (Player player in Players)
            {
                foreach (Hand hand in player.Hands)
                {
                    hand.Result = Evaluate(hand);
                }
            }
        }

        private HandResult Evaluate(Hand playerHand)
        {
            if(playerHand.Total == Hand.TARGET_MAX_TOTAL)
            {
                if (Dealer.Hand.Result == HandResult.Blackjack)
                {
                    return HandResult.Push;
                }
                else
                {
                    return HandResult.Blackjack;
                }
            }
            else
            {
                if (Dealer.Hand.Result == HandResult.Blackjack)
                {
                    return HandResult.Loss;
                }
                else
                {
                    return HandResult.None;
                }
            }
        }

        public bool ProcessDealerBlackjack()
        {
            if(Dealer.Hand.Result == HandResult.Blackjack)
            {
                Dealer.Hand.TurnFaceUp();
                _interactionService.Display($"\nDealer has Blackjack: {Dealer.Hand.Display}\n");

                int playerCount = 0;
                foreach (Player player in Players)
                {
                    playerCount++;
                    _interactionService.Display($"Player #{playerCount}");

                    int handCount = 0;
                    foreach(Hand hand in player.Hands)
                    {
                        handCount++;
                        if(hand.Result == HandResult.Push)
                        {
                            _interactionService.Display($"    Hand #{handCount}: {hand.Display} pushes");
                        }
                        else
                        {
                            _interactionService.Display($"    Hand #{handCount}: {hand.Display} loses");
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public void CollectHands()
        {
            Deck.Collect(Dealer.Hand);
            Players.ForEach(p => p.Hands.ForEach(h => Deck.Collect(h)));

            //we should also reset the number of hands each player has to 1,
            //in the case where a player has multiple hands
            //this is not implemented at this time.
        }
    }
}
