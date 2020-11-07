using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using MonopolyPreUnity.Components;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Autofac.Features.Indexed;
using MonopolyPreUnity.RequestHandlers;
using MonopolyPreUnity.Requests;

namespace MonopolyPreUnity.Managers
{
    public class PlayerEventArgs : EventArgs
    {
        public int PlayerId { get; set; }

        public PlayerEventArgs(int playerId)
        {
            PlayerId = playerId;
        }
    }

    class PlayerManager
    {
        #region Dependencies
        private readonly TileManager _tileManager;
        public PropertyManager _propertyManager { get; set; }
        private readonly RequestManager _requestManager;
        #endregion

        #region fields
        // ID to IPlayer and IUserScenario
        private readonly Dictionary<int, Player> _playerDict;
        #endregion

        #region IsBankrupt event
        public event EventHandler<PlayerEventArgs> PlayerBankruptEvent;

        void RaisePlayerBankruptEvent(int playerId) =>
            PlayerBankruptEvent?.Invoke(this, new PlayerEventArgs(playerId));
        #endregion

        #region GetPlayer and their stuff methods
        public Player GetPlayer(int playerId)
        {
            if (_playerDict.ContainsKey(playerId))
                return _playerDict[playerId];
            throw new KeyNotFoundException($"No {playerId} Id in PlayerDict");
        }

        public List<int> GetAllPlayerId() =>
            new List<int>(_playerDict.Keys);
        #endregion

        #region Destroy Player =)
        void BankruptPlayerAssetsTransfer(int playerId, int? chargerId)
        {
            var player = GetPlayer(playerId);

            // transfer property
            if (chargerId is int transferId)
            {
                var chargerPlayer = GetPlayer(transferId);

                // Cash and JailCards
                chargerPlayer.Cash += player.Cash;
                chargerPlayer.JailCards += player.JailCards;

                // Property transfer
                foreach (var propId in player.Properties)
                    _propertyManager.TransferProperty(propId, transferId);
            }
            else
            {
                foreach (var propId in player.Properties)
                {
                    var prop = _tileManager.GetTileComponent<PropertyComponent>(propId);
                    var dev = _tileManager.GetTileComponent<PropertyDevelopmentComponent>(propId);

                    prop.OwnerId = null;
                    prop.IsMortgaged = false;

                    if (dev != null)
                        dev.HousesBuilt = 0;
                }
            }
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
                if (chargerId != null)
                    PlayerCashGive((int)chargerId, amount);
            }
            else
            {
                if (IsBankrupt(player, amount))
                {
                    player.IsBankrupt = true;
                    BankruptPlayerAssetsTransfer(playerId, chargerId);
                    RaisePlayerBankruptEvent(playerId);
                    Logger.Log(playerId, "is bankrupt");
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
        bool EnoughPropertyToPayOff(Player player, int debtAmount)
        {
            var playerProperty = player.Properties;
            int sum = 0;
            foreach (int propertyId in playerProperty)
            {
                if (_tileManager.GetTileComponent<PropertyDevelopmentComponent>(propertyId, out var realEstate))
                    sum += realEstate.HousesBuilt * realEstate.HouseSellPrice;

                if (_tileManager.GetTileComponent<PropertyComponent>(propertyId, out var property))
                    sum += (int)(property.BasePrice * _propertyManager.MortageFee);
            }
            if (sum + player.Cash > debtAmount)
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
        bool IsBankrupt(Player player, int debtAmount)
        {
            if (!EnoughPropertyToPayOff(player, debtAmount))
            {
                return true;
            }
            else
            {
                Logger.Log("Man you can still sell houses and mortgage ur property to be in game!=)))");
                do
                {
                    _requestManager.SendRequest(player.Id, new BankruptcyRequest(debtAmount));
                }
                while (player.Cash < debtAmount);
                return false;
            }
        }
        #endregion

        #region Constructor
        public PlayerManager(GameData gameData, TileManager tileManager, RequestManager requestManager)
        {
            _playerDict = gameData.PlayerDict;
            _tileManager = tileManager;
            _requestManager = requestManager;
        }
        #endregion
    }
}
