using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.UI;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.HotSeatScenario
{
    class HotSeatBuyAuctionScenario : IHotSeatRequestScenario
    {
        #region Dependencies
        private readonly TileManager _tileManager;
        private readonly ConsoleUI _consoleUI;
        private readonly PlayerManager _playerManager;
        private readonly PropertyManager _propertyManager;
        private readonly AuctionManager _auctionManager;
        #endregion

        public void RunScenario(IRequest requestIn, Player player)
        {
            var request = requestIn as BuyAuctionRequest;
            var property = _tileManager.GetTileComponent<PropertyComponent>(request.PropertyId);

            var availableActions = new List<MonopolyCommand> { MonopolyCommand.AuctionProperty };
            if (player.Cash >= property.BasePrice)
                availableActions.Add(MonopolyCommand.BuyProperty);

            switch (_consoleUI.ChooseCommand(availableActions))
            {
                case MonopolyCommand.BuyProperty:
                    _playerManager.PlayerCashCharge(player.Id, property.BasePrice);
                    _propertyManager.TransferProperty(request.PropertyId, player.Id);
                    break;
                case MonopolyCommand.AuctionProperty:
                    _auctionManager.StartAuction(request.PropertyId);
                    break;
            }
        }

        #region ctor
        public HotSeatBuyAuctionScenario(TileManager tileManager, ConsoleUI consoleUI, PlayerManager playerManager, PropertyManager propertyManager, AuctionManager auctionManager)
        {
            _tileManager = tileManager;
            _consoleUI = consoleUI;
            _playerManager = playerManager;
            _propertyManager = propertyManager;
            _auctionManager = auctionManager;
        }
        #endregion
    }
}
