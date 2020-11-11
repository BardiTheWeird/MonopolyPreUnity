using MonopolyPreUnity.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintAction : IOutputRequest
    {
        public IMonopolyAction Action { get; set; }

        public PrintAction(IMonopolyAction action)
        {
            Action = action;
        }
    }
}
