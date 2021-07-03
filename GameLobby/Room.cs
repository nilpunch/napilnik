using System;
using System.Collections.Generic;

namespace Napilnik.GameLobby
{
    public enum RoomStatus
    {
        WaitForPlayers,
        InGame,
    }
    
    public class Room
    {
        private readonly Lobby _lobby;
        private readonly int _playersCapacity;

        private readonly List<Player> _connectedPlayers;
        
        public Room(Lobby lobby, int playersCapacity)
        {
            if (playersCapacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(playersCapacity));

            Status = RoomStatus.WaitForPlayers;
            _lobby = lobby;
            _playersCapacity = playersCapacity;
            _connectedPlayers = new List<Player>();
        }

        public RoomStatus Status { get; }

        private void ConnectPlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            
            if (_lobby.HasConnection(player))
                throw new InvalidOperationException("Connection already exists");
            
            
        }
        
        private void AddPlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            
            if (Status == RoomStatus.InGame)
                throw new InvalidOperationException();
        }

        private void DisconnectPlayer(Player player)
        {
            
        }
    }

    public enum PlayerStatus
    {
        Ready,
        NotReady,
    }
}