using MonopolyPreUnity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class ActionBoxComponent : ITileComponent
    {
        public List<IAction> ActionList { get; }

        public ActionBoxComponent(List<IAction> actions)
        {
            ActionList = actions;
        }
    }
}
