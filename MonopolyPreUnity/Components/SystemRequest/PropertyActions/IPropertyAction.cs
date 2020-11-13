using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.PropertyActions
{
    interface IPropertyAction : IEntityComponent
    {
        public int PropertyId { get; set; }
        public int PlayerId { get; set; }
    }
}
