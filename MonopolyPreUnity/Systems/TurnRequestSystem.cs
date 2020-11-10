using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class TurnRequestSystem : ISystem
    {
        private readonly Context _context;

        public void Execute()
        {
            if (_context.ContainsComponent<PlayerInputRequest>() || _context.ContainsComponent<PlayerBusy>())
                return;

            var curTurnPlayerId = _context.TurnInfo().CurTurnPlayerId;
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
