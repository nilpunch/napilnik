namespace Napilnik.GameLobby
{
    public interface IRoom
    {
        ILobby RelatedLobby { get; }
    }
}