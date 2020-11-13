using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Action
{
    class GoToJailActionBehavior : IActionBehavior
    {
        private readonly Context _context;

        public void Execute(int playerId, IMonopolyAction action)
        {
            var mapInfo = _context.MapInfo();
            if (mapInfo.JailId == null)
            {
                _context.Add(new PrintLine("No Jail present", OutputStream.GameLog));
                return;
            }

            _context.Add(new SendToJail(playerId));
        }

        public GoToJailActionBehavior(Context context) =>
            _context = context;
    }
}
