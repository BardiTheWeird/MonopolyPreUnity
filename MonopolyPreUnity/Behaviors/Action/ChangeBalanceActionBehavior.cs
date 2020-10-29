using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Action
{
    class ChangeBalanceActionBehavior : IActionBehavior
    {
        #region Dependencies
        private readonly PlayerManager _playerManager;
        #endregion

        public void Execute(int playerId, IMonopolyAction action) =>
            _playerManager.ChangeBalance(playerId, (action as ChangeBalanceAction).Amount);

        public ChangeBalanceActionBehavior(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }
    }
}
