using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Auction
{
    class AuctionWithdraw : IAuctionAction
    {
        public int PlayerId { get; set; }

        public AuctionWithdraw(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
