using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class Tile
    {
        #region Components
        public TileIdentityComponent IdentityComponent { get; }
        public ITileContentComponent ContentComponent { get; }
        #endregion

        public Tile(TileIdentityComponent identityComponent, ITileContentComponent contentComponent)
        {
            IdentityComponent = identityComponent;
            ContentComponent = contentComponent;
        }
    }
}
