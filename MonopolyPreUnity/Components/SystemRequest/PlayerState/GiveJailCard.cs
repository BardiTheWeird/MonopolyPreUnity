using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.PlayerState
{
    class GiveJailCard : IEntityComponent
    {
        public int PlayerId { get; set; }

        public GiveJailCard(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
