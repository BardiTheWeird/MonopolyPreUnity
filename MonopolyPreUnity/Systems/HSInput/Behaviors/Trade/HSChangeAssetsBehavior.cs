using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems.HSInput.Behaviors
{
    class HSChangeAssetsBehavior : IHSStateBehavior
    {
        private readonly Context _context;

        public void Run(HSInputState state)
        {
            var choice = _context.GetComponent<HSCommandChoice>();
            if (choice == null)
            {
                if (!_context.ContainsComponent<HSCommandChoiceRequest>())
                {
                    var availableCommands = new List<MonopolyCommand>
                    {
                        MonopolyCommand.ChangeCashAmount,
                        MonopolyCommand.ChangeJailCardsAmount,
                        MonopolyCommand.ChangeProperties,
                        MonopolyCommand.CancelAction,
                    };

                    _context.Add(new HSCommandChoiceRequest(availableCommands, state.PlayerId.Value));
                }
                return;
            }

            switch (choice.Command)
            {
                case MonopolyCommand.ChangeCashAmount:
                    state.CurState = HSState.TradeChooseCash;
                    break;
                case MonopolyCommand.ChangeJailCardsAmount:
                    state.CurState = HSState.TradeChooseJailCards;
                    break;
                case MonopolyCommand.ChangeProperties:
                    state.CurState = HSState.TradeChooseProperties;
                    break;
                case MonopolyCommand.CancelAction:
                    state.CurState = HSState.TradeChooseAction;
                    break;
            }

            _context.Remove(choice);
        }

        public HSChangeAssetsBehavior(Context context)
        {
            _context = context;
        }
    }
}
