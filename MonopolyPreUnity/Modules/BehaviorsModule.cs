using Autofac;
using MonopolyPreUnity.Behaviors;
using MonopolyPreUnity.Behaviors.PlayerLanded;
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
            builder.RegisterType<ActionBoxBehavior>().AsSelf().SingleInstance();
            builder.RegisterType<ActionTileBehavior>().AsSelf().SingleInstance();
            builder.RegisterType<CollectRentBehavior>().AsSelf().SingleInstance();
            builder.RegisterType<PropertyLandedBehavior>().AsSelf().SingleInstance();
        }
    }
}
