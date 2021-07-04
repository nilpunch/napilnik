namespace Napilnik.GameLobby
{
    public interface IChatPermissionProvider
    {
        bool CanInteractWithChat(Player player);
    }
}