using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.HotSeatScenario
{
    class HotSeatAuctionScenario : IHotSeatRequestScenario
    {
        #region Dependencies
        private readonly ConsoleUI _consoleUI;
        private readonly AuctionManager _auctionManager;
        private readonly AuctionInfo _auctionInfo;
        #endregion

        public void RunScenario(IRequest requestIn, Player player)
        {
            var maxBidAmount = player.Cash - _auctionInfo.AmountBid;

            _consoleUI.Print("Write an amount to bid (0 to resign from the auction):");
            var amount = _consoleUI.InputValue<int>(x => 0 <= x && x <= maxBidAmount, 
                $"Amount should be between 0 and {maxBidAmount}");

            if (amount == 0)
                _auctionManager.ResignFromAuction();
            else
                _auctionManager.Bid(amount);
        }

        #region ctor
        public HotSeatAuctionScenario(ConsoleUI consoleUI, 
            AuctionManager auctionManager,
            GameData gameData)
        {
            _consoleUI = consoleUI;
            _auctionManager = auctionManager;
            _auctionInfo = gameData.AuctionInfo;
        }
        #endregion
    }
}
