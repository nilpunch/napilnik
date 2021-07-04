using System;
using System.Collections;
using System.Collections.Generic;

namespace Napilnik.GameLobby
{
    public class ChatHistory : IEnumerable<Message>
    {
        private readonly List<Message> _logHistory;

        public ChatHistory()
        {
            _logHistory = new List<Message>();
        }
        
        internal void Log(Message message)
        {
            _logHistory.Add(message);
        }

        public IEnumerator<Message> GetEnumerator()
        {
            return _logHistory.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    
    public class Chat
    {
        private readonly ChatHistory _chatHistory;
        private readonly Player _player;
        private readonly IChatPermissionProvider _permissionProvider;

        public Chat(Player player, ChatHistory chatHistory, IChatPermissionProvider permissionProvider)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            if (chatHistory == null)
                throw new ArgumentNullException(nameof(chatHistory));
            if (permissionProvider == null)
                throw new ArgumentNullException(nameof(permissionProvider));
            
            _player = player;
            _chatHistory = chatHistory;
            _permissionProvider = permissionProvider;
        }

        public void Write(string message)
        {
            if (PlayerNotHavePermission)
                throw new InvalidOperationException();

            _chatHistory.Log(new Message(_player, message));
        }

        public IEnumerable<Message> Read()
        {
            foreach (var message in _chatHistory)
            {
                if (PlayerNotHavePermission)
                    throw new InvalidOperationException();
                
                yield return message;
            }
        }

        public bool PlayerHavePermission => _permissionProvider.CanInteractWithChat(_player);

        private bool PlayerNotHavePermission => PlayerHavePermission == false;
    }
}