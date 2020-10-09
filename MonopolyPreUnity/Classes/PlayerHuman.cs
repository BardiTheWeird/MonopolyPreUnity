using MonopolyPreUnity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class PlayerHuman : IPlayer
    {
        int IPlayer.Id => throw new NotImplementedException();

        string IPlayer.DisplayName => throw new NotImplementedException();

        List<Property> IPlayer.Properties { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int IPlayer.Cash { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int IPlayer.JailCards { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int IPlayer.TurnsInPrison { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
