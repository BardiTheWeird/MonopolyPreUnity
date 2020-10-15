using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class Tile
    {
        public List<ITileComponent> Components { get; }

        public Tile(List<ITileComponent> components)
        {
            Components = components;
        }
    }
}
