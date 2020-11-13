using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintFormattedLine : IOutputRequest
    {
        public string OutString { get; set; }
        public OutputStream OutputStream { get; set; }

        public PrintFormattedLine(string outString, OutputStream outputStream)
        {
            OutString = outString;
            OutputStream = outputStream;
        }

        public static implicit operator string(PrintFormattedLine formattedLine) => formattedLine.OutString;
    }
}
