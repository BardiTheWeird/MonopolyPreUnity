using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Request;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Systems.HSInput
{
    class HSChoosePropertyToManageBehavior : IHSStateBehavior
    {
        private readonly Context _context;

        public void Run(HSInputState state)
        {
            var player = _context.GetPlayer(state.PlayerId.Value);

            // if property isn't yet chosen
            var propertyChoice = _context.GetComponent<HSPropertyChoice>();
            if (propertyChoice == null)
            {
                if (!_context.ContainsComponent<HSPropertyChoiceRequest>())
                {
                    _context.Add(new PrintLine("Choose property to manage", OutputStream.HSInputLog));
                    _context.Add(new PrintProperties(player.Properties.ToList(), OutputStream.HSInputLog));
                    _context.Add(new PrintLine("Print -1 to cancel", OutputStream.HSInputLog));

                    _context.Add(new HSPropertyChoiceRequest(player.Id, player.Properties.ToList()));
                }
                return;
            }

            // if player canceled
            if (!propertyChoice.PropId.HasValue)
            {
                _context.Remove<HSPropertyChoice>();
                state.CurState = HSState.TurnChoice;
                return;
            }

            // if player is willing to force himself to manage his dreaded property
            state.CurState = HSState.PropManageChooseAction;
        }

        public HSChoosePropertyToManageBehavior(Context context) =>
            _context = context;
    }
}
