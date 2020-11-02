using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.HotSeatScenario
{
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
            InputValue<T>(x => possibleValues.Contains(x), "Value not in range");

        public int InputValueIndex<T>(IEnumerable<T> values) =>
            InputValue<int>(x => 0 <= x && x < values.Count(), "Index out of range");
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
                Console.WriteLine($"{id}, {GetPropertyTileString(id)}");
        }
        #endregion

        #region Specific
        public MonopolyCommand ChooseCommand(List<MonopolyCommand> commands)
        {
            Console.WriteLine("Choose command:");
            PrintCommands(commands);
            return commands[InputValueIndex(commands)];
        }

        public int? ChoosePropertyId(IEnumerable<int> properties)
        {
            PrintProperties(properties);

            var cancelInt = Math.Min(-1, properties.Min() - 1);
            properties.Append(cancelInt);
            int? input = InputValue(properties);

            return input == cancelInt ? null : input;
        }
        #endregion

        #region Misc
        public string CommandName(MonopolyCommand command)
        {
            var commandName = command.ToString();
            var lastDotIndex = commandName.LastIndexOf('.');
            return commandName.Substring(lastDotIndex == -1 ? 0 : lastDotIndex);
        }

        public string GetPropertyTileString(int propertyId)
        {
            var name = _tileManager.GetTileComponent<TileIdentityComponent>(propertyId).Name;

            var prop = GetTileComponentString(_tileManager.GetTileComponent<PropertyComponent>(propertyId));
            var dev = GetTileComponentString(_tileManager.GetTileComponent<PropertyDevelopmentComponent>(propertyId));
            var station = GetTileComponentString(_tileManager.GetTileComponent<TrainStationComponent>(propertyId));
            var utility = GetTileComponentString(_tileManager.GetTileComponent<UtilityComponent>(propertyId));

            Func<string, string> newlineTabulize = x => "\n" + Tabulize(x);

            var sb = new StringBuilder();
            sb.Append(name + ":");
            sb.Append(newlineTabulize(prop));

            if (dev != "")
                sb.Append(newlineTabulize(dev));
            if (station != "")
                sb.Append(newlineTabulize(station));
            if (utility != "")
                sb.Append(newlineTabulize(utility));

            return sb.ToString();
        }

        public string Tabulize(string str) =>
            "\t" + str.Select(x => x == '\n' ? "\t\n" : x.ToString());
        #endregion

        #region TileComponent Strings
        public string GetTileComponentString(ITileComponent component)
        {
            if (component == null)
                return "";

            switch (component)
            {
                case PropertyComponent prop:
                    return GetPropString(prop);
                case PropertyDevelopmentComponent dev:
                    return GetDevString(dev);
                case TrainStationComponent train:
                    return $"Base station rent: {train.BaseRent}";
                case UtilityComponent utility:
                    return "";
            }
            throw new NotImplementedException($"Method isn't implemented for {component.GetType()} yet");
        }

        public string GetPropString(PropertyComponent prop)
        {
            string ownerName = prop.OwnerId == null ? "No owner" : _playerManager.GetPlayer((int)prop.OwnerId).DisplayName;
            return $"Owner: {ownerName}\n" +
                $"SetId: {prop.SetId}\n" +
                $"BasePrice: {prop.BasePrice}\n" +
                $"IsMortgaged: {prop.IsMortgaged}";
        }

        public string GetDevString(PropertyDevelopmentComponent dev, bool writeRentList = false)
        {
            var outStr = $"Houses built: {dev.HousesBuilt} out of {dev.HouseCap}\n" +
                $"House Buy/Sell price: {dev.HouseBuyPrice}/{dev.HouseSellPrice}";

            if (writeRentList) 
            {
                string rentList = $"Base: {dev.RentList[0]}\n" +
                    $"Full set: {dev.RentList[1]}";
                for (int i = 2; i < dev.RentList.Count; i++)
                    rentList += $"\n{i - 1} houses: {dev.RentList[i]}";

                outStr += "\n" + rentList;
            }

            return outStr;
        }
        #endregion
    }
}
