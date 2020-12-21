using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.PlayerInput.Property;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.AIScenario.RequestScenarios
{
    class AIBuyAuctionScenario : IAIRequestScenario
    {
        private readonly Context _context;

        public void RunScenario(IRequest request, Player player, AiInfo aiInfo)
        {
            var propId = (request as BuyAuctionRequest).PropertyId;
            var prop = _context.GetTileComponent<Property>(propId);

            if (prop.BasePrice > player.Cash)
            {
                _context.Add(new StartAuction(propId));
                return;
            }

            var buyWeight = prop.BasePrice.PriceCashPow(player.Cash, offset: 1) 
                + _context.PropertyAcquisitionSetWeight(player, prop.SetId);

            var weights = new List<(MonopolyCommand, int)>
            {
                (MonopolyCommand.BuyProperty, buyWeight),
                (MonopolyCommand.AuctionProperty, 20)
            };

            if (weights.ChaosChoice(aiInfo.ChaosFactor) == MonopolyCommand.BuyProperty)
                _context.Add(new BuyProperty(player.Id, propId));
            else
                _context.Add(new StartAuction(propId));
        }

        public AIBuyAuctionScenario(Context context) =>
            _context = context;
    }
}
