using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class AuctionInfo
    {
        public int PropertyOnAuctionId { get; set; }
        public int AmountBid { get; set; }
        public List<int> BidOrder { get; set; }
        public int CurBidder { get; set; }
        public int CurBidderId => BidOrder[CurBidder];

        public AuctionInfo(int propertyOnAuctionId, int amountBid, List<int> bidOrder, int curBidder)
        {
            PropertyOnAuctionId = propertyOnAuctionId;
            AmountBid = amountBid;
            BidOrder = bidOrder;
            CurBidder = curBidder;
        }

        public AuctionInfo()
        {
            BidOrder = new List<int>();
        }
    }
}
