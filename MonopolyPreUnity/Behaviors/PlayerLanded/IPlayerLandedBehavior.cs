using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors
{
    interface IPlayerLandedBehavior
    {
        public void PlayerLanded(int playerId, ITileComponent tileComponent, int tileId);
    }
}
