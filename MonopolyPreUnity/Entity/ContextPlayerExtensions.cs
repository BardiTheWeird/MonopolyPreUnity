﻿using MonopolyPreUnity.Components;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Entity
{
    static class ContextPlayerExtensions
    {
        public static Player GetPlayer(this Context context, int id) =>
            context.GetComponent<Player>(p => p.Id == id);

        public static int GetPlayerCash(this Context context, int id) =>
            context.GetComponent<Player>(p => p.Id == id).Cash;

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
        #endregion
    }
}
