using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using MonopolyPreUnity.Behaviors;
using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Utitlity;

namespace MonopolyPreUnity.Classes
{
    class GameData
    {
        #region MapManager
        public List<int> MapIdSequence { get; private set; }
        public Dictionary<int, int> MapIndex { get; private set; } 
        #endregion

        #region MoveManager
        public (int, int) DiceValues { get; private set; }
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
    }
}
