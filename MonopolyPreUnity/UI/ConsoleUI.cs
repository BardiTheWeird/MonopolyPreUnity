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

    //    public static T GetComponent<T>(this List<IEntityComponent> components) where T : IEntityComponent =>
    //        (T)components.FirstOrDefault(x => x.GetType() == typeof(T));

    //    public static bool NotIdentityOrProperty(this IEntityComponent component)
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
        #region PlayerAsset
        //public string GetPlayerAssetsString(PlayerAssets assets)
        //{
        //    var sb = new StringBuilder();

        //    sb.AppendLine(FormattedString($"Player name: |player:{assets.PlayerId}|"));

        //    sb.AppendLine($"Cash: {assets.Cash}".AddTwoSpacesAtNewLine());
        //    if (assets.JailCards > 0) 
        //        sb.AppendLine($"Jail cards: {assets.JailCards}".AddTwoSpacesAtNewLine());
        //    if (assets.Properties.Count > 0)
        //    {
        //        var properties = GetStringOfListOfItems(assets.Properties, GetPropertyString, false);

        //        sb.AppendLine("Properties:".AddTwoSpacesAtNewLine());
        //        sb.Append(properties.AddTwoSpacesAtNewLine().AddTwoSpacesAtNewLine());
        //    }

        //    return sb.ToString().Trim();
        //}
        #endregion

        #region Trade
        //public void PrintTradeOffer(TradeOffer offer)
        //{
        //    Print("Initiator assets");
        //    Print(GetPlayerAssetsString(offer.InitiatorAssets));

        //    Print("Receiver assets");
        //    Print(GetPlayerAssetsString(offer.ReceiverAssets));
        //}
        #endregion

        #region Properties
        //public string GetPropertyListString(List<int> propertyIds)
        //{
        //    var sb = new StringBuilder();
        //    for (int i = 0; i < propertyIds.Count; i++)
        //        sb.Append($"{i}: {GetPropertyTileString(propertyIds[i])}");
        //    return sb.ToString();
        //}

        //public string GetPropertyTileString(int propertyId)
        //{
        //    var name = _tileManager.GetTileComponent<Tile>(propertyId).Name;
        //    return $"{name}: " + GetPropertyString(propertyId);
        //}

        //public string GetPropertyString(int tileId)
        //{
        //    var prop = GetTileComponentString(_tileManager.GetTileComponent<Property>(tileId));
        //    var dev = GetTileComponentString(_tileManager.GetTileComponent<PropertyDevelopment>(tileId));
        //    var station = GetTileComponentString(_tileManager.GetTileComponent<TrainStation>(tileId));
        //    var utility = GetTileComponentString(_tileManager.GetTileComponent<UtilityProperty>(tileId));

        //    Func<string, string> newLineDoubleSpace = x => "\n" + x.AddTwoSpacesAtNewLine();

        //    var sb = new StringBuilder();
        //    sb.Append(newLineDoubleSpace(prop));

        //    if (dev != "")
        //        sb.Append(newLineDoubleSpace(dev));
        //    if (station != "")
        //        sb.Append(newLineDoubleSpace(station));
        //    if (utility != "")
        //        sb.Append(newLineDoubleSpace(utility));

        //    return sb.ToString();
        //}
        #endregion
    }
}
