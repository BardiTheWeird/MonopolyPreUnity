using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using MonopolyPreUnity.Components;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Autofac.Features.Indexed;
using MonopolyPreUnity.RequestHandlers;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.UI;

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
        public ConsoleUI _consoleUI { get; set; }
        #endregion

        #region fields
        // ID to IPlayer and IUserScenario
        private readonly Dictionary<int, Player> _playerDict;
        #endregion

        #region const
        private readonly float _mortgageFee;
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
                TransferPlayerAssets(transferId, playerId);
            }
            else
            {
                // basically empty the ownership of properties
                foreach (var propId in player.Properties)
                {
                    var prop = _tileManager.GetTileComponent<Property>(propId);
                    var dev = _tileManager.GetTileComponent<PropertyDevelopment>(propId);

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

        public void PlayerCashCharge(int playerId, int amount, int? chargerId = null, string message = null)
        {
            var player = GetPlayer(playerId);
            if (player.Cash >= amount)
            {
                player.Cash -= amount;
                _consoleUI.PrintCashChargeMessage(playerId, chargerId, amount, message);
                if (chargerId != null)
                    PlayerCashGive((int)chargerId, amount);
            }
            else
            {
                if (IsBankrupt(player, amount))
                {
                    player.IsBankrupt = true;
                    BankruptPlayerAssetsTransfer(playerId, chargerId);
                    _consoleUI.PrintFormatted($"|player:{playerId}| is bankrupt");
                    RaisePlayerBankruptEvent(playerId);
                }
            }
        }

        public void PlayerCashGive(int playerId, int amount, string message = null)
        {
            var player = GetPlayer(playerId);
            player.Cash += amount;
            _consoleUI.PrintCashGiveMessage(playerId, amount, message);
        }
        #endregion

        #region TransferPlayerAssets
        public void TransferPlayerAssets(int receiverId, PlayerAssets assets)
        {
            var receiver = GetPlayer(receiverId);
            receiver.Cash += assets.Cash;
            receiver.JailCards += assets.JailCards;

            foreach (var prop in assets.Properties)
                _propertyManager.TransferProperty(prop, receiverId);
        }

        public void TransferPlayerAssets(TradeOffer trade)
        {
            TransferPlayerAssets(trade.ReceiverAssets.PlayerId, trade.InitiatorAssets);
            TransferPlayerAssets(trade.InitiatorAssets.PlayerId, trade.ReceiverAssets);
        }

        public void TransferPlayerAssets(int receiverId, int senderId) =>
            TransferPlayerAssets(receiverId, new PlayerAssets(GetPlayer(senderId)));
        #endregion

        #region Bankruptcy

        bool EnoughPropertyToPayOff(Player player, int debtAmount)
        {
            var playerProperty = player.Properties;
            int sum = 0;
            foreach (int propertyId in playerProperty)
            {
                if (_tileManager.GetTileComponent<PropertyDevelopment>(propertyId, out var realEstate))
                    sum += realEstate.HousesBuilt * realEstate.HouseSellPrice;

                if (_tileManager.GetTileComponent<Property>(propertyId, out var property))
                    sum += (int)(property.BasePrice * _mortgageFee);
            }
            if (sum + player.Cash > debtAmount)
                return true;
            return false;
        }

        bool IsBankrupt(Player player, int debtAmount)
        {
            if (!EnoughPropertyToPayOff(player, debtAmount))
            {
                return true;
            }
            else
            {
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
        public PlayerManager(GameData gameData, 
            TileManager tileManager, 
            RequestManager requestManager, 
            GameConfig gameConfig)
        {
            _playerDict = gameData.PlayerDict;
            _tileManager = tileManager;
            _requestManager = requestManager;
            _mortgageFee = gameConfig.MortgageFee;
        }
        #endregion
    }
}
