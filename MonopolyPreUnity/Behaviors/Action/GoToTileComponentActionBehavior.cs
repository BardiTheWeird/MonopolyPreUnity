using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Action
{
    class GoToTileComponentActionBehavior : IActionBehavior
    {
        #region Dependencies
        private readonly MapManager _mapManager;
        private readonly ConsoleUI _consoleUI; 
        #endregion

        public void Execute(int playerId, IMonopolyAction action)
        {
            var componentType = (action as GoToTileComponentAction).ComponentType;
            _mapManager.MoveByFunc(playerId, x => x.Components.FirstOrDefault(comp => comp.GetType() == componentType) != null);
            _consoleUI.PrintFormatted($"|player:{playerId}| was moved to a nearest {_consoleUI.GetTypeString(componentType)} tile");
        }

        public GoToTileComponentActionBehavior(MapManager mapManager, ConsoleUI consoleUI)
        {
            _mapManager = mapManager;
            _consoleUI = consoleUI;
        }
    }
}
