using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Action
{
    class GoToTileIdActionBehavior : IActionBehavior
    {
        private readonly MapManager _mapManager;
        public void Execute(int playerId, IMonopolyAction action)
        {
            _mapManager.MoveToTile(playerId, (action as GoToTileIdAction).TileId);
        }

        public GoToTileIdActionBehavior(MapManager mapManager)
        {
            _mapManager = mapManager;
        }
    }
}
