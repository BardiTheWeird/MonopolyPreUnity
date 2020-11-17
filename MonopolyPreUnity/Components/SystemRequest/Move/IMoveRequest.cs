using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Move
{
    interface IMoveRequest : IEntityComponent
    {
        public int PlayerId { get; set; }
        public bool CountGoPassed { get; set; }
        public bool CountPlayerLanded { get; set; }
    }
}
