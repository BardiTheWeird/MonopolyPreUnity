using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class PlayerManager
    {
        // ID to IPlayer and IUserScenario
        Dictionary<int, (Player, IUserScenario)> PlayerDict { get; }

        public Player GetPlayer(int playerId)
        {
            if (PlayerDict.ContainsKey(playerId))
                return PlayerDict[playerId].Item1;
            throw new KeyNotFoundException($"No such Player Id ({playerId}) in PlayerDict");
        }

        public IUserScenario GetUserScenario(int playerId)
        {
            if (PlayerDict.ContainsKey(playerId))
                return PlayerDict[playerId].Item2;
            throw new KeyNotFoundException($"No such Player Id ({playerId}) in PlayerDict");
        }

        public int GetPlayerCash(int playerId) =>
            GetPlayer(playerId).Cash;

        public void ChargePlayer(int playerId, int amount)
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

        bool IsBankrupt(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
