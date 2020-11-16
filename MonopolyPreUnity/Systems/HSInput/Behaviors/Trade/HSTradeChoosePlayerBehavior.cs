using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Request;
using MonopolyPreUnity.Components.Trade;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Systems.HSInput.Behaviors
{
    class HSTradeChoosePlayerBehavior : IHSStateBehavior
    {
        private readonly Context _context;

        public void Run(HSInputState state)
        {
            var choice = _context.GetComponent<HSPlayerChoice>();
            if (choice == null)
            {
                if (!_context.ContainsComponent<HSPlayerChoiceRequest>()) 
                {
                    var availablePlayers = _context.GetAllPlayers()
                        .Select(p => p.Id)
                        .Where(id => id != state.PlayerId.Value)
                        .ToList();

                    _context.Add(new HSPlayerChoiceRequest(state.PlayerId.Value, availablePlayers));
                }
                return;
            }

            _context.Remove(choice);
            
            // if player canceled his choice
            if (!choice.PlayerChoiceId.HasValue)
            {
                _context.Remove<TradeOffer>();
                state.Nullify();
                return;
            }

            _context.TradeOffer().ReceiverAssets.PlayerId = choice.PlayerChoiceId.Value;
            state.CurState = HSState.TradeChooseAction;
        }

        public HSTradeChoosePlayerBehavior(Context context)
        {
            _context = context;
        }
    }
}
