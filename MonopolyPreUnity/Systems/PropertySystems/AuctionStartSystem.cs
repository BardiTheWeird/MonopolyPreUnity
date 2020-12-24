using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Components.SystemRequest.PlayerInput.Property;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var propId = startAuction.PropertyId;
            // clones a turn order
            var bidOrder = _context.TurnInfo().TurnOrder.Select(x => x).ToList();
            var curBidder = _context.TurnInfo().CurTurnPlayer;

            _context.Add(new AuctionInfo(propId, 0, bidOrder, curBidder));

            _context.Add(new PrintFormattedLine($"The auction for |tile:{startAuction.PropertyId}| has started!",
                OutputStream.GameLog));

            _context.RenderCommunications.AuctionInfoChanged = !_context.RenderCommunications.AuctionInfoChanged;

            _context.Remove<StartAuction>();
        }

        #region ctor
        public AuctionStartSystem(Context context) =>
            _context = context;
        #endregion
    }
}
