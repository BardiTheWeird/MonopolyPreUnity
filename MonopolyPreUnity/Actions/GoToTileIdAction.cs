using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Actions
{
    class GoToTileIdAction : IMonopolyAction
    {
        public int TileId { get; set; }
        public string Descsription { get; set; }

        public GoToTileIdAction(int tileId) => TileId = tileId;

        public GoToTileIdAction(int tileId, string description)
        {
            TileId = tileId;
            Descsription = description;
        }
    }
}
