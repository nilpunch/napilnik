using System;
using System.Collections.Generic;

namespace Napilnik.GameLobby
{
    public class PlayerStatuses
    {
        private Dictionary<PlayerStatus> _playerConnections;
        private Player _player;

        public PlayerStatuses(Player player)
        {
            if (player == null)
                throw new ArgumentNullException();
            
            _player = player;
            _playerConnections = new Dictionary<PlayerStatus>();
        }
    }
}