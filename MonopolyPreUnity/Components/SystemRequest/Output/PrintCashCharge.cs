using MonopolyPreUnity.Components.SystemRequest.Cash;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintCashCharge : IOutputRequest
    {
        public int Amount { get; set; }
        public int PlayerChargedId { get; set; }
        public int? PlayerChargerId { get; set; }
        public string Message { get; set; }

        public PrintCashCharge(int amount, int playerChargedId, int? playerChargerId, string message)
        {
            Amount = amount;
            PlayerChargedId = playerChargedId;
            PlayerChargerId = playerChargerId;
            Message = message;
        }

        public PrintCashCharge(ChargeCash chargeCash)
        {
            Amount = chargeCash.Amount;
            PlayerChargedId = chargeCash.PlayerChargedId;
            PlayerChargerId = chargeCash.PlayerChargerId;
            Message = chargeCash.Message;
        }
    }
}
