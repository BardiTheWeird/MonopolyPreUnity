using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Interfaces
{
    interface ITile
    {
        public int Id { get; }
        public string Name { get; }
        public void OnPlayerLanded(int playerId);
    }
}
