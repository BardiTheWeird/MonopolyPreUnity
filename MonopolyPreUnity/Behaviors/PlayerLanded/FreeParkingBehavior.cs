using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.PlayerLanded
{
    class FreeParkingBehavior : IPlayerLandedBehavior
    {
        public void PlayerLanded(int playerId, ITileComponent tileComponent, int tileId)
        {
            Logger.Log("You`re on free parking. Try to relax and sleep for a while.(safety is not guaranteed)");
        }
    }
}
