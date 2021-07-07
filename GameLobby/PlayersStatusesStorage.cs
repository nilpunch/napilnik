using System.Collections.Generic;

namespace Napilnik.GameLobby
{
    public class PlayersStatusesStorage
    {
        private readonly HashSet<IPlayer> _readyPlayers;

        public PlayersStatusesStorage()
        {
            _readyPlayers = new HashSet<IPlayer>();
        }

        public void UpdatePlayerStatus(IPlayer player, PlayerStatus status)
        {
            if (status == PlayerStatus.NotReady && _readyPlayers.Contains(player))
            {
                _readyPlayers.Remove(player);
                return;
            }

            if (status == PlayerStatus.Ready && _readyPlayers.Contains(player) == false)
                _readyPlayers.Add(player);
        }

        public bool IsPlayerReady(IPlayer player)
        {
            return GetPlayerStatus(player) == PlayerStatus.Ready;
        }

        public bool IsPlayerNotReady(IPlayer player)
        {
            return GetPlayerStatus(player) == PlayerStatus.NotReady;
        }

        private PlayerStatus GetPlayerStatus(IPlayer player)
        {
            return _readyPlayers.Contains(player) ? PlayerStatus.Ready : PlayerStatus.NotReady;
        }
    }
}