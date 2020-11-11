using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.HSInput.Choice
{
    interface IHSChoice : IEntityComponent
    {
        public int PlayerId { get; set; }
    }
}
