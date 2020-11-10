using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Move
{
    class MoveSteps : IMoveRequest
    {
        public int PlayerId { get; set; }
        public bool CountGoPassed { get; set; }
        public int Steps { get; set; }
    }
}
