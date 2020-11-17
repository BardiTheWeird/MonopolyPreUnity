using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Move
{
    class MoveDice : IMoveRequest
    {
        public int PlayerId { get; set; }
        public bool CountGoPassed { get; set; }
        public bool CountPlayerLanded { get; set; }

        public bool MoveAgainAllowed { get; set; }

        public MoveDice(int playerId, bool countGoPassed = true, bool moveAgainAllowed = true, bool countPlayerLanded = true)
        {
            PlayerId = playerId;
            CountGoPassed = countGoPassed;
            MoveAgainAllowed = moveAgainAllowed;
            CountPlayerLanded = countPlayerLanded;
    }
    }
}
