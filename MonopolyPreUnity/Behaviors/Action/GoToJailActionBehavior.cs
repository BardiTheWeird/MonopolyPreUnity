using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Classes;
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
        private readonly MapInfo _mapInfo;
        #endregion

        public void Execute(int playerId, IMonopolyAction action)
        {
            if (_mapInfo.JailId == null)
                throw new Exception("No Jail present");

            _mapManager.MoveToTile(playerId, (int)_mapInfo.JailId, false);
            _playerManager.GetPlayer(playerId).TurnsInPrison = 0;
        }

        public GoToJailActionBehavior(PlayerManager playerManager, MapManager mapManager, TileManager tileManager, MapInfo mapInfo)
        {
            _playerManager = playerManager;
            _mapManager = mapManager;
            _tileManager = tileManager;
            _mapInfo = mapInfo;
        }
    }
}
