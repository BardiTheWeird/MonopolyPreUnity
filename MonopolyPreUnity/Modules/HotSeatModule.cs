using Autofac;
using MonopolyPreUnity.RequestHandlers.HotSeatScenario;
using MonopolyPreUnity.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Modules
{
    class HotSeatModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Request Scenarios
            builder.RegisterType<HotSeatAuctionScenario>().Keyed<IHotSeatRequestScenario>(typeof(AuctionBidRequest));
            builder.RegisterType<HotSeatBankruptcyScenario>().Keyed<IHotSeatRequestScenario>(typeof(BankruptcyRequest));
            builder.RegisterType<HotSeatBuyAuctionScenario>().Keyed<IHotSeatRequestScenario>(typeof(BuyAuctionRequest));
            builder.RegisterType<HotSeatTradeValidationScenario>().Keyed<IHotSeatRequestScenario>(typeof(TradeValidationRequest));
            builder.RegisterType<HotSeatTurnScenario>().Keyed<IHotSeatRequestScenario>(typeof(TurnRequest));

            // UI
            builder.RegisterType<ConsoleUI>().AsSelf().SingleInstance();
        }
    }
}
