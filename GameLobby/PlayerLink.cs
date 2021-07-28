using System;

namespace Napilnik.GameLobby
{
    public struct PlayerLink
    {
        public PlayerLink(IPlayer player, IRoom room)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Room = room ?? throw new ArgumentNullException(nameof(room));
        }

        public IPlayer Player { get; }
        
        public IRoom Room { get; }
    }
}