using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.PlayerState
{
    class PlayerDebt : IEntityComponent
    {
        public int DebtorId { get; set; }
        public int DebtAmount { get; set; }
        public int? CreditorId { get; set; }

        public PlayerDebt(int debtorId, int debtAmount, int? creditorId = null)
        {
            DebtorId = debtorId;
            DebtAmount = debtAmount;
            CreditorId = creditorId;
        }
    }
}
