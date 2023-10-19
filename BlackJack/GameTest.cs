using Blackjack.App;
using Blackjack.App.Services;
using BlackJack.UnitTests.Fixtures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.UnitTests
{
    public class GameTest
    {
        private IInteractionService interactionService = new InteractionServiceFake();

        [Fact]
        public void Tests_Are_Working()
        {
            Assert.False(false);
        }

        [Fact]
        public void Setup_AsksForOnePlayer_CreatesOnePlayer()
        {
            var interactionServiceMock = new Mock<IInteractionService>();
            interactionServiceMock.Setup(m => m.GetNumberOfPlayers(It.IsAny<int>())).Returns(1);

            Game game = new Game(interactionServiceMock.Object);
            game.Setup();

            Assert.Single(game.Players);
        }

        [Fact]
        public void Setup_AsksForTwoPlayers_CreatesTwoPlayers()
        {
            var interactionServiceMock = new Mock<IInteractionService>();
            interactionServiceMock.Setup(m => m.GetNumberOfPlayers(It.IsAny<int>())).Returns(2);

            Game game = new Game(interactionServiceMock.Object);
            game.Setup();

            Assert.Equal(2, game.Players.Count);
        }

        [Fact]
        public void Construct_HasADealer()
        {
            Game game = new Game(interactionService);
            Assert.NotNull(game.Dealer);
        }

        [Fact]
        public void Construct_CreatesDeck()
        {
            Game game = new Game(interactionService);
            Assert.NotNull(game.Deck);
        }

        [Fact]
        public void Construct_CreatesDeckWith312Cards()
        {
            Game game = new Game(interactionService);
            Assert.Equal(312, game.Deck.NumberOfCards);
        }

        [Fact]
        public void EvaluateHands_DealerHasTwoCard21_HandResulIsBlackjack()
        {
            Game game = new Game(interactionService);
            game.Dealer.Hand = new Hand(
                new List<Card>
                {
                    new Card(1, Suit.Spades),
                    new Card(10, Suit.Spades)
                });

            game.EvaluateStartingHands();

            Assert.Equal(HandResult.Blackjack, game.Dealer.Hand.Result);
        }

        [Fact]
        public void EvaluateHands_DealerHasBlackjack_HandWithBlackjackPushes()
        {
            Game game = new Game(interactionService);
            game.Dealer.Hand = new Hand(
                new List<Card>
                {
                    new Card(1, Suit.Spades),
                    new Card(10, Suit.Spades)
                });

            game.Players.Add(new Player());
            game.Players[0].Add(
                new Hand(
                    new List<Card>
                    {
                        new Card(1, Suit.Spades),
                        new Card(10, Suit.Spades)
                    }));

            game.EvaluateStartingHands();

            Assert.Equal(HandResult.Push, game.Players[0].GetHand(0).Result);
        }

        [Fact]
        public void EvaluateHands_DealerHasBlackjack_HandWithoutBlackjackLoses()
        {
            Game game = new Game(interactionService);
            game.Dealer.Hand = new Hand(
                new List<Card>
                {
                    new Card(1, Suit.Spades),
                    new Card(10, Suit.Spades)
                });

            game.Players.Add(new Player());
            game.Players[0].Add(
                new Hand(
                    new List<Card>
                    {
                        new Card(1, Suit.Spades),
                        new Card(9, Suit.Spades)
                    }));

            game.EvaluateStartingHands();

            Assert.Equal(HandResult.Loss, game.Players[0].GetHand(0).Result);
        }

        [Fact]
        public void EvaluateHands_DealerHas10_HandWithBlackjackWins()
        {
            Game game = new Game(interactionService);
            game.Dealer.Hand = new Hand(
                new List<Card>
                {
                    new Card(5, Suit.Spades),
                    new Card(5, Suit.Spades)
                });

            game.Players.Add(new Player());
            game.Players[0].Add(
                new Hand(
                    new List<Card> 
                    { 
                        new Card(1, Suit.Spades), 
                        new Card(10, Suit.Spades) 
                    }));

            game.EvaluateStartingHands();

            Assert.Equal(HandResult.Blackjack, game.Players[0].GetHand(0).Result);
        }

        [Fact]
        public void EvaluateHands_DealerHas10_HandWithout21IsNoResult()
        {
            Game game = new Game(interactionService);
            game.Dealer.Hand = new Hand(
                new List<Card>
                {
                    new Card(5, Suit.Spades),
                    new Card(5, Suit.Spades)
                });

            game.Players.Add(new Player());
            game.Players[0].Add(
                new Hand(
                    new List<Card>
                    {
                        new Card(1, Suit.Spades),
                        new Card(9, Suit.Spades)
                    }));

            game.EvaluateStartingHands();

            Assert.Equal(HandResult.None, game.Players[0].GetHand(0).Result);
        }

        [Fact]
        public void CollectHands_ClearsDealerHand()
        {
            Game game = new Game(interactionService);
            game.Dealer.Hand = new Hand(
                new List<Card>
                {
                    new Card(5, Suit.Spades),
                    new Card(5, Suit.Spades)
                });

            game.CollectHands();

            Assert.Empty(game.Dealer.Hand);
        }

        [Fact]
        public void CollectHands_ClearsPlayerHands()
        {
            Game game = new Game(interactionService);

            game.Players.Add(new Player());
            game.Players[0].Add(
                new Hand(
                    new List<Card>
                    {
                        new Card(1, Suit.Spades),
                        new Card(9, Suit.Spades)
                    }));

            game.CollectHands();

            Assert.Empty(game.Players[0].GetHand(0));
        }
    }
}
