using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Request;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems.HSInput.Behaviors.Trade
{
    class HSTradeChooseCashBehavior : IHSStateBehavior
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
                    var playerCash = _context.GetPlayer(assets.PlayerId).Cash;

                    _context.Add(new PrintLine($"Choose an amount of cash (up to {playerCash}):", OutputStream.HSInputLog));
                    _context.Add(new HSIntRequest(state.PlayerId.Value, 0, playerCash));
                }
                return;
            }

            assets.Cash = choice.Choice;

            state.CurState = HSState.TradeChangeAssets;

            _context.Add(new ClearOutput());
            _context.Remove(choice);
        }

        #region ctor
        public HSTradeChooseCashBehavior(Context context)
        {
            _context = context;
        }
        #endregion
    }
}
