using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.UI;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.HotSeatScenario
{
    class HotSeatBankruptcyScenario : IHotSeatRequestScenario
    {
        #region Dependencies
        private readonly ConsoleUI _consoleUI;
        private readonly TileManager _tileManager;
        private readonly PropertyManager _propertyManager;
        #endregion

        public void RunScenario(IRequest requestIn, Player player)
        {
            var request = requestIn as BankruptcyRequest;

            _consoleUI.PrintFormatted($"|player:{player.Id}|, you can still pay the debt off" +
                $"to stay in the game by selling houses or mortgaging property");
            _consoleUI.Print($"Amount needed to pay the debt: {request.DebtAmount - player.Cash}");

            while (true)
            {
                // choose property to manage
                if (!(_consoleUI.ChoosePropertyId(player.Properties) is int propId))
                    return;

                MonopolyCommand command;
                var property = _tileManager.GetTileComponent<PropertyComponent>(propId);
                var development = _tileManager.GetTileComponent<PropertyDevelopmentComponent>(propId);
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

                    if (request.DebtAmount - player.Cash > 0)
                        _consoleUI.Print($"Amount needed to pay the debt: {request.DebtAmount - player.Cash}");
                    else
                        _consoleUI.Print("The debt can be paid off!");

                } while (command != MonopolyCommand.CancelAction);
            }
        }

        #region ctor
        public HotSeatBankruptcyScenario(ConsoleUI consoleUI, TileManager tileManager, PropertyManager propertyManager)
        {
            _consoleUI = consoleUI;
            _tileManager = tileManager;
            _propertyManager = propertyManager;
        }
        #endregion
    }
}
