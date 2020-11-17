using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.PlayerState
{
    class PaidOffDebt : IEntityComponent
    {
        public int PlayerId { get; set; }

        public PaidOffDebt(int playerId) =>
            PlayerId = playerId;
    }
}
