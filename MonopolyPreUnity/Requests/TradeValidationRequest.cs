using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components.Trade;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Requests
{
    class TradeValidationRequest : IRequest
    {
        public TradeOffer TradeOffer { get; set; }

        public TradeValidationRequest(TradeOffer tradeOffer)
        {
            TradeOffer = tradeOffer;
        }
    }
}
