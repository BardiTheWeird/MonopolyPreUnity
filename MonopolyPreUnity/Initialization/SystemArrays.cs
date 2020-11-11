using Autofac;
using Autofac.Core;
using MonopolyPreUnity.RequestHandlers.HSScenario.RequestScenarios.BuyAuction;
using MonopolyPreUnity.RequestHandlers.HSScenario.RequestScenarios.TurnScenario;
using MonopolyPreUnity.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Initialization
{
    static class SystemArrays
    {
        public static ISystem[] GetAllSystems(this Container c)
        {
            ISystem[] systems =
            {
                c.Resolve<ChangeTurnSystem>(),
                c.Resolve<TurnRequestSystem>(),

                c.Resolve<ThrowDiceSystem>(),
                c.Resolve<MoveSystem>(),

                c.Resolve<PlayerLandedSystem>(),
                c.Resolve<ActionsSystem>(),
                c.Resolve<RentSystem>(),

                c.Resolve<PlayerInputSystem>(),
                
                c.Resolve<OutputSystem>(),

                c.Resolve<HSInputSystem>(),
                
                c.Resolve<HSTurnSystem>(),
                c.Resolve<HSBuyAuctionSystem>(),

                c.Resolve<PlayerCashSystem>(),
                c.Resolve<PlayerDebtSystem>(),
                c.Resolve<PlayerBankruptSystem>(),
                c.Resolve<AssetTransferSystem>()
            };

            return systems;
        }
    }
}
