using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class TradeManager
    {
        #region Dependencies
        private readonly RequestManager _requestManager;
        private readonly PropertyManager _propertyManager;
        private readonly PlayerManager _playerManager;
        private readonly ConsoleUI _consoleUI;
        private readonly TileManager _tileManager;
        #endregion

        #region Trade
        public void SendTradeValidationRequest(TradeOffer trade)
        {
            _consoleUI.PrintFormatted($"|player:{trade.InitiatorAssets.PlayerId}| " +
                $"sent a trade offer to |player:{trade.ReceiverAssets.PlayerId}|");

            _requestManager.SendRequest(trade.ReceiverAssets.PlayerId,
                new TradeValidationRequest(trade));
        }

        public void ValidateTrade(TradeOffer trade, bool validated)
        {
            if (!validated)
                _consoleUI.PrintFormatted($"|player:{trade.ReceiverAssets.PlayerId}| declined the trade offer");
            else
            {
                _consoleUI.PrintFormatted($"|player:{trade.ReceiverAssets.PlayerId}| accepted the trade offer");
                _playerManager.TransferPlayerAssets(trade);
            }
        }
        #endregion

        #region TradableProperties
        public IEnumerable<int> TradableProperties(IEnumerable<int> properties)
        {
            return properties.Where(id =>
            {
                var prop = _tileManager.GetTileComponent<Property>(id);
                var dev = _tileManager.GetTileComponent<PropertyDevelopment>(id);

                return !prop.IsMortgaged && (dev == null || dev.HousesBuilt == 0);
            });
        }
        #endregion

        #region ctor
        public TradeManager(RequestManager requestManager, 
            PropertyManager propertyManager, 
            PlayerManager playerManager, 
            ConsoleUI consoleUI,
            TileManager tileManager)
        {
            _requestManager = requestManager;
            _propertyManager = propertyManager;
            _playerManager = playerManager;
            _consoleUI = consoleUI;
            _tileManager = tileManager;
        }
        #endregion
    }
}
