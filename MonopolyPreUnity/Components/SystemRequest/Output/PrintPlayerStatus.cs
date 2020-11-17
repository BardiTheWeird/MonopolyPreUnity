using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintPlayerStatus : IOutputRequest
    {
        public int PlayerId { get; set; }
        public OutputStream OutputStream { get; set; }

        public PrintPlayerStatus(int playerId, OutputStream outputStream = OutputStream.HSInputLog)
        {
            PlayerId = playerId;
            OutputStream = outputStream;
        }

        public static implicit operator int(PrintPlayerStatus playerStatus) => playerStatus.PlayerId;
    }
}
