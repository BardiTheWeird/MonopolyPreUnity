using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Requests
{
    class AuctionBidRequest : IRequest
    {
        public int PropertyId { get; set; }
        public int CurPrice { get; set; }

        public AuctionBidRequest(int propertyId, int curPrice)
        {
            PropertyId = propertyId;
            CurPrice = curPrice;
        }
    }
}
