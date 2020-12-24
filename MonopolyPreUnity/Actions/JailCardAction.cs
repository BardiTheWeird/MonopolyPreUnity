using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Actions
{
    class JailCardAction : IMonopolyAction
    {
        public string Descsription { get; set; }

        public JailCardAction() { }

        public JailCardAction(string description)
        {
            Descsription = description;
        }
    }
}
