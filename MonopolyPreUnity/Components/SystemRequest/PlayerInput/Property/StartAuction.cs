using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.PlayerInput.Property
{
    class StartAuction : IEntityComponent
    {
        public int PropertyId { get; set; }

        public StartAuction(int propertyId)
        {
            PropertyId = propertyId;
        }
    }
}
