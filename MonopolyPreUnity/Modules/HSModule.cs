using Autofac;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.RequestHandlers.HSScenario;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.Systems;
using MonopolyPreUnity.Systems.HSInput;
using MonopolyPreUnity.Systems.HSInput.Behaviors;
using MonopolyPreUnity.Systems.HSInput.Behaviors.Debt;
using MonopolyPreUnity.Systems.HSInput.Behaviors.Trade;
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
            builder.RegisterType<HSDebtScenario>().Keyed<IHSRequestScenario>(typeof(PayOffDebtRequest));
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

            builder.RegisterType<HSTradeChoosePlayerBehavior>().Keyed<IHSStateBehavior>(HSState.TradeChoosePlayer);
            builder.RegisterType<HSTradeChooseActionBehavior>().Keyed<IHSStateBehavior>(HSState.TradeChooseAction);
            builder.RegisterType<HSChangeAssetsBehavior>().Keyed<IHSStateBehavior>(HSState.TradeChangeAssets);
            builder.RegisterType<HSTradeChooseCashBehavior>().Keyed<IHSStateBehavior>(HSState.TradeChooseCash);
            builder.RegisterType<HSTradeChooseJailCardsBehavior>().Keyed<IHSStateBehavior>(HSState.TradeChooseJailCards);
            builder.RegisterType<HSTradeChoosePropertiesBehavior>().Keyed<IHSStateBehavior>(HSState.TradeChooseProperties);
            builder.RegisterType<HSTradeValidationBehavior>().Keyed<IHSStateBehavior>(HSState.TradeValidation);

            builder.RegisterType<HSDebtBehavior>().Keyed<IHSStateBehavior>(HSState.Debt);
            builder.RegisterType<HSDebtChoosePropertyBehavior>().Keyed<IHSStateBehavior>(HSState.DebtChooseProperty);
            builder.RegisterType<HSDebtChoosePropertyActionBehavior>().Keyed<IHSStateBehavior>(HSState.DebtChoosePropertyAction);
        }
    }
}
