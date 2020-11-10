using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Rent
{
    class TrainStationRentBehavior : IRentBehavior
    {
        #region Dependency
        private readonly Context _context;
        #endregion

        public int GetRent(int renteeId, int ownerId, IEntityComponent component, int tileId)
        {
            var station = component as TrainStation;
            var property = _context.GetTileComponent<Property>(tileId);
            var owner = _context.GetPlayer(ownerId);

            var ownedPropertyInSet = _context.OwnedPropertiesInSet(owner, property.SetId);

            int rentValue = station.BaseRent * (int)Math.Round(Math.Pow(2, ownedPropertyInSet.Count - 1));
            return rentValue;
        }

        #region Constructor
        public TrainStationRentBehavior(Context context) =>
            _context = context;
        #endregion
    }
}
