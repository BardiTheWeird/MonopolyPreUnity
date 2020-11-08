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
    static class ConsoleUIExtensions
    {
        public static string SplitByCapitalLetter(this string str) =>
            Regex.Replace(str, @"[A-Z]", match => " " + match.Value).Trim();

        public static string Tabulize(this string str) =>
            "\t" + Regex.Replace(str, @"\n", "\n\t");

        public static string AddTwoSpacesAtNewLine(this string str) =>
            "  " + Regex.Replace(str, @"\n", "\n  ");

        public static T GetComponent<T>(this List<ITileComponent> components) where T : ITileComponent =>
            (T)components.FirstOrDefault(x => x.GetType() == typeof(T));

        public static bool NotIdentityOrProperty(this ITileComponent component)
        {
            var type = component.GetType();
            var check =
                !( type == typeof(TileIdentityComponent)
                || type == typeof(PropertyComponent)
                || type == typeof(PropertyDevelopmentComponent)
                || type == typeof(TrainStationComponent)
                || type == typeof(UtilityComponent));
            return check;
        }
    }

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
                    Console.WriteLine("Invalid input. Try again");
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
                Console.WriteLine(errorMessage + ". Try again");
            }
        }

        public T InputValue<T>(IEnumerable<T> possibleValues) where T : IConvertible =>
            InputValue<T>(x => possibleValues.Contains(x), "Value not present");

        public int InputValueIndex<T>(IEnumerable<T> values) =>
            InputValue<int>(x => 1 <= x && x <= values.Count(), "Index out of range");
        #endregion

        #region Output
        public void Print(string message) =>
            Console.WriteLine(message);

        public void PrintCommands(List<MonopolyCommand> commands)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {CommandName(commands[i])}");
            }
        }

        public void PrintProperties(IEnumerable<int> propertyIds)
        {
            propertyIds.OrderBy(x => x);
            foreach (var id in propertyIds)
                Console.WriteLine($"{id}: {GetPropertyTileString(id)}");
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

            Console.WriteLine(sb);
        }

        public void PrintCashGiveMessage(int receiverId, int amount, string message = null)
        {
            var receiver = _playerManager.GetPlayer(receiverId);

            var sb = new StringBuilder();
            sb.Append($"{receiver.DisplayName} received {amount}$.");
            sb.Append($"\n  Current balance: {receiver.Cash}$");

            if (message != null)
                sb.Append("\n  Message: " + message);

            Console.WriteLine(sb);
        }
        #endregion

        #region Actions
        public void PrintAction<T>(T action, string preface = "") where T : IMonopolyAction
        {
            var name = GetTypeString(action.GetType());
            var desc = GetActionDescription(action);

            if (preface.Length > 0)
                preface += " ";

            Console.WriteLine(preface + name);
            Console.WriteLine(desc.AddTwoSpacesAtNewLine());
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
        public void PrintFormatted(string message)
        {
            Console.WriteLine(FormattedString(message));
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
                        return _tileManager.GetTileComponent<TileIdentityComponent>(id).Name;
                }
                return match.Value;
            });
        }
        #endregion

        #region Tile
        public void PrintTileLanded(int tileId, int playerId)
        {
            var landedString = FormattedString($"|player:{playerId}| landed on ");
            var tileString = TileString(tileId);

            Console.WriteLine(landedString + tileString.Trim());
            //Console.WriteLine(tileString);
        }

        public string TileString(int tileId)
        {
            var sb = new StringBuilder();

            var identity = _tileManager.GetTileComponent<TileIdentityComponent>(tileId);
            sb.Append(identity.Name.Trim());

            if (_tileManager.ContainsComponent<PropertyComponent>(tileId))
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

        #region Specific
        public MonopolyCommand ChooseCommand(List<MonopolyCommand> commands)
        {
            Console.WriteLine("Choose command:");
            PrintCommands(commands);
            return commands[InputValueIndex(commands) - 1];
        }

        public int? ChoosePropertyId(IEnumerable<int> properties)
        {
            Print("Choose a property:");
            PrintProperties(properties);

            var cancelInt = properties.Count() > 0 ? Math.Min(-1, properties.Min() - 1) : -1;
            properties = properties.Concat(new[] { cancelInt });

            Print($"Enter {cancelInt} to cancel");

            int? input = InputValue(properties);
            return input == cancelInt ? null : input;
        }
        #endregion

        #region Misc
        public string CommandName(MonopolyCommand command)
        {
            var commandName = command.ToString();
            var lastDotIndex = commandName.LastIndexOf('.');
            return commandName.Substring(lastDotIndex == -1 ? 0 : lastDotIndex).SplitByCapitalLetter();
        }

        public string GetPropertyTileString(int propertyId)
        {
            var name = _tileManager.GetTileComponent<TileIdentityComponent>(propertyId).Name;
            return $"{ name}: " + GetPropertyString(propertyId);
        }

        public string GetPropertyString(int tileId)
        {
            var prop = GetTileComponentString(_tileManager.GetTileComponent<PropertyComponent>(tileId));
            var dev = GetTileComponentString(_tileManager.GetTileComponent<PropertyDevelopmentComponent>(tileId));
            var station = GetTileComponentString(_tileManager.GetTileComponent<TrainStationComponent>(tileId));
            var utility = GetTileComponentString(_tileManager.GetTileComponent<UtilityComponent>(tileId));

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
                case PropertyComponent prop:
                    return GetPropComponentString(prop);
                case PropertyDevelopmentComponent dev:
                    return GetDevComponentString(dev);
                case TrainStationComponent train:
                    return $"Base station rent: {train.BaseRent}";
                case UtilityComponent utility:
                    return "";

                case ActionBoxComponent actionBox:
                case ActionTileComponent actionTile:
                case FreeParkingComponent freeParking:
                case GoComponent go:
                case JailComponent jail:
                    return "";
            }
            return($"COULD NOT GET A STRING FOR {GetTypeString(component.GetType())}");
        }

        public string GetPropComponentString(PropertyComponent prop)
        {
            string ownerName = prop.OwnerId == null ? "No owner" : _playerManager.GetPlayer((int)prop.OwnerId).DisplayName;
            return $"Owner: {ownerName}\n" +
                $"SetId: {prop.SetId}\n" +
                $"BasePrice: {prop.BasePrice}\n" +
                $"IsMortgaged: {prop.IsMortgaged}";
        }

        public string GetDevComponentString(PropertyDevelopmentComponent dev, bool writeRentList = true)
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

        #region ctor
        public ConsoleUI(TileManager tileManager, PlayerManager playerManager)
        {
            _tileManager = tileManager;
            _playerManager = playerManager;
        }
        #endregion
    }
}
