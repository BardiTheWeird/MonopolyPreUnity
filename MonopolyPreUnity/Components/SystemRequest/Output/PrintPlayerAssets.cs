using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintPlayerAssets : IOutputRequest
    {
        public PlayerAssets Assets { get; set; }
        public OutputStream OutputStream { get; set; }

        public PrintPlayerAssets(PlayerAssets assets, OutputStream outputStream)
        {
            Assets = assets;
            OutputStream = outputStream;
        }

        public static implicit operator PlayerAssets(PrintPlayerAssets print) => print.Assets;
    }
}
