﻿using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Entity
{
    class Entity
    {
        public List<IEntityComponent> Components { get; set; }

        #region Components Methods
        public void AddComponent(IEntityComponent component) =>
            Components.Add(component);

        public void RemoveComponent<T>() where T : IEntityComponent
        {
            var index = Components.FindIndex(comp => comp.GetType() == typeof(T));
            if (index != -1)
                Components.RemoveAt(index);
        }

        public T GetComponent<T>() where T : IEntityComponent
        {
            var component = Components.FirstOrDefault(comp => comp.GetType() == typeof(T));
            if (component == null)
                return default;
            return (T)component;
        }

        public bool ContainsComponent<T>() where T : IEntityComponent =>
            GetComponent<T>() != null;
        #endregion

        #region ctor
        public Entity(params IEntityComponent[] components) : this(components.ToList()) { }

        public Entity(List<IEntityComponent> components)
        {
            Components = components;
        }

        public Entity()
        {
        }
        #endregion
    }
}
