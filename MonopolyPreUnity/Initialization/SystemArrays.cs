using Autofac;
using Autofac.Core;
using MonopolyPreUnity.RequestHandlers.HSScenario.RequestScenarios.BuyAuction;
using MonopolyPreUnity.RequestHandlers.HSScenario.RequestScenarios.TurnScenario;
using MonopolyPreUnity.Systems;
using MonopolyPreUnity.Systems.PropertySystems;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Initialization
{
    static class SystemArrays
    {
        public static ISystem[] GetAllSystems(this IContainer c)
        {
            ISystem[] systems =
            {
                c.Resolve<PlayerInputSystem>(),

                c.Resolve<OutputSystem>(),

                c.Resolve<HSInputSystem>(),

                c.Resolve<HSTurnSystem>(),
                c.Resolve<HSBuyAuctionSystem>(),

                c.Resolve<BuyPropertySystem>(),
                c.Resolve<AuctionStartSystem>(),

                c.Resolve<ThrowDiceSystem>(),
                c.Resolve<MoveSystem>(),

                c.Resolve<PlayerLandedSystem>(),
                c.Resolve<ActionsSystem>(),
                c.Resolve<RentSystem>(),

                c.Resolve<PlayerCashSystem>(),
                c.Resolve<PropertyTransferSystem>(),
                c.Resolve<PlayerDebtSystem>(),
                c.Resolve<PlayerBankruptSystem>(),
                c.Resolve<AssetTransferSystem>(),
                
                c.Resolve<ChangeTurnSystem>(),
                c.Resolve<TurnRequestSystem>(),
            };

            return systems;
        }

        public static void Execute(this ISystem[] systems)
        {
            foreach (var system in systems)
                system.Execute();
        }
    }
}
