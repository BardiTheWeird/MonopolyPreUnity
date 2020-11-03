using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors
{
    class PropertyLandedBehavior : IPlayerLandedBehavior
    {
        #region Dependencies
        private readonly RentManager _rentManager;
        private readonly PlayerManager _playerManager;
        private readonly RequestManager _requestManager;
        private readonly AuctionManager _auctionManager;
        #endregion

        #region Behavior
        public void PlayerLanded(int playerId, ITileComponent tileComponent, int tileId)
        {
            var property = (PropertyComponent)tileComponent;

            if (property.OwnerId == null)
            {
                _requestManager.SendRequest(playerId, new BuyAuctionRequest(tileId));
            }
            else if (property.OwnerId != playerId)
            {
                Logger.Log(playerId, $"landed on property that belongs to Player {property.OwnerId}");
                if (property.IsMortgaged == false)
                {
                    _rentManager.CollectRent(playerId, tileId, (int)property.OwnerId);
                }
                else
                {
                    Logger.Log("It is mortgaged");
                }
            }
            else
            {
                Logger.Log(playerId, $"lande on their own property");
            }
        }

        #endregion

        #region Constructor
        public PropertyLandedBehavior(RentManager rentManager,
            PlayerManager playerManager,
            RequestManager requestManager,
            AuctionManager auctionManager)
        {
            _rentManager = rentManager;
            _playerManager = playerManager;
            _requestManager = requestManager;
            _auctionManager = auctionManager;
        }
        #endregion
    }
}
