using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.Auction;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Requests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MonopolyPreUnity.Systems.PropertySystems
{
    class AuctionSystem : ISystem
    {
        private readonly Context _context;

        public void Execute()
        {
            var info = _context.AuctionInfo();
            // if auction is not taking place
            if (info == null)
                return;

            // if auction action was not chosen
            var auctionAction = _context.GetComponentInterface<IAuctionAction>();
            if (auctionAction == null)
            {
                // send a request if there is none
                if (!_context.ContainsComponent<PlayerInputRequest>() && _context.HSInputState().IsNull)
                    _context.Add(new PlayerInputRequest(info.CurBidderId, new AuctionBidRequest()));
                return;
            }

            // do the biddery actions
            if (auctionAction is AuctionBid bid)
            {
                info.AmountBid += bid.Amount;

                _context.Add(new PrintFormattedLine($"|player:{bid.PlayerId}| bid {bid.Amount}$." +
                    $"Total amount bid: {info.AmountBid}$", OutputStream.GameLog));

                info.CurBidder = (info.CurBidder + 1) % info.BiddersLeft;
            }
            else if (auctionAction is AuctionWithdraw withdraw)
            {
                _context.Add(new PrintFormattedLine($"|player:{info.CurBidderId}| withdrew from the auction",
                    OutputStream.GameLog));

                info.BidOrder.RemoveAt(info.CurBidder);
                if (info.CurBidder >= info.BidOrder.Count)
                    info.CurBidder = 0;

                // the winning scenario 
                if (info.BiddersLeft == 1)
                {
                    _context.Add(new PrintFormattedLine($"|player:{info.CurBidderId}| won the auction!",
                        OutputStream.GameLog));

                    _context.Add(new PropertyTransferRequest(info.PropertyOnAuctionId, info.CurBidderId));
                    _context.Remove(info);
                    _context.RenderCommunications.CurTileViewLock = false;
                }
            }

            _context.Remove(auctionAction);
        }

        public AuctionSystem(Context context) =>
            _context = context;
    }
}
