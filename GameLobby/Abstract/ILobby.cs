using System;

namespace Napilnik.GameLobby
{
    public interface ILobby
    {
        event Action<PlayerLink> PlayerLinked;
        event Action<PlayerLink> PlayerUnlinked;
        
        bool IsLinked(IPlayer player, IRoom room);
    }

    public static class ILobbyExtensions
    {
        public static bool IsNotLinked(this ILobby lobby, IPlayer player, IRoom room)
        {
            return lobby.IsLinked(player, room) == false;
        }
    }
}