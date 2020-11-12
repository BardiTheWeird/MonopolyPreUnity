using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Components.SystemRequest.PlayerInput.Property;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems.PropertySystems
{
    class AuctionStartSystem : ISystem
    {
        private readonly Context _context;

        public void Execute()
        {
            var startAuction = _context.GetComponent<StartAuction>();
            if (startAuction == null)
                return;

            _context.Add(new PrintLine("Auction isn't yet implemented, so you get a property for free"));

            var player = _context.GetPlayer(_context.TurnInfo().CurTurnPlayerId);

            var prop = _context.GetTileComponent<Property>(startAuction.PropertyId);
            _context.Add(new PropertyTransferRequest(startAuction.PropertyId, player.Id));

            _context.Remove<StartAuction>();
        }

        public AuctionStartSystem(Context context)
        {
            _context = context;
        }
    }
}
