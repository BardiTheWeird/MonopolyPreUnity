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

        private int amount;

        public void PlayerLanded(int playerId, ITileComponent tileComponent, int tileId)
        {
            _playerManager.PlayerCashGive(playerId, amount);
            Logger.Log($"Player recieved {amount} for passing the GO.");
        }
    }
}
