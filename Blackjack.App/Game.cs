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
                    CollectHands();
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

                Reset();


                //dealer hits until total > 17

                //evaluate hands
                //  remaining player hands win

                //temporary stop for testing purposes
                Console.ReadLine();
            
            }
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
                int handCount = 0;
                foreach(Hand hand in player.Hands)
                {
                    handCount++;
                    string playerHandId = $"Player #{playerCount}, Hand #{handCount}";
                    _interactionService.Display($"{playerHandId}, {hand.DisplayWithTotal()}");
                }
            }

            _interactionService.Display($"Dealer: {Dealer.Hand.Display}");
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
                    EvaluateAgainstDealer(hand);
                }
            }
        }

        private void EvaluateAgainstDealer(Hand hand)
        {
            if (Dealer.Hand.Result == HandResult.Bust)
            {
                if (hand.Result == HandResult.None || hand.Result == HandResult.Blackjack)
                {
                    hand.Result = HandResult.Win;
                }
            }
            else
            {
                if (Dealer.Hand.Total == hand.Total) { hand.Result = HandResult.Push; }
                if (Dealer.Hand.Total < hand.Total) { hand.Result = HandResult.Win; }
                if (Dealer.Hand.Total > hand.Total) { hand.Result = HandResult.Loss; }
            }
        }

        private void Evaluate(Hand hand)
        {
            if (hand.Total > Hand.TARGET_MAX_TOTAL)
            {
                hand.Result = HandResult.Bust;
            }
            if (hand.Total == Hand.TARGET_MAX_TOTAL)
            {
                hand.Result = HandResult.Blackjack;
            }
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
                Dealer.Hand.TurnFaceUp();
                _interactionService.Display($"\nDealer has Blackjack: {Dealer.Hand.Display}\n");

                int playerCount = 0;
                foreach (Player player in Players)
                {
                    playerCount++;

                    int handCount = 0;
                    foreach(Hand hand in player.Hands)
                    {
                        handCount++;
                        string playerHandId = $"Player # {playerCount}, Hand #{handCount}";
                        if (hand.Result == HandResult.Push)
                        {
                            _interactionService.Display($"{playerHandId}, {hand.DisplayWithTotal()} pushes");
                        }
                        else
                        {
                            _interactionService.Display($"{playerHandId}, {hand.DisplayWithTotal()} loses");
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
        }

        private void ProcessPlayerBlackjacks()
        {
            int playerCount = 0;
            foreach (Player player in Players)
            {
                playerCount++;
                int handCount = 0;
                foreach(Hand hand in player.Hands) 
                { 
                    handCount++;
                    if( hand.Result == HandResult.Blackjack)
                    {
                        _interactionService.Display($"Player #{playerCount}, Hand #{handCount} has Blackjack!");
                    }
                }
            }
        }

        private void ProcessPlayerActions()
        {
            int playerCount = 0;
            foreach(Player player in Players)
            {
                playerCount++;
                int handCount = 0;
                foreach(Hand hand in player.Hands)
                {
                    handCount++;
                    bool stand = false;
                    while(!stand && hand.Result == HandResult.None)
                    {
                        string playerHandId = $"Player # {playerCount}, Hand #{handCount}";

                        _interactionService.Display($"{playerHandId}: {hand.DisplayWithTotal()}");

                        HandAction action = _interactionService.GetHandAction();
                        switch(action)
                        {
                            case HandAction.Hit: 
                                Hit(hand);
                                Evaluate(hand); break;
                            case HandAction.Stand: stand = true; break;
                        }

                        if(hand.Result == HandResult.Blackjack)
                        {
                            _interactionService.Display($"{playerHandId}: {hand.DisplayWithTotal()}: Win!");
                        }
                        if(hand.Result == HandResult.Bust)
                        {
                            _interactionService.Display($"{playerHandId}: {hand.DisplayWithTotal()}: Bust!");
                        }
                    }
                }
            }
        }

        private void ShowDealerHand()
        {
            Dealer.Hand.TurnFaceUp();
            _interactionService.Display($"Dealer shows: {Dealer.Hand.DisplayWithTotal()}");
        }

        private void ProcessDealerActions()
        {            
            while (Dealer.Hand.Total < 17)
            {
                _interactionService.Display("Dealer Hits");
                Hit(Dealer.Hand);
                _interactionService.Display($"Dealer Shows: {Dealer.Hand.DisplayWithTotal()}");
            }

            if (Dealer.Hand.Total > Hand.TARGET_MAX_TOTAL)
            {
                _interactionService.Display("Dealer Busts");
                Dealer.Hand.Result = HandResult.Bust;
            } 
            else
            {
                _interactionService.Display("Dealer Stands");
            }
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
