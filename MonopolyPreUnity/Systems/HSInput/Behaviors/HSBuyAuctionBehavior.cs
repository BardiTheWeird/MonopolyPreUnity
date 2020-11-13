using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.PlayerInput.Property;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Systems;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Systems.HSInput
{
    class HSBuyAuctionBehavior : IHSStateBehavior
    {
        private readonly Context _context;

        public void Run(HSInputState state)
        {
            var commandChoice = _context.GetComponent<HSCommandChoice>();
            if (commandChoice == null)
                return;

            var player = _context.GetPlayer(state.PlayerId.Value);
            switch (commandChoice.Command)
            {
                case MonopolyCommand.BuyProperty:
                    _context.Add(new BuyProperty(player.Id, player.CurTileId));
                    break;
                case MonopolyCommand.AuctionProperty:
                    _context.Add(new StartAuction(player.CurTileId));
                    break;
            }
            _context.Remove<HSCommandChoice>(c => c.Command == commandChoice.Command);

            state.Nullify();
        }

        #region ctor
        public HSBuyAuctionBehavior(Context context)
        {
            _context = context;
        }
        #endregion
    }
}
