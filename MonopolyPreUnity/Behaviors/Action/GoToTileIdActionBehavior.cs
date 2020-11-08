using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Action
{
    class GoToTileIdActionBehavior : IActionBehavior
    {
        #region Dependencies
        private readonly MapManager _mapManager;
        private readonly ConsoleUI _consoleUI;
        #endregion

        public void Execute(int playerId, IMonopolyAction action)
        {
            var tileId = (action as GoToTileIdAction).TileId;
            _mapManager.MoveToTile(playerId, tileId);
            _consoleUI.PrintFormatted($"|player:{playerId}| was moved to |tile:{tileId}|");
        }

        public GoToTileIdActionBehavior(MapManager mapManager, ConsoleUI consoleUI)
        {
            _mapManager = mapManager;
            _consoleUI = consoleUI;
        }
    }
}
