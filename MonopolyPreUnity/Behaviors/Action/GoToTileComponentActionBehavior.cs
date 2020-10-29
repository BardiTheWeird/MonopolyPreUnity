using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Managers;
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
        #endregion

        public void Execute(int playerId, IMonopolyAction action)
        {
            var componentType = (action as GoToTileComponentAction).ComponentType;
            _mapManager.MoveByFunc(playerId, x => x.Components.FirstOrDefault(comp => comp.GetType() == componentType) != null);
        }

        public GoToTileComponentActionBehavior(MapManager mapManager)
        {
            _mapManager = mapManager;
        }
    }
}
