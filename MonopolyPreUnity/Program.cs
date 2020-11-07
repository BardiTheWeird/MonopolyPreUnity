using Autofac;
using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Behaviors;
using MonopolyPreUnity.Behaviors.PlayerLanded;
using MonopolyPreUnity.Behaviors.Rent;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Modules;
using MonopolyPreUnity.RequestHandlers;
using MonopolyPreUnity.RequestHandlers.HotSeatScenario;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace MonopolyPreUnity
{
    class Program
    {
        #region MockData
        private static GameData CreateMockData()
        {
            var mockData = new MockDataMaker();

            var startTile = mockData.AddTile("Go", new GoComponent());
            mockData.AddTile("Tax 100$", new ActionTileComponent(new ChangeBalanceAction(-100)));
            mockData.AddTile("Property 1 Set 1", new PropertyComponent(1, 100),
                new PropertyDevelopmentComponent(80, new List<int> { 10, 20, 50, 150, 450, 625, 750 }));
            mockData.AddTile("Property 2 Set 1", new PropertyComponent(1, 120),
                new PropertyDevelopmentComponent(80, new List<int> { 10, 20, 50, 150, 450, 625, 750 }));
            mockData.AddTile("MoneyGiver 100$", new ActionTileComponent(new ChangeBalanceAction(-100)));
            mockData.AddTile("Property 3 Set 2", new PropertyComponent(2, 80), new PropertyDevelopmentComponent(50,
                        new List<int> { 5, 10, 30, 100, 300, 400, 550 }));
            mockData.AddTile("Property 4 Set 2", new PropertyComponent(2, 90), new PropertyDevelopmentComponent(50,
                        new List<int> { 5, 10, 30, 100, 300, 400, 550 }));

            mockData.AddPlayer("John", cash: 0);
            mockData.AddPlayer("Jake", cash: 200);

            mockData.SetStartTile(startTile);

            return mockData.GetGameData();
        }
        #endregion

        private static void RegisterPlayerScenarios(ContainerBuilder builder, List<Player> players)
        {
            foreach (var player in players) 
            {
                builder.RegisterType<HotSeatScenario>().Keyed<IPlayerScenario>(player.Id)
                    .WithParameter(new TypedParameter(typeof(Player), player));
            }
        }

        private static GameConfig ReadGameConfig(string filePath)
        {
            var serializer = new XmlSerializer(typeof(GameConfig));
            var textReader = new StreamReader(filePath);

            return (GameConfig)serializer.Deserialize(textReader);
        }

        private static void WriteGameConfig(GameConfig config, string filePath)
        {
            var serializer = new XmlSerializer(typeof(GameConfig));
            var textWriter = new StreamWriter(filePath);

            serializer.Serialize(textWriter, config);
        }

        private static IContainer CreateDiContainer()
        {
            var builder = new ContainerBuilder();

            var gameConfig = ReadGameConfig(@"..\..\..\Resources\defaultGameConfig.xml");
            builder.RegisterInstance(gameConfig);

            var gameData = CreateMockData();
            builder.RegisterInstance(gameData);

            RegisterPlayerScenarios(builder, gameData.PlayerDict.Values.ToList());

            builder.RegisterModule<ManagersModule>();
            builder.RegisterModule<BehaviorsModule>();
            builder.RegisterModule<HotSeatModule>();

            var container = builder.Build();

            return container;
        }

        static void Main(string[] args)
        {
            IContainer container;
            try
            {
                container = CreateDiContainer();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Startup error:\n" + e);

                Environment.Exit(-1);
                return;
            }

            var gameManager = container.Resolve<GameManager>();
            gameManager.StartGame();
        }
    }
}
