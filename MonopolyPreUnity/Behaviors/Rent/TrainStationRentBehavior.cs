using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Rent
{
    class TrainStationRentBehavior : IRentBehavior
    {
        #region Dependency
        private readonly TileManager _tileManager;
        private readonly PropertyManager _propertyManager;
        #endregion

        public int GetRent(int renteeId, int ownerId, ITileComponent component, int tileId)
        {
            var station = component as TrainStation;
            var property = _tileManager.GetTileComponent<Property>(tileId);

            var ownedPropertyInSet = _propertyManager.OwnedPropertiesInSet(ownerId, property.SetId);

            int rentValue = station.BaseRent * (int)(Math.Round(Math.Pow(2, ownedPropertyInSet.Count - 1)));
            return rentValue;
        }

        #region Constructor
        public TrainStationRentBehavior(TileManager tileManager, PropertyManager propertyManager)
        {
            _tileManager = tileManager;
            _propertyManager = propertyManager;
        }
        #endregion
    }
}
