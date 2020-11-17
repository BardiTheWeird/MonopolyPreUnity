using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Request;
using MonopolyPreUnity.Components.SystemRequest.Output;
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

                    _context.Add(new PrintLine("Choose a player to trade with:", OutputStream.HSInputLog));
                    _context.Add(new PrintPlayers(availablePlayers, OutputStream.HSInputLog));
                    _context.Add(new PrintLine("Write -1 to cancel", OutputStream.HSInputLog));
                    _context.Add(new HSPlayerChoiceRequest(state.PlayerId.Value, availablePlayers));
                }
                return;
            }

            // if player canceled his choice
            if (!choice.PlayerChoiceId.HasValue)
            {
                _context.Remove<TradeOffer>();
                state.Nullify();
            }
            else
            {
                _context.TradeOffer().ReceiverAssets.PlayerId = choice.PlayerChoiceId.Value;
                state.CurState = HSState.TradeChooseAction;
            }
            _context.Remove(choice);
        }

        public HSTradeChoosePlayerBehavior(Context context)
        {
            _context = context;
        }
    }
}
