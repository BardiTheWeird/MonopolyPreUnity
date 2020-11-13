using MonopolyPreUnity.Components.SystemRequest.Auction;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems.HSInput.Behaviors
{
    class HSAuctionBidBehavior : IHSStateBehavior
    {
        private readonly Context _context;

        public void Run(HSInputState state)
        {
            var intChoice = _context.GetComponent<HSIntChoice>();
            if (intChoice == null)
                return;

            if (intChoice == 0)
                _context.Add(new AuctionWithdraw(state.PlayerId.Value));
            else
                _context.Add(new AuctionBid(state.PlayerId.Value, intChoice));

            _context.Add(new ClearOutput());
            _context.Remove(intChoice);
            state.Nullify();
        }

        #region ctor
        public HSAuctionBidBehavior(Context context) =>
            _context = context;
        #endregion
    }
}
