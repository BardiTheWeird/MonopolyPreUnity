using MonopolyPreUnity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class PlayerAI : IPlayer
    {
        public int Id => throw new NotImplementedException();

        public string DisplayName => throw new NotImplementedException();

        public List<Property> Properties { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Cash { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int JailCards { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int TurnsInPrison { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
