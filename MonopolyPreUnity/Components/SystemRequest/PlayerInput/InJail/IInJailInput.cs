using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.PlayerInput.InJail
{
    interface IInJailInput : IEntityComponent
    {
        public int PlayerId { get; set; }
    }
}
