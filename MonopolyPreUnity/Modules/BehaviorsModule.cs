﻿using Autofac;
using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Behaviors;
using MonopolyPreUnity.Behaviors.Action;
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
            builder.RegisterType<ActionBoxBehavior>().AsSelf().SingleInstance().Keyed<IPlayerLandedBehavior>(typeof(ActionBox));
            builder.RegisterType<ActionTileBehavior>().AsSelf().SingleInstance().Keyed<IPlayerLandedBehavior>(typeof(ActionTile));
            builder.RegisterType<PropertyLandedBehavior>().AsSelf().SingleInstance().Keyed<IPlayerLandedBehavior>(typeof(Property));
            builder.RegisterType<FreeParkingBehavior>().AsSelf().SingleInstance().Keyed<IPlayerLandedBehavior>(typeof(FreeParking));
            builder.RegisterType<JailLandedBehavior>().AsSelf().SingleInstance().Keyed<IPlayerLandedBehavior>(typeof(Jail));

            // Rent
            builder.RegisterType<DevelopmentRentBehavior>().AsSelf().SingleInstance().Keyed<IRentBehavior>(typeof(PropertyDevelopment));
            builder.RegisterType<TrainStationRentBehavior>().AsSelf().SingleInstance().Keyed<IRentBehavior>(typeof(TrainStation));
            builder.RegisterType<UtilityRentBehavior>().AsSelf().SingleInstance().Keyed<IRentBehavior>(typeof(UtilityProperty));

            // Action
            builder.RegisterType<ChangeBalanceActionBehavior>().AsSelf().SingleInstance().Keyed<IActionBehavior>(typeof(ChangeBalanceAction));
            builder.RegisterType<GiftFromPlayersActionBehavior>().AsSelf().SingleInstance().Keyed<IActionBehavior>(typeof(GiftFromPlayersAction));
            builder.RegisterType<GoToJailActionBehavior>().AsSelf().SingleInstance().Keyed<IActionBehavior>(typeof(GoToJailAction));
            builder.RegisterType<GoToTileComponentActionBehavior>().AsSelf().SingleInstance().Keyed<IActionBehavior>(typeof(GoToTileComponentAction));
            builder.RegisterType<GoToTileIdActionBehavior>().AsSelf().SingleInstance().Keyed<IActionBehavior>(typeof(GoToTileIdAction));
            builder.RegisterType<JailCardActionBehavior>().AsSelf().SingleInstance().Keyed<IActionBehavior>(typeof(JailCardAction));
            builder.RegisterType<TaxPerHouseActionBehavior>().AsSelf().SingleInstance().Keyed<IActionBehavior>(typeof(TaxPerHouseAction));
        }
    }
}
