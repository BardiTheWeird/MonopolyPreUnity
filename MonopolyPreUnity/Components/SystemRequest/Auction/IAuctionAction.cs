using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Auction
{
    interface IAuctionAction : IEntityComponent
    {
        public int PlayerId { get; set; }
    }
}
