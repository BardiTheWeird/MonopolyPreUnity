using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Requests
{
    class TradeValidationRequest : IRequest
    {
        public TradeOffer tradeOffer { get; set; }

        public TradeValidationRequest(TradeOffer tradeOffer)
        {
            this.tradeOffer = tradeOffer;
        }
    }
}
