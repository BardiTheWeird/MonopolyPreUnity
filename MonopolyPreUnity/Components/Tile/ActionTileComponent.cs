using MonopolyPreUnity.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class ActionTileComponent : IEntityComponent
    {
        public IMonopolyAction Action { get; }

        public ActionTileComponent(IMonopolyAction action)
        {
            Action = action;
        }
    }
}
