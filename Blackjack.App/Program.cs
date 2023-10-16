using Blackjack.App;
using System.Runtime.CompilerServices;

Deck deck = new Deck();
deck.Shuffle();
deck.ListContents();

//while (_inSetup)
//{
//    Console.Clear();
//    Console.WriteLine("How Many Players?");
//    string? numberOfPlayersString = Console.ReadLine();
//    if (numberOfPlayersString != null)
//    {
//        if(int.TryParse(numberOfPlayersString, out numberOfPlayers))
//        {
//            if(numberOfPlayers > 0) { _inSetup = false; }
//        }
//    }
//}



Console.WriteLine("Finished Setup");


/*while(_isRunning)
{

}*/

/* Main Loop
    Game Setup
        * How Many Players?
        * ForEach Player
            * Player Setup
                * Name
                * Bankroll
        Setup Dealer
    Dealer gets Deck
    Dealer shuffles
    Dealer marks reshuffle point
    *Each player makes a Wager
    Each player is dealt a hand
    Dealer is dealt a hand
    All player hands fully visible.
    Dealer hand has 1 card only visible.
    (players can see all information)
    ForEach Player
        * Split
        * Double
        While Player.Hand.Total < 21
            Player choooses to Hit or Stand
            Hand Evaluate


    A Game is:
        A list of players
        A Dealer
        A deck of cards
            A deck can consist of several standard decks
            
        



*/
