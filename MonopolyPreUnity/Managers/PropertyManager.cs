using MonopolyPreUnity.Components;
using MonopolyPreUnity.UI;
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
        #region Transfer Property
        //public void TransferProperty(int propertyId, int newOwnerId)
        //{
        //    var property = _tileManager.GetTileComponent<Property>(propertyId);
        //    if (property.OwnerId != null)
        //    {
        //        _consoleUI.PrintFormatted($"|player:{(int)property.OwnerId}| is no longer the owner of |tile:{propertyId}|");
        //    }
        //    property.OwnerId = newOwnerId;
        //    _playerManager.GetPlayer(newOwnerId).Properties.Add(propertyId);

        //    _consoleUI.PrintFormatted($"|player:{newOwnerId}| is the new owner of |tile:{propertyId}|");
        //}
        #endregion

        #region Subsidiary Methods        
        //public bool BuildOnPropertyAllowed(HashSet<int> playerSet, int playerId, Property propertyComponent, PropertyDevelopment propertyDevelopmentComponent)
        //    //набор зданий одного цвета
        //    => (playerSet.Max(id => _tileManager.GetTileComponent<PropertyDevelopment>(id).HousesBuilt) -
        //        propertyDevelopmentComponent.HousesBuilt + _house <= 1 &&/*проверка на возможность достроить дом
        //         на определенном тайле не нарушив баланс разности с максимально развитым тайлом*/
        //        propertyDevelopmentComponent.HousesBuilt + _house -
        //        playerSet.Min(id => _tileManager.GetTileComponent<PropertyDevelopment>(id).HousesBuilt) <= 1 &&
        //        /*аналогично для минимального тайла*/
        //        _playerManager.GetPlayerCash(playerId) >
        //        propertyDevelopmentComponent.HouseBuyPrice &&//достаток денег для постройки
        //            propertyDevelopmentComponent.HousesBuilt != _housesLimit &&//проверка максимальной развитости тайла
        //            IsSetOwned(playerId, propertyComponent));//проверка на наличие сета у игрока

        //public bool SellOnPropertyAllowed(HashSet<int> playerSet, int playerId, Property propertyComponent, PropertyDevelopment propertyDevelopmentComponent) => 
        //    playerSet.Max(id => _tileManager.GetTileComponent<PropertyDevelopment>(id).HousesBuilt) -
        //    propertyDevelopmentComponent.HousesBuilt - _house <= 1 &&
        //    propertyDevelopmentComponent.HousesBuilt - _house -
        //    playerSet.Min(id => _tileManager.GetTileComponent<PropertyDevelopment>(id).HousesBuilt) <= 1 &&
        //    IsSetOwned(playerId, propertyComponent);
        #endregion

        #region Available Actions
        //public List<MonopolyCommand> GetAvailableActions(int playerId,Property propertyComponent,PropertyDevelopment propertyDevelopmentComponent)
        //{
        //    var playerSet = _tileManager.GetPropertySet(propertyComponent.SetId);
        //    var AvailableActions= new List<MonopolyCommand>();
        //    if (propertyDevelopmentComponent != null) {
        //        if (BuildOnPropertyAllowed(playerSet,playerId,propertyComponent,propertyDevelopmentComponent))
        //            AvailableActions.Add(MonopolyCommand.BuyHouse);
        //        if (SellOnPropertyAllowed(playerSet,playerId,propertyComponent,propertyDevelopmentComponent))
        //            AvailableActions.Add(MonopolyCommand.SellHouse);
            
                
        //    }
        //    if (propertyComponent.IsMortgaged == true &&
        //        _playerManager.GetPlayerCash(playerId)>
        //        propertyComponent.BasePrice*_mortgageFee*_mortgageComission)
        //        AvailableActions.Add(MonopolyCommand.UnmortgageProperty);
        //    if (propertyComponent.IsMortgaged == false &&
        //        propertyDevelopmentComponent.HousesBuilt == 0)
        //        AvailableActions.Add(MonopolyCommand.MortgageProperty);

        //    return AvailableActions;
        //}
        #endregion

        #region Property Actions
    
        //public void BuildHouse(int playerId,PropertyDevelopment developmentComponent)
        //{
        //    developmentComponent.HousesBuilt++;
        //    _playerManager.PlayerCashCharge(playerId, developmentComponent.HouseBuyPrice);
        //}
        
        //public void SellHouse(int playerId,PropertyDevelopment developmentComponent)
        //{
        //    developmentComponent.HousesBuilt--;
        //    _playerManager.PlayerCashGive(playerId, developmentComponent.HouseSellPrice);
        //}
        
        //public void Mortage(int playerId, Property propertyComponent)
        //{
        //    _playerManager.PlayerCashGive(playerId, (int)(_mortgageFee * propertyComponent.BasePrice));
        //    propertyComponent.IsMortgaged = true;
        //}
        
        //public void UnMortage(int playerId, Property propertyComponent)
        //{
        //    propertyComponent.IsMortgaged = false;
        //    _playerManager.PlayerCashCharge(playerId, (int)((_mortgageFee + _mortgageComission) * propertyComponent.BasePrice));
        //}
        #endregion
    }
}
