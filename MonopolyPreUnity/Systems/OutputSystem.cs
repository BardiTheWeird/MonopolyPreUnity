using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
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
                if (print is ClearOutput clear)
                {
                    ClearOutput(clear.OutputStream);
                    continue;
                }

                var str = "";
                switch (print)
                {
                    case PrintLine line:
                        str = line;
                        break;
                    case PrintFormattedLine formattedLine:
                        str = GetFormattedLine(formattedLine);
                        break;

                    case PrintCommands commands:
                        str = GetCommandsString(commands);
                        break;
                    case PrintTile printTile:
                        str = GetTileString(printTile.TileId);
                        break;
                    case PrintAction printAction:
                        str = GetActionString(printAction.Action, printAction.Preface);
                        break;
                    case PrintProperties printProperties:
                        str = GetPropertiesString(printProperties);
                        break;

                    case PrintCashCharge cashCharge:
                        str = GetCashChargeString(cashCharge);
                        break;
                    case PrintCashGive cashGive:
                        str = GetCashGiveString(cashGive);
                        break;

                    case PrintPlayerStatus playerStatus:
                        str = GetPlayerStatus(playerStatus);
                        break;
                    case PrintGameStatus gameStatus:
                        str = GetGameStatus();
                        break;
                    case PrintMap printMap:
                        str = GetMap();
                        break;
                }

                if (str != "")
                    PrintLine(str, print.OutputStream);
            }
            _context.RemoveInterface<IOutputRequest>();
        }

        #region methods
        void PrintLine(string line, OutputStream outputStream)
        {
            if (outputStream == OutputStream.GameLog)
            {
                _context.GameLogString += line + "\n";
                _context.Logger.AppendLine(line + "\n");
            }
            else if (outputStream == OutputStream.HSInputLog)
            {
                _context.HSLogString += line + "\n";
                _context.Logger.AppendLine("> " + line + "\n");
            }
        }

        string GetFormattedLine(string formattedLine) =>
            _formatOutput.FormattedString(formattedLine);

        string GetCommandsString(List<MonopolyCommand> commands) =>
            "Choose a command:\n" + _formatOutput.GetStringOfListOfItems(commands, _formatOutput.CommandName, true);

        string GetTileString(int tileId) =>
            _formatOutput.GetTileString(tileId);

        string GetActionString(IMonopolyAction action, string preface) =>
            _formatOutput.GetActionString(action, preface);

        string GetCashChargeString(PrintCashCharge charge) =>
            _formatOutput.GetCashChargeString(
                charge.PlayerChargedId, charge.PlayerChargerId, charge.Amount, charge.Message);

        string GetCashGiveString(PrintCashGive give) =>
            _formatOutput.GetCashGiveString(give.PlayerId, give.Amount, give.Message);

        string GetPlayerStatus(int playerId) =>
            _formatOutput.GetPlayerString(playerId) + "\n";

        string GetGameStatus() =>
            "Printing Game Status is not implemented yet\n";

        string GetMap() =>
            _formatOutput.GetMapString() + "\n";

        string GetPropertiesString(PrintProperties print) =>
            _formatOutput.GetStringOfListOfItems(print.Properties, _formatOutput.GetPropertyString, print.Indexate);

        void ClearOutput(OutputStream outputStream)
        {
            if (outputStream == OutputStream.GameLog)
            {
                _context.GameLogString = "";
                _context.Logger.AppendLine("\n##GAME LOG CLEARED##\n\n");
            }
            else if (outputStream == OutputStream.HSInputLog)
            {
                _context.HSLogString = "";
                _context.Logger.AppendLine("\n> ##HSINPUTLOG CLEARED##\n\n");
            }
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
