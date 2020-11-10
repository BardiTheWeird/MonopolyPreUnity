using MonopolyPreUnity.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class ActionBox : IEntityComponent
    {
        public List<IMonopolyAction> ActionList { get; }

        public ActionBox(List<IMonopolyAction> actions) =>
            ActionList = actions;

        public static implicit operator List<IMonopolyAction>(ActionBox box) => box.ActionList;
    }
}
