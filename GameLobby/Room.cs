using System;
using Napilnik.GameLobby;

namespace Napilnik.GameLobby
{
    public class Room : IRoom, IChatPermissionChecker, IDisposable
    {
        private readonly ChatHistory _chatHistory;
        private readonly PlayersStatusesStorage _playersStatuses;
        private readonly PlayersReadyCount _playersCounter;
        private readonly ILobby _relatedLobby;

        private bool _disposed;
        private bool _inGame;

        public Room(ILobby lobby, int playersCapacity)
        {
            if (lobby == null)
                throw new ArgumentNullException(nameof(lobby));
            
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

        public Chat GetChat(IPlayer player)
        {
            return new Chat(player, _chatHistory, this);
        }

        public void Dispose()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(Room));
            
            _disposed = true;
            
            _relatedLobby.PlayerLinked -= OnPlayerLinked;
            _relatedLobby.PlayerUnlinked -= OnPlayerUnlinked;
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

        public bool HaveChattingPermission(IPlayer player)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(Room));
            
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            if (_relatedLobby.IsNotLinked(player, this))
                return false;

            if (InGame)
                return _playersStatuses.IsPlayerReady(player);
            
            return true;
        }
    }
}