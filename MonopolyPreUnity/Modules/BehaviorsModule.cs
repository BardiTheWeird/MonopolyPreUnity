﻿using Autofac;
using MonopolyPreUnity.Behaviors;
using MonopolyPreUnity.Behaviors.PlayerLanded;
using MonopolyPreUnity.Behaviors.Rent;
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
            builder.RegisterType<ActionBoxBehavior>().AsSelf().SingleInstance();
            builder.RegisterType<ActionTileBehavior>().AsSelf().SingleInstance();
            builder.RegisterType<PropertyLandedBehavior>().AsSelf().SingleInstance();
            builder.RegisterType<FreeParkingBehavior>().AsSelf().SingleInstance();
            builder.RegisterType<GOBehavior>().AsSelf().SingleInstance();

            // Rent
            builder.RegisterType<DevelopmentRentBehavior>().AsSelf().SingleInstance();
            builder.RegisterType<TrainStationRentBehavior>().AsSelf().SingleInstance();
            builder.RegisterType<UtilityRentBehavior>().AsSelf().SingleInstance();
        }
    }
}
