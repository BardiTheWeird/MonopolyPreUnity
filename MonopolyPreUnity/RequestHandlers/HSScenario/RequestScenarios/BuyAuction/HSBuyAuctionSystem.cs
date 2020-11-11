using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.PlayerInput.Property;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Systems;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.HSScenario.RequestScenarios.BuyAuction
{
    class HSBuyAuctionSystem : ISystem
    {
        #region fields
        private readonly Context _context;
        private readonly MonopolyCommand[] _appropiateCommands =
        {
            MonopolyCommand.BuyProperty,
            MonopolyCommand.AuctionProperty
        };
        #endregion

        public void Execute()
        {
            var commandChoice = _context.GetComponent<HSCommandChoice>();
            if (commandChoice == null || !_appropiateCommands.Contains(commandChoice.Command))
                return;

            var player = _context.GetPlayer(commandChoice.PlayerId);
            switch (commandChoice.Command)
            {
                case MonopolyCommand.BuyProperty:
                    _context.Add(new BuyProperty(player.Id, player.CurTileId));
                    break;
                case MonopolyCommand.AuctionProperty:
                    _context.Add(new StartAuction(player.CurTileId));
                    break;
            }

            _context.Remove<HSCommandChoice>(c => c.Command == commandChoice.Command);
            _context.Remove<PlayerBusy>();
        }

        #region ctor
        public HSBuyAuctionSystem(Context context)
        {
            _context = context;
        }
        #endregion
    }
}
