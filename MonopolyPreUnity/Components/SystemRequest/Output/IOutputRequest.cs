using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    enum OutputStream
    {
        GameLog,
        HSInputLog,
    }

    interface IOutputRequest : IEntityComponent
    {
        public OutputStream OutputStream { get; set; }
    }
}
