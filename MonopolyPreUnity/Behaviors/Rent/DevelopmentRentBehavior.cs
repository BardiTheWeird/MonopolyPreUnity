using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Rent
{
    class DevelopmentRentBehavior : IRentBehavior
    {
        #region Dependencies
        private readonly PropertyManager _propertyManager;
        private readonly TileManager _tileManager;
        #endregion

        public int GetRent(int renteeId, int ownerId, ITileComponent component, int tileId)
        {
            var development = component as PropertyDevelopmentComponent;
            var rentChoice = 0;
            var propertyComponent = _tileManager.GetTileComponent<PropertyComponent>(tileId);
            if (_propertyManager.IsSetOwned(ownerId, propertyComponent))
            {
                rentChoice += 1 + development.HousesBuilt;
            }
            return development.RentList[rentChoice];
        }
    }
}
