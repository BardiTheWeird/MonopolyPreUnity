using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintLine : IOutputRequest
    {
        public string OutString { get; set; }

        public PrintLine(string outString)
        {
            OutString = outString;
        }

        public static implicit operator string(PrintLine printLine) => printLine.OutString;
        // public static implicit operator PrintLine(string str) => new PrintLine(str);
    }
}
