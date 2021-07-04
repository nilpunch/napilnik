using System;

namespace Napilnik.GameLobby
{
    public class Message
    {
        private string _message;
        private Player _sender;

        public string Content => $"{_sender.Name}: {_message}";

        public Message(Player sender, string message)
        {
            if (sender == null)
                throw new ArgumentNullException(nameof(sender));
            
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException(message);
            
            _sender = sender;
            _message = message;
        }
    }
}