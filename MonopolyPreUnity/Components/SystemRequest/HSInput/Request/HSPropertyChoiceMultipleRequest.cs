using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.HSInput.Request
{
    class HSPropertyChoiceMultipleRequest : IHSRequest
    {
        public int PlayerId { get; set; }
        public List<int> AvailableProperties { get; set; }

        public HSPropertyChoiceMultipleRequest(int playerId, List<int> availableProperties)
        {
            PlayerId = playerId;
            AvailableProperties = availableProperties;
        }
    }
}
