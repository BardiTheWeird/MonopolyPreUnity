using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Action
{
    class GoToJailActionBehavior : IActionBehavior
    {
        #region Dependencies
        private readonly PlayerManager _playerManager;
        private readonly MapManager _mapManager;
        private readonly TileManager _tileManager;
        #endregion

        public void Execute(int playerId, IMonopolyAction action)
        {
            int jailId = _tileManager.GetTileWithComponent<JailComponent>();
            _mapManager.MoveToTile(playerId, jailId, false);

            _playerManager.GetPlayer(playerId).TurnsInPrison = 0;
        }

        public GoToJailActionBehavior(PlayerManager playerManager, MapManager mapManager, TileManager tileManager)
        {
            _playerManager = playerManager;
            _mapManager = mapManager;
            _tileManager = tileManager;
        }
    }
}
