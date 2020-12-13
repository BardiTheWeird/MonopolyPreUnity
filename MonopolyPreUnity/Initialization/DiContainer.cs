﻿using Autofac;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Modules;
using MonopolyPreUnity.RequestHandlers;
using MonopolyPreUnity.RequestHandlers.AIScenario;
using MonopolyPreUnity.RequestHandlers.HSScenario;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace MonopolyPreUnity.Initialization
{
    static class DiContainer
    {
        #region register scenarios
        public static void RegisterPlayerScenarios(ContainerBuilder builder, IEnumerable<(Player, ChaosFactor)> players)
        {
            foreach (var player in players)
            {
                if (player.Item2 == null)
                {
                    builder.RegisterType<HSScenario>().Keyed<IPlayerScenario>(player.Item1.Id)
                        .WithParameter(new TypedParameter(typeof(Player), player.Item1));
                }
                else
                {
                    var parameters = new List<Autofac.Core.Parameter> 
                    {
                        new TypedParameter(typeof(Player), player.Item1),
                        new TypedParameter(typeof(AiInfo), new AiInfo(player.Item2))
                    };
                    builder.RegisterType<AIScenario>().Keyed<IPlayerScenario>(player.Item1.Id)
                        .WithParameters(parameters);
                }
            }
        }
        #endregion

        public static IContainer CreateDiContainer(this Context context)
        {
            var builder = new ContainerBuilder();

            context.Add(new HSInputState());
            builder.RegisterInstance(context);

            var playersToRegister = context.GetComponents<Player>()
                .Select<Player, (Player, ChaosFactor)>(x => (x, null)).ToList();

            var chaosFactors = new List<ChaosFactor> { new ChaosFactor(0), new ChaosFactor(0) };

            RegisterPlayerScenarios(builder, playersToRegister.Concat(AddAIPlayers(context, chaosFactors)));

            builder.RegisterModule<BehaviorsModule>();
            builder.RegisterModule<HSModule>();
            builder.RegisterModule<SystemsModule>();
            builder.RegisterModule<AIModule>();

            return builder.Build();
        }

        public static IEnumerable<(Player, ChaosFactor)> AddAIPlayers(Context context, List<ChaosFactor> chaosFactors)
        {
            var outList = new List<(Player, ChaosFactor)>(chaosFactors.Count);
            int id = context.GetComponents<Player>().Max(x => x.Id) + 1;

            for (int i = 0; i < chaosFactors.Count(); i++, id++)
            {
                var player = new Player(id, $"AIPlayer{i + 1}");
                player.Cash = 200;
                player.CurTileId = 1;

                context.Add(player);
                outList.Add((player, chaosFactors[i]));
            }

            return outList;
        }
    }
}
