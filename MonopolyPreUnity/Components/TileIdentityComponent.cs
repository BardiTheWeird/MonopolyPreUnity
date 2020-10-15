using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class TileIdentityComponent : ITileComponent
    {
        public int Id { get; }
        public string Name { get; }

        public TileIdentityComponent(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
