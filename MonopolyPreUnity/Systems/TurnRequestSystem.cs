using MonopolyPreUnity.Components.SystemRequest;
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
            if (!_context.ContainsComponent<PlayerInputRequest>())
            {
                var curTurnPlayerId = _context.TurnInfo().CurTurnPlayerId;
                _context.AddEntity(new PlayerInputRequest(curTurnPlayerId, new TurnRequest()));
            }
        }

        #region ctor
        public TurnRequestSystem(Context context)
        {
            _context = context;
        }
        #endregion
    }
}
