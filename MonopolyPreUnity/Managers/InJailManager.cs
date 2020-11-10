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
        #region Dependencies
        private readonly PlayerManager _playerManager;
        private readonly Dice _dice;
        private readonly ConsoleUI _consoleUI;
        private readonly MoveManager _moveManager;
        #endregion

        #region const
        private readonly int _maxTurnsInJail;
        private readonly int _jailFine;
        #endregion

        public List<MonopolyCommand> GetAvailableJailCommands(int playerId)
        {
            var player = _playerManager.GetPlayer(playerId);
            var hasJailCard = player.JailCards > 0;
            var canRoll = !player.RolledJailDiceThisTurn && player.TurnsInJail < _maxTurnsInJail;
            var canPay = player.Cash >= _jailFine;

            var commands = new List<MonopolyCommand>();
            if (hasJailCard) commands.Add(MonopolyCommand.UseJailCard);
            if (canRoll) commands.Add(MonopolyCommand.JailRollDice);
            if (canPay || commands.Count == 0) commands.Add(MonopolyCommand.PayJailFine);

            return commands;
        }

        public void UseJailCard(int playerId)
        {
            var player = _playerManager.GetPlayer(playerId);
            if (player.JailCards <= 0)
            {
                _consoleUI.Print("Player has less than 0 jail cards");
                return;
            }
            player.JailCards--;
            player.TurnsInJail = null;
            _consoleUI.PrintFormatted($"|player:{playerId}| used a Jail Card to get out of prison. {player.JailCards} cards left");
        }

        public void PayFine(int playerId)
        {
            var player = _playerManager.GetPlayer(playerId);
            _playerManager.PlayerCashCharge(playerId, _jailFine, message: "to get out of prison");

            if (!player.IsBankrupt)
                player.TurnsInJail = null;
        }

        public void RollDice(int playerId)
        {
            var player = _playerManager.GetPlayer(playerId);
            if (player.TurnsInJail >= _maxTurnsInJail)
            {
                _consoleUI.Print($"Can't roll dice after being in jail for more than MaxTurnInJail ({_maxTurnsInJail})");
                return;
            }
            _dice.Throw();
            player.RolledJailDiceThisTurn = true;
            _consoleUI.PrintFormatted($"|player:{playerId}| threw the dice and got {_dice.Die1} and {_dice.Die2}");
            if (_dice.Die1 == _dice.Die2)
            {
                _consoleUI.Print("It's doubles! They get to get out of prison for free!");
                player.TurnsInJail = null;
                player.CanMove = false;
                _moveManager.MakeAMoveSteps(playerId, _dice.Sum);
            }
            else
            {
                player.TurnsInJail++;
                _consoleUI.Print($"It's not doubles. Current Turns in Jail: {player.TurnsInJail}");
            }
        }

        #region Constructor
        public InJailManager(MoveManager moveManager, 
            PlayerManager playerManager, 
            GameData gameData, 
            GameConfig gameConfig,
            ConsoleUI consoleUI)
        {
            _playerManager = playerManager;
            _dice = gameData.DiceValues;
            _consoleUI = consoleUI;
            _moveManager = moveManager;

            _maxTurnsInJail = gameConfig.MaxTurnsInJail;
            _jailFine = gameConfig.JailFine;
        }
        #endregion
    }
}
