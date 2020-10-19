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

        public (int, int) DiceValues { get; private set; }

        private void ThrowDice()
        {
            DiceValues = Dice.Throw();
        }

        public void MakeAMove(int playerId)
        {
            ThrowDice();
            var currentPlayer =_playerManager.GetPlayer(playerId);
            currentPlayer.CanMove = DiceValues.Item1 == DiceValues.Item2;

            int tileId = _mapManager.MoveBySteps(playerId, DiceValues.Item1 + DiceValues.Item2);
            _playerLandedManager.PlayerLanded(playerId, tileId);
            
        }
    }
}
