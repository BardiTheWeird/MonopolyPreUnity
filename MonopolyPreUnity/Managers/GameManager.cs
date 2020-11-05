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
                var curTurnPlayerId = _turnInfo.TurnOrder[_turnInfo.CurTurnPlayer];
                _requestManager.SendRequest(curTurnPlayerId, new TurnRequest());

                NextTurn();
            }
        }
        #endregion

        #region Methods
        void NextTurn()
        {
            _playerManager.GetPlayer(_turnInfo.TurnOrder[_turnInfo.CurTurnPlayer]).CanMove = true;

            _turnInfo.CurTurnPlayer++;
            if (_turnInfo.CurTurnPlayer >= _turnInfo.TurnOrder.Count)
                _turnInfo.CurTurnPlayer = 0;

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
