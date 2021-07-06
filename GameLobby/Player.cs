namespace Napilnik.GameLobby
{
    public class Player : IPlayer
    {
        public Player(Lobby lobby, string name)
        {
            Name = name;
            RelatedLobby = lobby;
        }

        public string Name { get; }

        public ILobby RelatedLobby { get; }
    }
}