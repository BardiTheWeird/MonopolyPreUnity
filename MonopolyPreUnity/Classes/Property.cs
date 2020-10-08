using MonopolyPreUnity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    abstract class Property : ITileComponent
    {
        string _name;
        public string Name { get => _name; }
        string _set; // a string for now
        public string Set { get => _set; }
        uint _basePrice;
        public uint BasePrice { get => _basePrice; }

        IPlayer _owner;
        public IPlayer Owner { get; set; }
        bool _isMortgaged;
        public bool IsMortgaged { get; set; }

        public Property(string name, string set, uint basePrice)
        {
            _name = name;
            _basePrice = basePrice; 
            _set = set;
            _owner = null;
            _isMortgaged = false;
        }

        abstract public void ChargeRent(IPlayer player);

        public void OnPlayerLanded(IPlayer player)
        {
            if(Owner == null)
            {
                
            }
            else if (!Owner.Equals(player))
            {
                ChargeRent(player);
            }
            else
            {
                // do nothing
                // maybe send a message like "it's your own property, dumbass!"
            }
        }
    }
}
