using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Interfaces
{
    interface IPlayer
    {
        public int Id { get; }
        public string DisplayName { get; }
        public List<Property> Properties { get; set; }
        public int Cash { get; set; }
        public int JailCards { get; set; }
        public int TurnsInPrison { get; set; }
    }
}
