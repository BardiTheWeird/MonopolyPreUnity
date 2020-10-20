using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Runtime.Serialization;

using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Behaviors;
using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Utitlity;



namespace MonopolyPreUnity.Classes
{   
    [DataContract]
    [Serializable]
    class GameData
    {

        #region MapManager
        public List<int> MapIdSequence { get; private set; }
        public Dictionary<int, int> MapIndex { get; private set; } 
        #endregion

        #region MoveManager
        public Dice DiceValues { get; private set; }
        #endregion

        #region TileManager

        [DataMember]
        public Dictionary<int, Tile> TileDict { get; private set; }
        public Dictionary<int, HashSet<int>> PropertySetDict { get; private set; }
        #endregion

        #region PlayerManager
        public Dictionary<int, (Player, IUserScenario)> PlayerDict { get; private set; } // IuserScenario problem
        #endregion

        #region GameManager
        public TurnInfo TurnInfo { get; private set; }
        #endregion


        // At least for testing initialization
        public GameData(List<int> mapIdSequence, 
            Dictionary<int, int> mapIndex, 
            Dice diceValues, 
            Dictionary<int, Tile> tileDict, 
            Dictionary<int, HashSet<int>> propertySetDict, 
            Dictionary<int, (Player, IUserScenario)> playerDict, 
            TurnInfo turnInfo)
        {
            MapIdSequence = mapIdSequence;
            MapIndex = mapIndex;
            DiceValues = diceValues;
            TileDict = tileDict;
            PropertySetDict = propertySetDict;
            PlayerDict = playerDict;
            TurnInfo = turnInfo;
        }
    }
}
