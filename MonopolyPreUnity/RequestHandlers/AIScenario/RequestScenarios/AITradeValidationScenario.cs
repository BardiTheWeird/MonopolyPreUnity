using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.Trade;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.AIScenario.RequestScenarios
{
    class AITradeValidationScenario : IAIRequestScenario
    {
        private readonly Context _context;

        public void RunScenario(IRequest request, Player player, AiInfo aiInfo)
        {
            var offer = (request as TradeValidationRequest).TradeOffer;

            // a point per 7$ of difference
            var weight = (CountCash(offer.InitiatorAssets) - CountCash(offer.ReceiverAssets)) / 7;

            // the following is really anti-DRY
            // prop acquisition weights
            var setsIn = offer.InitiatorAssets.Properties
                .GroupBy(id => _context.GetTileComponent<Property>(id).SetId);
            foreach (var set in setsIn)
            {
                var ownedSetCardinality = _context.OwnedPropertiesInSet(player, set.Key).Count();
                var inCardinality = set.Count();

                weight += SetWeightSum(ownedSetCardinality + 1, ownedSetCardinality + inCardinality);
            }

            // prop giving-away-for-free-to-stupid-players weight
            var setsOut = offer.ReceiverAssets.Properties
                .GroupBy(id => _context.GetTileComponent<Property>(id).SetId);
            foreach (var set in setsOut)
            {
                var ownedSetCardinality = _context.OwnedPropertiesInSet(player, set.Key).Count();
                var outCardinality = set.Count();

                weight -= SetWeightSum(ownedSetCardinality - outCardinality + 1, ownedSetCardinality);
            }

            int isPositive = Convert.ToInt32(weight > 0);
            weight = Math.Abs(weight);
            var weights = new List<(MonopolyCommand, int)>
            {
                (MonopolyCommand.AcceptOffer, isPositive * weight),
                (MonopolyCommand.DeclineOffer, (1 - isPositive) * weight)
            };

            if (weights.ChaosChoice(aiInfo.ChaosFactor) == MonopolyCommand.AcceptOffer)
                _context.Add(new TradeAccept());
            else
                _context.Add(new TradeRefuse());
        }

        int CountCash(PlayerAssets assets)
        {
            var config = _context.GameConfig();

            var cash = assets.Cash;
            cash += assets.JailCards * 50;

            foreach (var propId in assets.Properties)
            {
                var prop = _context.GetTileComponent<Property>(propId);
                var price = prop.BasePrice;
                if (prop.IsMortgaged)
                    cash += (int)(prop.BasePrice * config.MortgageFee);
                else
                    cash += prop.BasePrice;
            }

            return cash;
        }

        int SetWeightSum(int start, int finish)
        {
            var sum = 0;
            if (start == 0)
            {
                sum += 5;
                start++;
            }

            int alebraicSum(int a1, int d, int n)
            {
                int an = a1 + d * (n - 1);
                return n / 2 * (a1 + an);
            }

            int a1 = 10;
            int d = 10;

            return sum + alebraicSum(a1, d, finish) - alebraicSum(a1, d, start - 1);
        }

        public AITradeValidationScenario(Context context) =>
            _context = context;
    }
}
