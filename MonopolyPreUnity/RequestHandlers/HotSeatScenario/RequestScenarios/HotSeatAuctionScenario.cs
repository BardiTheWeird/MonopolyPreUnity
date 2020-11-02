using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Requests;
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
        #endregion

        public void RunScenario(IRequest requestIn, Player player)
        {
            var request = requestIn as AuctionBidRequest;
            _consoleUI.Print("Write an amount (0 to resign from the auction):");
            var amount = _consoleUI.InputValue<int>(x => 0 <= x && x <= player.Cash, 
                $"Amount should be between 0 and {player.Cash}");

            if (amount == 0)
                _auctionManager.RemoveFromAuction(player.Id);
            else
                _auctionManager.Bid(player.Id, amount);
        }

        #region ctor
        public HotSeatAuctionScenario(ConsoleUI consoleUI, AuctionManager auctionManager)
        {
            _consoleUI = consoleUI;
            _auctionManager = auctionManager;
        }
        #endregion
    }
}
