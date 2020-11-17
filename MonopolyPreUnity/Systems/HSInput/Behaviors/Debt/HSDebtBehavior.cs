using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems.HSInput.Behaviors.Debt
{
    class HSDebtBehavior : IHSStateBehavior
    {
        private readonly Context _context;

        public void Run(HSInputState state)
        {
            var debt = _context.GetComponent<PlayerDebt>();
            var player = _context.GetPlayer(state.PlayerId.Value);

            var choice = _context.GetComponent<HSCommandChoice>();
            if (choice == null)
            {
                if (!_context.ContainsComponent<HSCommandChoiceRequest>())
                {
                    _context.Add(new PrintLine($"You're in debt. Debt amount: {debt.DebtAmount}; current Cash: {player.Cash}",
                        OutputStream.HSInputLog));

                    var availableCommands = new List<MonopolyCommand> { MonopolyCommand.ChooseProperty };
                    if (player.Cash >= debt.DebtAmount)
                        availableCommands.Add(MonopolyCommand.PayDebt);
                    else
                        _context.Add(new PrintLine("You can sell houses and mortgage property to stay in the game",
                            OutputStream.HSInputLog));

                    _context.Add(new PrintCommands(availableCommands));
                    _context.Add(new HSCommandChoiceRequest(availableCommands, player.Id));
                }
                return;
            }

            switch (choice.Command)
            {
                case MonopolyCommand.ChooseProperty:
                    state.CurState = HSState.DebtChooseProperty;
                    break;

                case MonopolyCommand.PayDebt:
                    state.Nullify();
                    _context.Add(new PaidOffDebt(player.Id));
                    break;
            }

            _context.Add(new ClearOutput());
            _context.Remove(choice);
        }

        public HSDebtBehavior(Context context) =>
            _context = context;
    }
}
