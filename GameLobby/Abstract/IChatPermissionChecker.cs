namespace Napilnik.GameLobby
{
    public interface IChatPermissionChecker
    {
        bool HaveChattingPermission(IPlayer player);
    }
}