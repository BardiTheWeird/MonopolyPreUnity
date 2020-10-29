using Autofac.Features.Indexed;
using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Behaviors.Action;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class ActionManager
    {
        private readonly IIndex<Type, IActionBehavior> _behaviorIndex;

        public void ExecuteAction(int playerId, IMonopolyAction action) =>
            _behaviorIndex[action.GetType()].Execute(playerId, action);

        #region Constructor
        public ActionManager(IIndex<Type, IActionBehavior> behaviorIndex)
        {
            _behaviorIndex = behaviorIndex;
        }
        #endregion
    }
}
