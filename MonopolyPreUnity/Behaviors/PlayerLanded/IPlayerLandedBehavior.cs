using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors
{
    interface IPlayerLandedBehavior
    {
        public void PlayerLanded(Player player, IEntityComponent component);
    }
}