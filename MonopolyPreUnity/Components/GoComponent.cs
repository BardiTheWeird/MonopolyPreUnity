using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class GoComponent : ITileContentComponent
    {
        private readonly PlayerManager _playerManager;
        public int MoneyRewarded { get; }

        public void OnPlayerLanded(int playerId)
        {
            _playerManager.PlayerCashGive(playerId, MoneyRewarded);
        }

        public GoComponent(int moneyRewarded, PlayerManager playerManager)
        {
            MoneyRewarded = moneyRewarded;
            _playerManager = playerManager;
        }
    }
}
