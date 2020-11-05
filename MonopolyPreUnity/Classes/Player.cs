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
        public int Cash { get; set; }
        public int JailCards { get; set; }
        public int? TurnsInPrison { get; set; }
        public int CurrentTileId { get; set; }
        public bool CanMove { get; set; }
        public HashSet<int> Properties { get; }

        public Player(int id, string displayName, int cash, 
            HashSet<int> properties, int jailCards, int? turnsInPrison, int curTileId)
        {
            Id = id;
            DisplayName = displayName;
            Cash = cash;
            Properties = properties;
            JailCards = jailCards;
            TurnsInPrison = turnsInPrison;
            CanMove = true;
            CurrentTileId = curTileId;
        }

        public Player(int id, string displayName, HashSet<int> props = null)
        {
            Id = id;
            DisplayName = displayName;
            Properties = props ?? new HashSet<int>();
        }
    }
}
