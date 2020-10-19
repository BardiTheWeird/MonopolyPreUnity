using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Behaviors;
using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Utitlity;

namespace MonopolyPreUnity.Classes
{
    class GameData
    {
        #region Dependencies
        private readonly Serializer _serializationManager;
        #endregion

        #region MapManager
        public List<int> MapIdSequence { get; private set; }
        public Dictionary<int, int> MapIndex { get; private set; } 
        #endregion

        #region MoveManager
        // public (int, int) DiceValues { get; private set; }
        public Dice DiceValues { get; private set; }
        #endregion

        #region TileManager
        public Dictionary<int, Tile> TileDict { get; private set; }
        public Dictionary<int, HashSet<int>> PropertySetDict { get; private set; }
        #endregion

        #region PlayerLandedManager
        public Dictionary<Type, IPlayerLandedBehavior> PlayerLandedBehaviorDict { get; private set; }
        #endregion

        #region PlayerManager
        public Dictionary<int, (Player, IUserScenario)> playerDict { get; private set; }
        #endregion

        #region ActionManager
        public Dictionary<MonopolyActionType, Action<int, IMonopolyAction>> ActionDict { get; private set; }
        #endregion

        #region GameManager
        public TurnInfo TurnInfo { get; private set; }
        #endregion


        // TradeManager?

        public GameData()
        {

        }

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
            this.playerDict = playerDict;
            TurnInfo = turnInfo;
        }
    }
}
