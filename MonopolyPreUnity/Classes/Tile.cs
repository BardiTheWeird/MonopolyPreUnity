using MonopolyPreUnity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class Tile
    {
        string _name;
        public string Name { get => _name; }
        ITileComponent _tileComponent;
        public ITileComponent TileComponent { get => _tileComponent; }

        public void OnPlayerLanded(Player player) =>
            TileComponent.OnPlayerLanded(player);
    
        public Tile(string name, ITileComponent tileComponent)
        {
            _name = name;
            _tileComponent = tileComponent;
        }
    }
}
