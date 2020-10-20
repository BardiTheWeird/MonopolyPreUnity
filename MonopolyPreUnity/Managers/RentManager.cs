using MonopolyPreUnity.Behaviors.Rent;
using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class RentManager
    {
        #region Dependencies
        private readonly PlayerManager _playerManager;
        private readonly TileManager _tileManager;
        #endregion

        #region Behavior Dictionary
        private Dictionary<Type, IRentBehavior> _rentBehaviorDict;

        public void SetDict(Dictionary<Type, IRentBehavior> dict) =>
            _rentBehaviorDict = dict;
        #endregion

        #region Collect Rent
        public void CollectRent(int renteeId, int tileId, int ownerId)
        {
            foreach (var component in _tileManager.GetTile(tileId).Components)
            {
                if (_rentBehaviorDict.TryGetValue(component.GetType(), out var behavior))
                {
                    var rent = behavior.GetRent(renteeId, ownerId, component, tileId);
                    _playerManager.PlayerCashCharge(renteeId, rent, ownerId);
                }
            }
        }
        #endregion

        #region Constructor
        public RentManager(PlayerManager playerManager, TileManager tileManager)
        {
            _playerManager = playerManager;
            _tileManager = tileManager;
        }
        #endregion
    }
}
