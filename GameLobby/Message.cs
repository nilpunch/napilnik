using System;

namespace Napilnik.GameLobby
{
    public class Message
    {
        private string _message;
        private IPlayer _sender;

        public Message(IPlayer sender, string message)
        {
            if (sender == null)
                throw new ArgumentNullException(nameof(sender));

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException(message);

            _sender = sender;
            _message = message;
        }

        public string Content => $"{_sender.Name}: {_message}";
    }
}