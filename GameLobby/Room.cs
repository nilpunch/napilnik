using System;
using Napilnik.GameLobby;

namespace Napilnik.GameLobby
{
    public class Room : IRoom, IDisposable
    {
        private readonly ChatHistory _chatHistory;
        private readonly PlayersStatusesStorage _playersStatuses;
        private readonly PlayersReadyCount _playersCounter;

        private bool _disposed;
        private bool _inGame;
        private readonly ILobby _relatedLobby;

        public Room(ILobby lobby, int playersCapacity)
        {
            if (lobby == null)
                throw new ArgumentNullException(nameof(lobby));
            
            if (playersCapacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(playersCapacity));
            
            _chatHistory = new ChatHistory();
            _playersStatuses = new PlayersStatusesStorage();
            _playersCounter = new PlayersReadyCount(playersCapacity);

            InGame = false;
            _disposed = false;
            _relatedLobby = lobby;
            
            lobby.PlayerLinked += OnPlayerLinked;
            lobby.PlayerUnlinked += OnPlayerUnlinked;
        }

        public bool InGame
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException(nameof(Room));

                return _inGame;
            }
            private set
            {
                if (_disposed)
                    throw new ObjectDisposedException(nameof(Room));
                
                _inGame = value;
            }
        }

        public void Dispose()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(Room));
            
            _disposed = true;
            
            _relatedLobby.PlayerLinked -= OnPlayerLinked;
            _relatedLobby.PlayerUnlinked -= OnPlayerUnlinked;
        }

        private void OnPlayerLinked(PlayerLink playerLink)
        {
            if (playerLink.Room != this)
                return;

            if (InGame)
                throw new InvalidOperationException();

            if (_playersStatuses.IsPlayerNotReady(playerLink.Player))
                return;
            
            _playersCounter.AddReadyPlayer();
            
            if (_playersCounter.MaxCapacityReached)
                InGame = true;
        }

        private void OnPlayerUnlinked(PlayerLink playerLink)
        {
            if (playerLink.Room != this)
                return;

            if (_playersStatuses.IsPlayerReady(playerLink.Player))
                _playersCounter.RemoveReadyPlayer();
        }

        public void PlayerReady(IPlayer player)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(Room));
            
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            if (_relatedLobby.IsNotLinked(player, this))
                throw new InvalidOperationException();
            
            if (InGame)
                throw new InvalidOperationException();

            if (_playersStatuses.IsPlayerReady(player))
                return;
            
            _playersStatuses.UpdatePlayerStatus(player, PlayerStatus.Ready);
            _playersCounter.AddReadyPlayer();
            
            if (_playersCounter.MaxCapacityReached)
                InGame = true;
        }

        public void PlayerNotReady(IPlayer player)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(Room));
            
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            if (_relatedLobby.IsNotLinked(player, this))
                throw new InvalidOperationException();

            if (_playersStatuses.IsPlayerNotReady(player))
                return;
            
            _playersStatuses.UpdatePlayerStatus(player, PlayerStatus.NotReady);
            _playersCounter.RemoveReadyPlayer();
        }
    }
}