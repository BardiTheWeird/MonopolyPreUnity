using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Text;

using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Classes;

namespace MonopolyPreUnity.Behaviors.PlayerLanded
{
    class GOBehavior : IPlayerLandedBehavior
    {
        #region Dependencies
        private readonly PlayerManager _playerManager;
        #endregion

        #region constants
        private readonly int _moneyParLap;
        #endregion

        public void PlayerLanded(int playerId, ITileComponent tileComponent, int tileId)
        {
            _playerManager.PlayerCashGive(playerId, _moneyParLap);
            Logger.Log($"Player recieved {_moneyParLap} for passing the GO.");
        }

        #region Constructor
        public GOBehavior(PlayerManager playerManager, GameConfig gameConfig)
        {
            _playerManager = playerManager;
            _moneyParLap = gameConfig.CashPerLap;
        }
        #endregion
    }
}
