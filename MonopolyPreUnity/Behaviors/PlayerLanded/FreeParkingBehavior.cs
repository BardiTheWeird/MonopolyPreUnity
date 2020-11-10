using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.PlayerLanded
{
    class FreeParkingBehavior : IPlayerLandedBehavior
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        public void PlayerLanded(Player player, IEntityComponent component) =>
            _context.Add(new PrintLine("It's free parking, so you don't get to do nothing"));

        public FreeParkingBehavior(Context context) =>
            _context = context;
    }
}
