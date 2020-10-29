using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Action
{
    class TaxPerHouseActionBehavior : IActionBehavior
    {
        private readonly PlayerManager _playerManager;
        private readonly TileManager _tileManager;

        public void Execute(int playerId, IMonopolyAction action)
        {
            int sum = 0;
            foreach (var propertyId in _playerManager.GetPlayer(playerId).Properties)
            {
                if (_tileManager.GetTileComponent<PropertyDevelopmentComponent>(propertyId, out var developmentComponent))
                    sum += developmentComponent.HousesBuilt * (action as TaxPerHouseAction).Amount;
            }
            _playerManager.PlayerCashCharge(playerId, sum);
        }

        public TaxPerHouseActionBehavior(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }
    }
}
