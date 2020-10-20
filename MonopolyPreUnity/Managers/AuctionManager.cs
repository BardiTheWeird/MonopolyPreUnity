using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class AuctionManager
    {
        #region Dependencies
        private readonly PropertyTransferManager _propertyTransferManager;
        private readonly PlayerManager _playerManager;
        #endregion

        public void StartAuction(int tileId, PropertyComponent property)
        {
            Logger.Log("Auction is not yet implemented, so a random player gets this property for free (;");
            var players = _playerManager.GetAllPlayerId();
            var luckyPlayer = players[new Random().Next(0, players.Count)];
            Logger.Log(luckyPlayer, "is lucky today!");

            _propertyTransferManager.TransferProperty(tileId, luckyPlayer);
        }

        #region Constructor
        public AuctionManager(PropertyTransferManager propertyTransferManager, PlayerManager playerManager)
        {
            _propertyTransferManager = propertyTransferManager;
            _playerManager = playerManager;
        }
        #endregion
    }
}
