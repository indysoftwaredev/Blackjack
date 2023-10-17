using Blackjack.App;
using Blackjack.App.Services;
using BlackJack.UnitTests.Fixtures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
