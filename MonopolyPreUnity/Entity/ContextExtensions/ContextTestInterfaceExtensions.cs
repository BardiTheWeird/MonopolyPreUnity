﻿using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Entity.ContextExtensions
{
    static class ContextTestInterfaceExtensions
    {
        public static List<IT> GetComponentsInterface<IT>(this Context context) where IT : IEntityComponent =>
            context.GetEntities(entity => entity.ContainsComponent(comp => comp is IT))
            .Select(entity => (IT)entity.GetComponent(comp => comp is IT))
            .ToList();

        public static IT GetComponentInterface<IT>(this Context context) where IT : IEntityComponent =>
            context.GetComponentsInterface<IT>().FirstOrDefault();

        public static void RemoveInterface<IT>(this Context context) where IT : IEntityComponent =>
            context.Remove(entity => entity.ContainsComponent(comp => comp is IT));

        public static bool ContainsComponentInterface<IT>(this Context context) where IT : IEntityComponent =>
            context.GetComponentsInterface<IT>().Count > 0;
    }
}
