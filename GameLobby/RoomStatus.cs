using System;

namespace Napilnik.GameLobby
{
    public class RoomStatus
    {
        public bool InGame { get; private set; }
        private readonly int _roomCapacity;
        private int _readyPlayers;

        public RoomStatus(int roomCapacity)
        {
            _roomCapacity = roomCapacity;
            InGame = false;
            _readyPlayers = 0;
        }

        public void AddReadyPlayer()
        {
            if (InGame)
                throw new InvalidOperationException();

            _readyPlayers += 1;

            if (_readyPlayers == _roomCapacity)
                InGame = true;
        }
        
        public void RemoveReadyPlayer()
        {
            if (_readyPlayers == 0)
                throw new InvalidOperationException();

            _readyPlayers -= 1;
        }
    }
}