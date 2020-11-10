using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace MonopolyPreUnity.UI
{
    #region extensions
    //static class ConsoleUIExtensions
    //{
    //    public static string SplitByCapitalLetter(this string str) =>
    //        Regex.Replace(str, @"[A-Z]", match => " " + match.Value).Trim();

    //    public static string Tabulize(this string str) =>
    //        "\t" + Regex.Replace(str, @"\n", "\n\t");

    //    public static string AddTwoSpacesAtNewLine(this string str) =>
    //        "  " + Regex.Replace(str, @"\n", "\n  ");

    //    public static T GetComponent<T>(this List<ITileComponent> components) where T : ITileComponent =>
    //        (T)components.FirstOrDefault(x => x.GetType() == typeof(T));

    //    public static bool NotIdentityOrProperty(this ITileComponent component)
    //    {
    //        var type = component.GetType();
    //        var check =
    //            !( type == typeof(Tile)
    //            || type == typeof(Property)
    //            || type == typeof(PropertyDevelopment)
    //            || type == typeof(TrainStation)
    //            || type == typeof(UtilityProperty));
    //        return check;
    //    }
    //}
    #endregion

    class ConsoleUI
    {
        #region Dependencies
        private readonly TileManager _tileManager;
        private readonly PlayerManager _playerManager;
        #endregion

        #region Input
        public T InputValue<T>() where T : IConvertible
        {
            while (true)
            {
                try
                {
                    var input = Console.ReadLine();
                    return (T)Convert.ChangeType(input, typeof(T));
                }
                catch
                {
                    _context.Add(new PrintLine(("Invalid input. Try again");
                }
            }
        }

        public T InputValue<T>(Func<T, bool> pred, string errorMessage) where T : IConvertible
        {
            while (true)
            {
                var val = InputValue<T>();
                if (pred(val))
                    return val;
                _context.Add(new PrintLine((errorMessage + ". Try again");
            }
        }

        public int InputValueIndex<T>(IEnumerable<T> values, bool canCancel = false)
        {
            var value = InputValue<int>(x => (1 <= x && x <= values.Count()) || (canCancel && x == -1), "Index out of range");
            return value == -1 ? -1 : value - 1;
        }
        #endregion

        #region Specific Input
        public MonopolyCommand ChooseCommand(List<MonopolyCommand> commands)
        {
            _context.Add(new PrintLine(("Choose command:");
            PrintCommands(commands);
            return commands[InputValueIndex(commands)];
        }

        public int? ChoosePropertyId(IEnumerable<int> properties)
        {
            var propList = properties.ToList();
            Print("Choose a property:");
            Print(GetStringOfListOfItems(propList, GetPropertyString, true));

            Print($"Enter -1 to cancel");

            int? input = InputValueIndex(properties, true);
            return input == -1 ? null : (int?)propList[(int)input];
        }

        public List<int> ChoosePropertyIdMultiple(IEnumerable<int> properties)
        {
            var propList = properties.ToList();
            var outPropList = new List<int>();
            while (true)
            {
                var curProp = ChoosePropertyId(propList);
                if (curProp == null)
                    break;

                outPropList.Add((int)curProp);
                propList.Remove((int)curProp);
            }
            return outPropList;
        }

        public PlayerAssets ChoosePlayerAssets(int playerId, IEnumerable<int> availableProperties)
        {
            var player = _playerManager.GetPlayer(playerId);
            var assets = new PlayerAssets();
            assets.PlayerId = playerId;

            if (availableProperties.Count() > 0)
            {
                Print("Choose properties to trade");
                assets.Properties = ChoosePropertyIdMultiple(availableProperties);
            }

            Print($"Write an amount of cash you wish to trade (between 0 and {player.Cash})");
            assets.Cash = InputValue<int>(x => 0 <= x && x <= player.Cash, "Input not in available range");

            if (player.JailCards > 0)
            {
                Print($"Write an amount of jail cards you wish to trade (between 0 and {player.JailCards})");
                assets.JailCards = InputValue<int>(x => 0 <= x && x <= player.JailCards, "Input not in available range");
            }

            return assets;
        }
        #endregion

        #region Print
        public void Print(string message) =>
            _context.Add(new PrintLine((message);

        public void PrintFormatted(string message)
        {
            _context.Add(new PrintLine((FormattedString(message));
        }

        public string FormattedString(string str)
        {
            return Regex.Replace(str, @"\|(\w+):(\d+)\|", match =>
            {
                var formatVal = match.Groups[1].Value;
                var id = Convert.ToInt32(match.Groups[2].Value);
                switch (formatVal)
                {
                    case "player":
                        return _playerManager.GetPlayer(id).DisplayName;
                    case "tile":
                        return _tileManager.GetTileComponent<Tile>(id).Name;
                }
                return match.Value;
            });
        }
        #endregion

        #region Cash
        public void PrintCashChargeMessage(int payerId, int? chargerId, int amount, string message = null)
        {
            var payer = _playerManager.GetPlayer(payerId);
            var charger = chargerId == null ? null : _playerManager.GetPlayer((int)chargerId);
            var chargerName = charger == null ? "bank" : charger.DisplayName;

            var sb = new StringBuilder();
            sb.Append($"{payer.DisplayName} paid {amount}$ to {chargerName}");
            if (message != null)
                sb.Append(" " + message);

            sb.Append($"\n  Current balance: " +
                $"\n    Payer: {payer.Cash}$");

            if (charger != null)
                sb.Append($"; Charger: {charger.Cash}$");

            _context.Add(new PrintLine((sb);
        }

        public void PrintCashGiveMessage(int receiverId, int amount, string message = null)
        {
            var receiver = _playerManager.GetPlayer(receiverId);

            var sb = new StringBuilder();
            sb.Append($"{receiver.DisplayName} received {amount}$.");
            sb.Append($"\n  Current balance: {receiver.Cash}$");

            if (message != null)
                sb.Append("\n  Message: " + message);

            _context.Add(new PrintLine((sb);
        }
        #endregion

        #region Actions
        public void PrintAction<T>(T action, string preface = "") where T : IMonopolyAction
        {
            var name = GetTypeString(action.GetType());
            var desc = GetActionDescription(action);

            if (preface.Length > 0)
                preface += " ";

            _context.Add(new PrintLine((preface + name);
            _context.Add(new PrintLine((desc.AddTwoSpacesAtNewLine());
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
                    var compTypeName = GetTypeString(tileComponent.ComponentType);
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

        #region Player
        public void PrintPlayer(int playerId) =>
            Print(GetPlayerString(playerId));

        public void PrintPlayers(IEnumerable<int> ids) =>
            Print(GetStringOfListOfItems(ids.ToList(), GetPlayerString, true));

        public string GetPlayerString(int playerId)
        {
            var player = _playerManager.GetPlayer(playerId);
            var sb = new StringBuilder();

            sb.Append($"Name: {player.DisplayName}\n");
            sb.Append(GetPlayerStateString(player).AddTwoSpacesAtNewLine());

            return sb.ToString().Trim();
        }

        public string GetPlayerStateString(Player player, bool addCurrentTile = false, bool addInJail = true, bool addProperties = true)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Cash: {player.Cash}");
            sb.AppendLine($"JailCards: {player.JailCards}");
            if (addCurrentTile)
                sb.AppendLine(FormattedString($"Currently at: |tile:{player.CurrentTileId}|"));
            if (addInJail) 
            {
                var str = player.TurnsInJail == null ? "Not in jail" : "In jail"; 
                sb.AppendLine(str);
            }
            if (addProperties)
            {
                sb.AppendLine("Properties:");
                foreach (var prop in player.Properties)
                {
                    sb.AppendLine(GetPropertyString(prop).AddTwoSpacesAtNewLine());
                }
            }
            return sb.ToString().Trim();
        }

        public string GetPlayerAssetsString(PlayerAssets assets)
        {
            var sb = new StringBuilder();

            sb.AppendLine(FormattedString($"Player name: |player:{assets.PlayerId}|"));

            sb.AppendLine($"Cash: {assets.Cash}".AddTwoSpacesAtNewLine());
            if (assets.JailCards > 0) 
                sb.AppendLine($"Jail cards: {assets.JailCards}".AddTwoSpacesAtNewLine());
            if (assets.Properties.Count > 0)
            {
                var properties = GetStringOfListOfItems(assets.Properties, GetPropertyString, false);

                sb.AppendLine("Properties:".AddTwoSpacesAtNewLine());
                sb.Append(properties.AddTwoSpacesAtNewLine().AddTwoSpacesAtNewLine());
            }

            return sb.ToString().Trim();
        }
        #endregion

        #region Trade
        public void PrintTradeOffer(TradeOffer offer)
        {
            Print("Initiator assets");
            Print(GetPlayerAssetsString(offer.InitiatorAssets));

            Print("Receiver assets");
            Print(GetPlayerAssetsString(offer.ReceiverAssets));
        }
        #endregion

        #region Tile
        public void PrintTileLanded(int tileId, int playerId)
        {
            var landedString = FormattedString($"|player:{playerId}| landed on ");
            var tileString = TileString(tileId);

            _context.Add(new PrintLine((landedString + tileString.Trim());
        }

        public string TileString(int tileId)
        {
            var sb = new StringBuilder();

            var identity = _tileManager.GetTileComponent<Tile>(tileId);
            sb.Append(identity.Name.Trim());

            if (_tileManager.ContainsComponent<Property>(tileId))
                sb.Append(GetPropertyString(tileId));

            var components = _tileManager.GetTile(tileId).Components.Where(comp => comp.NotIdentityOrProperty());
            foreach (var comp in components)
            {
                var compString = GetTileComponentString(comp);
                if (compString.Length > 0)
                    sb.Append("\n" + compString);
            }

            return sb.ToString();
        }
        #endregion

        #region TileComponent Strings
        public string GetTypeString(Type type)
        {
            var typeStr = type.ToString();
            typeStr = typeStr.Substring(typeStr.LastIndexOf('.') + 1);
            return typeStr.SplitByCapitalLetter();
        }

        public string GetTileComponentString(ITileComponent component)
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
            return($"COULD NOT GET A STRING FOR {GetTypeString(component.GetType())}");
        }

        public string GetPropComponentString(Property prop)
        {
            string ownerName = prop.OwnerId == null ? "No owner" : _playerManager.GetPlayer((int)prop.OwnerId).DisplayName;
            return $"Owner: {ownerName}\n" +
                $"SetId: {prop.SetId}\n" +
                $"BasePrice: {prop.BasePrice}\n" +
                $"IsMortgaged: {prop.IsMortgaged}";
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

        #region Properties
        public string GetPropertyListString(List<int> propertyIds)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < propertyIds.Count; i++)
                sb.Append($"{i}: {GetPropertyTileString(propertyIds[i])}");
            return sb.ToString();
        }

        public string GetPropertyTileString(int propertyId)
        {
            var name = _tileManager.GetTileComponent<Tile>(propertyId).Name;
            return $"{name}: " + GetPropertyString(propertyId);
        }

        public string GetPropertyString(int tileId)
        {
            var prop = GetTileComponentString(_tileManager.GetTileComponent<Property>(tileId));
            var dev = GetTileComponentString(_tileManager.GetTileComponent<PropertyDevelopment>(tileId));
            var station = GetTileComponentString(_tileManager.GetTileComponent<TrainStation>(tileId));
            var utility = GetTileComponentString(_tileManager.GetTileComponent<UtilityProperty>(tileId));

            Func<string, string> newLineDoubleSpace = x => "\n" + x.AddTwoSpacesAtNewLine();

            var sb = new StringBuilder();
            sb.Append(newLineDoubleSpace(prop));

            if (dev != "")
                sb.Append(newLineDoubleSpace(dev));
            if (station != "")
                sb.Append(newLineDoubleSpace(station));
            if (utility != "")
                sb.Append(newLineDoubleSpace(utility));

            return sb.ToString();
        }
        #endregion

        #region MiscOutput
        public string GetStringOfListOfItems<T>(List<T> list, Func<T, string> valToString, bool indexate)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (indexate)
                    sb.Append($"{i + 1}: ");
                sb.Append(valToString(list[i]).Trim());
            }
            return sb.ToString().Trim();
        }
        #endregion

        #region Commands
        public void PrintCommands(List<MonopolyCommand> commands)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                _context.Add(new PrintLine(($"{i + 1}: {CommandName(commands[i])}");
            }
        }

        public string CommandName(MonopolyCommand command)
        {
            var commandName = command.ToString();
            var lastDotIndex = commandName.LastIndexOf('.');
            return commandName.Substring(lastDotIndex == -1 ? 0 : lastDotIndex).SplitByCapitalLetter();
        }
        #endregion

        #region ctor
        public ConsoleUI(TileManager tileManager, PlayerManager playerManager)
        {
            _tileManager = tileManager;
            _playerManager = playerManager;
        }
        #endregion
    }
}
