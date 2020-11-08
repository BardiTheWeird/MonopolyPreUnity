using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class AuctionManager
    {
        #region Dependencies
        private readonly RequestManager _requestManager;
        private readonly PropertyManager _propertyManager;
        private readonly PlayerManager _playerManager;
        private readonly ConsoleUI _consoleUI;
        private readonly AuctionInfo _auctionInfo;
        private readonly TurnInfo _turnInfo;
        #endregion

        public void StartAuction(int propertyId)
        {
            // Create an auction
            _auctionInfo.PropertyOnAuctionId = propertyId;
            _auctionInfo.AmountBid = 0;
            _auctionInfo.BidOrder = _turnInfo.TurnOrder.Select(x => x).ToList();
            _auctionInfo.CurBidder = _turnInfo.CurTurnPlayer;

            // Manage auction
            while (_auctionInfo.BidOrder.Count > 1)
            {
                _requestManager.SendRequest(_auctionInfo.CurBidderId, new AuctionBidRequest());
            }

            // Charge for and transfer property
            var winnerId = _auctionInfo.BidOrder[0];
            _consoleUI.PrintFormatted($"The winner of the auction is |player:{winnerId}|");
            _playerManager.PlayerCashCharge(winnerId, _auctionInfo.AmountBid);
            _propertyManager.TransferProperty(propertyId, winnerId);
        }

        public void Bid(int amount)
        {
            _consoleUI.PrintFormatted($"|player:{_auctionInfo.CurBidderId}| bid {amount}");

            _auctionInfo.AmountBid += amount;
            _auctionInfo.CurBidder = (_auctionInfo.CurBidder + 1) % _auctionInfo.BidOrder.Count;

            _consoleUI.Print($"Total amount bid: {_auctionInfo.AmountBid}");
        }

        public void ResignFromAuction()
        {
            _consoleUI.PrintFormatted($"|player:{_auctionInfo.CurBidderId}| resigned from the auction");

            _auctionInfo.BidOrder.RemoveAt(_auctionInfo.CurBidder);
            if (_auctionInfo.CurBidder >= _auctionInfo.BidOrder.Count)
                _auctionInfo.CurBidder = 0;
        }

        #region Constructor
        public AuctionManager(PropertyManager propertyManager,
            PlayerManager playerManager,
            RequestManager requestManager,
            ConsoleUI consoleUI,
            GameData gameData)
        {
            _propertyManager = propertyManager;
            _playerManager = playerManager;
            _requestManager = requestManager;
            _consoleUI = consoleUI;
            
            _auctionInfo = gameData.AuctionInfo;
            _turnInfo = gameData.TurnInfo;
        }
        #endregion
    }
}
