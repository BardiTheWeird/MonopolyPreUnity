using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.HSInput.Request
{
    class HSPropertyChoiceRequest : IHSRequest
    {
        public int PlayerId { get; set; }
        public List<int> Properties { get; set; }

        public HSPropertyChoiceRequest(int playerId, List<int> properties)
        {
            PlayerId = playerId;
            Properties = properties;
        }
    }
}
