using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class PropertyTransferManager
    {
        #region Dependencies
        private readonly PlayerManager _playerManager;
        private readonly TileManager _tileManager;
        #endregion

        public void TransferProperty(int propertyId, int newOwnerId)
        {
            var property = _tileManager.GetTileComponent<PropertyComponent>(propertyId);
            if (property.OwnerId != null)
            {
                Logger.Log((int)property.OwnerId, $"is no longer the owner of property {propertyId}");
            }
            property.OwnerId = newOwnerId;
            _playerManager.GetPlayer(newOwnerId).Properties.Add(propertyId);
            Logger.Log(newOwnerId, $"is the the owner of property {propertyId}");
        }

        public PropertyTransferManager(PlayerManager playerManager, TileManager tileManager)
        {
            _playerManager = playerManager;
            _tileManager = tileManager;
        }
    }
}
