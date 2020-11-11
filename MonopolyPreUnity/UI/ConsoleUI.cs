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
        #region Cash
        //public void PrintCashChargeMessage(int payerId, int? chargerId, int amount, string message = null)
        //{
        //    var payer = _playerManager.GetPlayer(payerId);
        //    var charger = chargerId == null ? null : _playerManager.GetPlayer((int)chargerId);
        //    var chargerName = charger == null ? "bank" : charger.DisplayName;

        //    var sb = new StringBuilder();
        //    sb.Append($"{payer.DisplayName} paid {amount}$ to {chargerName}");
        //    if (message != null)
        //        sb.Append(" " + message);

        //    sb.Append($"\n  Current balance: " +
        //        $"\n    Payer: {payer.Cash}$");

        //    if (charger != null)
        //        sb.Append($"; Charger: {charger.Cash}$");

        //    _context.Add(new PrintLine((sb);
        //}

        //public void PrintCashGiveMessage(int receiverId, int amount, string message = null)
        //{
        //    var receiver = _playerManager.GetPlayer(receiverId);

        //    var sb = new StringBuilder();
        //    sb.Append($"{receiver.DisplayName} received {amount}$.");
        //    sb.Append($"\n  Current balance: {receiver.Cash}$");

        //    if (message != null)
        //        sb.Append("\n  Message: " + message);

        //    _context.Add(new PrintLine((sb);
        //}
        #endregion

        #region Player
        //public void PrintPlayer(int playerId) =>
        //    Print(GetPlayerString(playerId));

        //public void PrintPlayers(IEnumerable<int> ids) =>
        //    Print(GetStringOfListOfItems(ids.ToList(), GetPlayerString, true));

        //public string GetPlayerString(int playerId)
        //{
        //    var player = _playerManager.GetPlayer(playerId);
        //    var sb = new StringBuilder();

        //    sb.Append($"Name: {player.DisplayName}\n");
        //    sb.Append(GetPlayerStateString(player).AddTwoSpacesAtNewLine());

        //    return sb.ToString().Trim();
        //}

        //public string GetPlayerStateString(Player player, bool addCurrentTile = false, bool addInJail = true, bool addProperties = true)
        //{
        //    var sb = new StringBuilder();
        //    sb.AppendLine($"Cash: {player.Cash}");
        //    sb.AppendLine($"JailCards: {player.JailCards}");
        //    if (addCurrentTile)
        //        sb.AppendLine(FormattedString($"Currently at: |tile:{player.CurrentTileId}|"));
        //    if (addInJail) 
        //    {
        //        var str = player.TurnsInJail == null ? "Not in jail" : "In jail"; 
        //        sb.AppendLine(str);
        //    }
        //    if (addProperties)
        //    {
        //        sb.AppendLine("Properties:");
        //        foreach (var prop in player.Properties)
        //        {
        //            sb.AppendLine(GetPropertyString(prop).AddTwoSpacesAtNewLine());
        //        }
        //    }
        //    return sb.ToString().Trim();
        //}

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
