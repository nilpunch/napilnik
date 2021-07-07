using System;

namespace Napilnik.GameLobby
{
    public struct PlayerLink
    {
        public PlayerLink(IPlayer player, IRoom room)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
                
            if (room == null)
                throw new ArgumentNullException(nameof(room));
                
            Player = player;
            Room = room;
        }

        public IPlayer Player { get; }
        
        public IRoom Room { get; }
    }
}