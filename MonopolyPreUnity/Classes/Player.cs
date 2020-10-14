using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class Player
    {
        public int Id { get; }
        public string DisplayName { get; }
        public List<PropertyComponent> Properties { get; }
        public int Cash { get; set; }
        public int JailCards { get; set; }
        public int? TurnsInPrison { get; set; }
        public int CurrentTileId { get; set; }

        public Player(int id, string displayName, int cash)
        {
            Id = id;
            DisplayName = displayName;
            Properties = new List<PropertyComponent>();
            Cash = cash;
            JailCards = 0;
            TurnsInPrison = null;
            CurrentTile = 0; // represents startTile
        }
    }
}
