using Autofac;
using MonopolyPreUnity.RequestHandlers.HSScenario;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.Systems.HSInput;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Modules
{
    class HSModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Request Scenarios
            builder.RegisterType<HSAuctionScenario>().Keyed<IHSRequestScenario>(typeof(AuctionBidRequest));
            builder.RegisterType<HSBankruptcyScenario>().Keyed<IHSRequestScenario>(typeof(BankruptcyRequest));
            builder.RegisterType<HSBuyAuctionScenario>().Keyed<IHSRequestScenario>(typeof(BuyAuctionRequest));
            builder.RegisterType<HSTradeValidationScenario>().Keyed<IHSRequestScenario>(typeof(TradeValidationRequest));
            builder.RegisterType<HSTurnScenario>().Keyed<IHSRequestScenario>(typeof(TurnRequest));

            // Systems
            builder.RegisterType<HSTurnScenario>().AsSelf().SingleInstance();
            builder.RegisterType<HSBuyAuctionSystem>().AsSelf().SingleInstance();
        }
    }
}
