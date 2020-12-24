using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Entity;

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
        {
            var chosenAction = ChooseAction((ActionBox)component);
            _context.Add(new ExecuteAction(chosenAction, player.Id));

            _context.RenderCommunications.CurDescription = chosenAction.Descsription;
        }

        public ActionBoxBehavior(Context context) =>
            _context = context;
    }
}
