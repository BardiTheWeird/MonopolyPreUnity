using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.HotSeatInput.Choice
{
    interface IHotSeatChoice : IEntityComponent
    {
        public int PlayerId { get; set; }
    }
}
