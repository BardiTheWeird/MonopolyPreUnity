using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Interfaces;
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
        #endregion

        #region fields
        private readonly Dice _diceValues;
        #endregion

        public void MakeAMove(int playerId)
        {
            _diceValues.Throw();
            Logger.Log(playerId, $"throws the dice and gets {_diceValues.Die1} and {_diceValues.Die2}");
            var currentPlayer =_playerManager.GetPlayer(playerId);
            currentPlayer.CanMove = _diceValues.Die1 == _diceValues.Die2;

            int tileId = _mapManager.MoveBySteps(playerId, _diceValues.Sum);
            _playerLandedManager.PlayerLanded(playerId, tileId);
        }

        #region Constructor
        public MoveManager(PlayerManager playerManager, 
            TileManager tileManager, 
            MapManager mapManager, 
            PlayerLandedManager playerLandedManager,
            GameData gameData)
        {
            _playerManager = playerManager;
            _tileManager = tileManager;
            _mapManager = mapManager;
            _playerLandedManager = playerLandedManager;
            _diceValues = gameData.DiceValues;
        }
        #endregion
    }
}
