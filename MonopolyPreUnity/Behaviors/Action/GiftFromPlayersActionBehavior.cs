using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Action
{
    class GiftFromPlayersActionBehavior : IActionBehavior
    {
        #region Dependencies
        private readonly PlayerManager _playerManager;
        #endregion

        public void Execute(int playerId, IMonopolyAction action)
        {
            var sum = 0;
            var amountPerPlayer = (action as GiftFromPlayersAction).Amount;
            foreach (var player in _playerManager.GetAllPlayerId())
            {
                _playerManager.PlayerCashCharge(playerId, amountPerPlayer, playerId);
                sum += amountPerPlayer;
            }
            _playerManager.PlayerCashGive(playerId, sum);
        }

        public GiftFromPlayersActionBehavior(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }
    }
}
