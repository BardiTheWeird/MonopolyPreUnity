using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.PlayerState
{
    class SendToJail : IEntityComponent
    {
        public int PlayerId { get; set; }

        public SendToJail(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
