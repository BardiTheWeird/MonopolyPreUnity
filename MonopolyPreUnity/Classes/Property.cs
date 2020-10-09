using MonopolyPreUnity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    abstract class Property : ITileComponent
    {
        public string Name { get; }
        public string Set { get; }
        public int BasePrice { get; }

        public int? OwnerId { get; set; }
        public bool IsMortgaged { get; set; }

        public Property(string name, string set, int basePrice)
        {
            Name = name;
            BasePrice = basePrice; 
            Set = set;
            OwnerId = null;
            IsMortgaged = false;
        }

        abstract public void ChargeRent(int playerId);

        public void OnPlayerLanded(int playerId)
        {
            if(OwnerId == null)
            {
                // buying stuff
            }
            else if (OwnerId != playerId)
            {
                ChargeRent(playerId);
            }
            else
            {
                // do nothing
                // maybe send a message like "it's your own property, dumbass!"
            }
        }
    }
}
