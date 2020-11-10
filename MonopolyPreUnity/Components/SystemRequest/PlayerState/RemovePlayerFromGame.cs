using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.PlayerState
{
    class RemovePlayerFromGame : IEntityComponent
    {
        public int PlayerId { get; set; }

        public RemovePlayerFromGame(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
