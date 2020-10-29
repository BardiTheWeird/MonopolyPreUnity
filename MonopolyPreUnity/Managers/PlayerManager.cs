using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Interfaces;
using System;
using System.Collections.Generic;
using MonopolyPreUnity.Components;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class PlayerManager
    {
        #region Dependencies
        private readonly TileManager _tileManager;
        public PropertyManager _propertyManager { get; set; }
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

        #region Destroy Player =)
        void KickPlayer(int playerId)
        {
            Logger.Log(playerId, "ну этот додик с игры вылетел лол бомж гей");
            _playerDict.Remove(playerId);
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

        public void PlayerCashCharge(int playerId, int amount, int? chargerId = null)
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
                    KickPlayer(playerId);
                    
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

        /// <summary>
        /// Player complete bankrupt checker bool method
        /// </summary>
        /// <param name="player">player</param>
        /// <returns></returns>
        
        bool EnoughPropertyToPayOff(Player player)
        {
            var playerProperty = player.Properties;
            int sum = 0;
            foreach (int propertyId in playerProperty)
            {
                if (_tileManager.GetTileComponent<PropertyDevelopmentComponent>(propertyId, out var realEstate))
                    sum += realEstate.HousesBuilt * realEstate.HouseSellPrice;

                if ((_tileManager.GetTileComponent<PropertyComponent>(propertyId, out var property)))
                    sum += (int)(property.BasePrice * _propertyManager.MortageFee);
            }
            if (sum + player.Cash > 0)
                return true;
            return false;

        }

        /// <summary>
        /// Method allows to sell property in case of negative balance and returns false or returns true
        /// and....
        /// to be continued in PlayerCashCharge...
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        
        bool IsBankrupt(Player player)
        {
            if (!(EnoughPropertyToPayOff(player)))
            {
                return true;

            }
            else
            {
                Logger.Log("Man you can still sell houses and mortage ur property to be in game!=)))");
                do
                {
                    _propertyManager.ManageProperty(player.Id);
                }
                while (player.Cash < 0);
                return false;
            }
      
        }
        #endregion

        #region Constructor
        //public PlayerManager(GameData gameData,TileManager tileManager,PropertyManager propertyManager)
        //{
        //    _playerDict = gameData.PlayerDict;
        //    _tileManager = tileManager;
        //    _propertyManager = propertyManager;
        //}

        public PlayerManager(GameData gameData, TileManager tileManager)
        {
            _playerDict = gameData.PlayerDict;
            _tileManager = tileManager;
        }
        #endregion
    }
}
