using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Initialization
{
    static class MockData
    {
        #region MockData
        public static GameData CreateMockDataSmallTest(GameConfig gameConfig)
        {
            var mockData = new MockDataMaker(gameConfig);

            var startTile = mockData.AddTile("Go", new Go());
            //mockData.AddTile("Tax 100$", new ActionTileComponent(new ChangeBalanceAction(-100)));
            mockData.AddTile("Property 1 Set 1", new Property(1, 100),
                new PropertyDevelopment(80, new List<int> { 10, 20, 50, 150, 450, 625, 750 }));
            mockData.AddTile("Property 2 Set 1", new Property(1, 120),
                new PropertyDevelopment(80, new List<int> { 10, 20, 50, 150, 450, 625, 750 }));
            //mockData.AddTile("MoneyGiver 100$", new ActionTileComponent(new ChangeBalanceAction(100)));
            mockData.AddTile("Property 3 Set 2", new Property(2, 80), new PropertyDevelopment(50,
                        new List<int> { 5, 10, 30, 100, 300, 400, 550 }));
            mockData.AddTile("Property 4 Set 2", new Property(2, 90), new PropertyDevelopment(50,
                        new List<int> { 5, 10, 30, 100, 300, 400, 550 }));

            mockData.AddPlayer("John", cash: 200);
            mockData.AddPlayer("Jake", cash: 200);

            mockData.SetStartTile(startTile);

            return mockData.GetGameData();
        }

        public static GameData CreateMockDataJailTest(GameConfig gameConfig)
        {
            var mockData = new MockDataMaker(gameConfig);

            var startTile = mockData.AddTile("Go to jail", new ActionTile(new GoToJailAction()));
            //mockData.AddTile("Give Jail Card", new ActionTileComponent(new JailCardAction()));
            mockData.AddTile("Go to jail", new ActionTile(new GoToJailAction()));
            mockData.AddTile("Go to jail", new ActionTile(new GoToJailAction()));
            mockData.AddTile("Jail", new Jail());

            mockData.AddPlayer("John", cash: 200);
            //mockData.AddPlayer("Jake", cash: 200);

            mockData.SetStartTile(startTile);

            return mockData.GetGameData();
        }
        #endregion
    }
}
