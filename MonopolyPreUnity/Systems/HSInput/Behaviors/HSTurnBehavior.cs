using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.Move;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Components.SystemRequest.PlayerInput;
using MonopolyPreUnity.Components.SystemRequest.PlayerInput.InJail;
using MonopolyPreUnity.Components.Trade;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Systems;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Systems.HSInput
{
    class HSTurnBehavior : IHSStateBehavior
    {
        private readonly Context _context;

        public void Run(HSInputState state)
        {
            var player = _context.GetPlayer(state.PlayerId.Value);

            var commandChoice = _context.GetComponent<HSCommandChoice>();
            if (commandChoice == null)
            {
                if (!_context.ContainsComponent<HSCommandChoiceRequest>())
                {
                    var commands = GetAvailableCommands(player);
                    _context.Add(new PrintCommands(commands));
                    _context.Add(new HSCommandChoiceRequest(commands, player.Id));
                }
                return;
            }

            _context.Add(new ClearOutput());
            switch (commandChoice.Command)
            {
                case MonopolyCommand.ManageProperty:
                    state.Set(HSState.PropManageChooseProperty, player.Id);
                    break;

                case MonopolyCommand.CreateTradeOffer:
                    var offer = new TradeOffer();
                    offer.InitiatorAssets.PlayerId = player.Id;
                    _context.Add(offer);
                    state.Set(HSState.TradeChoosePlayer, player.Id);
                    break;

                case MonopolyCommand.MakeMove:
                    _context.Add(new ThrowDice());
                    _context.Add(new MoveDice(player.Id));
                    state.Nullify();
                    break;
                case MonopolyCommand.EndTurn:
                    _context.Add(new EndTurn());
                    state.Nullify();
                    break;

                case MonopolyCommand.PrintPlayerStatus:
                    _context.Add(new PrintPlayerStatus(commandChoice.PlayerId));
                    break;
                case MonopolyCommand.PrintGameStatus:
                    _context.Add(new PrintGameStatus());
                    break;
                case MonopolyCommand.PrintMap:
                    _context.Add(new PrintMap());
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

        public List<MonopolyCommand> GetAvailableCommands(Player player)
        {
            var commandList = new List<MonopolyCommand>
            {
                MonopolyCommand.ManageProperty,
                MonopolyCommand.CreateTradeOffer
            };
            var outputRequestCommands = new List<MonopolyCommand>
            {
                MonopolyCommand.PrintPlayerStatus,
                MonopolyCommand.PrintGameStatus,
                MonopolyCommand.PrintMap,
            };

            commandList.AddRange(_context.GetAvailableTurnCommands(player));
            commandList.AddRange(outputRequestCommands);

            return commandList;
        }

        #region ctor
        public HSTurnBehavior(Context context)
        {
            _context = context;
        }
        #endregion
    }
}
