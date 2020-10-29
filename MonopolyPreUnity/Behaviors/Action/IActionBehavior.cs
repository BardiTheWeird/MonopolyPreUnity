using MonopolyPreUnity.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Action
{
    interface IActionBehavior
    {
        public void Execute(int playerId, IMonopolyAction action);
    }
}
