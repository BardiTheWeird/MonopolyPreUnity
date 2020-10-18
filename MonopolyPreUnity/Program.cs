using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MonopolyPreUnity
{
    /// <summary>
    /// Test stuff. Initialization and stuff
    /// </summary>
    class Program
    {
        [Serializable]
        class EmptyUserScenario : IUserScenario
        {
            public TInput HandleRequest<TInput>(Request<TInput> request)
            {
                throw new NotImplementedException();
            }
        }
        static void BinarySerializationTest()
        {
            List<int> mapidsequence = new List<int> { 10, 1, 3 };
            Dictionary<int, int> mapIndex = new Dictionary<int, int> { { 0, 1 }, { 1, 10 }, { 2, 13 } };
            (int, int) diceValues = (1, 1);
            Dictionary<int, Tile> tileDict = new Dictionary<int, Tile> { { 1, new Tile(new List<ITileComponent> {new TileIdentityComponent(1, "smth")}) } };
            Dictionary<int, HashSet<int>> properySet = new Dictionary<int, HashSet<int>> { { 1, new HashSet<int>() { 1, 2, 3, 4 } } };
            Dictionary<int, (Player, Interfaces.IUserScenario)> playerDict = new Dictionary<int, (Player, Interfaces.IUserScenario)> { { 1, (new Player(1, "Hellout", 1000, new HashSet<int>() { 1, 2, 3 }, 0, 999), new EmptyUserScenario()) } };
            TurnInfo turnI = new TurnInfo(new List<int> { 1, 2, 3 }, 1);

            GameData gameData = new GameData(mapidsequence, mapIndex, diceValues, tileDict, properySet, playerDict, turnI);

            BinaryFormatter serializer = new BinaryFormatter();

            using (FileStream f = new FileStream("TestGameProccess.dat", FileMode.OpenOrCreate))
            {
                serializer.Serialize(f, gameData);
            }

            GameData g1;
            using (FileStream f = new FileStream("TestGameProccess.dat", FileMode.Open))
            {
                g1 = (GameData)serializer.Deserialize(f);
            }

            Console.WriteLine($"GameData:\nMAPIDSEQUENCE:{g1.MapIdSequence[0]},{g1.MapIdSequence[1]}, {g1.MapIdSequence[2]}");
            Console.WriteLine($"GameData:\nMAPINDEX:{g1.MapIndex[0]},{g1.MapIndex[1]}, {g1.MapIndex[2]}");
            Console.WriteLine($"GameData:\nDICEVALUES:{g1.DiceValues.Item1},{g1.DiceValues.Item2}");
            Console.WriteLine($"GameData:\nTILEDICT:{g1.TileDict[1].Components[0]}");
            Console.WriteLine($"GameData:\nPROPERTYSET:{g1.PropertySetDict[1]}");
            Console.WriteLine($"GameData:\nPLAYER:{g1.PlayerDict[1].Item1.Cash} {g1.PlayerDict[1].Item1.DisplayName}");
            Console.WriteLine($"GameData:\nTURNINFO:{g1.TurnInfo.curTurnPlayer}");



        }
        static void Main(string[] args)
        {
            BinarySerializationTest();
        }
    }
}
