using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Requests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class TurnRequestSystem : ISystem
    {
        private readonly Context _context;

        public void Execute()
        {
            if (!_context.ContainsComponent<TurnInfo>())
                return;

            if (_context.ContainsComponent<PlayerInputRequest>())
                return;
            if (!_context.HSInputState().IsNull)
                return;

            var curTurnPlayerId = _context.TurnInfo().CurTurnPlayerId;
            //_context.Add(new PrintFormattedLine($"|player:{curTurnPlayerId}| makes a move"));
            _context.Add(new PlayerInputRequest(curTurnPlayerId, new TurnRequest()));
        }

        #region ctor
        public TurnRequestSystem(Context context)
        {
            _context = context;
        }
        #endregion
    }
}
