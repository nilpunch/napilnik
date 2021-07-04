using Napilnik.GameLobby;
using NUnit.Framework;

namespace Napilnik
{
    public static class GameLobbyTests
    {
        [Test]
        public static void UseCase()
        {
            Player player1 = new Player("A");
            Player player2 = new Player("B");
            Player player3 = new Player("C");

            Room room1 = new Room(1);
            Room room2 = new Room(2);
            
            

            room1.ConnectPlayer(player1);
            room1.ConnectPlayer(player2);
            
            player1.SetRoom(null);
            
            player1.Chat.Write();
        }
    }
}