using System;
using NUnit.Framework;

namespace Napilnik.GameLobby
{
    public static class GameLobbyTests
    {
        // Not good formatted code
        [Test]
        public static void UseCase()
        {
            Lobby lobby = new Lobby();
            
            Player player1 = lobby.CreatePlayer("A");
            Player player2 = lobby.CreatePlayer("B");

            Room room1 = lobby.CreateRoom(1);
            Room room2 = lobby.CreateRoom(2);
            
            lobby.LinkPlayer(player1, room1);
            lobby.LinkPlayer(player2, room1);
            
            room1.PlayerReady(player1);
            
            if (room1.InGame == false)
                room1.PlayerReady(player2);

            if (lobby.HaveLink(player2) == false && room2.InGame == false)
                lobby.LinkPlayer(player2, room2);

            Chat chat11 = room1.GetChat(player1);

            if (chat11.NotLocked)
            {
                chat11.Write("Hello");
                chat11.Write("Somebody there?");
            }
            
            if (chat11.NotLocked)
            {
                foreach (var message in chat11.Read())
                    Console.WriteLine(message.Content);
            }
            
            room1.Dispose();
            room2.Dispose();
        }
    }
}