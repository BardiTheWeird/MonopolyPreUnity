using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemState
{
    class PlayerLanded : IEntityComponent
    {
        public int PlayerId { get; set; }

        public PlayerLanded(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
