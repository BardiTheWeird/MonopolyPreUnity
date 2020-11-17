using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class ClearOutput : IOutputRequest
    {
        public OutputStream OutputStream {get; set;}

        public ClearOutput(OutputStream outputStream = OutputStream.HSInputLog)
        {
            OutputStream = outputStream;
        }
    }
}
