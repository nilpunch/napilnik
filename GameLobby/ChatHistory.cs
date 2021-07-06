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
}