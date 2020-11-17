using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Components.SystemState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class GameStartSystem : ISystem
    {
        private readonly Context _context;

        public void Execute()
        {
            var gameStart = _context.GetComponent<GameStart>();
            if (gameStart == null)
                return;

            var turnInfo = _context.TurnInfo();

            _context.Add(new PrintLine("Hooray! The game has started!", OutputStream.GameLog));
            _context.Add(new PrintFormattedLine($"|player:{turnInfo.CurTurnPlayerId}| begins the game", OutputStream.GameLog));

            _context.Remove(gameStart);
        }

        public GameStartSystem(Context context) =>
            _context = context;
    }
}
