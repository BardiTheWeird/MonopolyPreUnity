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
        private readonly RequestManager _requestManager;
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

        public void EndPlayer(int playerId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Constructor
        public GameManager(PlayerManager playerManager, 
            RequestManager requestManager,
            GameData gameData)
        {
            _playerManager = playerManager;
            _requestManager = requestManager;
            _turnInfo = gameData.TurnInfo;
        }
        #endregion
    }
}
