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
        public OutputStream OutputStream { get; set; }

        public PrintCashCharge(int amount, int playerChargedId, int? playerChargerId, string message,
            OutputStream outputStream = OutputStream.GameLog)
        {
            Amount = amount;
            PlayerChargedId = playerChargedId;
            PlayerChargerId = playerChargerId;
            Message = message;
            OutputStream = outputStream;
        }

        public PrintCashCharge(ChargeCash chargeCash) : 
            this(chargeCash.Amount, chargeCash.PlayerChargedId, 
                chargeCash.PlayerChargerId, chargeCash.Message) { }
    }
}
