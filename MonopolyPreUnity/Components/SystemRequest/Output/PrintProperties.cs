using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintProperties : IOutputRequest
    {
        public List<int> Properties { get; set; }
        public OutputStream OutputStream { get; set; }

        public PrintProperties(List<int> properties, OutputStream outputStream)
        {
            Properties = properties;
            OutputStream = outputStream;
        }

        public static implicit operator List<int>(PrintProperties printProperties) => printProperties.Properties;
    }
}
