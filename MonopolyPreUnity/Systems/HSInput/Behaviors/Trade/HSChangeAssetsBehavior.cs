using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
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
            var assets = (PlayerAssets)state.MiscInfo;

            var choice = _context.GetComponent<HSCommandChoice>();
            if (choice == null)
            {
                if (!_context.ContainsComponent<HSCommandChoiceRequest>())
                {
                    var availableCommands = new List<MonopolyCommand>
                    {
                        MonopolyCommand.ChooseCashAmount,
                        MonopolyCommand.ChooseJailCardsAmount,
                        MonopolyCommand.CancelAction,
                    };

                    if (_context.TradableProperties(assets.PlayerId).Count > 0)
                        availableCommands.Add(MonopolyCommand.ChangeProperties);

                    _context.Add(new PrintCommands(availableCommands));
                    _context.Add(new HSCommandChoiceRequest(availableCommands, state.PlayerId.Value));
                }
                return;
            }

            switch (choice.Command)
            {
                case MonopolyCommand.ChooseCashAmount:
                    state.CurState = HSState.TradeChooseCash;
                    break;
                case MonopolyCommand.ChooseJailCardsAmount:
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
