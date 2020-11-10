using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Move
{
    class MoveDice : IMoveRequest
    {
        public int PlayerId { get; set; }
        public bool CountGoPassed { get; set; }

        public MoveDice(int playerId, bool countGoPassed = true)
        {
            PlayerId = playerId;
            CountGoPassed = countGoPassed;
        }
    }
}
