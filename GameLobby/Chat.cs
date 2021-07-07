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
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            
            if (chatHistory == null)
                throw new ArgumentNullException(nameof(chatHistory));
            
            if (permissionChecker == null)
                throw new ArgumentNullException(nameof(permissionChecker));

            _player = player;
            _chatHistory = chatHistory;
            _permissionChecker = permissionChecker;
        }

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

        public bool Locked => _permissionChecker.HaveChattingPermission(_player);

        public bool NotLocked => Locked == false;
    }
}