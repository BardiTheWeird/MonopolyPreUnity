using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Components.SystemRequest.Move;
using MonopolyPreUnity.Entity;

using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Action
{
    class GoToTileIdActionBehavior : IActionBehavior
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        public void Execute(int playerId, IMonopolyAction action)
        {
            var tileId = (action as GoToTileIdAction).TileId;
            _context.Add(new MoveTileId(playerId, tileId, true));
        }

        public GoToTileIdActionBehavior(Context context) =>
            _context = context;
    }
}
