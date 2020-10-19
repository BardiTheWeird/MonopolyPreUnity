using MonopolyPreUnity.Behaviors;
using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class PlayerLandedManager
    {
        #region Dependencies
        private readonly TileManager _tileManager;
        #endregion

        private readonly Dictionary<Type, IPlayerLandedBehavior> _playerLandedBehaviorDict;

        public void PlayerLanded(int playerId, int tileId)
        {
            foreach(var component in _tileManager.GetTile(tileId).Components)
            {
                if (_playerLandedBehaviorDict.TryGetValue(component.GetType(), out var behavior))
                    behavior.PlayerLanded(playerId, component, tileId);
            }
        }

        #region Constructor
        public PlayerLandedManager(TileManager tileManager)
        {
            _tileManager = tileManager;
        }
        #endregion
    }
}
