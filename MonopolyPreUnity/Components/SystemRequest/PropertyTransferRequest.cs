using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest
{
    class PropertyTransferRequest : IEntityComponent
    {
        public int PropertyId { get; set; }
        public int NewOwnerId { get; set; }

        public PropertyTransferRequest(int propertyId, int newOwnerId)
        {
            PropertyId = propertyId;
            NewOwnerId = newOwnerId;
        }
    }
}
