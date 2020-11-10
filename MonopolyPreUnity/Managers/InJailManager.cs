using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.UI;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class InJailManager
    {
        #region methods
        //public void UseJailCard(int playerId)
        //{
        //    var player = _playerManager.GetPlayer(playerId);
        //    if (player.JailCards <= 0)
        //    {
        //        _consoleUI.Print("Player has less than 0 jail cards");
        //        return;
        //    }
        //    player.JailCards--;
        //    player.TurnsInJail = null;
        //    _consoleUI.PrintFormatted($"|player:{playerId}| used a Jail Card to get out of prison. {player.JailCards} cards left");
        //}

        //public void PayFine(int playerId)
        //{
        //    var player = _playerManager.GetPlayer(playerId);
        //    _playerManager.PlayerCashCharge(playerId, _jailFine, message: "to get out of prison");

        //    if (!player.IsBankrupt)
        //        player.TurnsInJail = null;
        //}

        //public void RollDice(int playerId)
        //{
        //    var player = _playerManager.GetPlayer(playerId);
        //    if (player.TurnsInJail >= _maxTurnsInJail)
        //    {
        //        _consoleUI.Print($"Can't roll dice after being in jail for more than MaxTurnInJail ({_maxTurnsInJail})");
        //        return;
        //    }
        //    _dice.Throw();
        //    player.RolledJailDiceThisTurn = true;
        //    _consoleUI.PrintFormatted($"|player:{playerId}| threw the dice and got {_dice.Die1} and {_dice.Die2}");
        //    if (_dice.Die1 == _dice.Die2)
        //    {
        //        _consoleUI.Print("It's doubles! They get to get out of prison for free!");
        //        player.TurnsInJail = null;
        //        player.CanMove = false;
        //        _moveManager.MakeAMoveSteps(playerId, _dice.Sum);
        //    }
        //    else
        //    {
        //        player.TurnsInJail++;
        //        _consoleUI.Print($"It's not doubles. Current Turns in Jail: {player.TurnsInJail}");
        //    }
        //}
        #endregion
    }
}
