using MonopolyPreUnity.Behaviors.Rent;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.UI;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyPreUnity.RequestHandlers.HotSeatScenario
{
    class HotSeatTurnScenario : IHotSeatRequestScenario
    {
        #region Dependencies
        private readonly MoveManager _moveManager;
        private readonly ConsoleUI _consoleUI;
        private readonly PropertyManager _propertyManager;
        private readonly TileManager _tileManager;
        private readonly InJailManager _inJailManager;
        private readonly PlayerManager _playerManager;
        private readonly TradeManager _tradeManager;
        #endregion

        #region TurnChoice
        public void TurnChoice(Player player)
        {
            MonopolyCommand curCommand;
            do
            {
                var commandList = new List<MonopolyCommand>
                {
                    MonopolyCommand.ManageProperty,
                    MonopolyCommand.MakeDeal
                };

                if (player.TurnsInJail == null)
                {
                    if (player.CanMove)
                        commandList.Add(MonopolyCommand.MakeMove);
                    else
                        commandList.Add(MonopolyCommand.EndTurn);
                }
                else if (!player.RolledJailDiceThisTurn)
                    commandList.AddRange(_inJailManager.GetAvailableJailCommands(player.Id));
                else
                    commandList.Add(MonopolyCommand.EndTurn);

                switch (curCommand = _consoleUI.ChooseCommand(commandList))
                {
                    case MonopolyCommand.ManageProperty:
                        ManageProperty(player);
                        break;
                    case MonopolyCommand.MakeDeal:
                        MakeDeal(player);
                        break;
                    case MonopolyCommand.MakeMove:
                        _moveManager.MakeAMove(player.Id);
                        break;
                    case MonopolyCommand.PayJailFine:
                        _inJailManager.PayFine(player.Id);
                        break;
                    case MonopolyCommand.JailRollDice:
                        _inJailManager.RollDice(player.Id);
                        break;
                    case MonopolyCommand.UseJailCard:
                        _inJailManager.UseJailCard(player.Id);
                        break;
                }
            } while (curCommand != MonopolyCommand.EndTurn);
        }
        #endregion

        #region ManageProperty
        public void ManageProperty(Player player)
        {
            while (true)
            {
                // choose property to manage
                if (!(_consoleUI.ChoosePropertyId(player.Properties) is int propId))
                    return;

                MonopolyCommand command;
                var property = _tileManager.GetTileComponent<Property>(propId);
                var development = _tileManager.GetTileComponent<PropertyDevelopment>(propId);
                do
                {
                    var availableActions = _propertyManager.GetAvailableActions(player.Id, property, development);
                    availableActions.Add(MonopolyCommand.CancelAction);

                    command = _consoleUI.ChooseCommand(availableActions);

                    switch (command)
                    {
                        case MonopolyCommand.MortgageProperty:
                            _propertyManager.Mortage(player.Id, property);
                            break;

                        case MonopolyCommand.UnmortgageProperty:
                            _propertyManager.UnMortage(player.Id, property);
                            break;

                        case MonopolyCommand.BuyHouse:
                            _propertyManager.BuildHouse(player.Id, development);
                            break;

                        case MonopolyCommand.SellHouse:
                            _propertyManager.SellHouse(player.Id, development);
                            break;
                    }
                } while (command != MonopolyCommand.CancelAction);
            }
        }
        #endregion

        #region MakeDeal
        public void MakeDeal(Player player)
        {
            // Creating a trade
            _consoleUI.Print("Choose a player to trade with");

            var playersNoSelf = _playerManager.GetAllPlayerId().Where(id => id != player.Id).ToList();
            _consoleUI.PrintPlayers(playersNoSelf);
            _consoleUI.Print("Write -1 to cancel");

            var playerIndex = _consoleUI.InputValueIndex(playersNoSelf, true);
            if (playerIndex == -1)
                return;

            var receiverId = playersNoSelf[playerIndex];
            var receiver = _playerManager.GetPlayer(receiverId);

            var offer = new TradeOffer();

            _consoleUI.Print("Choose assets the receiver will trade");
            offer.ReceiverAssets = _consoleUI.ChoosePlayerAssets(receiverId,
                _tradeManager.TradableProperties(receiver.Properties));

            _consoleUI.Print("Choose assets you will trade");
            offer.InitiatorAssets = _consoleUI.ChoosePlayerAssets(player.Id,
                _tradeManager.TradableProperties(player.Properties));

            _tradeManager.SendTradeValidationRequest(offer);
        }
        #endregion

        public void RunScenario(IRequest request, Player player)
        {
            TurnChoice(player);
        }

        #region ctor
        public HotSeatTurnScenario(MoveManager moveManager, 
            ConsoleUI consoleUI, 
            PropertyManager propertyManager, 
            TileManager tileManager,
            InJailManager inJailManager,
            PlayerManager playerManager,
            TradeManager tradeManager)
        {
            _moveManager = moveManager;
            _consoleUI = consoleUI;
            _propertyManager = propertyManager;
            _tileManager = tileManager;
            _inJailManager = inJailManager;
            _playerManager = playerManager;
            _tradeManager = tradeManager;
        }
        #endregion
    }
}
