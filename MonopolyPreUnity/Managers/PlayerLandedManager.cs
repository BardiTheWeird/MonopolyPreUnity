using Autofac.Features.Indexed;
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

        private readonly IIndex<Type, IPlayerLandedBehavior> _playerLandedBehaviorindex;

        public void PlayerLanded(int playerId, int tileId)
        {
            foreach(var component in _tileManager.GetTile(tileId).Components)
            {
                if (_playerLandedBehaviorindex.TryGetValue(component.GetType(), out var behavior))
                {
                    Logger.Log(playerId, $"landed on a tile with {component.GetType()}");
                    behavior.PlayerLanded(playerId, component, tileId);
                }
            }
        }

        #region Constructor
        public PlayerLandedManager(TileManager tileManager, IIndex<Type, IPlayerLandedBehavior> index)
        {
            _tileManager = tileManager;
            _playerLandedBehaviorindex = index;
        }
        #endregion
    }
}
