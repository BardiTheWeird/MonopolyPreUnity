using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class GameManager
    {
        #region Dependencies
        private readonly PlayerManager _playerManager;
        private readonly MoveManager _moveManager;
        private readonly RequestManager _requestManager;
        private readonly PropertyManager _propertyManager;
        #endregion

        #region fields
        private readonly TurnInfo _turnInfo;
        #endregion

        #region Methods
        void StartNextTurn()
        {
            _turnInfo.curTurnPlayer++;
            if (_turnInfo.curTurnPlayer >= _turnInfo.turnOrder.Count)
                _turnInfo.curTurnPlayer = 0;

            CommandDecision(_turnInfo.turnOrder[_turnInfo.curTurnPlayer]);
        }

        void CommandDecision(int playerId)
        {
            var player = _playerManager.GetPlayer(playerId);
            var possibleCommands = new List<MonopolyCommand>();

            possibleCommands.Add(MonopolyCommand.TurnMakeDeal);
            if (player.Properties.Count > 0)
                possibleCommands.Add(MonopolyCommand.TurnManageProperty);

            if (player.CanMove)
                possibleCommands.Add(MonopolyCommand.TurnMakeMove);
            else
                possibleCommands.Add(MonopolyCommand.TurnEndTurn);

            MonopolyCommand command;
            do
            {
                command = _requestManager.SendRequest(playerId,
                    new Request<MonopolyCommand>(MonopolyRequest.TurnCommandChoice, possibleCommands));

                switch (command)
                {
                    case MonopolyCommand.TurnManageProperty:
                        _propertyManager.ManageProperty(playerId);
                        break;

                    case MonopolyCommand.TurnMakeDeal:
                        Console.WriteLine("You can't trade yet)");
                        break;

                    case MonopolyCommand.TurnMakeMove:
                        _moveManager.MakeAMove(playerId);
                        break;
                }
            } while (command != MonopolyCommand.TurnEndTurn);
            StartNextTurn();
        }
        #endregion

        #region Constructor
        public GameManager(PlayerManager playerManager, 
            MoveManager moveManager, 
            RequestManager requestManager, 
            PropertyManager propertyManager, 
            TurnInfo turnInfo)
        {
            _playerManager = playerManager;
            _moveManager = moveManager;
            _requestManager = requestManager;
            _propertyManager = propertyManager;
            _turnInfo = turnInfo;
        }
        #endregion
    }
}
