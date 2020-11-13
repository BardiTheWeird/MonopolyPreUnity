using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.Cash;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Entity.ContextExtensions
{
    static class ContextPropertyExtensions
    {
        #region Property sets
        public static HashSet<int> GetPropertySet(this Context context, int setId) =>
            context
            .GetEntities<Tile>()
            .GetEntities<Property>()
            .Select(e => e.GetComponent<Property>().SetId)
            .Where(id => id == setId)
            .ToHashSet();

        public static bool IsPropertySetOwned(this Context context, Player player, int setId)
        {
            HashSet<int> set = context.GetPropertySet(setId);
            var intersection = player.Properties.Intersect(set);
            if (set.Count == intersection.Count())
                return true;
            return false;
        }

        public static HashSet<int> OwnedPropertiesInSet(this Context context, Player player, int setId) =>
            player.Properties
            .Intersect(context.GetPropertySet(setId))
            .ToHashSet();
        #endregion

        #region available actions
        public static bool CanMortgage(this Context context, Player player, int propId)
        {
            var prop = context.GetTileComponent<Property>(propId);

            if (prop == null)
                return false;
            if (prop.IsMortgaged)
                return false;

            // if houses built
            var dev = context.GetTileComponent<PropertyDevelopment>(propId);
            if (dev != null && dev.HousesBuilt > 0)
                return false;

            return true;
        }

        public static bool CanUnmortgage(this Context context, Player player, int propId)
        {
            var prop = context.GetTileComponent<Property>(propId);

            if (prop == null)
                return false;
            if (!prop.IsMortgaged)
                return false;

            // if player has enough money to unmortgage
            var config = context.GameConfig();
            var unmortgagePrice = (int)(prop.BasePrice * config.MortgageFee * (1 + config.MortgageCommission));
            if (player.Cash < unmortgagePrice)
                return false;

            return true;
        }

        public static bool CanDoHouseOperations(this Context context, Player player, int propId)
        {
            var prop = context.GetTileComponent<Property>(propId);
            var dev = context.GetTileComponent<PropertyDevelopment>(propId);

            // if there is a place where you can build, lol
            if (prop == null || dev == null)
                return false;
            // if it isn't mortgaged
            if (prop.IsMortgaged)
                return false;
            // if player owns a full set
            if (!context.IsPropertySetOwned(player, prop.SetId))
                return false;
            return true;
        }

        public static bool CanBuildHouse(this Context context, Player player, int propId)
        {
            if (!context.CanDoHouseOperations(player, propId))
                return false;

            // if maximum difference between two HousesBuilt after building a house is <= 1
            var dev = context.GetTileComponent<PropertyDevelopment>(propId);
            var minHousesInSet = context.GetPropertySet(propId)
                .Select(id => context.GetTileComponent<PropertyDevelopment>(id))
                .Min(dev => dev.HousesBuilt);

            if (dev.HousesBuilt > minHousesInSet && dev.HousesBuilt == dev.HouseCap)
                return false;
            return true;
        }

        public static bool CanSellHouse(this Context context, Player player, int propId)
        {
            if (!context.CanDoHouseOperations(player, propId))
                return false;

            // if maximum difference between two HousesBuilt after building a house is <= 1
            var devHouses = context.GetTileComponent<PropertyDevelopment>(propId).HousesBuilt;
            var maxHousesInSet = context.GetPropertySet(propId)
                .Select(id => context.GetTileComponent<PropertyDevelopment>(id))
                .Max(dev => dev.HousesBuilt);

            if (devHouses < maxHousesInSet || devHouses == 0)
                return false;
            return true;
        }

        public static List<MonopolyCommand> GetAvailablePropertyActions(this Context context, Player player, int propId)
        {
            var AvailableActions = new List<MonopolyCommand>();
            if (context.CanMortgage(player, propId))
                AvailableActions.Add(MonopolyCommand.MortgageProperty);
            if (context.CanUnmortgage(player, propId))
                AvailableActions.Add(MonopolyCommand.UnmortgageProperty);
            if (context.CanBuildHouse(player, propId))
                AvailableActions.Add(MonopolyCommand.BuyHouse);
            if (context.CanSellHouse(player, propId))
                AvailableActions.Add(MonopolyCommand.SellHouse);

            return AvailableActions;
        }
        #endregion

        #region actions
        public static void Mortgage(this Context context, Player player, int propId)
        {
            var prop = context.GetTileComponent<Property>(propId);

            prop.IsMortgaged = true;
            var mortgageMoney = (int)(prop.BasePrice * context.GameConfig().MortgageCommission);

            context.Add(new PrintFormattedLine($"|player:{player.Id}| mortgaged |tile:{propId}|", OutputStream.GameLog));
            context.Add(new GiveCash(mortgageMoney, player.Id, "mortgaging property"));
        }

        public static void UnMortgage(this Context context, Player player, int propId)
        {
            var prop = context.GetTileComponent<Property>(propId);
            var config = context.GameConfig();

            prop.IsMortgaged = false;
            var unmortgagePrice = (int)(prop.BasePrice * config.MortgageFee * (1 + config.MortgageCommission));

            context.Add(new PrintFormattedLine($"|player:{player.Id}| unmortgaged |tile:{propId}|", OutputStream.GameLog));
            context.Add(new ChargeCash(unmortgagePrice, player.Id, message: "unmortgaging property"));
        }

        public static void BuyHouse(this Context context, Player player, int propId)
        {
            var dev = context.GetTileComponent<PropertyDevelopment>(propId);
            dev.HousesBuilt++;

            context.Add(new PrintFormattedLine($"|player:{player.Id}| built a house on |tile:{propId}|. Number of houses: {dev.HousesBuilt}",
                OutputStream.GameLog));
            context.Add(new ChargeCash(dev.HouseBuyPrice, player.Id, message: "building a house"));
        }

        public static void SellHouse(this Context context, Player player, int propId)
        {
            var dev = context.GetTileComponent<PropertyDevelopment>(propId);
            dev.HousesBuilt--;

            context.Add(new PrintFormattedLine($"|player:{player.Id}| sold a house on |tile:{propId}|. Number of houses: {dev.HousesBuilt}",
                OutputStream.GameLog));
            context.Add(new GiveCash(dev.HouseSellPrice, player.Id, message: "selling a house"));
        }
        #endregion
    }
}
