using MonopolyPreUnity.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Initialization
{
    class SystemsBag
    {
        public ISystem[] Systems { get; set; }
        public HashSet<Type> TurnedOffSystems { get; set; }

        public void Execute()
        {
            var systemsOn = Systems.Where(sys => !TurnedOffSystems.Contains(sys.GetType())).ToList();
            foreach (var system in systemsOn)
                system.Execute();
        }

        public void TurnOn<T>() where T : ISystem =>
            TurnedOffSystems.Remove(typeof(T));

        public void TurnOff<T>() where T : ISystem =>
            TurnedOffSystems.Add(typeof(T));

        #region ctor
        public SystemsBag(ISystem[] systems)
        {
            Systems = systems;
            TurnedOffSystems = new HashSet<Type>();
        }
        #endregion
    }
}
