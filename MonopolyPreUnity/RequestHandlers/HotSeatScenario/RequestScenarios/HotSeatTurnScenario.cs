using MonopolyPreUnity.Behaviors.Rent;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Requests;
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
        #endregion

        #region TurnChoice
        public void TurnChoice(Player player)
        {
            MonopolyCommand curCommand;
            do
            {
                var commandList = new List<MonopolyCommand>
                {
                    MonopolyCommand.TurnManageProperty,
                    MonopolyCommand.TurnMakeDeal
                };

                if (player.CanMove)
                    commandList.Add(MonopolyCommand.TurnMove);
                else
                    commandList.Add(MonopolyCommand.EndTurn);

                switch (curCommand = _consoleUI.ChooseCommand(commandList))
                {
                    case MonopolyCommand.TurnManageProperty:
                        ManageProperty(player);
                        break;
                    case MonopolyCommand.TurnMakeDeal:
                        MakeDeal(player);
                        break;
                    case MonopolyCommand.TurnMove:
                        _moveManager.MakeAMove(player.Id);
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
                var property = _tileManager.GetTileComponent<PropertyComponent>(propId);
                var development = _tileManager.GetTileComponent<PropertyDevelopmentComponent>(propId);
                do
                {
                    var availableActions = _propertyManager.GetAvailableActions(player.Id, property, development);
                    availableActions.Add(MonopolyCommand.CancelAction);

                    command = _consoleUI.ChooseCommand(availableActions);

                    switch (command)
                    {
                        case MonopolyCommand.PropertyMortgage:
                            _propertyManager.Mortage(player.Id, property);
                            break;

                        case MonopolyCommand.PropertyUnmortgage:
                            _propertyManager.UnMortage(player.Id, property);
                            break;

                        case MonopolyCommand.PropertyBuyHouse:
                            _propertyManager.BuildHouse(player.Id, development);
                            break;

                        case MonopolyCommand.PropertySellHouse:
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
            Console.WriteLine("Not doing anything yet");
        }
        #endregion

        public void RunScenario(IRequest request, Player player)
        {
            TurnChoice(player);
        }

        #region ctor
        public HotSeatTurnScenario(MoveManager moveManager, ConsoleUI consoleUI)
        {
            _moveManager = moveManager;
            _consoleUI = consoleUI;
        }
        #endregion
    }
}
