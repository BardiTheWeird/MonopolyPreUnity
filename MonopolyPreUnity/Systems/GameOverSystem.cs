using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Components.SystemState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class GameOverSystem : ISystem
    {
        private readonly Context _context;

        public void Execute()
        {
            var gameOver = _context.GetComponent<GameOver>();
            if (gameOver == null)
                return;

            var turnInfo = _context.TurnInfo();

            string winnerLine;
            try
            {
                var winner = _context.GetPlayer(turnInfo.CurTurnPlayerId);
                winnerLine = $"{winner.DisplayName} is the winner!";
            }
            catch 
            {
                winnerLine = "No winner is found though. Weird";
            }

            _context.Add(new PrintLine("The game is over. " + winnerLine, OutputStream.GameLog));
            _context.Remove(gameOver);
        }

        public GameOverSystem(Context context) =>
            _context = context;
    }
}
