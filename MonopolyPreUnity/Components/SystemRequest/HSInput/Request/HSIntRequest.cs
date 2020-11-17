using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.HSInput.Request
{
    class HSIntRequest : IHSRequest
    {
        public int PlayerId { get; set; }
        public int? LowerBound { get; set; }
        public int? UpperBound { get; set; }

        public HSIntRequest(int playerId, int? lowerBound, int? upperBound)
        {
            PlayerId = playerId;
            LowerBound = lowerBound;
            UpperBound = upperBound;
        }
    }

}
