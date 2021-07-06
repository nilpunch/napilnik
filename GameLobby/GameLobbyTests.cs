using Napilnik.GameLobby;
using NUnit.Framework;

namespace Napilnik.GameLobby
{
    public static class GameLobbyTests
    {
        [Test]
        public static void UseCase()
        {
            Lobby lobby = new Lobby();
            
            Player player1 = new Player(lobby, "A");
            Player player2 = new Player(lobby, "B");
            Player player3 = new Player(lobby, "C");

            Room room1 = new Room(lobby, 1);
            Room room2 = new Room(lobby, 2);
        }
    }
}