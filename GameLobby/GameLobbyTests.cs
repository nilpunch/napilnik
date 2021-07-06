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
            
            Player player1 = lobby.CreatePlayer("A");
            Player player2 = lobby.CreatePlayer("B");
            Player player3 = lobby.CreatePlayer("C");

            Room room1 = lobby.CreateRoom(1);
            Room room2 = lobby.CreateRoom(2);
            
            lobby.LinkPlayer(player1, room1);
            lobby.LinkPlayer(player2, room1);
            
            room1.PlayerReady(player1);
        }
    }
}