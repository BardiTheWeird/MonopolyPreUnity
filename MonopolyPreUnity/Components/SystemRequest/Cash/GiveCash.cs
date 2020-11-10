using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Cash
{
    class GiveCash : ICashOperation
    {
        public int Amount { get; set; }
        public int PlayerId { get; set; }
        public string Message { get; set; }

        public GiveCash(int amount, int playerId, string message = "")
        {
            Amount = amount;
            PlayerId = playerId;
            Message = message;
        }
    }
}
