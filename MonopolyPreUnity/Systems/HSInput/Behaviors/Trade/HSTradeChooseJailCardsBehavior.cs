using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Request;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems.HSInput.Behaviors.Trade
{
    class HSTradeChooseJailCardsBehavior : IHSStateBehavior
    {
        private readonly Context _context;

        public void Run(HSInputState state)
        {
            var assets = (PlayerAssets)state.MiscInfo;

            var choice = _context.GetComponent<HSIntChoice>();
            if (choice == null)
            {
                if (!_context.ContainsComponent<HSIntRequest>())
                {
                    var playerCards = _context.GetPlayer(assets.PlayerId).JailCards;

                    _context.Add(new PrintLine($"Choose an amount of jail cards (up to {playerCards}):", OutputStream.HSInputLog));
                    _context.Add(new HSIntRequest(state.PlayerId.Value, 0, playerCards));
                }
                return;
            }

            assets.JailCards = choice.Choice;

            state.CurState = HSState.TradeChangeAssets;
            _context.Add(new ClearOutput());
            _context.Remove(choice);
        }

        public HSTradeChooseJailCardsBehavior(Context context)
        {
            _context = context;
        }
    }
}
