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
        #endregion

        public bool CanMove { get; private set; }
        public (int, int) DiceValues { get; private set; }

        private void ThrowDice()
        {
            DiceValues = Dice.Throw();
        }

        public void MakeAMove(int playerId)
        {
            this.ThrowDice();
            CanMove = DiceValues.Item1 == DiceValues.Item2;

            int tileId = _mapManager.MoveBySteps(playerId, DiceValues.Item1 + DiceValues.Item2);
            _tileManager.Get<ITileContentComponent>(tileId).OnPlayerLanded(playerId);
            
        }
    }
}
