using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Move
{
    class MoveType : IMoveRequest
    {
        public int PlayerId { get; set; }
        public Type ComponentType { get; set; }
        public bool CountGoPassed { get; set; }
        public bool CountPlayerLanded { get; set; }

        public MoveType(int playerId, Type componentType, bool countGoPassed, bool countPlayerLanded = true)
        {
            PlayerId = playerId;
            ComponentType = componentType;
            CountGoPassed = countGoPassed;
            CountPlayerLanded = countPlayerLanded;
        }
    }
}
