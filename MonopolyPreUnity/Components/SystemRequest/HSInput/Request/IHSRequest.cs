using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.HSInput
{
    interface IHSRequest : IEntityComponent
    {
        public int PlayerId { get; set; }
    }
}
