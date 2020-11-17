using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    interface IIdentifiable : IEntityComponent
    {
        public int Id { get; }
    }
}
