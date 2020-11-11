using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.Move;
using MonopolyPreUnity.Components.SystemRequest.PlayerInput;
using MonopolyPreUnity.Components.SystemRequest.PlayerInput.InJail;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Systems;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.HSScenario.RequestScenarios.TurnScenario
{
    class HSTurnSystem : ISystem
    {
        private readonly Context _context;
        private readonly MonopolyCommand[] _appropriateCommands =
        {
            MonopolyCommand.MakeMove,
            MonopolyCommand.EndTurn,

            MonopolyCommand.PayJailFine,
            MonopolyCommand.JailRollDice,
            MonopolyCommand.UseJailCard,

            //MonopolyCommand.EndGame,
            //MonopolyCommand.FileBankruptcy
        };

        public void Execute()
        {
            var commandChoice = _context.GetComponent<HSCommandChoice>();
            if (commandChoice != null && _appropriateCommands.Contains(commandChoice.Command))
            {
                var player = _context.GetPlayer(commandChoice.PlayerId);
                switch (commandChoice.Command)
                {
                    case MonopolyCommand.MakeMove:
                        _context.Add(new ThrowDice());
                        _context.Add(new MoveDice(player.Id));
                        break;
                    case MonopolyCommand.EndTurn:
                        _context.Add(new EndTurn());
                        break;

                    case MonopolyCommand.PayJailFine:
                        _context.Add(new JailPayFine(player.Id));
                        break;
                    case MonopolyCommand.JailRollDice:
                        _context.Add(new ThrowDice());
                        _context.Add(new JailDiceRoll(player.Id));
                        break;
                    case MonopolyCommand.UseJailCard:
                        _context.Add(new JailUseCard(player.Id));
                        break;
                }

                _context.Remove<HSCommandChoice>(c => c.Command == commandChoice.Command);
            }
        }

        #region ctor
        public HSTurnSystem(Context context)
        {
            _context = context;
        }
        #endregion
    }
}
