using Autofac;
using MonopolyPreUnity.Systems;
using MonopolyPreUnity.Systems.PropertySystems;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Modules
{
    class SystemsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // player state
            builder.RegisterType<AssetTransferSystem>().AsSelf().SingleInstance();
            builder.RegisterType<PlayerBankruptSystem>().AsSelf().SingleInstance();
            builder.RegisterType<PlayerCashSystem>().AsSelf().SingleInstance();
            builder.RegisterType<PlayerDebtSystem>().AsSelf().SingleInstance();

            // property systems
            builder.RegisterType<AuctionStartSystem>().AsSelf().SingleInstance();
            builder.RegisterType<BuyPropertySystem>().AsSelf().SingleInstance();

            // rest (not api)
            builder.RegisterType<ActionsSystem>().AsSelf().SingleInstance();
            builder.RegisterType<ChangeTurnSystem>().AsSelf().SingleInstance();
            builder.RegisterType<HSInputSystem>().AsSelf().SingleInstance();
            builder.RegisterType<MoveSystem>().AsSelf().SingleInstance();
            builder.RegisterType<OutputSystem>().AsSelf().SingleInstance();
            builder.RegisterType<PlayerInputSystem>().AsSelf().SingleInstance();
            builder.RegisterType<PlayerLandedSystem>().AsSelf().SingleInstance();
            builder.RegisterType<RentSystem>().AsSelf().SingleInstance();
            builder.RegisterType<TurnRequestSystem>().AsSelf().SingleInstance();
        }
    }
}
