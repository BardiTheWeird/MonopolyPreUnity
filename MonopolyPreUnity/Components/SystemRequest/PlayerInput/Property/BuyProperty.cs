using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.PlayerInput.Property
{
    class BuyProperty : IEntityComponent
    {
        public int BuyerId { get; set; }
        public int PropertyId { get; set; }

        public BuyProperty(int buyerId, int propertyId)
        {
            BuyerId = buyerId;
            PropertyId = propertyId;
        }
    }
}
