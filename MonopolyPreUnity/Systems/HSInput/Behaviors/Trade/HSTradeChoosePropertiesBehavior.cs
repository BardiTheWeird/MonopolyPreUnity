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
    class HSTradeChoosePropertiesBehavior : IHSStateBehavior
    {
        private readonly Context _context;

        public void Run(HSInputState state)
        {
            var assets = (PlayerAssets)state.MiscInfo;

            var choice = _context.GetComponent<HSPropertyChoiceMultiple>();
            if (choice == null)
            {
                if (!_context.ContainsComponent<HSPropertyChoiceMultipleRequest>())
                {
                    var tradableProperties = _context.TradableProperties(assets.PlayerId);

                    _context.Add(new PrintLine("Choose properties from the list:", OutputStream.HSInputLog));
                    _context.Add(new PrintProperties(tradableProperties, OutputStream.HSInputLog, indexate: true));
                    _context.Add(new HSPropertyChoiceMultipleRequest(assets.PlayerId, tradableProperties));
                }
                return;
            }

            assets.Properties = choice.PropertiesChosen;

            state.CurState = HSState.TradeChangeAssets;
            _context.Add(new ClearOutput());
            _context.Remove(choice);
        }

        public HSTradeChoosePropertiesBehavior(Context context)
        {
            _context = context;
        }
    }
}
