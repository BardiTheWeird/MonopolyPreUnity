using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.UI;
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
        private readonly MapInfo _mapInfo;
        private readonly ConsoleUI _consoleUI;
        #endregion

        public void Execute(int playerId, IMonopolyAction action)
        {
            if (_mapInfo.JailId == null)
                _consoleUI.Print("No Jail present");
            else
            {
                _mapManager.MoveToTile(playerId, (int)_mapInfo.JailId, false);
                _playerManager.GetPlayer(playerId).TurnsInJail = 0;
                _consoleUI.PrintFormatted($"|player:{playerId}| was moved to Jail");
            }
        }

        public GoToJailActionBehavior(PlayerManager playerManager, 
            MapManager mapManager, 
            ConsoleUI consoleUI, 
            MapInfo mapInfo)
        {
            _playerManager = playerManager;
            _mapManager = mapManager;
            _consoleUI = consoleUI;
            _mapInfo = mapInfo;
        }
    }
}
