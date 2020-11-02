using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;

namespace MonopolyPreUnity.RequestHandlers.HotSeatScenario
{
    class HotSeatTurnScenario : IHotSeatRequestScenario
    {
        #region Dependencies
        private readonly MoveManager _moveManager;
        private readonly ConsoleUI _consoleUI;
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
