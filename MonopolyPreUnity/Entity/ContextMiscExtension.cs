using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Entity
{
    static class ContextMiscExtension
    {
        public static List<Entity> GetEntities<T>(this List<Entity> entities) where T : IEntityComponent =>
            entities.Where(e => e.GetType() == typeof(T)).ToList();
    }
}
