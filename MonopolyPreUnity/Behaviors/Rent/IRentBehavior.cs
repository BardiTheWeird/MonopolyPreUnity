using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Rent
{
    interface IRentBehavior
    {
        public int GetRent(int renteeId, int ownerId, IEntityComponent component, int tileId);
    }
}
