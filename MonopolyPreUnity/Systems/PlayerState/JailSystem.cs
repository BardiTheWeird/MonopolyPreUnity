using MonopolyPreUnity.Components.SystemRequest.Cash;
using MonopolyPreUnity.Components.SystemRequest.Jail;
using MonopolyPreUnity.Components.SystemRequest.Move;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems.PlayerState
{
    class JailSystem : ISystem
    {
        private readonly Context _context;

        public void Execute()
        {
            foreach (var request in _context.GetComponentsInterface<IJailRequest>())
            {
                var player = _context.GetPlayer(request.PlayerId);
                switch(request)
                {
                    case GiveJailCard giveJailCard:
                        player.JailCards++;
                        _context.Add(new PrintFormattedLine($"|player:{player.Id}| received a jail card!\n" +
                            $"  Jail cards: {player.JailCards}", OutputStream.GameLog));
                        break;

                    case JailDiceRoll diceRoll:
                        if (_context.Dice().IsPair)
                        {
                            _context.Add(new PrintFormattedLine($"It's doubles! |player:{player.Id}| gets to get out of prison for free!",
                                OutputStream.GameLog));
                            player.TurnsInJail = null;
                            _context.Add(new MoveDice(player.Id, true, false));
                        }
                        else
                        {
                            player.RolledJailDiceThisTurn = true;
                            player.TurnsInJail++;
                            _context.Add(new PrintLine($"It's not doubles. Current Turns in Jail: {player.TurnsInJail}",
                                OutputStream.GameLog));
                        }
                        player.CanMove = false;
                        break;

                    case JailPayFine payFine:
                        player.Cash -= _context.GameConfig().JailFine;
                        player.TurnsInJail = null;

                        _context.Add(new ChargeCash(_context.GameConfig().JailFine, player.Id, message: "to get out of jail"));
                        break;

                    case JailUseCard useCard:
                        player.JailCards--;
                        player.TurnsInJail = null;

                        _context.Add(new PrintFormattedLine($"|player:{player.Id}| used a jail card " +
                            $"to get out of jail. {player.JailCards} cards left", OutputStream.GameLog));
                        break;

                    case SendToJail sendToJail:
                        player.TurnsInJail = 0;
                        player.CanMove = false;

                        _context.Add(new PrintFormattedLine($"|player:{player.Id}| was sent to jail", OutputStream.GameLog));
                        _context.Add(new MoveTileId(player.Id, _context.MapInfo().JailId.Value, false, false));
                        break;
                }
            }
            _context.RemoveInterface<IJailRequest>();
        }

        #region ctor
        public JailSystem(Context context) =>
            _context = context;
        #endregion
    }
}
