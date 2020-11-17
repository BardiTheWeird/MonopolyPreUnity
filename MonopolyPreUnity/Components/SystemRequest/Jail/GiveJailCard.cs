using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Jail
{
    class GiveJailCard : IJailRequest
    {
        public int PlayerId { get; set; }

        public GiveJailCard(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
