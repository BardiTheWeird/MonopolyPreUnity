using MonopolyPreUnity.Behaviors.Rent;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.UI;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyPreUnity.RequestHandlers.HSScenario
{
    class HSTurnScenario : IHSRequestScenario
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        #region ManageProperty
        //public void ManageProperty(Player player)
        //{
        //    while (true)
        //    {
        //        // choose property to manage
        //        if (!(_consoleUI.ChoosePropertyId(player.Properties) is int propId))
        //            return;

        //        MonopolyCommand command;
        //        var property = _tileManager.GetTileComponent<Property>(propId);
        //        var development = _tileManager.GetTileComponent<PropertyDevelopment>(propId);
        //        do
        //        {
        //            var availableActions = _propertyManager.GetAvailableActions(player.Id, property, development);
        //            availableActions.Add(MonopolyCommand.CancelAction);

        //            command = _consoleUI.ChooseCommand(availableActions);

        //            switch (command)
        //            {
        //                case MonopolyCommand.MortgageProperty:
        //                    _propertyManager.Mortage(player.Id, property);
        //                    break;

        //                case MonopolyCommand.UnmortgageProperty:
        //                    _propertyManager.UnMortage(player.Id, property);
        //                    break;

        //                case MonopolyCommand.BuyHouse:
        //                    _propertyManager.BuildHouse(player.Id, development);
        //                    break;

        //                case MonopolyCommand.SellHouse:
        //                    _propertyManager.SellHouse(player.Id, development);
        //                    break;
        //            }
        //        } while (command != MonopolyCommand.CancelAction);
        //    }
        //}
        #endregion

        #region MakeDeal
        //public void MakeDeal(Player player)
        //{
        //    // Creating a trade
        //    _consoleUI.Print("Choose a player to trade with");

        //    var playersNoSelf = _playerManager.GetAllPlayerId().Where(id => id != player.Id).ToList();
        //    _consoleUI.PrintPlayers(playersNoSelf);
        //    _consoleUI.Print("Write -1 to cancel");

        //    var playerIndex = _consoleUI.InputValueIndex(playersNoSelf, true);
        //    if (playerIndex == -1)
        //        return;

        //    var receiverId = playersNoSelf[playerIndex];
        //    var receiver = _playerManager.GetPlayer(receiverId);

        //    var offer = new TradeOffer();

        //    _consoleUI.Print("Choose assets the receiver will trade");
        //    offer.ReceiverAssets = _consoleUI.ChoosePlayerAssets(receiverId,
        //        _tradeManager.TradableProperties(receiver.Properties));

        //    _consoleUI.Print("Choose assets you will trade");
        //    offer.InitiatorAssets = _consoleUI.ChoosePlayerAssets(player.Id,
        //        _tradeManager.TradableProperties(player.Properties));

        //    _tradeManager.SendTradeValidationRequest(offer);
        //}
        #endregion

        public void RunScenario(IRequest request, Player player)
        {
            var commandList = new List<MonopolyCommand>
                {
                    MonopolyCommand.ManageProperty,
                    MonopolyCommand.MakeDeal
                };
            commandList.AddRange(_context.GetAvailableTurnCommands(player));

            _context.Remove<PlayerInputRequest>();
            _context.Add(new HSCommandChoiceRequest(commandList, player.Id));
        }

        #region ctor
        public HSTurnScenario(Context context) =>
            _context = context;
        #endregion
    }
}
