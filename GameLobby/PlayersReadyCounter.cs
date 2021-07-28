using System;

namespace Napilnik.GameLobby
{
    public class PlayersReadyCounter
    {
        private readonly int _playersCapacity;
        private int _readyPlayers;

        public PlayersReadyCounter(int playersCapacity)
        {
            if (playersCapacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(playersCapacity));
            
            _playersCapacity = playersCapacity;
            _readyPlayers = 0;
        }

        public bool MaxCapacityReached => _readyPlayers == _playersCapacity;

        public void AddReadyPlayer()
        {
            if (MaxCapacityReached)
                throw new InvalidOperationException();

            _readyPlayers += 1;
        }
        
        public void RemoveReadyPlayer()
        {
            if (_readyPlayers == 0)
                throw new InvalidOperationException();

            _readyPlayers -= 1;
        }
    }
}