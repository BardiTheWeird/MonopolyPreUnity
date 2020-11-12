using MonopolyPreUnity.Components;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Entity.ContextExtensions
{
    static class ContextPlayerExtensions
    {
        #region get player(s)
        public static Player GetPlayer(this Context context, int id) =>
            context.GetComponent<Player>(p => p.Id == id);

        public static List<Player> GetPlayers(this Context context, Func<Player, bool> predicate) =>
            context.GetAllPlayers().Where(predicate).ToList();

        public static List<Player> GetAllPlayers(this Context context) =>
            context.GetComponents<Player>();

        #endregion

        #region available commands
        public static List<MonopolyCommand> GetAvailableTurnCommands(this Context context, Player player)
        {
            var commandList = new List<MonopolyCommand>();
            if (player.TurnsInJail == null)
            {
                if (player.CanMove)
                    commandList.Add(MonopolyCommand.MakeMove);
                else
                    commandList.Add(MonopolyCommand.EndTurn);
            }
            else if (!player.RolledJailDiceThisTurn)
                commandList.AddRange(context.GetAvailableJailCommands(player));
            else
                commandList.Add(MonopolyCommand.EndTurn);

            return commandList;
        }

        public static List<MonopolyCommand> GetAvailableJailCommands(this Context context, Player player)
        {
            var config = context.GameConfig();

            var hasJailCard = player.JailCards > 0;
            var canRoll = !player.RolledJailDiceThisTurn && player.TurnsInJail < config.MaxTurnsInJail;
            var canPay = player.Cash >= config.JailFine;

            var commands = new List<MonopolyCommand>();
            if (hasJailCard) commands.Add(MonopolyCommand.UseJailCard);
            if (canRoll) commands.Add(MonopolyCommand.JailRollDice);
            if (canPay || commands.Count == 0) commands.Add(MonopolyCommand.PayJailFine);

            return commands;
        }

        public static List<MonopolyCommand> GetBuyAuctionCommands(this Context context, Player player, int propId)
        {
            var property = context.GetTileComponent<Property>(propId);

            var availableActions = new List<MonopolyCommand> { MonopolyCommand.AuctionProperty };
            if (player.Cash >= property.BasePrice)
                availableActions.Add(MonopolyCommand.BuyProperty);

            return availableActions;
        }
        #endregion
    }
}
