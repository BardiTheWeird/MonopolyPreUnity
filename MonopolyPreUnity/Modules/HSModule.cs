using Autofac;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.RequestHandlers.HSScenario;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.Systems;
using MonopolyPreUnity.Systems.HSInput;
using MonopolyPreUnity.Systems.HSInput.Behaviors;
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
            builder.RegisterType<HSInputSystem>().AsSelf().SingleInstance();

            // State Behaviors
            builder.RegisterType<HSTurnBehavior>().Keyed<IHSStateBehavior>(HSState.TurnChoice);
            
            builder.RegisterType<HSChoosePropertyToManageBehavior>().Keyed<IHSStateBehavior>(HSState.PropManageChooseProperty);
            builder.RegisterType<HSChoosePropertyActionBehavior>().Keyed<IHSStateBehavior>(HSState.PropManageChooseAction);

            builder.RegisterType<HSBuyAuctionBehavior>().Keyed<IHSStateBehavior>(HSState.BuyAuctionChoice);
            builder.RegisterType<HSAuctionBidBehavior>().Keyed<IHSStateBehavior>(HSState.AuctionBidChoice);
        }
    }
}
