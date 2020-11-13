using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Output
{
    class PrintCommands : IOutputRequest
    {
        public List<MonopolyCommand> Commands { get; set; }
        public OutputStream OutputStream { get; set; }

        public PrintCommands(List<MonopolyCommand> commands, OutputStream outputStream = OutputStream.HSInputLog)
        {
            Commands = commands;
            OutputStream = outputStream;
        }

        public static implicit operator List<MonopolyCommand>(PrintCommands printCommands) => printCommands.Commands;
    }
}
