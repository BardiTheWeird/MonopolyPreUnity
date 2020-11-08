using Autofac.Features.Indexed;
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

        #region fields
        private readonly IIndex<Type, IRentBehavior> _rentBehaviorIndex;
        #endregion

        #region Collect Rent
        public void CollectRent(int renteeId, int tileId, int ownerId)
        {
            foreach (var component in _tileManager.GetTile(tileId).Components)
            {
                if (_rentBehaviorIndex.TryGetValue(component.GetType(), out var behavior))
                {
                    var rent = behavior.GetRent(renteeId, ownerId, component, tileId);
                    _playerManager.PlayerCashCharge(renteeId, rent, ownerId, "for rent");
                }
            }
        }
        #endregion

        #region Constructor
        public RentManager(PlayerManager playerManager, TileManager tileManager, IIndex<Type, IRentBehavior> index)
        {
            _playerManager = playerManager;
            _tileManager = tileManager;
            _rentBehaviorIndex = index;
        }
        #endregion
    }
}
