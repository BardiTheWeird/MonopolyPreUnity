using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Auction
{
    class AuctionBid : IAuctionAction
    {
        public int PlayerId { get; set; }
        public int Amount { get; set; }

        public AuctionBid(int playerId, int amount)
        {
            PlayerId = playerId;
            Amount = amount;
        }
    }
}
