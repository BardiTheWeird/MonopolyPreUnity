using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Components.SystemState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class PlayerBankruptSystem : ISystem
    {
        #region dependencies
        private readonly Context _context;
        #endregion

        public void Execute()
        {
            foreach (var bankrupt in _context.GetComponents<PlayerBankrupt>())
            {
                var player = _context.GetPlayer(bankrupt.PlayerId);
                _context.Add(new PrintLine($"{player.DisplayName} was removed from the game", OutputStream.GameLog));
                RemovePlayerFromGame(player);
            }
            _context.Remove<PlayerBankrupt>();
        }

        void RemovePlayerFromGame(Player player)
        {
            var turnInfo = _context.TurnInfo();
            int curTurnOrderPosition = turnInfo.TurnOrder.FindIndex(x => x == player.Id);
            turnInfo.TurnOrder.RemoveAt(curTurnOrderPosition);

            if (curTurnOrderPosition < turnInfo.CurTurnPlayer || curTurnOrderPosition == turnInfo.CurTurnPlayer)
            {
                turnInfo.CurTurnPlayer--;
                if (turnInfo.CurTurnPlayer < 0)
                    turnInfo.CurTurnPlayer = turnInfo.TurnOrder.Count - 1;
            }

            if (turnInfo.PlayersLeft <= 1)
                _context.Add(new GameOver());
        }

        #region ctor
        public PlayerBankruptSystem(Context context)
        {
            _context = context;
        }
        #endregion
    }
}
