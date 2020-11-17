using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Components.SystemRequest.Cash;
using MonopolyPreUnity.Entity;

using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Action
{
    class ChangeBalanceActionBehavior : IActionBehavior
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        public void Execute(int playerId, IMonopolyAction actionIn)
        {
            var action = actionIn as ChangeBalanceAction;
            if (action.Amount < 0)
                _context.Add(new ChargeCash(-action.Amount, playerId));
            else
                _context.Add(new GiveCash(action.Amount, playerId));
        }

        #region ctor
        public ChangeBalanceActionBehavior(Context context) =>
            _context = context;
        #endregion
    }
}
