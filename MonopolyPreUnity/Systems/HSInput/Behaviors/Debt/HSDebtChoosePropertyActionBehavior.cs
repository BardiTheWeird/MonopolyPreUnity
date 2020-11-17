using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Systems.HSInput.Behaviors.Debt
{
    class HSDebtChoosePropertyActionBehavior : IHSStateBehavior
    {
        private readonly Context _context;

        public void Run(HSInputState state)
        {
            var debt = _context.GetComponent<PlayerDebt>();
            var player = _context.GetPlayer(state.PlayerId.Value);
            var propId = (int)state.MiscInfo;

            var choice = _context.GetComponent<HSCommandChoice>();
            if (choice == null)
            {
                if (!_context.ContainsComponent<HSCommandChoiceRequest>())
                {
                    _context.Add(new PrintLine($"You're in debt. Debt amount: {debt.DebtAmount}; current Cash: {player.Cash}",
                        OutputStream.HSInputLog));

                    var availableCommands = _context.GetAvailablePropertyActions(player, propId)
                        .Where(command => IsSuitablePropertyAction(command))
                        .ToList();

                    availableCommands.Add(MonopolyCommand.CancelAction);
                    if (player.Cash >= debt.DebtAmount)
                        availableCommands.Add(MonopolyCommand.PayDebt);

                    _context.Add(new PrintLine("\nProperty to manage:", OutputStream.HSInputLog));
                    _context.Add(new PrintProperties(propId, OutputStream.HSInputLog));
                    _context.Add(new PrintLine("", OutputStream.HSInputLog));
                    _context.Add(new PrintCommands(availableCommands));

                    _context.Add(new HSCommandChoiceRequest(availableCommands, player.Id));
                }

                return;
            }

            switch (choice.Command)
            {
                case MonopolyCommand.MortgageProperty:
                    _context.Mortgage(player, propId);
                    break;
                case MonopolyCommand.SellHouse:
                    _context.SellHouse(player, propId);
                    break;
                case MonopolyCommand.CancelAction:
                    state.Set(HSState.DebtChooseProperty, player.Id);
                    break;
                case MonopolyCommand.PayDebt:
                    _context.Add(new PaidOffDebt(player.Id));
                    state.Nullify();
                    break;
            }

            _context.Remove(choice);
            _context.Add(new ClearOutput());
        }

        bool IsSuitablePropertyAction(MonopolyCommand command)
        {
            if (command == MonopolyCommand.UnmortgageProperty)
                return false;
            if (command == MonopolyCommand.BuyHouse)
                return false;

            return true;
        }

        #region ctor
        public HSDebtChoosePropertyActionBehavior(Context context) =>
            _context = context;
        #endregion
    }
}
