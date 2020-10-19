using MonopolyPreUnity.Classes;
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
        private const float _mortageComission = 0.1f;
        private const float _mortageFee = 0.5f;
        private const int _housesLimit = 5;
        private const int _house = 1;
        #endregion

        #region Dependencies
        private readonly PlayerManager _playerManager;
        private readonly TileManager _tileManager;
        private readonly RequestManager _requestManager;
        #endregion

        #region Constuctor
        public PropertyManager(PlayerManager playerManager, TileManager tileManager)
        {
            _playerManager = playerManager;
            _tileManager = tileManager;
        }
        #endregion

        #region Subsidiary Methods
        public bool IsSetOwned(int playerId, PropertyComponent propertyComponent)
        {
            var playerSet = _tileManager.GetPropertySet(propertyComponent.setId);
            var player = _playerManager.GetPlayer(playerId);
            if (playerSet.IsSubsetOf(player.Properties))
            {
                return true;
            }
            return false;
        }


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
        public List<MonopolyCommand> GetAvailableActions(int playerId,PropertyComponent propertyComponent,PropertyDevelopmentComponent propertyDevelopmentComponent)
        {
            var playerSet = _tileManager.GetPropertySet(propertyComponent.setId);
            var AvailableActions= new List<MonopolyCommand>();
            if (propertyDevelopmentComponent != null) {
                if (BuildOnPropertyAllowed(playerSet,playerId,propertyComponent,propertyDevelopmentComponent))
                    AvailableActions.Add(MonopolyCommand.PropertyBuyHouse);
                if (SellOnPropertyAllowed(playerSet,playerId,propertyComponent,propertyDevelopmentComponent))
                    AvailableActions.Add(MonopolyCommand.PropertySellHouse);
            
                
            }
            if (propertyComponent.IsMortgaged == true &&
                _playerManager.GetPlayerCash(playerId)>
                propertyComponent.BasePrice*_mortageFee*_mortageComission)
                AvailableActions.Add(MonopolyCommand.PropertyUnmortgage);
            if (propertyComponent.IsMortgaged == false &&
                propertyDevelopmentComponent.HousesBuilt == 0)
                AvailableActions.Add(MonopolyCommand.PropertyMortgage);

            return AvailableActions;
        }
        #endregion

        #region Property Actions
        public void BuildHouse(int playerId,PropertyDevelopmentComponent developmentComponent)
        {
            developmentComponent.HousesBuilt++;
            _playerManager.PlayerCashCharge(playerId, developmentComponent.HouseBuyPrice);

        }
        
        public void SellHouse(int playerId,PropertyDevelopmentComponent developmentComponent)
        {

            developmentComponent.HousesBuilt--;
            _playerManager.PlayerCashGive(playerId, developmentComponent.HouseBuyPrice);

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
        #endregion

        #region Manage Property
        public void ManageProperty(int playerId)
        {
            while (true)
            {
                var request = new Request<int?>(MonopolyRequest.PropertyManagePropertyChoice,
                    _playerManager.GetPlayer(playerId).Properties.Select(x => (int?)x).ToList());

                if (!(_requestManager.SendRequest(playerId, request) is int propertyId))
                    return;

                MonopolyCommand command;
                do
                {
                    var availableActions = GetAvailableActions(playerId,
                            _tileManager.GetTileComponent<PropertyComponent>(propertyId),
                            _tileManager.GetTileComponent<PropertyDevelopmentComponent>(propertyId));
                    availableActions.Add(MonopolyCommand.CancelAction);

                    command = _requestManager.SendRequest(playerId,
                        new Request<MonopolyCommand>(MonopolyRequest.PropertyManageActionChoice, availableActions));

                    switch (command)
                    {
                        case MonopolyCommand.PropertyMortgage:

                            break;

                        case MonopolyCommand.PropertyUnmortgage:

                            break;

                        case MonopolyCommand.PropertyBuyHouse:

                            break;

                        case MonopolyCommand.PropertySellHouse:

                            break;
                    }
                } while (command != MonopolyCommand.CancelAction);
            }
        }
        #endregion
    }
}
