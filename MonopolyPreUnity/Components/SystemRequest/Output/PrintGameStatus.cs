using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintGameStatus : IOutputRequest
    {
        public OutputStream OutputStream { get; set; }

        public PrintGameStatus(OutputStream outputStream = OutputStream.HSInputLog) =>
            OutputStream = outputStream;
    }
}
