using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MonopolyPreUnity.UI
{
    static class FormatOutputExtension
    {
        public static string SplitByCapitalLetter(this string str) =>
            Regex.Replace(str, @"[A-Z]", match => " " + match.Value).Trim();

        public static string Tabulize(this string str) =>
            "\t" + Regex.Replace(str, @"\n", "\n\t");

        public static string AddTwoSpacesAtNewLine(this string str) =>
            "  " + Regex.Replace(str, @"\n", "\n  ");

        public static string GetShortString(this Type type)
        {
            var typeString = type.ToString();
            var lastDotIndex = typeString.LastIndexOf('.');
            return typeString.Substring(lastDotIndex == -1 ? 0 : lastDotIndex + 1);
        }
    }

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

        #region commands
        public string CommandName(MonopolyCommand command)
        {
            var commandName = command.ToString();
            var lastDotIndex = commandName.LastIndexOf('.');
            return commandName.Substring(lastDotIndex == -1 ? 0 : lastDotIndex).SplitByCapitalLetter();
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

        public FormatOutput(Context context)
        {
            _context = context;
        }
    }
}
