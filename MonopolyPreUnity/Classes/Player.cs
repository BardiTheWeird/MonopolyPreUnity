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
        public int? TurnsInJail { get; set; }
        public int CurrentTileId { get; set; }
        public bool CanMove { get; set; }
        public bool IsBankrupt { get; set; }
        public bool IsWinner { get; set; }
        public bool RolledJailDiceThisTurn { get; set; }
        public HashSet<int> Properties { get; }

        public Player(int id, string displayName, int cash, 
            HashSet<int> properties, int jailCards, int? turnsInPrison, int curTileId, bool rolledJailDiceThisTurn)
        {
            Id = id;
            DisplayName = displayName;
            Cash = cash;
            Properties = properties;
            JailCards = jailCards;
            TurnsInJail = turnsInPrison;
            CanMove = true;
            CurrentTileId = curTileId;
            RolledJailDiceThisTurn = rolledJailDiceThisTurn;
        }

        public Player(int id, string displayName, HashSet<int> props = null)
        {
            Id = id;
            DisplayName = displayName;
            Properties = props ?? new HashSet<int>();
        }
    }
}
