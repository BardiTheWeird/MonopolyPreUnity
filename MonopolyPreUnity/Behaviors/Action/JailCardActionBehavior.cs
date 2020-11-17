using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Components.SystemRequest.Jail;
using MonopolyPreUnity.Entity;

using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Action
{
    class JailCardActionBehavior : IActionBehavior
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        public void Execute(int playerId, IMonopolyAction action)
        {
            _context.Add(new GiveJailCard(playerId));
        }

        public JailCardActionBehavior(Context context) =>
            _context = context;
    }
}
