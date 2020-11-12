using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.Cash;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Action
{
    class GiftFromPlayersActionBehavior : IActionBehavior
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        public void Execute(int playerId, IMonopolyAction action)
        {
            var amountPerPlayer = (action as GiftFromPlayersAction).Amount;
            foreach (var player in _context.GetPlayers(p => p.Id != playerId))
                _context.Add(new ChargeCash(amountPerPlayer, player.Id, playerId));
        }

        #region ctor
        public GiftFromPlayersActionBehavior(Context context) =>
            _context = context;
        #endregion
    }
}
