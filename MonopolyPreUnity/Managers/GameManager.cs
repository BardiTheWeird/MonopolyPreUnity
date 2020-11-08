using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.UI;
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
        private readonly ConsoleUI _consoleUI;
        #endregion

        #region fields
        private readonly TurnInfo _turnInfo;
        private bool isGameOver;
        #endregion

        #region StartGame
        public void StartGame()
        {
            _consoleUI.Print("Hooray! The game has started!");
            _consoleUI.PrintFormatted($"|player:{_turnInfo.CurTurnPlayerId}| begins the game");
            while (!isGameOver)
            {
                _requestManager.SendRequest(_turnInfo.CurTurnPlayerId, new TurnRequest());
                NextTurn();
            }
            _consoleUI.Print("The game is over!");
        }
        #endregion

        #region NextTurn
        void NextTurn()
        {
            _turnInfo.CurTurnPlayer = (_turnInfo.CurTurnPlayer + 1) % _turnInfo.TurnOrder.Count;
            _playerManager.GetPlayer(_turnInfo.CurTurnPlayerId).CanMove = true;

            _consoleUI.PrintFormatted($"Next turn. It's time for |player:{_turnInfo.CurTurnPlayerId}| to make a move!");
        }
        #endregion

        #region OnPlayerBankrupt
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

            _consoleUI.PrintFormatted($"|player:{winner.Id}| is the winner!!!");
        }
        #endregion

        #region Constructor
        public GameManager(PlayerManager playerManager, 
            RequestManager requestManager,
            GameData gameData,
            ConsoleUI consoleUI)
        {
            _playerManager = playerManager;
            _requestManager = requestManager;
            _turnInfo = gameData.TurnInfo;
            _consoleUI = consoleUI;

            playerManager.PlayerBankruptEvent += EndPlayer;
        }
        #endregion
    }
}
