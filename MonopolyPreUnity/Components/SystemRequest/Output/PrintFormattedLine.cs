using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintFormattedLine : IOutputRequest
    {
        public string OutString { get; set; }

        public PrintFormattedLine(string outString) =>
            OutString = outString;

        public static implicit operator string(PrintFormattedLine formattedLine) => formattedLine.OutString;
    }
}
