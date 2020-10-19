using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Serialization;

namespace MonopolyPreUnity.Components
{
    [Serializable]
    class TileIdentityComponent : ITileComponent
    {
        public int Id { get; }
        public string Name { get { return _name; } set { _name = value; } } //undo later
        [NonSerialized]
        public string _name;

        public TileIdentityComponent(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
