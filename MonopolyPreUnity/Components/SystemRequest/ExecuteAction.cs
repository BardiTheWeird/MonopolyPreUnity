using MonopolyPreUnity.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest
{
    class ExecuteAction : IEntityComponent
    {
        public IMonopolyAction Action { get; set; }
        public int PlayerId { get; set; }

        public ExecuteAction(IMonopolyAction action, int playerId)
        {
            Action = action;
            PlayerId = playerId;
        }
    }
}
