﻿using MonopolyPreUnity.Actions;
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
                    case PrintTile printTile:
                        PrintTile(printTile.TileId);
                        break;
                    case PrintAction printAction:
                        PrintAction(printAction.Action);
                        break;
                    case PrintCashCharge cashCharge:
                        PrintCashCharge(cashCharge);
                        break;
                    case PrintCashGive cashGive:
                        PrintCashGive(cashGive);
                        break;
                    case ClearOutput clear:
                        ClearOutput();
                        break;
                }
            }
            _context.RemoveEntitiesInterface<IOutputRequest>();
        }

        #region methods
        void PrintLine(string line)
        {
            _context.OutputString += line + "\n";
            _context.Logger.AppendLine(line + "\n");
        }

        void PrintFormattedLine(string formattedLine) =>
            PrintLine(_formatOutput.FormattedString(formattedLine));

        void PrintCommands(List<MonopolyCommand> commands)
        {
            PrintLine("Choose a command");
            PrintLine(_formatOutput.GetStringOfListOfItems(commands, _formatOutput.CommandName, true));
        }

        void PrintTile(int tileId) =>
            PrintLine(_formatOutput.GetTileString(tileId));

        void PrintAction(IMonopolyAction action) =>
            PrintLine(_formatOutput.GetActionString(action));

        void PrintCashCharge(PrintCashCharge charge) =>
            PrintLine(_formatOutput.GetCashChargeString(
                charge.PlayerChargedId, charge.PlayerChargerId, charge.Amount, charge.Message));

        void PrintCashGive(PrintCashGive give) =>
            PrintLine(_formatOutput.GetCashGiveString(give.PlayerId, give.Amount));

        void ClearOutput()
        {
            _context.OutputString = "";
            _context.Logger.AppendLine("\n##SCREEN CLEARED##\n\n");
        }
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