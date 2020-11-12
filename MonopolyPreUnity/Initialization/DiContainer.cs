using Autofac;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Modules;
using MonopolyPreUnity.RequestHandlers;
using MonopolyPreUnity.RequestHandlers.HSScenario;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MonopolyPreUnity.Initialization
{
    static class DiContainer
    {
        #region register scenarios
        public static void RegisterPlayerScenarios(ContainerBuilder builder, List<Player> players)
        {
            foreach (var player in players)
            {
                builder.RegisterType<HSScenario>().Keyed<IPlayerScenario>(player.Id)
                    .WithParameter(new TypedParameter(typeof(Player), player));
            }
        }
        #endregion

        public static IContainer CreateDiContainer(this Context context)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(context);

            RegisterPlayerScenarios(builder, context.GetComponents<Player>());

            builder.RegisterModule<BehaviorsModule>();
            builder.RegisterModule<HSModule>();
            builder.RegisterModule<SystemsModule>();

            return builder.Build();
        }
    }
}
