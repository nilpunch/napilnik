using System;
using System.Collections.Generic;

namespace Napilnik.GameLobby
{
    public class Lobby : ILobby
    {
        public event Action<PlayerLink> PlayerLinked = delegate {  };
        public event Action<PlayerLink> PlayerUnlinked = delegate {  };
        
        public event Action<IPlayer> PlayerReady = delegate {  };
        public event Action<IPlayer> PlayerNotReady = delegate {  };

        private List<PlayerLink> _links;

        public Lobby()
        {
            _links = new List<PlayerLink>();
        }

        public void LinkPlayer(IPlayer player, IRoom room)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            
            if (room == null)
                throw new ArgumentNullException(nameof(room));

            if (player.RelatedLobby != this)
                throw new InvalidOperationException();
            
            if (room.RelatedLobby != this)
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

            if (player.RelatedLobby != this)
                throw new InvalidOperationException();
            
            if (room.RelatedLobby != this)
                throw new InvalidOperationException();
            
            return _links.Exists(link => link.Player == player && link.Room == room);
        }

        public void UnlinkPlayer(IPlayer player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            
            if (player.RelatedLobby != this)
                throw new InvalidOperationException();

            if (HaveLink(player) == false)
                throw new InvalidOperationException();

            PlayerLink playerLink = _links.Find(link => link.Player == player);
            _links.Remove(playerLink);
            PlayerUnlinked.Invoke(playerLink);
        }

        public void NotifyPlayerReady(IPlayer player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            
            if (player.RelatedLobby != this)
                throw new InvalidOperationException();
            
            PlayerReady.Invoke(player);
        }

        public void NotifyPlayerNotReady(IPlayer player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            
            if (player.RelatedLobby != this)
                throw new InvalidOperationException();
            
            PlayerNotReady.Invoke(player);
        }

        private bool HaveLink(IPlayer player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            if (player.RelatedLobby != this)
                throw new InvalidOperationException();
            
            return _links.Exists(link => link.Player == player);
        }
    }
}