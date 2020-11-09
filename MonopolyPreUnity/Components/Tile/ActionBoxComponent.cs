using MonopolyPreUnity.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class ActionBoxComponent : IEntityComponent
    {
        public List<IMonopolyAction> ActionList { get; }

        public ActionBoxComponent(List<IMonopolyAction> actions)
        {
            ActionList = actions;
        }
    }
}
