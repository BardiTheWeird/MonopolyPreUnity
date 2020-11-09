using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Serialization;

namespace MonopolyPreUnity.Components
{
    class TileComponent : IEntityComponent
    {
        public int Id { get; }
        public int MapPosition { get; set; }
        public string Name { get; set; }

        public TileComponent(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
