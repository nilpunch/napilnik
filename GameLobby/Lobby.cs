using System;
using System.Collections.Generic;

namespace Napilnik.GameLobby
{
    public struct PlayerConnection
    {
        public Player Player { get; }
        public Room Room { get; }

        private PlayerConnection(Player player, Room room)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            
            if (room == null)
                throw new ArgumentNullException(nameof(room));
            
            Player = player;
            Room = room;
        }

        internal static PlayerConnection Create(Player player, Room room)
        {
            return new PlayerConnection(player, room);
        }
    }
    
    public class Lobby
    {
        private readonly List<PlayerConnection> _uniqueConnections;
        
        public Lobby()
        {
            _uniqueConnections = new List<PlayerConnection>();
        }

        public void RegisterConnection(Player player, Room room)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            
            if (room == null)
                throw new ArgumentNullException(nameof(room));

            if (HasConnection(player))
                throw new InvalidOperationException("Player actually connected");
            
            _uniqueConnections.Add(PlayerConnection.Create(player, room));
        }

        public bool HasConnection(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            
            return _uniqueConnections.Exists(connection => connection.Player == player);
        }
        
        public void UnregisterConnection(Player player)
        {
            if (HasConnection(player) == false)
                throw new InvalidOperationException();
            
            _uniqueConnections.RemoveAll(connection => connection.Player == player);
        }

        public Room GetConnectedRoom()
        {
            
        }
    }
}