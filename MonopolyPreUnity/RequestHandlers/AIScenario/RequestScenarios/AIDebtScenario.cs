using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.AIScenario.RequestScenarios
{
    class AIDebtScenario : IAIRequestScenario
    {
        private readonly Context _context;

        public void RunScenario(IRequest request, Player player, AiInfo aiInfo)
        {
            var debtLeft = (request as PayOffDebtRequest).DebtAmount - player.Cash;
            var config = _context.GameConfig();

            // cycles while there is still any debt left
            while (true)
            {
                var availableProperties = player.Properties
                    .Where(propId => !_context.GetTileComponent<Property>(propId).IsMortgaged)
                    .GroupBy(propId => {
                        var dev = _context.GetTileComponent<PropertyDevelopment>(propId);
                        if (dev == null)
                            return false;
                        if (dev.HousesBuilt == 0)
                            return false;
                        return true;
                        })
                    .OrderByDescending(group => group.Key);

                // separate groups for withDev and without
                foreach (var group in availableProperties)
                {
                    foreach (var propId in group)
                    {
                        var prop = _context.GetTileComponent<Property>(propId);

                        // sell all the houses you can
                        if (group.Key == true)
                        {
                            var dev = _context.GetTileComponent<PropertyDevelopment>(propId);
                            while (_context.CanSellHouse(player, propId))
                            {
                                _context.SellHouse(player, propId);
                                debtLeft -= dev.HouseSellPrice;
                                if (debtLeft <= 0)
                                {
                                    _context.Add(new PaidOffDebt(player.Id));
                                    return;
                                }
                            }
                        }

                        if (_context.CanMortgage(player, propId))
                        {
                            _context.Mortgage(player, propId);
                            debtLeft -= (int)(prop.BasePrice * config.MortgageFee);
                            if (debtLeft <= 0)
                            {
                                _context.Add(new PaidOffDebt(player.Id));
                                return;
                            }
                        }
                    }
                }
            }
        }

        public AIDebtScenario(Context context) =>
            _context = context;
    }
}
