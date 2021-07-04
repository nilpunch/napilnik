using System.Collections.Generic;

namespace Napilnik.GameLobby
{
    public class PlayersStatuses
    {
        private readonly Dictionary<Player, PlayerVisitStatus> _playerVisits;

        public PlayersStatuses()
        {
            _playerVisits = new Dictionary<Player, PlayerVisitStatus>();
        }

        public void UpdatePlayerStatus(Player player, PlayerVisitStatus status)
        {
            if (_playerVisits.ContainsKey(player))
            {
                _playerVisits[player] = status;
                return;
            }

            _playerVisits.Add(player, status);
        }

        public bool IsStatusUnchanged(Player player, PlayerVisitStatus status)
        {
            return GetPlayerStatus(player) == status;
        }

        public bool IsPlayerReady(Player player)
        {
            return GetPlayerStatus(player) == PlayerVisitStatus.Ready;
        }

        public bool IsPlayerNotReady(Player player)
        {
            return GetPlayerStatus(player) == PlayerVisitStatus.NotReady;
        }

        private PlayerVisitStatus GetPlayerStatus(Player player)
        {
            if (_playerVisits.ContainsKey(player))
            {
                return _playerVisits[player];
            }

            return PlayerVisitStatus.NotVisited;
        }
    }
}