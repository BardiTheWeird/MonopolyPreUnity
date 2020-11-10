using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.HotSeatInput
{
    interface IHotSeatRequest : IEntityComponent
    {
        public int PlayerId { get; set; }
    }
}
