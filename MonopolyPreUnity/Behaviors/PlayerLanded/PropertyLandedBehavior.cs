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
        private readonly RentManager _rentManager;
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
            AuctionManager auctionManager,
            PropertyTransferManager propertyTransferManager)
        {
            _rentManager = rentManager;
            _playerManager = playerManager;
            _requestManager = requestManager;
            _auctionManager = auctionManager;
            _propertyTransferManager = propertyTransferManager;
        }
        #endregion
    }
}
