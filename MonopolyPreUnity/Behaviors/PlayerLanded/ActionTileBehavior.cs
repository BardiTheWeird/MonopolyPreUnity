using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Entity;

using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.PlayerLanded
{
    class ActionTileBehavior : IPlayerLandedBehavior
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        public void PlayerLanded(Player player, IEntityComponent component) =>
            _context.Add(new ExecuteAction(((ActionTile)component).Action, player.Id));

        public ActionTileBehavior(Context context) =>
            _context = context;
    }
}
