using MonopolyPreUnity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class TileManager
    {
        private readonly Dictionary<int, ITile> tileDict;

        public ITile GetTile(int tileId) =>
            tileDict[tileId];

        public void OnPlayerLanded(int tileId, int playerId) =>
            GetTile(tileId).OnPlayerLanded(playerId);
    }
}
