using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class PlayerManager
    {
        #region Dependencies
        private readonly TileManager _tileManager;
        #endregion


        #region fields
        // ID to IPlayer and IUserScenario
        private readonly Dictionary<int, (Player, IUserScenario)> _playerDict;
        #endregion

        #region GetPlayer and their stuff methods
        public Player GetPlayer(int playerId)
        {
            if (_playerDict.ContainsKey(playerId))
                return _playerDict[playerId].Item1;
            throw new KeyNotFoundException($"No such Player Id ({playerId}) in PlayerDict");
        }

        public List<int> GetAllPlayerId() =>
            new List<int>(_playerDict.Keys);

        public IUserScenario GetUserScenario(int playerId)
        {
            if (_playerDict.ContainsKey(playerId))
                return _playerDict[playerId].Item2;
            throw new KeyNotFoundException($"No such Player Id ({playerId}) in PlayerDict");
        }
        #endregion

        #region Cash Methods
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
                Logger.Log(playerId, $"paid {amount}$. {player.Cash}$ left");
            }
            else
            {
                if (IsBankrupt(player))
                {
                    Logger.Log(playerId, "is bankrupt");
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
            var player = GetPlayer(playerId);
            player.Cash += amount;
            Logger.Log(playerId, $"received {amount}$. {player.Cash}$ left");
        }
        #endregion

        #region Bankruptcy
        bool EnoughPropertyToPayOff(Player player)
        {
            var playerProperty = player.Properties;
            int sum = 0;
            foreach (int propertyId in playerProperty)
            {
                if (_tileManager.GetTileComponent<PropertyComponent>(propertyId, out var component))
                    sum += 0;
            }


            if (true)
            return true;
            return false;

        }


        bool IsBankrupt(Player player)
        {
            if (player.Cash <= 0)
            {
                return true;
                

            }
            return false;
        }
        #endregion

        #region Constructor
        public PlayerManager(GameData gameData,TileManager tileManager)
        {
            _playerDict = gameData.PlayerDict;
            _tileManager = tileManager;
        }
        #endregion
    }
}
