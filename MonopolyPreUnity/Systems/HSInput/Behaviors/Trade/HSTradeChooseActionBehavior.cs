using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems.HSInput.Behaviors
{
    class HSTradeChooseActionBehavior : IHSStateBehavior
    {
        private readonly Context _context;

        public void Run(HSInputState state)
        {
            var offer = _context.TradeOffer();

            var choice = _context.GetComponent<HSCommandChoice>();
            if (choice == null)
            {
                if (!_context.ContainsComponent<HSCommandChoiceRequest>()) 
                {
                    var commands = new List<MonopolyCommand>
                    {
                        MonopolyCommand.ConfirmTrade,
                        MonopolyCommand.ChangeInitiatorAssets,
                        MonopolyCommand.ChangeReceiverAssets,
                        MonopolyCommand.CancelAction,
                    };

                    _context.Add(new HSCommandChoiceRequest(commands, state.PlayerId.Value));
                }
                return;
            }

            switch (choice.Command)
            {
                case MonopolyCommand.ConfirmTrade:
                    state.Nullify();
                    break;

                case MonopolyCommand.ChangeReceiverAssets:
                case MonopolyCommand.ChangeInitiatorAssets:
                    state.CurState = HSState.TradeChangeAssets; 
                    if (choice.Command == MonopolyCommand.ChangeInitiatorAssets)
                        state.MiscInfo = offer.InitiatorAssets;
                    else
                        state.MiscInfo =  offer.ReceiverAssets;
                    break;

                case MonopolyCommand.CancelAction:
                    _context.Remove(offer);
                    state.Nullify();
                    break;
            }

            _context.Remove<HSCommandChoice>();
        }

        public HSTradeChooseActionBehavior(Context context)
        {
            _context = context;
        }
    }
}
