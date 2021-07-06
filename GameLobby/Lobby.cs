using System;
using System.Collections.Generic;

namespace Napilnik.GameLobby
{
    public class Lobby : ILobby
    {
        public event Action<PlayerLink> PlayerLinked = delegate {  };
        public event Action<PlayerLink> PlayerUnlinked = delegate {  };

        private List<PlayerLink> _links;

        private HashSet<IPlayer> _registeredPlayers = new HashSet<IPlayer>();
        private HashSet<IRoom> _registeredRooms = new HashSet<IRoom>();
        
        public Lobby()
        {
            _links = new List<PlayerLink>();
        }

        public Player CreatePlayer(string name)
        {
            Player player = new Player(name);
            _registeredPlayers.Add(player);
            return player;
        }

        public Room CreateRoom(int playersCapacity)
        {
            Room room = new Room(this, playersCapacity);
            _registeredRooms.Add(room);
            return room;
        }

        public void LinkPlayer(IPlayer player, IRoom room)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            
            if (room == null)
                throw new ArgumentNullException(nameof(room));

            if (_registeredPlayers.Contains(player) == false)
                throw new InvalidOperationException();
            
            if (_registeredRooms.Contains(room) == false)
                throw new InvalidOperationException();
            
            if (HaveLink(player))
                throw new InvalidOperationException();

            PlayerLink playerLink = new PlayerLink(player, room);
            _links.Add(playerLink);
            PlayerLinked.Invoke(playerLink);
        }

        public bool IsLinked(IPlayer player, IRoom room)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            
            if (room == null)
                throw new ArgumentNullException(nameof(room));

            if (_registeredPlayers.Contains(player) == false)
                throw new InvalidOperationException();
            
            if (_registeredRooms.Contains(room) == false)
                throw new InvalidOperationException();
            
            return _links.Exists(link => link.Player == player && link.Room == room);
        }

        public void UnlinkPlayer(IPlayer player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            
            if (_registeredPlayers.Contains(player) == false)
                throw new InvalidOperationException();

            if (HaveLink(player) == false)
                throw new InvalidOperationException();

            PlayerLink playerLink = _links.Find(link => link.Player == player);
            _links.Remove(playerLink);
            PlayerUnlinked.Invoke(playerLink);
        }

        private bool HaveLink(IPlayer player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            if (_registeredPlayers.Contains(player) == false)
                throw new InvalidOperationException();
            
            return _links.Exists(link => link.Player == player);
        }
    }
}