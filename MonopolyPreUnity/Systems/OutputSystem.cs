using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.UI;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class OutputSystem : ISystem
    {
        private readonly Context _context;
        private readonly FormatOutput _formatOutput;

        public void Execute()
        {
            foreach (var print in _context.GetComponentsInterface<IOutputRequest>())
            {
                switch (print)
                {
                    case PrintLine line:
                        PrintLine(line);
                        break;
                    case PrintFormattedLine formattedLine:
                        PrintFormattedLine(formattedLine);
                        break;
                    case PrintCommands commands:
                        PrintCommands(commands);
                        break;
                    case ClearOutput clear:
                        ClearOutput();
                        break;
                }
            }
            _context.RemoveEntitiesInterface<IOutputRequest>();
        }

        #region methods
        void PrintLine(string line) =>
            _context.OutputString += line + "\n";

        void PrintFormattedLine(string formattedLine) =>
            PrintLine(_formatOutput.FormattedString(formattedLine));

        void PrintCommands(List<MonopolyCommand> commands) =>
            PrintLine(_formatOutput.GetStringOfListOfItems(commands, _formatOutput.CommandName, true));

        void ClearOutput() =>
            _context.OutputString = "";
        #endregion

        #region ctor
        public OutputSystem(Context context, FormatOutput formatOutput)
        {
            _context = context;
            _formatOutput = formatOutput;
        }
        #endregion
    }
}
