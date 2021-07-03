using System;
using System.Collections.Generic;

namespace Napilnik.GameLobby
{
    public class Player
    {
        private readonly Dictionary<Room, PlayerStatus> _savedStates;
        
        public Player()
        {
            _savedStates = new Dictionary<Room, PlayerStatus>();
        }

        
    }
}