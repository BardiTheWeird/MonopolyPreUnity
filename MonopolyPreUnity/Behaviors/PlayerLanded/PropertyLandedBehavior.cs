using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors
{
    class PropertyLandedBehavior : IPlayerLandedBehavior
    {
        #region Dependencies
        private readonly CollectRentBehavior _collectRentBehavior;
        private readonly PlayerManager _playerManager;
        private readonly RequestManager _requestManager;
        private readonly AuctionManager _auctionManager;
        private readonly PropertyTransferManager _propertyTransferManager;
        #endregion

        #region Behavior
        public void PlayerLanded(int playerId, ITileComponent tileComponent, int tileId)
        {
            var property = (PropertyComponent)tileComponent;

            if (property.OwnerId == null)
            {
                var command = MonopolyCommand.TileLandedPropertyAuction;
                if (_playerManager.GetPlayerCash(playerId) >= property.BasePrice)
                {
                    var request = new Request<MonopolyCommand>(
                        MonopolyRequest.TileLandedPropertyChoice,
                        new List<MonopolyCommand>()
                        {
                            MonopolyCommand.TileLandedPropertyBuy,
                            MonopolyCommand.TileLandedPropertyAuction
                        });
                    command = _requestManager.SendRequest(playerId, request);
                }

                if (command == MonopolyCommand.TileLandedPropertyBuy)
                {
                    _playerManager.PlayerCashCharge(playerId, property.BasePrice);
                    _propertyTransferManager.TransferProperty(tileId, playerId);
                }
                else
                {
                    _auctionManager.StartAuction(tileId, property);
                }
            }
            else if (property.OwnerId != playerId)
            {
                _collectRentBehavior.CollectRent(playerId, tileId);
            }
            else
            {
                // do nothing
                // maybe send a message like "it's your own property, dumbass!"
            }
        }

        #endregion

        #region Constructor
        public PropertyLandedBehavior(CollectRentBehavior collectRentBehavior, 
            PlayerManager playerManager,
            RequestManager requestManager,
            AuctionManager auctionManager,
            PropertyTransferManager propertyTransferManager)
        {
            _collectRentBehavior = collectRentBehavior;
            _playerManager = playerManager;
            _requestManager = requestManager;
            _auctionManager = auctionManager;
            _propertyTransferManager = propertyTransferManager;
        }
        #endregion
    }
}
