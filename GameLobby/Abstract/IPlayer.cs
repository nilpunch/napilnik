namespace Napilnik.GameLobby
{
    public interface IPlayer
    {
        ILobby RelatedLobby { get; }
    }
}