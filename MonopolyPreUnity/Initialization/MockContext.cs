using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using System.Data;


namespace MonopolyPreUnity.Initialization
{

    static class MockContext
    {
        private static readonly string csvFilePath = @"..\..\..\..\MonopolyPreUnity\Resources\MonTable.csv";
        private static readonly string csvActionsPath = @"..\..\..\..\MonopolyPreUnity\Resources\actions.csv";
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


        public static MockContextMaker ParseDefaultMap(GameConfig gameConfig, List<IMonopolyAction> actions)
        {
            var commandTable = new DataTable();
            var mock = new MockContextMaker(gameConfig);
            using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(csvFilePath)), true))
            {
                commandTable.Load(csvReader);
            }
            int set = 0;
            int price = 0;
            int pricePerHouse = 0;
            List<int> rent = null; 

            for (int i = 0; i < commandTable.Rows.Count; i++)
            {
                #region variables
                string name = commandTable.Rows[i][0].ToString().Trim();
                string type = commandTable.Rows[i][1].ToString().Trim();
                string temp_str = commandTable.Rows[i][2].ToString().Trim();
                if (temp_str.Length > 0)
                {
                    price = Convert.ToInt32(commandTable.Rows[i][2].ToString().Trim());
                }
                if (type == "Dev") {
                    set = Convert.ToInt32(commandTable.Rows[i][3].ToString().Trim());
                    pricePerHouse = Convert.ToInt32(commandTable.Rows[i][4].ToString().Trim());
                    rent = new List<int> {
                    Convert.ToInt32(commandTable.Rows[i][5].ToString().Trim()),
                    2*Convert.ToInt32(commandTable.Rows[i][5].ToString().Trim()),
                    Convert.ToInt32(commandTable.Rows[i][6].ToString().Trim()),
                    Convert.ToInt32(commandTable.Rows[i][7].ToString().Trim()),
                    Convert.ToInt32(commandTable.Rows[i][8].ToString().Trim()),
                    Convert.ToInt32(commandTable.Rows[i][9].ToString().Trim()),
                    Convert.ToInt32(commandTable.Rows[i][10].ToString().Trim()),
                };
                }
                #endregion

                switch (type)
                {
                    case "Go":
                        mock.AddTile(name, new Go());
                        break;
                    case "Dev":
                        mock.AddTile(name, new Property(set, price),
               new PropertyDevelopment(pricePerHouse, rent));
                        break;
                    case "Tax":
                        mock.AddTile(name, new ActionTile(new ChangeBalanceAction(price)));
                        break;
                    case "NoDev":
                        mock.AddTile(name, new Property(set,price),new UtilityProperty());
                        break;
                    case "ToJail":
                        mock.AddTile(name, new ActionTile(new GoToJailAction()));
                        break;
                    case "Park":
                        mock.AddTile(name, new FreeParking());
                        break;
                    case "Jail":
                        mock.AddTile(name, new Jail());
                        break;
                    case "Station":
                        mock.AddTile(name, new TrainStation(price/4),new Property(set,price));
                        break;
                    case "Chest":
                        mock.AddTile(name, new ActionBox(actions));
                        break;
                    default:
                        break;
                }
            }
            return mock;
        }

        public static Context CreateDefaultMapContext()
        {
            var actionList = MockContextMaker.GetActionBoxList(csvActionsPath);
            var mock = ParseDefaultMap(GameConfigMaker.DefaultGameConfig(), actionList);

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
