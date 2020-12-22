using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Initialization
{

    static class MockContext
    {
        #region MockData
         
       
        public static Context CreateMockDataSmallTest(GameConfig gameConfig)
        {
            var mock = new MockContextMaker(gameConfig);

            mock.AddTile("Go", new Go());
            mock.AddTile("Tax 100$", new ActionTile(new ChangeBalanceAction(-100)));
            mock.AddTile("Property 1 Set 1", new Property(1, 100),
                new PropertyDevelopment(80, new List<int> { 10, 20, 50, 150, 450, 625, 750 }));
            mock.AddTile("Property 2 Set 1", new Property(1, 120),
                new PropertyDevelopment(80, new List<int> { 10, 20, 50, 150, 450, 625, 750 }));
            mock.AddTile("MoneyGiver 100$", new ActionTile(new ChangeBalanceAction(100)));
            mock.AddTile("Property 3 Set 2", new Property(2, 80), new PropertyDevelopment(50,
                        new List<int> { 5, 10, 30, 100, 300, 400, 550 }));
            mock.AddTile("Property 4 Set 2", new Property(2, 90), new PropertyDevelopment(50,
                        new List<int> { 5, 10, 30, 100, 300, 400, 550 }));

            mock.AddPlayer("John", cash: 200);
            mock.AddPlayer("AIPlayer2 Chaos:4", cash: 200);

            return mock.GetContext();
        }

        public static Context CreateMockDataSmallTest() =>
            CreateMockDataSmallTest(GameConfigMaker.DefaultGameConfig());

        public static Context CreateMockDataJailTest(GameConfig gameConfig)
        {
            var mockData = new MockContextMaker(gameConfig);

            mockData.AddTile("Go to jail", new ActionTile(new GoToJailAction()));
            mockData.AddTile("Give Jail Card", new ActionTile(new JailCardAction()));
            mockData.AddTile("Go to jail", new ActionTile(new GoToJailAction()));
            mockData.AddTile("Go to jail", new ActionTile(new GoToJailAction()));
            mockData.AddTile("Jail", new Jail());

            mockData.AddPlayer("John", cash: 200);
            //mockData.AddPlayer("Jake", cash: 200);

            return mockData.GetContext();
        }

        public static Context CreateMockDataJailTest() =>
            CreateMockDataJailTest(GameConfigMaker.DefaultGameConfig());
        #endregion
    }
}
