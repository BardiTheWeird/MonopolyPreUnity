using MonopolyPreUnity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class Tile
    {
        public string Name { get; }
        public ITileComponent TileComponent { get; }

        public void OnPlayerLanded(int playerId) =>
            TileComponent.OnPlayerLanded(playerId);
    
        public Tile(string name, ITileComponent tileComponent)
        {
            Name = name;
            TileComponent = tileComponent;
        }
    }
}
