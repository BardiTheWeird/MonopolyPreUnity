﻿using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Entity
{
    class Context : INotifyPropertyChanged
    {
        public List<Entity> Entities { get; set; }

        #region I/O strings
        string _outputString = "";
        public string OutputString
        {
            get => _outputString;
            set
            {
                if (value != _outputString)
                {
                    _outputString = value;
                    RaisePropertyChanged(nameof(OutputString));
                }
            }
        }

        public string InputString { get; set; } = "";
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        #endregion

        #region Component Methods
        public T GetComponent<T>(Func<T, bool> predicate) where T : IEntityComponent =>
            GetComponents<T>().FirstOrDefault(predicate);

        public T GetComponent<T>() where T : IEntityComponent =>
            GetEntity<T>().GetComponent<T>();

        public List<T> GetComponents<T>() where T : IEntityComponent =>
            GetEntities<T>().Select(entity => entity.GetComponent<T>()).ToList();

        public List<T> GetComponents<T>(Func<T, bool> predicate) where T : IEntityComponent =>
            GetComponents<T>().Where(predicate).ToList();
        #endregion

        #region ContainsComponent
        public bool ContainsComponent<T>() where T : IEntityComponent =>
            GetComponent<T>() != null;
        #endregion

        #region Entity Methods
        public Entity GetEntity<T>(Func<Entity, bool> predicate) where T : IEntityComponent=>
            GetEntities<T>().FirstOrDefault(predicate);

        public Entity GetEntity(Func<Entity, bool> predicate) =>
            Entities.FirstOrDefault(predicate);

        public Entity GetEntity<T>() where T : IEntityComponent =>
            GetEntity(entity => entity.ContainsComponent<T>());

        public List<Entity> GetEntities(Func<Entity, bool> predicate) =>
            Entities.Where(predicate).ToList();

        public List<Entity> GetEntities<T>() where T : IEntityComponent =>
            GetEntities(entity => entity.ContainsComponent<T>());

        public void Remove(IEntityComponent component) =>
            Entities.Where(e => e.ContainsComponent(component));

        public void Remove(Entity entity) =>
            Entities.Remove(entity);

        public void RemoveRange(IEnumerable<Entity> entities) =>
            Entities.RemoveAll(entity => entities.Contains(entity));

        public void Remove<T>() where T : IEntityComponent =>
            Remove(entity => entity.ContainsComponent<T>());

        public void Remove<T>(Func<T, bool> predicate) where T : IEntityComponent =>
            Remove(entity => entity.ContainsComponent<T>() && predicate(entity.GetComponent<T>()));

        public void Remove(Func<Entity, bool> predicate) =>
            Entities.RemoveAll(new Predicate<Entity>(predicate));

        public void Add(Entity entity) =>
            Entities.Add(entity);

        public void Add(params IEntityComponent[] components) =>
            Add(new Entity(components));

        public void AddEntities(params Entity[] entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public void AddEntities(params IEntityComponent[] components)
        {
            foreach (var comp in components)
                Add(comp);
        }
        #endregion

        #region ctor
        public Context(List<Entity> entities, string output, string input) : this(entities)
        {
            OutputString = output;
            InputString = input;
        }

        public Context(List<Entity> entities) =>
            Entities = entities;

        public Context(params Entity[] entities) : this(entities.ToList()) { }

        public Context() : this(new List<Entity>()) { }
        #endregion
    }
}
