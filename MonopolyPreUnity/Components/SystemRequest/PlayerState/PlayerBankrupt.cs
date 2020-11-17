using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.PlayerState
{
    class PlayerBankrupt : IEntityComponent
    {
        public int PlayerId { get; set; }

        public PlayerBankrupt(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
