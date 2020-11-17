using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Request;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.HSScenario
{
    class HSAuctionScenario : IHSRequestScenario
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        public void RunScenario(IRequest requestIn, Player player)
        {
            Debug.WriteLine("So we're chilling in the RunScenario for AuctionScenario atm");
            var auctionInfo = _context.AuctionInfo();
            var maxBidAmount = player.Cash - auctionInfo.AmountBid;

            _context.Add(new PrintFormattedLine($"|player:{player.Id}|, " +
                $"choose an mount to bid (write 0 to withdraw from the auction)", OutputStream.HSInputLog));

            _context.HSInputState().Set(HSState.AuctionBidChoice, player.Id);
            _context.Add(new HSIntRequest(player.Id, 0, maxBidAmount));
        }

        #region ctor
        public HSAuctionScenario(Context context) =>
            _context = context;
        #endregion
    }
}
