using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class MoveManager
    {
        #region dependencies
        private readonly PlayerManager _playerManager;
        private readonly TileManager _tileManager;
        private readonly MapManager _mapManager;
        private readonly PlayerLandedManager _playerLandedManager;
        private readonly InJailManager _inJailManager;
        private readonly ConsoleUI _consoleUI;
        #endregion

        #region fields
        private readonly Dice _diceValues;
        private readonly int _maxDicePairThrows;
        #endregion

        public void MakeAMove(int playerId)
        {
            var currentPlayer = _playerManager.GetPlayer(playerId);
            if (currentPlayer.TurnsInPrison == null)
            {
                _diceValues.Throw();

                _consoleUI.PrintFormatted($"|player:{playerId}| threw the dice and got {_diceValues.Die1} and {_diceValues.Die2}");

                if (currentPlayer.CanMove = _diceValues.Die1 == _diceValues.Die2)
                    _consoleUI.Print("It's doubles, so they can move again!");

                int tileId = _mapManager.MoveBySteps(playerId, _diceValues.Sum);
                _consoleUI.PrintTileLanded(tileId, playerId);

                _playerLandedManager.PlayerLanded(playerId, tileId);
            }
            else
            {
                var result = _inJailManager.InJailMove(currentPlayer);
                
                if(result.Item1 == true && result.Item2 == Utitlity.MonopolyCommand.JailUseDice)
                {
                    currentPlayer.CanMove = false;
                    int tileId = _mapManager.MoveBySteps(playerId, _diceValues.Sum);

                    Logger.Log(playerId, $"landed on tile {tileId}");

                    _playerLandedManager.PlayerLanded(playerId, tileId);
                }
                else if(result.Item1)
                {
                    MakeAMove(playerId);
                }
            }
        }

        #region Constructor
        public MoveManager(PlayerManager playerManager, 
            TileManager tileManager, 
            MapManager mapManager, 
            PlayerLandedManager playerLandedManager,
            GameData gameData,
            GameConfig gameConfig,
            ConsoleUI consoleUI,
            InJailManager inJailManager)
        {
            _playerManager = playerManager;
            _tileManager = tileManager;
            _mapManager = mapManager;
            _playerLandedManager = playerLandedManager;
            _diceValues = gameData.DiceValues;
            _maxDicePairThrows = gameConfig.MaxDicePairThrows;
            _consoleUI = consoleUI;
            _inJailManager = inJailManager;
        }
        #endregion
    }
}
