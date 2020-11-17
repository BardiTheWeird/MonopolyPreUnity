using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintProperties : IOutputRequest
    {
        public List<int> Properties { get; set; }
        public OutputStream OutputStream { get; set; }
        public bool Indexate { get; set; }

        public PrintProperties(List<int> properties, OutputStream outputStream, bool indexate = true)
        {
            Properties = properties;
            OutputStream = outputStream;
            Indexate = indexate;
        }

        public PrintProperties(int propId, OutputStream outputStream, bool indexate = false) 
            : this(new List<int> { propId }, outputStream, indexate) { }

        public static implicit operator List<int>(PrintProperties printProperties) => printProperties.Properties;
    }
}
