namespace Napilnik.GameLobby
{
    public class Player : IPlayer
    {
        public Player(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}