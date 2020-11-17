using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using MonopolyPreUnity.Behaviors;
using MonopolyPreUnity.Utitlity;
using MonopolyPreUnity.Components;

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
        public Dictionary<int, Player> PlayerDict { get; private set; }
        #endregion

        #region GameManager
        public TurnInfo TurnInfo { get; private set; }
        #endregion

        #region AuctionManager
        public AuctionInfo AuctionInfo { get; private set; }
        #endregion

        #region ctor
        // At least for testing initialization
        public GameData(List<int> mapIdSequence, 
            Dictionary<int, int> mapIndex, 
            Dice diceValues, 
            Dictionary<int, Tile> tileDict, 
            Dictionary<int, HashSet<int>> propertySetDict, 
            Dictionary<int, Player> playerDict, 
            TurnInfo turnInfo,
            AuctionInfo auctionInfo)
        {
            MapIdSequence = mapIdSequence;
            MapIndex = mapIndex;
            DiceValues = diceValues;
            TileDict = tileDict;
            PropertySetDict = propertySetDict;
            PlayerDict = playerDict;
            TurnInfo = turnInfo;
            AuctionInfo = auctionInfo;
            AuctionInfo = auctionInfo;
        }

        public GameData(GameConfig gameConfig)
        {
            MapIdSequence = new List<int>();
            MapIndex = new Dictionary<int, int>();
            DiceValues = new Dice(gameConfig.DieSides);
            TileDict = new Dictionary<int, Tile>();
            PropertySetDict = new Dictionary<int, HashSet<int>>();
            PlayerDict = new Dictionary<int, Player>();
            TurnInfo = new TurnInfo();
            AuctionInfo = new AuctionInfo();
        }
        #endregion
    }
}
