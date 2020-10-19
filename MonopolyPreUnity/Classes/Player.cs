using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Serialization;

namespace MonopolyPreUnity.Classes
{
    [Serializable]
    class Player
    {
        public int Id { get; }
        public string DisplayName { get; }
        public HashSet<int> Properties { get; }
        public int Cash { get; set; }
        public int JailCards { get; set; }
        public int? TurnsInPrison { get; set; }
        public int CurrentTileId { get; set; }
        public bool CanMove { get; set; }

        public Player(int id, string displayName, int cash, 
            HashSet<int> properties, int jailCards, int? turnsInPrison)
        {
            Id = id;
            DisplayName = displayName;
            Cash = cash;
            Properties = properties;
            JailCards = jailCards;
            TurnsInPrison = turnsInPrison;
        }
    }
}
