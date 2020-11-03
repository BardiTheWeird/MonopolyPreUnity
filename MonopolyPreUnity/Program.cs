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
using System.Linq;

namespace MonopolyPreUnity
{
    class Program
    {
        #region MockData
        private static GameData CreateMockData()
        {
            var mapIdSequence = Enumerable.Range(1, 7).ToList();
            var mapIndex = new Dictionary<int, int>
            {
                { 1, 0 },
                { 2, 1 },
                { 3, 2 },
                { 4, 3 },
                { 5, 4 },
                { 6, 5 },
                { 7, 6 }
            };

            var dice = new Dice();

            var tileDict = new Dictionary<int, Tile>
            {
                { 1, new Tile(new List<ITileComponent>{ new TileIdentityComponent(1, "Go"), new GoComponent(200) }) },

                { 2, new Tile(new List<ITileComponent>
                    { new TileIdentityComponent(1, "Tax 100$"),
                    new ActionTileComponent(new ChangeBalanceAction(-100)) }) },

                { 3, new Tile(new List<ITileComponent>{ new TileIdentityComponent(1, "Property 1 Set 1"), 
                    new PropertyComponent(1, 100), 
                    new PropertyDevelopmentComponent(80,
                        new List<int>{ 10, 20, 50, 150, 450, 625, 750 }) }) },

                { 4, new Tile(new List<ITileComponent>{ new TileIdentityComponent(1, "Property 2 Set 1"), 
                    new PropertyComponent(1, 120), new PropertyDevelopmentComponent(80,
                        new List<int>{ 10, 20, 50, 150, 450, 625, 750 }) }) },

                { 5, new Tile(new List<ITileComponent>
                    { new TileIdentityComponent(1, "MoneyGiver 100$"),
                    new ActionTileComponent(new ChangeBalanceAction(100)) }) },

                { 6, new Tile(new List<ITileComponent>{ new TileIdentityComponent(1, "Property 3 Set 2"),
                    new PropertyComponent(2, 80), new PropertyDevelopmentComponent(50,
                        new List<int>{ 5, 10, 30, 100, 300, 400, 550 }) }) },

                { 7, new Tile(new List<ITileComponent>{ new TileIdentityComponent(1, "Property 4 Set 2"), 
                    new PropertyComponent(2, 90), new PropertyDevelopmentComponent(50,
                        new List<int>{ 5, 10, 30, 100, 300, 400, 550 }) }) }
            };

            var propertySetDict = new Dictionary<int, HashSet<int>>
            {
                { 1, new HashSet<int> { 3, 4 } },
                { 2, new HashSet<int> { 6, 7 } }
            };

            var playerDict = new Dictionary<int, Player>
            {
                { 1, new Player(1, "John", 200, new HashSet<int>(), 0, null, 1) },
                { 2, new Player(2, "Jake", 200, new HashSet<int>(), 0, null, 1) },
            };

            var turnInfo = new TurnInfo(new List<int> { 1, 2 }, 0);

            var gameData = new GameData(mapIdSequence, mapIndex, dice, tileDict, propertySetDict, playerDict, turnInfo);
            return gameData;
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

        private static IContainer CreateDiContainer()
        {
            /// Registering
            var builder = new ContainerBuilder();

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
