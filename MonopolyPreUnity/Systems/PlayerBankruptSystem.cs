using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
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
                _context.AddEntity(new TransferAssets(bankrupt.CreditorId, player));
                _context.AddEntity(new RemovePlayerFromGame(player.Id));
            }
            _context.RemoveEntities<PlayerBankrupt>();
        }

        #region ctor
        public PlayerBankruptSystem(Context context)
        {
            _context = context;
        }
        #endregion
    }
}
