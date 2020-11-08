using MonopolyPreUnity.Actions;
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

        public void Execute(int playerId, IMonopolyAction actionIn)
        {
            var action = actionIn as ChangeBalanceAction;
            if (action.Amount < 0)
                _playerManager.PlayerCashCharge(playerId, -action.Amount);
            else
                _playerManager.PlayerCashGive(playerId, action.Amount);
        }

        public ChangeBalanceActionBehavior(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }
    }
}
