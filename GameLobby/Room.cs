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
            _chatHistory = new ChatHistory();
            _playersStatuses = new PlayersStatusesStorage();
            _playersCounter = new PlayersReadyCount(playersCapacity);

            InGame = false;
            _disposed = false;
            _relatedLobby = lobby;
            
            lobby.PlayerLinked += OnPlayerLinked;
            lobby.PlayerUnlinked += OnPlayerUnlinked;
            lobby.PlayerReady += OnPlayerReady;
            lobby.PlayerNotReady += OnPlayerNotReady;
        }

        public ILobby RelatedLobby
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException(nameof(Room));
                
                return _relatedLobby;
            }
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
            
            RelatedLobby.PlayerLinked -= OnPlayerLinked;
            RelatedLobby.PlayerUnlinked -= OnPlayerUnlinked;
            RelatedLobby.PlayerReady -= OnPlayerReady;
            RelatedLobby.PlayerNotReady -= OnPlayerNotReady;
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

        private void OnPlayerReady(IPlayer player)
        {
            if (RelatedLobby.IsNotLinked(player, this))
                return;
            
            if (InGame)
                throw new InvalidOperationException();

            if (_playersStatuses.IsPlayerReady(player))
                return;
            
            _playersStatuses.UpdatePlayerStatus(player, PlayerStatus.Ready);
            _playersCounter.AddReadyPlayer();
            
            if (_playersCounter.MaxCapacityReached)
                InGame = true;
        }

        private void OnPlayerNotReady(IPlayer player)
        {
            if (RelatedLobby.IsNotLinked(player, this))
                return;

            if (_playersStatuses.IsPlayerNotReady(player))
                return;
            
            _playersStatuses.UpdatePlayerStatus(player, PlayerStatus.NotReady);
            _playersCounter.RemoveReadyPlayer();
        }
    }
}