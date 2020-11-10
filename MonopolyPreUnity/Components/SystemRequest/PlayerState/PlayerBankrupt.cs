using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.PlayerState
{
    class PlayerBankrupt : IEntityComponent
    {
        public int PlayerId { get; set; }
        public int? CreditorId { get; set; }

        public PlayerBankrupt(int playerId, int? creditorId = null)
        {
            PlayerId = playerId;
            CreditorId = creditorId;
        }

        public PlayerBankrupt(PlayerDebt debt) : this(debt.DebtorId, debt.CreditorId) { }
    }
}
