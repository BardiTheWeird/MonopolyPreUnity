using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Serialization;

namespace MonopolyPreUnity.Components
{
    class Tile : IIdentifiable
    {
        public int Id { get; }
        public int MapPosition { get; set; }
        public string Name { get; set; }

        public Tile(int id, int mapPosition, string name)
        {
            Id = id;
            MapPosition = mapPosition;
            Name = name;
        }
    }
}
