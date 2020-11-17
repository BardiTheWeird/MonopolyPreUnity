using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Components.Trade;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Requests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class TradeValidationSystem : ISystem
    {
        private readonly Context _context;

        public void Execute()
        {
            // if trade is going on
            var offer = _context.GetComponent<TradeOffer>();
            if (offer == null)
                return;

            // if receiving player responded
            var response = _context.GetComponentInterface<ITradeResponse>();
            if (response == null)
            {
                // if hotseat player is busy answering or creating a trade
                if (!_context.HSInputState().IsNull)
                    return;

                // send validation request
                _context.Add(new PlayerInputRequest(offer.ReceiverAssets.PlayerId,
                    new TradeValidationRequest(offer)));

                return;
            }

            var initiatorId = offer.InitiatorAssets.PlayerId;
            var receiverId = offer.ReceiverAssets.PlayerId;
            if (response is TradeAccept)
            {

                _context.Add(new PrintFormattedLine($"|player:{receiverId}| accepted |player:{initiatorId}|'s trade offer",
                    OutputStream.GameLog));
                _context.Add(new AssetTransferRequest(receiverId, offer.InitiatorAssets));
                _context.Add(new AssetTransferRequest(initiatorId, offer.ReceiverAssets));
            }
            else if (response is TradeRefuse)
            {
                _context.Add(new PrintFormattedLine($"|player:{receiverId}| refused |player:{initiatorId}|'s trade offer",
                    OutputStream.GameLog));
            }

            // consume offer and response
            _context.Remove(offer);
            _context.RemoveInterface<ITradeResponse>();
        }

        #region ctor
        public TradeValidationSystem(Context context) =>
            _context = context;
        #endregion
    }
}
