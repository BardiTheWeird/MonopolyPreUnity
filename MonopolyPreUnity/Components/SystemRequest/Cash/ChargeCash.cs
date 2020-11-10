using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Cash
{
    class ChargeCash : ICashOperation
    {
        public int Amount { get; set; }
        public int PlayerChargedId { get; set; }
        public int? PlayerChargerId { get; set; }
        public string Message { get; set; }

        public ChargeCash(int amount, int playerChargedId, int? playerChargerId = null, string message = "")
        {
            Amount = amount;
            PlayerChargedId = playerChargedId;
            PlayerChargerId = playerChargerId;
            Message = message;
        }
    }
}
