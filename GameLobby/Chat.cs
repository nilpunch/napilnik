using System;
using System.Collections.Generic;

namespace Napilnik.GameLobby
{
    public class Chat
    {
        private readonly ChatHistory _chatHistory;
        private readonly IPlayer _player;
        private readonly IChatPermissionChecker _permissionChecker;

        public Chat(IPlayer player, ChatHistory chatHistory, IChatPermissionChecker permissionChecker)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
            _chatHistory = chatHistory ?? throw new ArgumentNullException(nameof(chatHistory));
            _permissionChecker = permissionChecker ?? throw new ArgumentNullException(nameof(permissionChecker));
        }

        public bool Locked => _permissionChecker.HaveChattingPermission(_player);

        public bool NotLocked => Locked == false;

        public void Write(string message)
        {
            if (Locked)
                throw new InvalidOperationException();

            _chatHistory.Log(new Message(_player, message));
        }

        public IEnumerable<Message> Read()
        {
            if (Locked)
                throw new InvalidOperationException();

            return _chatHistory;
        }
    }
}