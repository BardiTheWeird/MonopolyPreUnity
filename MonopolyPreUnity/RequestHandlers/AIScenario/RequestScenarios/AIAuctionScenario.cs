using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.Auction;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.AIScenario.RequestScenarios
{
    class AIAuctionScenario : IAIRequestScenario
    {
        private readonly Context _context;

        public void RunScenario(IRequest request, Player player, AiInfo aiInfo)
        {
            var auction = _context.AuctionInfo();
            if (auction.AmountBid > player.Cash)
            {
                _context.Add(new AuctionWithdraw(player.Id));
                return;
            }

            var prop = _context.GetTileComponent<Property>(auction.PropertyOnAuctionId);
            var acquisitionWeight = _context.PropertyAcquisitionSetWeight(player, prop.SetId);

            var weights = new List<(int, int)>(4);
            for (int i = 1; i <= 100; i *= 10)
            {
                var newAmountBid = auction.AmountBid + i;
                var newPriceWeight = (newAmountBid).PriceCashPow(player.Cash);
                if (newAmountBid > prop.BasePrice)
                    newPriceWeight -= (int)((newAmountBid - prop.BasePrice) * 1.5f);

                weights.Add((i, acquisitionWeight + Math.Max(newPriceWeight, 0)));
            }
            weights.Add((0, 20));

            var choice = weights.ChaosChoice(aiInfo.ChaosFactor);
            if (choice == 0)
                _context.Add(new AuctionWithdraw(player.Id));
            else
                _context.Add(new AuctionBid(player.Id, choice));
        }

        public AIAuctionScenario(Context context) =>
            _context = context;
    }
}
