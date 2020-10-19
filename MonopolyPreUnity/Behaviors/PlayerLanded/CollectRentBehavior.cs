using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors
{
    class CollectRentBehavior : IPlayerLandedBehavior
    {
        #region Dependencies
        private readonly TileManager _tileManager;
        private readonly PlayerManager _playerManager;
        #endregion

        /// <summary>
        /// Assumes that 1) player is not the owner 2) tileId is an id of a property
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="tileId"></param>
        public void CollectRent(int playerId, int tileId)
        {
            var property = _tileManager.GetTileComponent<PropertyComponent>(tileId);
            if (property == null)
                throw new ArgumentException($"Tile with Id {tileId} doesn't have a Property component");

            if (_tileManager.GetTileComponent<PropertyDevelopmentComponent>(tileId, out var propertyDevelopment))
            {
                // count depending on the amount of developed stuff
                throw new NotImplementedException();
            }
            else if (_tileManager.GetTileComponent<UtilityRentComponent>(tileId, out var utilityRent))
            {
                // count depending on da rules!
                throw new NotImplementedException();
            }
            else
                throw new ArgumentException($"Tile with Id {tileId} has neither Development nor a Utility Rent component");
        }

        public void PlayerLanded(int playerId, ITileComponent tileComponent, int tileId)
        {
            throw new NotImplementedException();
        }

        public CollectRentBehavior(TileManager tileManager, PlayerManager playerManager)
        {
            _tileManager = tileManager;
            _playerManager = playerManager;
        }
    }
}
