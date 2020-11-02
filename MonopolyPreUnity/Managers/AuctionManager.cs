using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class AuctionManager
    {
        #region Dependencies
        private readonly PropertyManager _propertyManager;
        private readonly PlayerManager _playerManager;
        #endregion

        public void StartAuction(int tileId)
        {
            Logger.Log("Auction is not yet implemented, so a random player gets this property for free (;");
            var players = _playerManager.GetAllPlayerId();
            var luckyPlayer = players[new Random().Next(0, players.Count)];
            Logger.Log(luckyPlayer, "is lucky today!");

            _propertyManager.TransferProperty(tileId, luckyPlayer);
        }

        public void Bid(int playerId, int amount)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromAuction(int playerId)
        {
            throw new NotImplementedException();
        }

        #region Constructor
        public AuctionManager(PropertyManager propertyManager, PlayerManager playerManager)
        {
            _propertyManager = propertyManager;
            _playerManager = playerManager;
        }
        #endregion
    }
}
