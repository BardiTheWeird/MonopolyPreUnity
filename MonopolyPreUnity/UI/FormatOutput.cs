using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace MonopolyPreUnity.UI
{
    #region extensions
    static class FormatOutputExtension
    {
        public static string SplitByCapitalLetter(this string str) =>
            Regex.Replace(str, @"[A-Z]", match => " " + match.Value).Trim();

        public static string Tabulize(this string str) =>
            "\t" + Regex.Replace(str, @"\n", "\n\t");

        public static string AddTwoSpacesAtNewLine(this string str) =>
            "  " + Regex.Replace(str, @"\n", "\n  ");

        public static string GetShortTypeString(this Type type)
        {
            var typeString = type.ToString();
            var lastDotIndex = typeString.LastIndexOf('.');
            return typeString.Substring(lastDotIndex == -1 ? 0 : lastDotIndex + 1);
        }

        public static bool NotIdentityOrProperty(this IEntityComponent component)
        {
            var type = component.GetType();
            var check =
                type == typeof(Tile)
                || type == typeof(Property)
                || type == typeof(PropertyDevelopment)
                || type == typeof(TrainStation)
                || type == typeof(UtilityProperty);
            return !check;
        }
    }
    #endregion

    class FormatOutput
    {
        #region dependencies
        private readonly Context _context;
        #endregion

        #region formatted
        public string FormattedString(string str)
        {
            return Regex.Replace(str, @"\|(\w+):(\d+)\|", match =>
            {
                var formatVal = match.Groups[1].Value;
                var id = Convert.ToInt32(match.Groups[2].Value);
                switch (formatVal)
                {
                    case "player":
                        return _context.GetPlayer(id).DisplayName;
                    case "tile":
                        return _context.GetTileId(id).Name;
                }
                return match.Value;
            });
        }
        #endregion

        #region Cash
        public string GetCashChargeString(int payerId, int? chargerId, int amount, string message = null)
        {
            var payer = _context.GetPlayer(payerId);
            var charger = chargerId == null ? null : _context.GetPlayer((int)chargerId);
            var chargerName = charger == null ? "bank" : charger.DisplayName;

            var sb = new StringBuilder();
            sb.Append($"{payer.DisplayName} paid {amount}$ to {chargerName}");
            if (message != null)
                sb.Append(" " + message);

            sb.Append($"\n  Current balance: " +
                $"\n    Payer: {payer.Cash}$");

            if (charger != null)
                sb.Append($"; Charger: {charger.Cash}$");

            return sb.ToString();
        }

        public string GetCashGiveString(int receiverId, int amount, string message = null)
        {
            var receiver = _context.GetPlayer(receiverId);
            message = message == null || message == "" ? "" : $"({message})";

            var sb = new StringBuilder();
            sb.Append($"{receiver.DisplayName} received {amount}$ {message}");
            sb.Append($"\n  Current balance: {receiver.Cash}$");

            //if (message != null)
            //    sb.Append("\n  Message: " + message);

            return sb.ToString();
        }
        #endregion

        #region commands
        public string CommandName(MonopolyCommand command)
        {
            var commandName = command.ToString();
            var lastDotIndex = commandName.LastIndexOf('.');
            return commandName.Substring(lastDotIndex == -1 ? 0 : lastDotIndex).SplitByCapitalLetter();
        }
        #endregion

        #region PlayerStatus
        public string GetPlayerString(int playerId)
        {
            var player = _context.GetPlayer(playerId);
            var sb = new StringBuilder();

            sb.Append($"Name: {player.DisplayName}\n");
            sb.Append(GetPlayerStateString(player, addCurrentTile: true).AddTwoSpacesAtNewLine());

            return sb.ToString().Trim();
        }

        public string GetPlayerStateString(Player player, bool addCurrentTile = false, bool addInJail = true, bool addProperties = true)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Cash: {player.Cash}");
            sb.AppendLine($"JailCards: {player.JailCards}");
            if (addCurrentTile)
                sb.AppendLine(FormattedString($"Currently at: |tile:{player.CurTileId}|"));
            if (addInJail)
            {
                var str = player.TurnsInJail == null ? "Not in jail" : $"In jail, {player.TurnsInJail} turns";
                sb.AppendLine(str);
            }
            if (addProperties)
            {
                if (player.Properties.Count == 0)
                    sb.AppendLine("No Properties owned");
                else
                {
                    sb.AppendLine("Properties:");
                    foreach (var prop in player.Properties)
                        sb.AppendLine(GetPropertyString(prop, printOwner: false).AddTwoSpacesAtNewLine());
                }
            }
            return sb.ToString().Trim();
        }
        #endregion

        #region Map
        public string GetMapString()
        {
            var map = _context.GetMap();
            var mapString = GetStringOfListOfItems(map, GetMapTileString, false);
            return $"Map:\n" +
                $"{mapString.AddTwoSpacesAtNewLine()}";
        }

        string GetMapTileString(Tile tile)
        {
            var players = _context.GetAllPlayers()
                .Where(p => p.CurTileId == tile.Id)
                .Select(p => p.DisplayName);

            var playersString = "";
            if (players.Count() > 0)
                playersString = players.Aggregate("", (ps, p) => ps += p + ", ", ps => $"(players: {ps.Substring(0, ps.Length - 2)})");

            return $"{tile.Name} {playersString}".Trim();
        }
        #endregion

        #region Actions
        public string GetActionString<T>(T action, string preface = "") where T : IMonopolyAction
        {
            var name = action.GetType().GetShortTypeString();
            var desc = GetActionDescription(action);

            if (preface.Length > 0)
                preface += ": ";

            return $"{preface}{name} ({desc})";
        }

        public string GetActionDescription(IMonopolyAction action)
        {
            var desc = "No description found";
            switch (action)
            {
                case ChangeBalanceAction balance:
                    desc = $"{(balance.Amount < 0 ? "Charges" : "Gives")} {Math.Abs(balance.Amount)}$";
                    break;
                case GiftFromPlayersAction gift:
                    desc = $"Each player gifts you {gift.Amount}";
                    break;
                case GoToJailAction jailGo:
                    desc = "Sends player to jail";
                    break;
                case GoToTileIdAction tileId:
                    break;
                case GoToTileComponentAction tileComponent:
                    var compTypeName = tileComponent.ComponentType.GetShortTypeString();
                    desc = $"Send player to tile with {compTypeName}";
                    break;
                case JailCardAction jailCard:
                    desc = "Gifts player a jail card";
                    break;
                case TaxPerHouseAction taxPerHouse:
                    desc = $"Taxes player {taxPerHouse.Amount} per house built";
                    break;
            }
            return desc;
        }
        #endregion

        #region tile
        public string GetTileString(int tileId)
        {
            var sb = new StringBuilder();

            var identity = _context.GetTileId(tileId);
            sb.AppendLine(identity.Name.Trim());

            var prop = _context.GetTileComponent<Property>(tileId);
            if (prop != null)
                sb.Append(GetPropertyString(tileId));

            var components = _context.GetTileComponents(tileId).Where(comp => comp.NotIdentityOrProperty());
            foreach (var comp in components)
            {
                var compString = GetTileComponentString(comp);
                if (compString.Length > 0)
                    sb.AppendLine(compString.Trim());
            }

            return sb.ToString().Trim();
        }
        #endregion

        #region TileComponent Strings
        public string GetTileComponentString(IEntityComponent component)
        {
            if (component == null)
                return "";

            switch (component)
            {
                case Property prop:
                    return GetPropComponentString(prop);
                case PropertyDevelopment dev:
                    return GetDevComponentString(dev);
                case TrainStation train:
                    return $"Base station rent: {train.BaseRent}";
                case UtilityProperty utility:
                    return "";

                case ActionBox actionBox:
                case ActionTile actionTile:
                case FreeParking freeParking:
                case Go go:
                case Jail jail:
                    return "";
            }
            return ($"COULD NOT GET A STRING FOR {component.GetType().GetShortTypeString()}");
        }

        public string GetPropComponentString(Property prop, bool printOwner = true)
        {
            var sb = new StringBuilder();
            if (printOwner)
            {
                if (prop.OwnerId.HasValue)
                    sb.AppendLine($"Owner: {_context.GetPlayer((int)prop.OwnerId).DisplayName}");
                else
                    sb.AppendLine("No owner");
            }
            sb.AppendLine($"SetId: {prop.SetId}");
            sb.AppendLine($"BasePrice: {prop.BasePrice}");
            sb.Append($"IsMortgaged: {prop.IsMortgaged}");

            return sb.ToString();
        }

        public string GetDevComponentString(PropertyDevelopment dev, bool writeRentList = true)
        {
            var outStr = $"Houses built: {dev.HousesBuilt} out of {dev.HouseCap}\n" +
                $"House Buy/Sell price: {dev.HouseBuyPrice}/{dev.HouseSellPrice}";

            if (writeRentList)
            {
                string rentList = $"Base: {dev.RentList[0]}\n" +
                    $"Full set: {dev.RentList[1]}";
                for (int i = 2; i < dev.RentList.Count; i++)
                    rentList += $"\n{i - 1} houses: {dev.RentList[i]}";

                outStr += "\n" + rentList.AddTwoSpacesAtNewLine();
            }

            return outStr;
        }
        #endregion

        #region Property
        public string GetPropertyString(int tileId, bool printOwner = true)
        {
            var prop = GetPropComponentString(_context.GetTileComponent<Property>(tileId), printOwner);
            var dev = GetTileComponentString(_context.GetTileComponent<PropertyDevelopment>(tileId));
            var station = GetTileComponentString(_context.GetTileComponent<TrainStation>(tileId));
            var utility = GetTileComponentString(_context.GetTileComponent<UtilityProperty>(tileId));

            var sb = new StringBuilder();
            sb.AppendLine(prop.AddTwoSpacesAtNewLine());

            if (dev != "")
                sb.AppendLine(dev.AddTwoSpacesAtNewLine());
            if (station != "")
                sb.AppendLine(station.AddTwoSpacesAtNewLine());
            if (utility != "")
                sb.AppendLine(utility.AddTwoSpacesAtNewLine());

            return sb.ToString();
        }
        #endregion

        #region misc
        public string GetStringOfListOfItems<T>(List<T> list, Func<T, string> valToString, bool indexate)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (indexate)
                    sb.Append($"{i + 1}: ");
                sb.AppendLine(valToString(list[i]).Trim());
            }
            return sb.ToString().Trim();
        }
        #endregion

        #region ctor
        public FormatOutput(Context context) =>
            _context = context;
        #endregion
    }
}
