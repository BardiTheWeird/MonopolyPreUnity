using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintMap : IOutputRequest
    {
        public OutputStream OutputStream { get; set; }

        public PrintMap(OutputStream outputStream = OutputStream.HSInputLog)
        {
            OutputStream = outputStream;
        }
    }
}
