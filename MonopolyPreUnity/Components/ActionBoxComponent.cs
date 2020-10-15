using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class ActionBoxComponent : ITileComponent
    {
        public List<IMonopolyAction> Actions { get; }

        public ActionBoxComponent(List<IMonopolyAction> actions)
        {
            Actions = actions;
        }
    }
}
