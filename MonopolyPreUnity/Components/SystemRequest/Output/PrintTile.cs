using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintTile : IOutputRequest
    {
        public int TileId { get; set; }

        public PrintTile(int tileId)
        {
            TileId = tileId;
        }
    }
}
