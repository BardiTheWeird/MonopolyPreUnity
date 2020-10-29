using Autofac;
using MonopolyPreUnity.Behaviors;
using MonopolyPreUnity.Behaviors.PlayerLanded;
using MonopolyPreUnity.Behaviors.Rent;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MonopolyPreUnity.Modules
{
    class BehaviorsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // PropertyLanded
            builder.RegisterType<ActionBoxBehavior>().AsSelf().SingleInstance().Keyed<IPlayerLandedBehavior>(typeof(ActionBoxComponent));
            builder.RegisterType<ActionTileBehavior>().AsSelf().SingleInstance().Keyed<IPlayerLandedBehavior>(typeof(ActionTileComponent));
            builder.RegisterType<PropertyLandedBehavior>().AsSelf().SingleInstance().Keyed<IPlayerLandedBehavior>(typeof(PropertyComponent));
            builder.RegisterType<FreeParkingBehavior>().AsSelf().SingleInstance().Keyed<IPlayerLandedBehavior>(typeof(FreeParkingComponent));
            builder.RegisterType<GOBehavior>().AsSelf().SingleInstance().Keyed<IPlayerLandedBehavior>(typeof(GoComponent));

            // Rent
            builder.RegisterType<DevelopmentRentBehavior>().AsSelf().SingleInstance().Keyed<IPlayerLandedBehavior>(typeof(PropertyDevelopmentComponent));
            builder.RegisterType<TrainStationRentBehavior>().AsSelf().SingleInstance().Keyed<IPlayerLandedBehavior>(typeof(TrainStationComponent));
            builder.RegisterType<UtilityRentBehavior>().AsSelf().SingleInstance().Keyed<IPlayerLandedBehavior>(typeof(UtilityComponent));
        }
    }
}
