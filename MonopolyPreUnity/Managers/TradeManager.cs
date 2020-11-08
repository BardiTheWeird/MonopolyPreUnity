using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
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

        #region ctor
        public TradeManager(RequestManager requestManager, 
            PropertyManager propertyManager, 
            PlayerManager playerManager, 
            ConsoleUI consoleUI)
        {
            _requestManager = requestManager;
            _propertyManager = propertyManager;
            _playerManager = playerManager;
            _consoleUI = consoleUI;
        }
        #endregion
    }
}
