using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Actions
{
    class GoToTileComponentAction : IMonopolyAction
    {
        public Type ComponentType { get; set; }

        public string Descsription { get; set; }

        public GoToTileComponentAction(Type componentType, string description)
        {
            ComponentType = componentType;
            Descsription = description;
        }
    }
}
