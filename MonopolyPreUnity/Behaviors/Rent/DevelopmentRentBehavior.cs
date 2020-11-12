using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Rent
{
    class DevelopmentRentBehavior : IRentBehavior
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        public int GetRent(int renteeId, int ownerId, IEntityComponent component, int tileId)
        {
            var dev = component as PropertyDevelopment;
            var rentChoice = 0;
            var prop = _context.GetTileComponent<Property>(tileId);

            var owner = _context.GetPlayer(ownerId);
            if (_context.IsPropertySetOwned(owner, prop.SetId))
                rentChoice += 1 + dev.HousesBuilt;
            return dev.RentList[rentChoice];
        }

        #region Constructor
        public DevelopmentRentBehavior(Context context) => 
            _context = context;
        #endregion
    }
}
