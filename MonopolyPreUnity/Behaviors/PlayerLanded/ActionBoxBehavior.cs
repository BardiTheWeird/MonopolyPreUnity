﻿using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Components;
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
            _actionManager.ExecuteAction(playerId, ChooseAction(((ActionBox)tileComponent).ActionList));
        }

        public ActionBoxBehavior(ActionManager actionManager)
        {
            _actionManager = actionManager;
        }
    }
}
