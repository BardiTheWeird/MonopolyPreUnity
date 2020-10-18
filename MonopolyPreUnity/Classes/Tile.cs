using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


namespace MonopolyPreUnity.Classes
{
    [Serializable]
    [DataContract]
    [KnownType(typeof(PropertyComponent))]
    [KnownType(typeof(JailComponent))]
    class Tile
    {
        public List<ITileComponent> Components { get; }

        public Tile(List<ITileComponent> components)
        {
            Components = components;
        }
    }
}
