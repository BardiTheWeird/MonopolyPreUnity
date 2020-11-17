using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Jail
{
    interface IJailRequest : IEntityComponent
    {
        public int PlayerId { get; set; }
    }
}
