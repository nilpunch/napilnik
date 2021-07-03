using NUnit.Framework;

namespace Napilnik.GameLobby
{
    public static class GameLobbyTests
    {
        [Test]
        public static void UseCase()
        {
            Lobby lobby = new Lobby();

            Player player1 = new Player(lobby);
            Player player2 = new Player(lobby);
            Player player3 = new Player(lobby);

            Room room1 = new Room(1);
            Room room2 = new Room(2);
            
            
            player1.ConnectTo(room1);
            player2.ConnectTo(room1);
            
            player1.Ready();
            player2.Ready();
        }
    }
}