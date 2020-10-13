using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    abstract class Property : ITile
    {
        #region Dependencies
        private readonly RequestManager _requestManager;
        private readonly PlayerManager _playerManager;
        private readonly PropertyTransferManager _propertyTransferManager;
        private readonly AuctionManager _auctionManager;
        #endregion

        #region Properties
        public int Id { get; }
        public string Name { get; }
        public string Set { get; }
        public int BasePrice { get; }
        public int? OwnerId { get; set; } = null;
        public bool IsMortgaged { get; set; } = false;
        #endregion

        #region Constructor
        protected Property(int id, string name, string set, int basePrice, 
            RequestManager requestManager, 
            PlayerManager playerManager,
            PropertyTransferManager propertyTransferManager,
            AuctionManager auctionManager)
        {
            Id = id;
            Name = name;
            Set = set;
            BasePrice = basePrice;
            _requestManager = requestManager;
            _playerManager = playerManager;
            _propertyTransferManager = propertyTransferManager;
            _auctionManager = auctionManager;
        }
        #endregion

        abstract public void ChargeRent(int playerId);

        public void OnPlayerLanded(int playerId)
        {
            if (OwnerId == null)
            {
                var command = MonopolyCommand.TileLandedPropertyAuction;
                if (_playerManager.GetPlayerCash(playerId) >= BasePrice)
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
                    _playerManager.PlayerCashCharge(playerId, BasePrice);
                    _propertyTransferManager.TransferProperty(this, playerId);
                }
            }
            else if (OwnerId != playerId)
            {
                ChargeRent(playerId);
            }
            else
            {
                // do nothing
                // maybe send a message like "it's your own property, dumbass!"
            }
        }
    }
}
