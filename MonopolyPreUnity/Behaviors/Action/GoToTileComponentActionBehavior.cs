using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Components.SystemRequest.Move;
using MonopolyPreUnity.Entity;

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
        private readonly Context _context;
        #endregion

        public void Execute(int playerId, IMonopolyAction action)
        {
            var componentType = (action as GoToTileComponentAction).ComponentType;
            _context.Add(new MoveType(playerId, componentType, false));
        }

        public GoToTileComponentActionBehavior(Context context) =>
            _context = context;
    }
}
