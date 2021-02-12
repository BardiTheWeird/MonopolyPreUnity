using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;

using MonopolyPreUnity.Requests;
using MonopolyPreUnity.UI;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors
{
    class PropertyLandedBehavior : IPlayerLandedBehavior
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        #region Behavior
        public void PlayerLanded(Player player, IEntityComponent component)
        {
            var property = (Property)component;

            if (property.OwnerId == null)
            {
                _context.Add(new PlayerInputRequest(player.Id, new BuyAuctionRequest(player.CurTileId)));
            }
            else if (property.OwnerId != player.Id && property.IsMortgaged == false)
            {
                _context.Add(new CollectRent(player.Id));
            }
            else
                _context.Add(new PrintLine("It's their own property", OutputStream.GameLog));
        }

        #endregion

        #region Constructor
        public PropertyLandedBehavior(Context context) =>
            _context = context;
        #endregion
    }
}
