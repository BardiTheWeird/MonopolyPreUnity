using Autofac;
using MonopolyPreUnity.RequestHandlers.AIScenario.RequestScenarios;
using MonopolyPreUnity.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Modules
{
    class AIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Request Scenarios
            builder.RegisterType<AIAuctionScenario>().Keyed<IAIRequestScenario>(typeof(AuctionBidRequest));
            builder.RegisterType<AIDebtScenario>().Keyed<IAIRequestScenario>(typeof(PayOffDebtRequest));
            builder.RegisterType<AIBuyAuctionScenario>().Keyed<IAIRequestScenario>(typeof(BuyAuctionRequest));
            builder.RegisterType<AITradeValidationScenario>().Keyed<IAIRequestScenario>(typeof(TradeValidationRequest));
            builder.RegisterType<AITurnScenario>().Keyed<IAIRequestScenario>(typeof(TurnRequest));
        }
    }
}
