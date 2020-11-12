using MonopolyPreUnity.Actions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintAction : IOutputRequest
    {
        public IMonopolyAction Action { get; set; }
        public string Preface { get; set; }

        public PrintAction(IMonopolyAction action, string preface = null)
        {
            Action = action;
            Preface = preface;
        }
    }
}
