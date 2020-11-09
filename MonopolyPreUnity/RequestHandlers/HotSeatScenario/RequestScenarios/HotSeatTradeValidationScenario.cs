using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.HotSeatScenario
{
    class HotSeatTradeValidationScenario : IHotSeatRequestScenario
    {
        #region Dependencies
        private readonly ConsoleUI _consoleUI;
        private readonly TradeManager _tradeManager;
        #endregion

        public void RunScenario(IRequest request, Player player)
        {
            var offer = (request as TradeValidationRequest).tradeOffer;

            _consoleUI.Print("Choose whether to accept a following trade offer");
            _consoleUI.PrintTradeOffer(offer);

            _consoleUI.Print("1: Accept\n" +
                "2: Decline");
            var answer = _consoleUI.InputValue<int>(x => x == 1 || x == 2, "Invalid value");

            _tradeManager.ValidateTrade(offer, !Convert.ToBoolean(answer - 1));
        }

        public HotSeatTradeValidationScenario(ConsoleUI consoleUI, TradeManager tradeManager)
        {
            _consoleUI = consoleUI;
            _tradeManager = tradeManager;
        }
    }
}
