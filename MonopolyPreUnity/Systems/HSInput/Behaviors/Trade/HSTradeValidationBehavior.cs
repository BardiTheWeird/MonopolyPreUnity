using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Components.Trade;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems.HSInput.Behaviors.Trade
{
    class HSTradeValidationBehavior : IHSStateBehavior
    {
        private readonly Context _context;

        public void Run(HSInputState state)
        {
            var choice = _context.GetComponent<HSCommandChoice>();
            if (choice == null)
            {
                if (!_context.ContainsComponent<HSCommandChoiceRequest>())
                {
                    var commands = new List<MonopolyCommand> 
                    { 
                        MonopolyCommand.AcceptOffer, 
                        MonopolyCommand.DeclineOffer,
                    };

                    _context.Add(new PrintCommands(commands));
                    _context.Add(new HSCommandChoiceRequest(commands, state.PlayerId.Value));
                }
                return;
            }

            switch (choice.Command)
            {
                case MonopolyCommand.AcceptOffer:
                    _context.Add(new TradeAccept());
                    break;
                case MonopolyCommand.DeclineOffer:
                    _context.Add(new TradeRefuse());
                    break;
            }

            _context.Remove(choice);
            state.Nullify();
        }

        public HSTradeValidationBehavior(Context context)
        {
            _context = context;
        }
    }
}
