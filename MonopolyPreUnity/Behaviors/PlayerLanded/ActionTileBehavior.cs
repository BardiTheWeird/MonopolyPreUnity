using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.PlayerLanded
{
    class ActionTileBehavior : IPlayerLandedBehavior
    {
        #region Dependencies
        private readonly ActionManager _actionManager;
        #endregion

        public void PlayerLanded(int playerId, ITileComponent tileComponent, int tileId)
        {
            _actionManager.ExecuteAction(playerId, ((ActionComponent)tileComponent).Action);
        }

        public ActionTileBehavior(ActionManager actionManager)
        {
            _actionManager = actionManager;
        }
    }
}
