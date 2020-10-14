using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace MonopolyPreUnity.Managers
{

    class PropertyManager
    {
        #region Constants
        private const float _mortageComission = 0.1f;
        private const float _mortageFee = 0.5f;
        private const int _housesLimit = 5;
        #endregion

        #region Dependencies
        private readonly PlayerManager _playerManager;
        private readonly TileManager _tileManager;
        #endregion

        #region constuctor
        public PropertyManager(PlayerManager playerManager, TileManager tileManager)
        {
            _playerManager = playerManager;
            _tileManager = tileManager;
        }
        #endregion

        public void GetAvailableActions(int playerId,PropertyComponent propertyComponent)
        {
            var AvailableActions= new List<MonopolyCommand>();
            var set = _tileManager.GetSet(propertyComponent.setId);
            foreach (int property in set)
            {
               var player = _playerManager.GetPlayer(playerId);
                player.

                if (property != )
    }
            if (propertyComponent.DevelopmentComponent != null) {
                if (_playerManager.GetPlayerCash(playerId) > propertyComponent.DevelopmentComponent.HouseBuyPrice &&
                    propertyComponent.DevelopmentComponent.HousesBuilt != _housesLimit)
                     {
                    AvailableActions.Add(MonopolyCommand.PropertyBuyHouse);
                }

               if (propertyComponent.DevelopmentComponent.HousesBuilt > 0)
                {
                    AvailableActions.Add(MonopolyCommand.PropertySellHouse);
                }
            
                
            }
            if (propertyComponent.IsMortgaged == true && _playerManager.GetPlayerCash>)
            {

            }
            
            


        }

        public void BuildHouse(int playerId,PropertyDevelopmentComponent developmentComponent)
        {
            developmentComponent.HousesBuilt++;
                _playerManager.PlayerCashCharge(playerId, developmentComponent.HouseBuyPrice);
                if (developmentComponent.HousesBuilt == _housesLimit) developmentComponent.BuildAllowed = false;


        }
        
        public void SellHouse(int playerId,PropertyDevelopmentComponent developmentComponent)
        {

            developmentComponent.HousesBuilt--;
            _playerManager.PlayerCashGive(playerId, developmentComponent.HouseBuyPrice);
            if (developmentComponent.HousesBuilt < _housesLimit) developmentComponent.BuildAllowed = true;

        }
        public void Mortage(int playerId, PropertyComponent propertyComponent)
        {

                _playerManager.PlayerCashGive(playerId, (int)(_mortageFee * propertyComponent.BasePrice));
                propertyComponent.IsMortgaged = true;


        }
        
        public void UnMortage(int playerId, PropertyComponent propertyComponent)
        {

                propertyComponent.IsMortgaged = false;
                _playerManager.PlayerCashCharge(playerId, (int)(_mortageFee + _mortageComission) * propertyComponent.BasePrice);

        }


        }
}
