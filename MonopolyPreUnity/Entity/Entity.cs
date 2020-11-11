using MonopolyPreUnity.Components;
using MonopolyPreUnity.UI;
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
            var component = GetComponent(comp => comp.GetType() == typeof(T));
            if (component == null)
                return default;
            return (T)component;
        }

        public IEntityComponent GetComponent(Func<IEntityComponent, bool> predicate) =>
            Components.FirstOrDefault(predicate);

        public bool ContainsComponent(IEntityComponent component) =>
            Components.FirstOrDefault(c => c == component) != null;

        public bool ContainsComponent<T>() where T : IEntityComponent =>
            GetComponent<T>() != null;

        public bool ContainsComponent(Func<IEntityComponent, bool> predicate) =>
            GetComponent(predicate) != null;
        #endregion

        #region to string
        public override string ToString()
        {
            if (Components.Count == 1)
                return Components[0].GetType().GetShortString() + " Entity";
            if (ContainsComponent<Tile>())
                return GetComponent<Tile>().GetType().GetShortString() + " Entity";

            return $"Entity, Components.Count=\"{Components.Count}\"";
        }
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
