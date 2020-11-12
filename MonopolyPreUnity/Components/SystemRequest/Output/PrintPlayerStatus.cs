using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintPlayerStatus : IOutputRequest
    {
        public int PlayerId { get; set; }

        public PrintPlayerStatus(int playerId) =>
            PlayerId = playerId;

        public static implicit operator int(PrintPlayerStatus playerStatus) => playerStatus.PlayerId;
    }
}
