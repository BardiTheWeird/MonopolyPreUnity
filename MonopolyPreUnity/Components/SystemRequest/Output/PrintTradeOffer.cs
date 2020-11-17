using MonopolyPreUnity.Components.Trade;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintTradeOffer : IOutputRequest
    {
        public TradeOffer Offer { get; set; }
        public OutputStream OutputStream { get; set; }

        public PrintTradeOffer(TradeOffer offer, OutputStream outputStream)
        {
            Offer = offer;
            OutputStream = outputStream;
        }

        public static implicit operator TradeOffer(PrintTradeOffer print) => print.Offer;
    }
}
