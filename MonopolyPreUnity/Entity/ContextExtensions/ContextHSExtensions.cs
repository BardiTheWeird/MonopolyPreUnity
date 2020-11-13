using MonopolyPreUnity.Components.SystemRequest.HSInput;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Entity.ContextExtensions
{
    static class ContextHSExtensions
    {
        public static HSInputState HSInputState(this Context context) =>
            context.GetComponent<HSInputState>();
    }
}
