using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintCashGive : IOutputRequest
    {
        public int PlayerId { get; set; }
        public int Amount { get; set; }
        public string Message { get; set; }
        public OutputStream OutputStream { get; set; }

        public PrintCashGive(int playerId, int amount, string message = "",
            OutputStream outputStream = OutputStream.GameLog)
        {
            PlayerId = playerId;
            Amount = amount;
            Message = message;
            OutputStream = outputStream;
        }
    }
}
