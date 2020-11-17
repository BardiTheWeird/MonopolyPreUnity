using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Move
{
    class MoveTileId : IMoveRequest
    {
        public int PlayerId { get; set; }
        public bool CountGoPassed { get; set; }
        public bool CountPlayerLanded { get; set; }
        public int TileId { get; set; }

        public MoveTileId(int playerId, int tileId, bool countGoPassed, bool countPlayerLanded = true)
        {
            PlayerId = playerId;
            TileId = tileId;
            CountGoPassed = countGoPassed;
            CountPlayerLanded = countPlayerLanded;
        }
    }
}
