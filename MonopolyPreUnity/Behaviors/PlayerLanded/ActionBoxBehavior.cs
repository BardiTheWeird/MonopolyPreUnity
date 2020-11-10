using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.PlayerLanded
{
    class ActionBoxBehavior : IPlayerLandedBehavior
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        IMonopolyAction ChooseAction(List<IMonopolyAction> actionList) =>
            actionList[new Random().Next(0, actionList.Count)];

        public void PlayerLanded(Player player, IEntityComponent component)
            => _context.Add(new ExecuteAction(ChooseAction((ActionBox)component), player.Id));

        public ActionBoxBehavior(Context context) =>
            _context = context;
    }
}
