using Autofac.Features.Indexed;
using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Behaviors.Action;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class ActionsSystem : ISystem
    {
        #region fields
        private readonly IIndex<Type, IActionBehavior> _behaviorIndex;
        private readonly Context _context;
        #endregion

        public void Execute()
        {
            foreach (var actionRequest in _context.GetComponents<ExecuteAction>())
            {
                var action = actionRequest.Action;
                _behaviorIndex[action.GetType()].Execute(actionRequest.PlayerId, action);
            }
        }

        public ActionsSystem(IIndex<Type, IActionBehavior> behaviorIndex, Context context)
        {
            _behaviorIndex = behaviorIndex;
            _context = context;
        }
    }
}
