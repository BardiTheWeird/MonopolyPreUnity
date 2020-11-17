using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintPlayers : IOutputRequest
    {
        public List<int> Players { get; set; }
        public OutputStream OutputStream { get; set; }

        public PrintPlayers(List<int> players, OutputStream outputStream)
        {
            Players = players;
            OutputStream = outputStream;
        }

        public static implicit operator List<int>(PrintPlayers print) => print.Players;
    }
}
