using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintLine : IOutputRequest
    {
        public string OutString { get; set; }
        public OutputStream OutputStream { get; set; }

        public PrintLine(string outString, OutputStream outputStream)
        {
            OutString = outString;
            OutputStream = outputStream;
        }

        public static implicit operator string(PrintLine printLine) => printLine.OutString;
    }
}
