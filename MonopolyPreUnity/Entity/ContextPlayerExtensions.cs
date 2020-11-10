using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Entity
{
    static class ContextPlayerExtensions
    {
        public static Player GetPlayer(this Context context, int id) =>
            context.GetComponent<Player>(p => p.Id == id);

        public static int GetPlayerCash(this Context context, int id) =>
            context.GetComponent<Player>(p => p.Id == id).Cash;
    }
}
