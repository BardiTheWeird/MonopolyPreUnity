using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MonopolyPreUnity.Entity
{
    static class ContextGameStateExtensions
    {
        public static TurnInfo TurnInfo(this Context context) =>
            context.GetComponent<TurnInfo>();

        public static Dice Dice(this Context context) =>
            context.GetComponent<Dice>();
    }
}
