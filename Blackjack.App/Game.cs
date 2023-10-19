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
                EvaluateStartingDealerHand();
                EvaluateStartingPlayerHands();

                if (ProcessDealerBlackjack())
                {
                    Reset();
                    PressEnterToContinue();
                    continue;
                };

                ProcessPlayerBlackjacks();
                ProcessPlayerActions();

                if (HasActivePlayers())
                {
                    ShowDealerHand();
                    ProcessDealerActions();
                }

                EvaluateFinalHands();

                DisplayHandsWithResults();
                PressEnterToContinue();

                Reset();
            
            }
        }

        private void PressEnterToContinue()
        {
            Display("\nPress Enter to Continue");
            Console.ReadLine();
        }

        private void Reset()
        {
            CollectHands();
            Dealer.ResetHand();            
            Players.ForEach(p => p.ResetHands());
        }

        public void Setup()
        {
            int numberOfPlayers = _interactionService.GetNumberOfPlayers(MAX_PLAYERS);

            for(int i = 0; i < numberOfPlayers; i++)
            {
                Players.Add(new Player(i + 1));
            }
        }

        private void Shuffle()
        {
            Deck.Shuffle();
        }

        public void Deal()
        {
            _interactionService.ClearDisplay();
            //_interactionService.Display("********** Dealing Hands...\n");

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
            foreach(Player player in Players)
            {
                int handCount = 0;
                foreach(Hand hand in player.Hands)
                {
                    handCount++;
                    string playerHandId = $"Player #{player.PlayerNumber}, Hand #{handCount}";
                    Display($"{playerHandId}, {hand.DisplayWithTotal}");
                }
            }

            Display($"\nDealer: {Dealer.Hand.Display}");
        }

        private void DisplayHandsWithResults()
        {
            Display("\n********** Results **********"); 
            Dealer.Hand.TurnFaceUp();
            Display($"\nDealer: {Dealer.Hand.DisplayWithTotal}");

            foreach (Player player in Players)
            {
                int handCount = 0;
                foreach (Hand hand in player.Hands)
                {
                    handCount++;
                    string playerHandId = $"Player #{player.PlayerNumber}, Hand #{handCount}";
                    Display($"{playerHandId}, {hand.DisplayWithTotal}: {hand.ResultDisplay}");
                }
            }
        }

        public void EvaluateStartingHands()
        {
            EvaluateStartingDealerHand();
            EvaluateStartingPlayerHands();

        }

        private void EvaluateStartingDealerHand()
        {
            if (Dealer.Hand.Total == Hand.TARGET_MAX_TOTAL)
            {
                Dealer.Hand.Result = HandResult.Blackjack;
            }
        }

        private void EvaluateStartingPlayerHands()
        {
            foreach (Player player in Players)
            {
                foreach (Hand hand in player.Hands)
                {
                    hand.Result = EvaluateStartingPlayerHandAgainstDealerHand(hand);
                }
            }
        }
        private void EvaluateFinalHands()
        {
            foreach (Player player in Players)
            {
                foreach (Hand hand in player.Hands)
                {
                    hand.Result = EvaluateAgainstDealer(hand);
                }
            }
        }

        private HandResult EvaluateAgainstDealer(Hand hand)
        {
            
            if (Dealer.Hand.Result == HandResult.Bust)
            {
                if(hand.Result == HandResult.Blackjack) { return hand.Result; }
                if(hand.Total < Hand.TARGET_MAX_TOTAL) { return HandResult.Win; }
                if(hand.Total > Hand.TARGET_MAX_TOTAL) { return HandResult.Loss; }                
            }
            else
            {
                if (hand.Result == HandResult.Bust) { return HandResult.Loss; }
                if (hand.Result == HandResult.Blackjack) 
                {
                    return Dealer.Hand.Result == HandResult.Blackjack ? HandResult.Push : HandResult.Blackjack;
                }
                if (Dealer.Hand.Total == hand.Total) { return HandResult.Push; }
                if (Dealer.Hand.Total < hand.Total) { return HandResult.Win; }
                if (Dealer.Hand.Total > hand.Total) { return HandResult.Loss; }
            }
            return HandResult.Win;
        }

        private HandResult EvaluateStartingPlayerHandAgainstDealerHand(Hand playerHand)
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
                DisplayDealerBlackjack();

                foreach (Player player in Players)
                {

                    int handCount = 0;
                    foreach (Hand hand in player.Hands)
                    {
                        handCount++;
                        string playerHandId = $"Player #{player.PlayerNumber}, Hand #{handCount}";
                        if (hand.Result == HandResult.Push)
                        {
                            Display($"{playerHandId}, {hand.DisplayWithTotal} pushes");
                        }
                        else
                        {
                            Display($"{playerHandId}, {hand.DisplayWithTotal} loses");
                        }
                    }
                }
                return true;
            }
            return false;
        }

        private void DisplayDealerBlackjack()
        {
            ShowDealerHand();
            Display($"\nDealer has Blackjack: {Dealer.Hand.Display}");
        }

        private void ShowDealerHand()
        {
            _interactionService.Display("\n**********");
            Dealer.Hand.TurnFaceUp();
            DisplayDealerShowsHand();
        }

        private void DisplayDealerShowsHand()
        {
            Display($"\nDealer Shows: {Dealer.Hand.DisplayWithTotal}");
        }

        public void CollectHands()
        {
            Deck.Collect(Dealer.Hand);
            Players.ForEach(p => p.Hands.ForEach(h => Deck.Collect(h)));
        }

        private void ProcessPlayerBlackjacks()
        {
            foreach (Player player in Players)
            {
                int handCount = 0;
                foreach(Hand hand in player.Hands) 
                { 
                    handCount++;
                    if( hand.Result == HandResult.Blackjack)
                    {
                        Display($"Player #{player.PlayerNumber}, Hand #{handCount} has Blackjack!");
                    }
                }
            }
        }

        private void ProcessPlayerActions()
        {
            foreach(Player player in Players)
            {
                int handCount = 0;
                foreach(Hand hand in player.Hands)
                {
                    handCount++;
                    bool stand = false;
                    while(!stand && hand.Result == HandResult.None)
                    {
                        Display("\n**********");
                        string playerHandId = $"\nPlayer #{player.PlayerNumber}, Hand #{handCount}";

                        Display($"{playerHandId}: {hand.DisplayWithTotal}");

                        HandAction action = _interactionService.GetHandAction();
                        switch(action)
                        {
                            case HandAction.Hit:
                                Display("\nPlayer Hits");
                                Hit(hand);
                                CheckForBust(hand); 
                                break;
                            case HandAction.Stand:
                                Display("\nPlayer Stands");
                                stand = true; 
                                break;
                        }

                        if(hand.Total == Hand.TARGET_MAX_TOTAL)
                        {
                            Display($"{playerHandId}: {hand.DisplayWithTotal}");
                            Display($"\nPlayer has {Hand.TARGET_MAX_TOTAL}... standing");
                            stand = true;
                        }
                        if(hand.Result == HandResult.Bust)
                        {
                            Display($"{playerHandId}: {hand.DisplayWithTotal}: Bust!");
                        }
                    }
                }
            }
        }
        private void CheckForBust(Hand hand)
        {
            if (hand.Total > Hand.TARGET_MAX_TOTAL)
            {
                hand.Result = HandResult.Bust;
            }
        }

        private void ProcessDealerActions()
        {            
            while (Dealer.Hand.Total < 17)
            {
                Display("\nDealer Hits");
                Hit(Dealer.Hand);
                DisplayDealerShowsHand();
            }

            if (Dealer.Hand.Total > Hand.TARGET_MAX_TOTAL)
            {
                Display("\nDealer Busts\n");
                Dealer.Hand.Result = HandResult.Bust;
            } 
            else
            {
                Display("\nDealer Stands\n");
            }
        }

        private void Display(string message)
        {
            _interactionService.Display(message);
        }


        public bool HasActivePlayers()
        {
            foreach(Player player in Players)
            {
                foreach (Hand hand in player.Hands)
                {
                    if (hand.Result == HandResult.None)
                    {
                        return true;      
                    }
                }
            }
            return false;
        }

        private void Hit(Hand hand)
        {
            hand.Add(Deck.DealFaceUp());
        }
    }
}
