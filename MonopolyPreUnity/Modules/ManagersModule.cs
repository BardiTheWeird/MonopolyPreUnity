using Autofac;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Modules
{
    class ManagersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameManager>().AsSelf().SingleInstance();

            builder.RegisterType<PlayerManager>().AsSelf().SingleInstance()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<MoveManager>().AsSelf().SingleInstance();
            builder.RegisterType<RequestManager>().AsSelf().SingleInstance();
            builder.RegisterType<PropertyManager>().AsSelf().SingleInstance();
            //builder.RegisterType<TradeManager>().AsSelf().SingleInstance();

            builder.RegisterType<PlayerLandedManager>().AsSelf().SingleInstance();
            builder.RegisterType<RentManager>().AsSelf().SingleInstance();
            builder.RegisterType<TileManager>().AsSelf().SingleInstance();
            builder.RegisterType<MapManager>().AsSelf().SingleInstance();
            builder.RegisterType<PropertyTransferManager>().AsSelf().SingleInstance();

            builder.RegisterType<AuctionManager>().AsSelf().SingleInstance();
            builder.RegisterType<ActionManager>().AsSelf().SingleInstance();
        }
    }
}
