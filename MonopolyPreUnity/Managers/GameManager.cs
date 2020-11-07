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
        private bool isGameOver;
        #endregion

        #region StartGame
        public void StartGame()
        {
            Logger.Log("Hooray! The game has started!");
            while (!isGameOver)
            {
                Logger.Log("");
                var curTurnPlayerId = _turnInfo.TurnOrder[_turnInfo.CurTurnPlayer];
                _requestManager.SendRequest(curTurnPlayerId, new TurnRequest());

                NextTurn();
            }
            Logger.Log("The game is over!");
        }
        #endregion

        #region Methods
        void NextTurn()
        {
            _turnInfo.CurTurnPlayer = (_turnInfo.CurTurnPlayer + 1) % _turnInfo.TurnOrder.Count;

            _playerManager.GetPlayer(_turnInfo.CurTurnPlayerId).CanMove = true;

            Logger.Log("Next turn has commenced");
        }

        void EndPlayer(object sender, PlayerEventArgs args)
        {
            int curTurnOrderPosition = _turnInfo.TurnOrder.FindIndex(x => x == args.PlayerId);
            _turnInfo.TurnOrder.RemoveAt(curTurnOrderPosition);

            if (curTurnOrderPosition < _turnInfo.CurTurnPlayer || curTurnOrderPosition == _turnInfo.CurTurnPlayer)
            {
                _turnInfo.CurTurnPlayer--;
                if (_turnInfo.CurTurnPlayer < 0)
                    _turnInfo.CurTurnPlayer = _turnInfo.TurnOrder.Count - 1;
            }

            if (_turnInfo.TurnOrder.Count <= 1)
                OnGameOver();
        }

        void OnGameOver()
        {
            var winner = _playerManager.GetPlayer(_turnInfo.CurTurnPlayerId);
            winner.IsWinner = true;
            isGameOver = true;

            Logger.Log(winner.Id, "is the winner, I guess");
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

            playerManager.PlayerBankruptEvent += EndPlayer;
        }
        #endregion
    }
}
