using System;
using System.Collections.Generic;

namespace Napilnik.GameLobby
{
    public class Room : IChatPermissionProvider
    {
        private readonly ChatHistory _chatHistory;
        private readonly PlayersStatuses _playersStatuses;
        private readonly RoomStatus _roomStatus;

        private readonly List<Player> _connectedPlayers;

        public Room(int roomCapacity)
        {
            _roomStatus = new RoomStatus(roomCapacity);
            _playersStatuses = new PlayersStatuses();
            _connectedPlayers = new List<Player>();
            _chatHistory = new ChatHistory();
        }

        internal Chat GetChat(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            
            return new Chat(player, _chatHistory, this);
        }

        public bool CanInteractWithChat(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            
            if (_connectedPlayers.Contains(player) == false)
                return false;

            return _roomStatus.InGame && _playersStatuses.IsPlayerReady(player);
        }

        public void ConnectPlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            if (_roomStatus.InGame)
                throw new InvalidOperationException();
            
            if (_connectedPlayers.Contains(player))
                throw new InvalidOperationException();

            if (player.HasConnection)
                throw new InvalidOperationException();

            if (_playersStatuses.IsPlayerReady(player))
            {
                _roomStatus.AddReadyPlayer();
            }
            else
            {
                _playersStatuses.UpdatePlayerStatus(player, PlayerVisitStatus.NotReady);
            }
            
            _connectedPlayers.Add(player);
            
            player.SetRoom(this);
        }

        public void DisconnectPlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            if (_connectedPlayers.Contains(player) == false)
                throw new InvalidOperationException();

            _connectedPlayers.Remove(player);

            if (_playersStatuses.IsPlayerReady(player))
            {
                _roomStatus.RemoveReadyPlayer();
            }
            
            player.SetRoom(null);
        }

        internal void OnPlayerReady(Player player)
        {
            if (_roomStatus.InGame)
                throw new InvalidOperationException();

            if (_playersStatuses.IsPlayerReady(player))
                return;
            
            _roomStatus.AddReadyPlayer();
            _playersStatuses.UpdatePlayerStatus(player, PlayerVisitStatus.Ready);
        }

        internal void OnPlayerNotReady(Player player)
        {
            if (_playersStatuses.IsPlayerNotReady(player))
                return;
                
            _roomStatus.RemoveReadyPlayer();
            _playersStatuses.UpdatePlayerStatus(player, PlayerVisitStatus.NotReady);
        }
    }

    public class Player
    {
        private Room _connectedRoom;

        public Player(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
            _connectedRoom = null;
            Chat = null;
        }

        public string Name { get; }

        public bool HasConnection => _connectedRoom != null;

        public bool NotConnected => HasConnection == false;

        public Chat Chat { get; private set; }

        public void Ready()
        {
            if (NotConnected)
                throw new NullReferenceException(nameof(_connectedRoom));
            
            _connectedRoom.OnPlayerReady(this);
        }

        public void NotReady()
        {
            if (NotConnected)
                throw new NullReferenceException(nameof(_connectedRoom));
            
            _connectedRoom.OnPlayerNotReady(this);
        }

        public void Disconnect()
        {
            if (NotConnected)
                throw new InvalidOperationException();

            _connectedRoom.DisconnectPlayer(this);
        }

        internal void SetRoom(Room room)
        {
            if (HasConnection)
                throw new InvalidOperationException();

            _connectedRoom = room;

            if (room != null)
            {
                Chat = room.GetChat(this);
            }
            else
            {
                Chat = null;
            }
        }
    }
}