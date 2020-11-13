using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Components.SystemRequest.PlayerInput.Property;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MonopolyPreUnity.Systems.HSInput.Behaviors
{
    class HSChoosePropertyActionBehavior : IHSStateBehavior
    {
        private readonly Context _context;

        public void Run(HSInputState state)
        {
            var player = _context.GetPlayer(state.PlayerId.Value);
            var propId = _context.GetComponent<HSPropertyChoice>().PropId.Value;
            var commandChoice = _context.GetComponent<HSCommandChoice>();

            // if command isn't chosen
            if (commandChoice == null)
            {
                if (!_context.ContainsComponent<HSCommandChoiceRequest>())
                {
                    var avilableCommands = _context.GetAvailablePropertyActions(player, propId);
                    avilableCommands.Add(MonopolyCommand.CancelAction);

                    _context.Add(new PrintCommands(avilableCommands));
                    _context.Add(new HSCommandChoiceRequest(avilableCommands, player.Id));
                }
                return;
            }

            switch (commandChoice.Command)
            {
                case MonopolyCommand.MortgageProperty:
                    _context.Mortgage(player, propId);
                    break;
                case MonopolyCommand.UnmortgageProperty:
                    _context.UnMortgage(player, propId);
                    break;
                case MonopolyCommand.BuyHouse:
                    _context.BuyHouse(player, propId);
                    break;
                case MonopolyCommand.SellHouse:
                    _context.SellHouse(player, propId);
                    break;

                case MonopolyCommand.CancelAction:
                    state.CurState = HSState.PropManageChooseProperty;
                    _context.Remove<HSPropertyChoice>();
                    break;
            }
            _context.Remove<HSCommandChoice>();
        }

        public HSChoosePropertyActionBehavior(Context context) =>
            _context = context;
    }
}
