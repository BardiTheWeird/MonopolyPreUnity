using MonopolyPreUnity.Components;
using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.PlayerLanded
{
    class ActionBoxBehavior : IPlayerLandedBehavior
    {
        #region Dependencies
        private readonly ActionManager _actionManager;
        #endregion

        IMonopolyAction ChooseAction(List<IMonopolyAction> actionList)
        {
            return actionList[new Random().Next(0, actionList.Count)];
        }

        public void PlayerLanded(int playerId, ITileComponent tileComponent, int tileId)
        {
            _actionManager.ExecuteAction(playerId, ChooseAction(((ActionBoxComponent)tileComponent).ActionList));
        }
    }
}
