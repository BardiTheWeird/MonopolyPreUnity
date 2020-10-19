using Autofac;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Modules;
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
                { 1, new Tile(new List<ITileComponent>{ new GoComponent(200) }) },
                { 2, new Tile(new List<ITileComponent>
                    { new ActionComponent(new MonopolyAction<int>(Utitlity.MonopolyActionType.ChangeBalance, -100)) }) },
                { 3, new Tile(new List<ITileComponent>{ new PropertyComponent(1, 100), new PropertyDevelopmentComponent(80) }) },
                { 4, new Tile(new List<ITileComponent>{ new PropertyComponent(1, 120), new PropertyDevelopmentComponent(80) }) },
                { 5, new Tile(new List<ITileComponent>
                    { new ActionComponent(new MonopolyAction<int>(Utitlity.MonopolyActionType.ChangeBalance, 100)) }) },
                { 6, new Tile(new List<ITileComponent>{ new PropertyComponent(2, 80), new PropertyDevelopmentComponent(50) }) },
                { 7, new Tile(new List<ITileComponent>{ new PropertyComponent(2, 90), new PropertyDevelopmentComponent(50) }) }
            };

            var propertySetDict = new Dictionary<int, HashSet<int>>
            {
                { 1, new HashSet<int> { 3, 4 } },
                { 2, new HashSet<int> { 6, 7 } }
            };

            var playerDict = new Dictionary<int, (Player, IUserScenario)>
            {
                { 1, (new Player(1, "John", 500, new HashSet<int>(), 0, null), null) },
                { 2, (new Player(2, "Jake", 500, new HashSet<int>(), 0, null), null) },
            };

            var turnInfo = new TurnInfo(new List<int> { 1, 2 }, 0);

            var gameData = new GameData(mapIdSequence, mapIndex, dice, tileDict, propertySetDict, playerDict, turnInfo);
            return gameData;
        }
        #endregion

        private static GameData CreateGameData()
        {
            return new GameData();
        }

        private static IContainer CreateDiContainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterInstance(CreateGameData());

            containerBuilder.RegisterModule<ManagersModule>();
            containerBuilder.RegisterModule<BehaviorsModule>();

            return containerBuilder.Build();
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
                Console.WriteLine($"Startup error", e);

                Environment.Exit(-1);
                return;
            }

            var gameManager = container.Resolve<GameManager>();
            gameManager.StartGame();
        }
    }
}
