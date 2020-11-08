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
        private readonly ConsoleUI _consoleUI;
        #endregion

        #region fields
        private readonly Dice _diceValues;
        private readonly int _maxDicePairThrows;
        #endregion

        public void MakeAMove(int playerId)
        {
            var currentPlayer = _playerManager.GetPlayer(playerId);
            _diceValues.Throw();

            _consoleUI.PrintFormatted($"|player:{playerId}| threw the dice and got {_diceValues.Die1} and {_diceValues.Die2}");

            if (currentPlayer.CanMove = _diceValues.Die1 == _diceValues.Die2)
                _consoleUI.Print("It's doubles, so they can move again!");

            MakeAMoveSteps(playerId, _diceValues.Sum);
        }

        public void MakeAMoveSteps(int playerId, int steps)
        {
            int tileId = _mapManager.MoveBySteps(playerId, steps);
            _consoleUI.PrintTileLanded(tileId, playerId);

            _playerLandedManager.PlayerLanded(playerId, tileId);
        }

        #region Constructor
        public MoveManager(PlayerManager playerManager, 
            TileManager tileManager, 
            MapManager mapManager, 
            PlayerLandedManager playerLandedManager,
            GameData gameData,
            GameConfig gameConfig,
            ConsoleUI consoleUI)
        {
            _playerManager = playerManager;
            _tileManager = tileManager;
            _mapManager = mapManager;
            _playerLandedManager = playerLandedManager;
            _diceValues = gameData.DiceValues;
            _maxDicePairThrows = gameConfig.MaxDicePairThrows;
            _consoleUI = consoleUI;
        }
        #endregion
    }
}
