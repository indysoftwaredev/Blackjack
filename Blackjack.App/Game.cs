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

        private void Display(string message)
        {
            _interactionService.Display(message);
        }

        private string HandNumberForDisplay(Player player, Hand hand)
        {
            return $"Player #{player.PlayerNumber}, Hand #{hand.HandNumber}";
        }

        private string HandNumberWithTotalForDisplay(Player player, Hand hand)
        {
            return $"{HandNumberForDisplay(player, hand)}: {hand.DisplayWithTotal}";
        }

        private string HandNumberWithTotalAndResultForDisplay(Player player, Hand hand)
        {
            return $"{HandNumberWithTotalForDisplay(player, hand)}: {hand.ResultDisplay}";
        }

        private string DealerShowsHandWithTotalForDisplay()
        {
            return $"Dealer Shows: {Dealer.Hand.DisplayWithTotal}";
        }

        private string DealerShowsHandWithoutTotalForDisplay()
        {
           return $"Dealer Shows: {Dealer.Hand.Display}";
        }

        public void Run()
        {
            Setup();
            Shuffle();

            while(Players.Count > 0) //main game loop
            {
                Deal();
                DisplayHands();
                EvaluateStartingHands();

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

        public void Setup()
        {
            int numberOfPlayers = _interactionService.GetNumberOfPlayers(MAX_PLAYERS);
            CreatePlayers(numberOfPlayers);
        }

        private void CreatePlayers(int numberOfPlayers)
        {
            for (int i = 0; i < numberOfPlayers; i++)
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
            GiveEachPlayerEmptyHand();

            _interactionService.ClearDisplay();
            //_interactionService.Display("********** Dealing Hands...\n");

            //deal each player a card face up
            Players.ForEach(p => p.GetHand(0).Add(Deck.DealFaceUp()));

            //deal the dealer a card face up
            Dealer.Hand.Add(Deck.DealFaceUp());
            //Dealer.Hand.Add(new Card(1, Suit.Spades, false));

            //deal each player a card face up
            Players.ForEach(p => p.GetHand(0).Add(Deck.DealFaceUp()));

            //deal the dealer a card face down
            Dealer.Hand.Add(Deck.Deal());
            //Dealer.Hand.Add(new Card(10, Suit.Hearts, true));
        }

        private void GiveEachPlayerEmptyHand()
        {
            Players.ForEach(p => p.Add(new Hand()));
        }

        private void DisplayHands()
        {
            foreach (Player player in Players)
            {
                foreach (Hand hand in player.GetHands())
                {
                    Display($"{HandNumberWithTotalForDisplay(player, hand)}");
                }
            }

            Display($"\nDealer: {Dealer.Hand.Display}");
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
                foreach (Hand hand in player.GetHands())
                {
                    hand.Result = EvaluateStartingPlayerHandAgainstDealerHand(hand);
                }
            }
        }

        private HandResult EvaluateStartingPlayerHandAgainstDealerHand(Hand playerHand)
        {
            if (playerHand.Total == Hand.TARGET_MAX_TOTAL)
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
            if (Dealer.Hand.Result == HandResult.Blackjack)
            {
                DisplayDealerBlackjack();

                foreach (Player player in Players)
                {
                    foreach (Hand hand in player.GetHands())
                    {
                        if (hand.Result == HandResult.Push)
                        {
                            Display($"{HandNumberWithTotalForDisplay(player, hand)} pushes");
                        }
                        else
                        {
                            Display($"{HandNumberWithTotalForDisplay(player, hand)} loses");
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

        private void Reset()
        {
            CollectHands();
            Dealer.ResetHand();
            Players.ForEach(p => p.ResetHands());
        }

        public void CollectHands()
        {
            Deck.Collect(Dealer.Hand);
            Players.ForEach(p => p.GetHands().ForEach(h => Deck.Collect(h)));
        }

        private void PressEnterToContinue()
        {
            Display("\nPress Enter to Continue");
            Console.ReadLine();
        }

        private void ProcessPlayerBlackjacks()
        {
            foreach (Player player in Players)
            {
                foreach (Hand hand in player.GetHands())
                {
                    if (hand.Result == HandResult.Blackjack)
                    {
                        Display($"{HandNumberForDisplay(player, hand)} has Blackjack!");
                    }
                }
            }
        }

        private void ProcessPlayerActions()
        {
            foreach (Player player in Players)
            {
                foreach (Hand hand in player.GetHands())
                {
                    bool stand = false;
                    while (!stand && hand.Result == HandResult.None)
                    {
                        Display($"\n********** {DealerShowsHandWithoutTotalForDisplay()}");

                        Display($"{HandNumberWithTotalForDisplay(player, hand)}");

                        HandAction action = _interactionService.GetHandAction();
                        switch (action)
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

                        if (hand.Total == Hand.TARGET_MAX_TOTAL)
                        {
                            Display(HandNumberWithTotalForDisplay(player, hand));
                            Display($"\nPlayer has {Hand.TARGET_MAX_TOTAL}... standing");
                            stand = true;
                        }
                        if (hand.Result == HandResult.Bust)
                        {
                            Display($"{HandNumberWithTotalForDisplay(player, hand)}: Bust!");
                        }
                    }
                }
            }
        }

        private void Hit(Hand hand)
        {
            hand.Add(Deck.DealFaceUp());
        }

        private void CheckForBust(Hand hand)
        {
            if (hand.Total > Hand.TARGET_MAX_TOTAL)
            {
                hand.Result = HandResult.Bust;
            }
        }


        public bool HasActivePlayers()
        {
            foreach (Player player in Players)
            {
                foreach (Hand hand in player.GetHands())
                {
                    if (hand.Result == HandResult.None)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void ShowDealerHand()
        {
            _interactionService.Display("\n**********");
            Dealer.Hand.TurnFaceUp();
            Display($"{DealerShowsHandWithTotalForDisplay()}");
        }

        private void ProcessDealerActions()
        {
            while (Dealer.Hand.Total < 17)
            {
                Display("\nDealer Hits");
                Hit(Dealer.Hand);
                Display($"{DealerShowsHandWithTotalForDisplay()}");
            }

            if (Dealer.Hand.Total > Hand.TARGET_MAX_TOTAL)
            {
                Display("\nDealer Busts");
                Dealer.Hand.Result = HandResult.Bust;
            }
            else
            {
                Display("\nDealer Stands");
            }
        }

        private void EvaluateFinalHands()
        {
            foreach (Player player in Players)
            {
                foreach (Hand hand in player.GetHands())
                {
                    hand.Result = EvaluateAgainstDealer(hand);
                }
            }
        }

        private HandResult EvaluateAgainstDealer(Hand hand)
        {

            if (Dealer.Hand.Result == HandResult.Bust)
            {
                if (hand.Result == HandResult.Blackjack) { return hand.Result; }
                if (hand.Total < Hand.TARGET_MAX_TOTAL) { return HandResult.Win; }
                if (hand.Total > Hand.TARGET_MAX_TOTAL) { return HandResult.Loss; }
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

        private void DisplayHandsWithResults()
        {
            Display("\n********** Results **********"); 
            Dealer.Hand.TurnFaceUp();
            Display($"{DealerShowsHandWithTotalForDisplay()}");

            foreach (Player player in Players)
            {
                foreach (Hand hand in player.GetHands())
                {
                    Display(HandNumberWithTotalAndResultForDisplay(player, hand));
                }
            }
        }
    }
}
