﻿using Autofac;
using MonopolyPreUnity.Behaviors;
using MonopolyPreUnity.Behaviors.PlayerLanded;
using MonopolyPreUnity.Behaviors.Rent;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Modules;
using MonopolyPreUnity.UserScenario;
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
                    new ActionComponent(new MonopolyAction<int>(Utitlity.MonopolyActionType.ChangeBalance, -100)) }) },

                { 3, new Tile(new List<ITileComponent>{ new TileIdentityComponent(1, "Property 1 Set 1"), 
                    new PropertyComponent(1, 100), 
                    new PropertyDevelopmentComponent(80,
                        new List<int>{ 10, 20, 50, 150, 450, 625, 750 }) }) },

                { 4, new Tile(new List<ITileComponent>{ new TileIdentityComponent(1, "Property 2 Set 1"), 
                    new PropertyComponent(1, 120), new PropertyDevelopmentComponent(80,
                        new List<int>{ 10, 20, 50, 150, 450, 625, 750 }) }) },

                { 5, new Tile(new List<ITileComponent>
                    { new TileIdentityComponent(1, "MoneyGiver 100$"),
                    new ActionComponent(new MonopolyAction<int>(Utitlity.MonopolyActionType.ChangeBalance, 100)) }) },

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

            var playerDict = new Dictionary<int, (Player, IUserScenario)>
            {
                { 1, (new Player(1, "John", 200, new HashSet<int>(), 0, null, 1), null) },
                { 2, (new Player(2, "Jake", 200, new HashSet<int>(), 0, null, 1), null) },
            };

            var turnInfo = new TurnInfo(new List<int> { 1, 2 }, 0);

            var gameData = new GameData(mapIdSequence, mapIndex, dice, tileDict, propertySetDict, playerDict, turnInfo);
            return gameData;
        }
        #endregion

        private static IContainer CreateDiContainer()
        {
            /// Registering
            var builder = new ContainerBuilder();

            var gameData = CreateMockData();
            builder.RegisterInstance(gameData);

            builder.RegisterModule<ManagersModule>();
            builder.RegisterModule<BehaviorsModule>();

            builder.RegisterType<HotSeatUserScenario>().AsSelf();

            var container = builder.Build();

            /// Property injection
            var playerLandedBehaviorDict = new Dictionary<Type, IPlayerLandedBehavior>
            {
                { typeof(ActionComponent), container.Resolve<ActionTileBehavior>() },
                { typeof(ActionBoxComponent), container.Resolve<ActionBoxBehavior>() },
                { typeof(PropertyComponent), container.Resolve<PropertyLandedBehavior>() },
                { typeof(FreeParkingComponent), container.Resolve<FreeParkingBehavior>() },
                { typeof(GoComponent), container.Resolve<GOBehavior>() },
            };

            var rentBehaviorDict = new Dictionary<Type, IRentBehavior>
            {
                { typeof(PropertyDevelopmentComponent), container.Resolve<DevelopmentRentBehavior>() },
                { typeof(TrainStationComponent), container.Resolve<TrainStationRentBehavior>() },
                { typeof(UtilityComponent), container.Resolve<UtilityRentBehavior>() }
            };

            container.Resolve<PlayerLandedManager>().SetDict(playerLandedBehaviorDict);
            container.Resolve<RentManager>().SetDict(rentBehaviorDict);

            // Mock scenarios
            gameData.PlayerDict[1] = (gameData.PlayerDict[1].Item1, container.Resolve<HotSeatUserScenario>());
            ((HotSeatUserScenario)gameData.PlayerDict[1].Item2).SetIdName(1, gameData.PlayerDict[1].Item1.DisplayName);
            gameData.PlayerDict[2] = (gameData.PlayerDict[2].Item1, container.Resolve<HotSeatUserScenario>());
            ((HotSeatUserScenario)gameData.PlayerDict[2].Item2).SetIdName(2, gameData.PlayerDict[2].Item1.DisplayName);

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
