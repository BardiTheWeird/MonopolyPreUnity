﻿using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class PlayerManager
    {
        // ID to IPlayer and IUserScenario
        private readonly Dictionary<int, (Player, IUserScenario)> playerDict;

        public Player GetPlayer(int playerId)
        {
            if (playerDict.ContainsKey(playerId))
                return playerDict[playerId].Item1;
            throw new KeyNotFoundException($"No such Player Id ({playerId}) in PlayerDict");
        }

        public List<int> GetAllPlayerId() =>
            new List<int>(playerDict.Keys);

        public IUserScenario GetUserScenario(int playerId)
        {
            if (playerDict.ContainsKey(playerId))
                return playerDict[playerId].Item2;
            throw new KeyNotFoundException($"No such Player Id ({playerId}) in PlayerDict");
        }

        public int GetPlayerCash(int playerId) =>
            GetPlayer(playerId).Cash;

        public void ChangeBalance(int playerId, int amount)
        {
            if (amount < 0)
                PlayerCashCharge(playerId, Math.Abs(amount));
            else
                PlayerCashGive(playerId, amount);
        }

        public void PlayerCashCharge(int playerId, int amount)
        {
            var player = GetPlayer(playerId);
            if (player.Cash >= amount)
            {
                player.Cash -= amount;
            }
            else
            {
                if (IsBankrupt(player))
                {
                    throw new NotImplementedException();
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        public void PlayerCashGive(int playerId, int amount)
        {
            GetPlayer(playerId).Cash += amount;
        }

        bool IsBankrupt(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
