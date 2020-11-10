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
        #region StartGame
        //public void StartGame()
        //{
        //    _consoleUI.Print("Hooray! The game has started!");
        //    _consoleUI.PrintFormatted($"|player:{_turnInfo.CurTurnPlayerId}| begins the game");
        //    while (!isGameOver)
        //    {
        //        _requestManager.SendRequest(_turnInfo.CurTurnPlayerId, new TurnRequest());
        //        NextTurn();
        //    }
        //    _consoleUI.Print("The game is over!");
        //}
        #endregion

        #region OnPlayerBankrupt
        //void EndPlayer(object sender, PlayerEventArgs args)
        //{
        //    int curTurnOrderPosition = _turnInfo.TurnOrder.FindIndex(x => x == args.PlayerId);
        //    _turnInfo.TurnOrder.RemoveAt(curTurnOrderPosition);

        //    if (curTurnOrderPosition < _turnInfo.CurTurnPlayer || curTurnOrderPosition == _turnInfo.CurTurnPlayer)
        //    {
        //        _turnInfo.CurTurnPlayer--;
        //        if (_turnInfo.CurTurnPlayer < 0)
        //            _turnInfo.CurTurnPlayer = _turnInfo.TurnOrder.Count - 1;
        //    }

        //    if (_turnInfo.TurnOrder.Count <= 1)
        //        OnGameOver();
        //}

        //void OnGameOver()
        //{
        //    var winner = _playerManager.GetPlayer(_turnInfo.CurTurnPlayerId);
        //    winner.IsWinner = true;
        //    isGameOver = true;

        //    _consoleUI.PrintFormatted($"|player:{winner.Id}| is the winner!!!");
        //}
        #endregion
    }
}
