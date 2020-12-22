using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Actions
{
    class GoToJailAction : IMonopolyAction
    {
        public string Descsription { get; set; }

        public GoToJailAction() { }

        public GoToJailAction(string description)
        {
            Descsription = description;
        }
    }
}
