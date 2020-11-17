using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Move
{
    class MoveSteps : IMoveRequest
    {
        public int PlayerId { get; set; }
        public bool CountGoPassed { get; set; }
        public bool CountPlayerLanded { get; set; }
        public int Steps { get; set; }

        public MoveSteps(int playerId, bool countGoPassed, int steps, bool countPlayerLanded = true)
        {
            PlayerId = playerId;
            CountGoPassed = countGoPassed;
            Steps = steps;
            CountPlayerLanded = countPlayerLanded;
        }
    }
}
