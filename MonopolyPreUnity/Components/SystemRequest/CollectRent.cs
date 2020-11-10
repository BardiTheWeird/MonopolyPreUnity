using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest
{
    class CollectRent : IEntityComponent
    {
        public int RenteeId { get; set; }

        public CollectRent(int renteeId)
        {
            RenteeId = renteeId;
        }
    }
}
