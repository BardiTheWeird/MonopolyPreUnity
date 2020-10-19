using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MonopolyPreUnity.UserScenario
{
    class HotSeatUserScenario : IUserScenario
    {
        #region Dependencies
        public TileManager TileManager { get; private set; }
        #endregion
        private TInput ChooseFromList<TInput>(List<TInput> list, string message, Func<object, HotSeatUserScenario, string> inputToString)
        {
            Console.WriteLine(message);
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write($"{i + 1}: ");
                Console.WriteLine(inputToString(list[i], this));
            }
            int choice;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out choice))
                    break;
                Console.WriteLine("Invalid input. Try again");
            }
            return list[choice];
        }

        /*private TInput InputManually<TInput>(string message)
        {

        }*/

        static string CommandToString(object obj, HotSeatUserScenario scenario = null) =>
            ((MonopolyCommand)obj).ToString("g");

        static string PropertyIdToString(object obj, HotSeatUserScenario scenario)
        {
            var id = (int)obj;
            var tileManager = scenario.TileManager;
            var identity = tileManager.GetTileComponent<TileIdentityComponent>(id);
            var property = tileManager.GetTileComponent<PropertyComponent>(id);
            var development = tileManager.GetTileComponent<PropertyDevelopmentComponent>(id);

            var str = $"Name: {identity.Name}\n" +
                $"\tMortgaged: {property.IsMortgaged}\n" +
                $"\tBasePrice: {property.BasePrice}\n";
            if (development == null)
                str += $"\tIsUtility: true";
            else
                str += $"\tHouses: {development.HousesBuilt}";

            return str;
        }

        static Dictionary<MonopolyRequest, (string, Func<object, HotSeatUserScenario, string>)> RequestDict = 
            new Dictionary<MonopolyRequest, (string, Func<object, HotSeatUserScenario, string>)> 
            {
                { MonopolyRequest.TurnCommandChoice,            ("Choose what to do", CommandToString) },
                { MonopolyRequest.PropertyManagePropertyChoice, ("Choose a property to manage", PropertyIdToString) },
                { MonopolyRequest.PropertyManageActionChoice,   ("Choose what to do with this property", CommandToString) },
                { MonopolyRequest.MoveJailChoice,               ("Choose whether to pay fine or throw dice", CommandToString) },
                { MonopolyRequest.TileLandedPropertyChoice,     ("Choose what to do with this property", CommandToString) },
                { MonopolyRequest.AuctionBidChoice,             ("Choose how much to bid", null) },
                { MonopolyRequest.TradePlayerChoice,            ("Choose who to trade with", null) },
                { MonopolyRequest.TradeDealChoice,              ("Make a trade deal", null) },
                { MonopolyRequest.TradeValidationChoice,        ("Choose whether to accept this trade", CommandToString) }
            };

        public TInput HandleRequest<TInput>(Request<TInput> request)
        {
            TInput input = default;
            string message;

            /*
            switch (request.RequestType)
            {
                case MonopolyRequest.TurnCommandChoice:
                    input = ChooseFromList(request.Choices);
                    break;

                case MonopolyRequest.PropertyManagePropertyChoice:
                    input = ChooseFromList(request.Choices);
                    break;
                case MonopolyRequest.PropertyManageActionChoice:
                    break;

                case MonopolyRequest.MoveJailChoice:
                    break;

                case MonopolyRequest.TileLandedPropertyChoice:
                    break;
                case MonopolyRequest.AuctionBidChoice:
                    break;

                case MonopolyRequest.TradePlayerChoice:
                    break;
                case MonopolyRequest.TradeDealChoice:
                    break;
                case MonopolyRequest.TradeValidationChoice:
                    break;
            }
            */

            if (request.Choices != null)
            {
                input = ChooseFromList(request.Choices, 
                    RequestDict[request.RequestType].Item1, 
                    RequestDict[request.RequestType].Item2);
            }
            else
            {
                Console.WriteLine("Manual input is not yet implemented");
                input = default;
                //input = InputManually<TInput>(null);
            }
            return input;
        }
    }
}
