using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Action
{
    class JailCardActionBehavior : IActionBehavior
    {
        #region Dependencies
        private readonly PlayerManager _playerManager;
        #endregion

        public void Execute(int playerId, IMonopolyAction action) =>
            _playerManager.GetPlayer(playerId).JailCards++;

        public JailCardActionBehavior(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }
    }
}
