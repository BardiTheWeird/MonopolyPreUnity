using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class ActionBoxComponent : ITileContentComponent
    {
        public List<IMonopolyAction> Actions { get; }

        public void OnPlayerLanded(int playerId)
        {
            Actions[new Random().Next(0, Actions.Count)].Execute(playerId);
        }

        public ActionBoxComponent(List<IMonopolyAction> actions)
        {
            Actions = actions;
        }
    }
}
