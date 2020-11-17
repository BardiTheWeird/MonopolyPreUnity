using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Request;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Systems.HSInput.Behaviors.Debt
{
    class HSDebtChoosePropertyBehavior : IHSStateBehavior
    {
        private readonly Context _context;

        public void Run(HSInputState state)
        {
            var player = _context.GetPlayer(state.PlayerId.Value);

            var choice = _context.GetComponent<HSPropertyChoice>();
            if (choice == null)
            {
                if (!_context.ContainsComponent<HSPropertyChoiceRequest>())
                {
                    var availableProperties = player.Properties
                        .Where(propId => !_context.GetTileComponent<Property>(propId).IsMortgaged)
                        .ToList();

                    _context.Add(new HSPropertyChoiceRequest(player.Id, availableProperties));

                    _context.Add(new PrintLine("Choose property to manage:", OutputStream.HSInputLog));
                    _context.Add(new PrintProperties(availableProperties, OutputStream.HSInputLog));
                    _context.Add(new PrintLine("Write -1 to go back", OutputStream.HSInputLog));
                }
                return;
            }

            if (!choice.PropId.HasValue)
                state.CurState = HSState.Debt;
            else
            {
                state.CurState = HSState.DebtChoosePropertyAction;
                state.MiscInfo = choice.PropId.Value;
            }

            _context.Remove(choice);
            _context.Add(new ClearOutput());
        }

        public HSDebtChoosePropertyBehavior(Context context) =>
            _context = context;
    }
}
