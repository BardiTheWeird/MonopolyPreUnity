using MonopolyPreUnity.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class ActionTile : IEntityComponent
    {
        public IMonopolyAction Action { get; }

        public ActionTile(IMonopolyAction action) =>
            Action = action;
    }
}
