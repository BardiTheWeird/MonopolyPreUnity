using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.PlayerState
{
    class PlayerBusy : IEntityComponent
    {
        public int PlayerId { get; set; }

        public PlayerBusy(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
