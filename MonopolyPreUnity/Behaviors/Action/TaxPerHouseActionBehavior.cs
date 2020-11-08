using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Action
{
    class TaxPerHouseActionBehavior : IActionBehavior
    {
        #region Dependencies
        private readonly PlayerManager _playerManager;
        private readonly TileManager _tileManager;
        #endregion

        public void Execute(int playerId, IMonopolyAction action)
        {
            int sum = 0;
            foreach (var propertyId in _playerManager.GetPlayer(playerId).Properties)
            {
                if (_tileManager.GetTileComponent<PropertyDevelopmentComponent>(propertyId, out var developmentComponent))
                    sum += developmentComponent.HousesBuilt * (action as TaxPerHouseAction).Amount;
            }
            _playerManager.PlayerCashCharge(playerId, sum, message: "for houses built");
        }

        public TaxPerHouseActionBehavior(PlayerManager playerManager, TileManager tileManager)
        {
            _playerManager = playerManager;
            _tileManager = tileManager;
        }
    }
}
