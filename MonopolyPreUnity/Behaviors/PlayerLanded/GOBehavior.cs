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


        public void PlayerLanded(int playerId, ITileComponent tileComponent, int tileId)
        {
            var amount = (tileComponent as GoComponent).MoneyRewarded;
            _playerManager.PlayerCashGive(playerId, amount);
            Logger.Log($"Player recieved {amount} for passing the GO.");
        }

        #region Constructor
        public GOBehavior(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }
        #endregion
    }
}
