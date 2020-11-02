using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Requests;
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

        #region StartGame
        public void StartGame()
        {
            Logger.Log("Hooray! The game has started!");
            while (true)
            {
                Logger.Log("");
                var curTurnPlayerId = _turnInfo.turnOrder[_turnInfo.curTurnPlayer];
                _requestManager.SendRequest(curTurnPlayerId, new TurnRequest());

                NextTurn();
            }
        }
        #endregion

        #region Methods
        void NextTurn()
        {
            _playerManager.GetPlayer(_turnInfo.turnOrder[_turnInfo.curTurnPlayer]).CanMove = true;

            _turnInfo.curTurnPlayer++;
            if (_turnInfo.curTurnPlayer >= _turnInfo.turnOrder.Count)
                _turnInfo.curTurnPlayer = 0;

            Logger.Log("Next turn has commenced");
        }

        // kinda obsolete
        void CommandDecision(int playerId)
        {
            var player = _playerManager.GetPlayer(playerId);

            MonopolyCommand command;
            do
            {
                var possibleCommands = new List<MonopolyCommand>();

                possibleCommands.Add(MonopolyCommand.TurnMakeDeal);
                if (player.Properties.Count > 0)
                    possibleCommands.Add(MonopolyCommand.TurnManageProperty);

                if (player.CanMove)
                    possibleCommands.Add(MonopolyCommand.TurnMove);
                else
                    possibleCommands.Add(MonopolyCommand.EndTurn);

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

                    case MonopolyCommand.TurnMove:
                        _moveManager.MakeAMove(playerId);
                        break;
                }
            } while (command != MonopolyCommand.EndTurn);
            player.CanMove = true;
        }
        #endregion

        #region Constructor
        public GameManager(PlayerManager playerManager, 
            MoveManager moveManager, 
            RequestManager requestManager, 
            PropertyManager propertyManager, 
            GameData gameData)
        {
            _playerManager = playerManager;
            _moveManager = moveManager;
            _requestManager = requestManager;
            _propertyManager = propertyManager;
            _turnInfo = gameData.TurnInfo;
        }
        #endregion
    }
}
