using Autofac;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Modules;
using MonopolyPreUnity.RequestHandlers;
using MonopolyPreUnity.RequestHandlers.HotSeatScenario;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MonopolyPreUnity.Initialization
{
    static class InitContainerBuilder
    {
        #region register scenarios
        public static void RegisterPlayerScenarios(ContainerBuilder builder, List<Player> players)
        {
            foreach (var player in players)
            {
                builder.RegisterType<HotSeatScenario>().Keyed<IPlayerScenario>(player.Id)
                    .WithParameter(new TypedParameter(typeof(Player), player));
            }
        }
        #endregion

        #region config
        public static GameConfig ReadGameConfig(string filePath)
        {
            var serializer = new XmlSerializer(typeof(GameConfig));
            var textReader = new StreamReader(filePath);

            return (GameConfig)serializer.Deserialize(textReader);
        }

        public static void WriteGameConfig(GameConfig config, string filePath)
        {
            var serializer = new XmlSerializer(typeof(GameConfig));
            var textWriter = new StreamWriter(filePath);

            serializer.Serialize(textWriter, config);
        }
        #endregion

        public static IContainer CreateDiContainer()
        {
            var builder = new ContainerBuilder();

            var gameConfig = ReadGameConfig(@"..\..\..\Resources\defaultGameConfig.xml");
            builder.RegisterInstance(gameConfig);

            var gameData = CreateMockDataSmallTest(gameConfig);
            builder.RegisterInstance(gameData);

            builder.RegisterType<MapInfo>().AsSelf().SingleInstance();

            RegisterPlayerScenarios(builder, gameData.PlayerDict.Values.ToList());

            builder.RegisterModule<BehaviorsModule>();
            builder.RegisterModule<HotSeatModule>();

            var container = builder.Build();

            return container;
        }
    }
}
