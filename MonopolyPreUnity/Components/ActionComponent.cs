using MonopolyPreUnity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class ActionComponent : ITileComponent
    {
        public IMonopolyAction Action { get; }
    }
}
