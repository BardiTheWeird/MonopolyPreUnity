using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Jail
{
    class SendToJail : IJailRequest
    {
        public int PlayerId { get; set; }

        public SendToJail(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
