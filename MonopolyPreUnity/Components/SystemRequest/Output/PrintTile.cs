using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintTile : IOutputRequest
    {
        public int TileId { get; set; }
        public OutputStream OutputStream { get; set; }

        public PrintTile(int tileId, OutputStream outputStream)
        {
            TileId = tileId;
            OutputStream = outputStream;
        }
    }
}
