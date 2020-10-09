﻿using MonopolyPreUnity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class PlayerManager
    {
        // ID to IPlayer and IUserScenario
        Dictionary<int, (IPlayer, IUserScenario)> _playerDict;
        Dictionary<int, (IPlayer, IUserScenario)> PlayerDict { get; set; }

        public IPlayer GetPlayer(int playerId)
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
    }
}
