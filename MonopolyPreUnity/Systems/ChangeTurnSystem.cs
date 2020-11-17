using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Components.SystemRequest.PlayerInput;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class ChangeTurnSystem : ISystem
    {
        private readonly Context _context;

        public void Execute()
        {
            var request = _context.GetComponent<EndTurn>();
            if (request == null)
                return;

            var turnInfo = _context.TurnInfo();
            turnInfo.CurTurnPlayer = (turnInfo.CurTurnPlayer + 1) % turnInfo.TurnOrder.Count;
            var curTurnPlayer = _context.GetPlayer(turnInfo.CurTurnPlayerId);
            curTurnPlayer.CanMove = true;
            curTurnPlayer.RolledJailDiceThisTurn = false;

            _context.Add(new ClearOutput());
            _context.Add(new PrintFormattedLine
                ($"Next turn. It's time for |player:{turnInfo.CurTurnPlayerId}| to make a move!", OutputStream.GameLog));

            _context.Remove<EndTurn>();
        }

        public ChangeTurnSystem(Context context)
        {
            _context = context;
        }
    }
}
