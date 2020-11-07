﻿using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace MonopolyPreUnity.Managers
{

    class PropertyManager
    {
        #region Constants
        private readonly float _mortgageFee;
        private readonly float _mortgageComission;
        private const int _housesLimit = 5;
        private const int _house = 1;
        #endregion

        #region Dependencies
        private readonly PlayerManager _playerManager;
        private readonly TileManager _tileManager;
        private readonly RequestManager _requestManager;
        #endregion

        #region Constuctor
        public PropertyManager(PlayerManager playerManager, TileManager tileManager, RequestManager requestManager, GameConfig gameConfig)
        {
            _playerManager = playerManager;
            _tileManager = tileManager;
            _requestManager = requestManager;

            _mortgageFee = gameConfig.MortgageFee;
            _mortgageComission = gameConfig.MortgageCommission;
        }
        #endregion

        #region Transfer Property
        public void TransferProperty(int propertyId, int newOwnerId)
        {
            var property = _tileManager.GetTileComponent<PropertyComponent>(propertyId);
            if (property.OwnerId != null)
            {
                Logger.Log((int)property.OwnerId, $"is no longer the owner of property {propertyId}");
            }
            property.OwnerId = newOwnerId;
            _playerManager.GetPlayer(newOwnerId).Properties.Add(propertyId);
            Logger.Log(newOwnerId, $"is the the owner of property {propertyId}");
        }
        #endregion

        #region Subsidiary Methods
        public HashSet<int> OwnedPropertiesInSet(int playerId, int setId)
        {
            var propertySet = _tileManager.GetPropertySet(setId);
            var ownerOwnedProperty = _playerManager.GetPlayer(playerId).Properties;

            var ownedPropertyInSet = new HashSet<int>(propertySet);
            ownedPropertyInSet.IntersectWith(ownerOwnedProperty);

            return ownedPropertyInSet;
        }

        /// <summary>
        /// if statement separated to corresponding method
        /// </summary>
        /// <param name="playerId">player</param>
        /// <param name="propertyComponent">property to check for set</param>
        /// <returns></returns>
        public bool IsSetOwned(int playerId, PropertyComponent propertyComponent)
        {
            var playerSet = _tileManager.GetPropertySet(propertyComponent.SetId);
            var player = _playerManager.GetPlayer(playerId);
            if (playerSet.IsSubsetOf(player.Properties))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// If statement separated to corresponding method to check allowed of build on property
        /// </summary>
        /// <param name="playerSet">set of properties</param>
        /// <param name="playerId">player(owner)</param>
        /// <param name="propertyComponent">specific property</param>
        /// <param name="propertyDevelopmentComponent">specific real estate</param>
        /// <returns></returns>
        
        public bool BuildOnPropertyAllowed(HashSet<int> playerSet, int playerId, PropertyComponent propertyComponent, PropertyDevelopmentComponent propertyDevelopmentComponent)
            //набор зданий одного цвета
            => (playerSet.Max(id => _tileManager.GetTileComponent<PropertyDevelopmentComponent>(id).HousesBuilt) -
                propertyDevelopmentComponent.HousesBuilt + _house <= 1 &&/*проверка на возможность достроить дом
                 на определенном тайле не нарушив баланс разности с максимально развитым тайлом*/
                propertyDevelopmentComponent.HousesBuilt + _house -
                playerSet.Min(id => _tileManager.GetTileComponent<PropertyDevelopmentComponent>(id).HousesBuilt) <= 1 &&
                /*аналогично для минимального тайла*/
                _playerManager.GetPlayerCash(playerId) >
                propertyDevelopmentComponent.HouseBuyPrice &&//достаток денег для постройки
                    propertyDevelopmentComponent.HousesBuilt != _housesLimit &&//проверка максимальной развитости тайла
                    IsSetOwned(playerId, propertyComponent));//проверка на наличие сета у игрока

        public bool SellOnPropertyAllowed(HashSet<int> playerSet, int playerId, PropertyComponent propertyComponent, PropertyDevelopmentComponent propertyDevelopmentComponent)

            => playerSet.Max(id => _tileManager.GetTileComponent<PropertyDevelopmentComponent>(id).HousesBuilt) -
                propertyDevelopmentComponent.HousesBuilt - _house <= 1 &&
                propertyDevelopmentComponent.HousesBuilt - _house -
                playerSet.Min(id => _tileManager.GetTileComponent<PropertyDevelopmentComponent>(id).HousesBuilt) <= 1 &&
                IsSetOwned(playerId, propertyComponent);

    


        #endregion

        #region Available Actions

        /// <summary>
        /// GetAllActions available for player
        /// </summary>
        /// <param name="playerId">player to deal with</param>
        /// <param name="propertyComponent">property</param>
        /// <param name="propertyDevelopmentComponent">real estate</param>
        /// <returns></returns>
        /// 
        
        public List<MonopolyCommand> GetAvailableActions(int playerId,PropertyComponent propertyComponent,PropertyDevelopmentComponent propertyDevelopmentComponent)
        {
            var playerSet = _tileManager.GetPropertySet(propertyComponent.SetId);
            var AvailableActions= new List<MonopolyCommand>();
            if (propertyDevelopmentComponent != null) {
                if (BuildOnPropertyAllowed(playerSet,playerId,propertyComponent,propertyDevelopmentComponent))
                    AvailableActions.Add(MonopolyCommand.BuyHouse);
                if (SellOnPropertyAllowed(playerSet,playerId,propertyComponent,propertyDevelopmentComponent))
                    AvailableActions.Add(MonopolyCommand.SellHouse);
            
                
            }
            if (propertyComponent.IsMortgaged == true &&
                _playerManager.GetPlayerCash(playerId)>
                propertyComponent.BasePrice*_mortgageFee*_mortgageComission)
                AvailableActions.Add(MonopolyCommand.UnmortgageProperty);
            if (propertyComponent.IsMortgaged == false &&
                propertyDevelopmentComponent.HousesBuilt == 0)
                AvailableActions.Add(MonopolyCommand.MortgageProperty);

            return AvailableActions;
        }
        #endregion

        #region Property Actions

        /// <summary>
        /// build house on property
        /// </summary>
        /// <param name="playerId">Player to deal with</param>
        /// <param name="developmentComponent">Real estate to deal with</param>
        
        public void BuildHouse(int playerId,PropertyDevelopmentComponent developmentComponent)
        {
            developmentComponent.HousesBuilt++;
            _playerManager.ChangeBalance(playerId, -developmentComponent.HouseBuyPrice);
        }

        /// <summary>
        /// sell house on property
        /// </summary>
        /// <param name="playerId">Player to deal with</param>
        /// <param name="developmentComponent">Real estate to deal with</param>
        
        public void SellHouse(int playerId,PropertyDevelopmentComponent developmentComponent)
        {
            developmentComponent.HousesBuilt--;
            _playerManager.ChangeBalance(playerId, developmentComponent.HouseSellPrice);
        }

        /// <summary>
        /// mortage property
        /// </summary>
        /// <param name="playerId">Player to deal with</param>
        /// <param name="developmentComponent">Real estate to deal with</param>
        
        public void Mortage(int playerId, PropertyComponent propertyComponent)
        {
            _playerManager.PlayerCashGive(playerId, (int)(_mortgageFee * propertyComponent.BasePrice));
            propertyComponent.IsMortgaged = true;
        }

        /// <summary>
        /// unmortage property
        /// </summary>
        /// <param name="playerId">Player to deal with</param>
        /// <param name="developmentComponent">Real estate to deal with</param>
        
        public void UnMortage(int playerId, PropertyComponent propertyComponent)
        {
            propertyComponent.IsMortgaged = false;
            _playerManager.PlayerCashCharge(playerId, (int)((_mortgageFee + _mortgageComission) * propertyComponent.BasePrice));
        }
        #endregion
    }
}
